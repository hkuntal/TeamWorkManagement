using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Text;
using COR.Interfaces;
using System.ComponentModel.Composition;

namespace COR.BusinessObjects
{
    public interface IRequestHandlerMetadata
    {
        Type SuccessorOf { get; }
    }

    public class RequestHandlerGateway
    {
        //The metadata to tie a handler to next successor


        //This is going to handle the composition
        //Let us create a collection to store the list of all the approvers
        [ImportMany(typeof (IRequestHandler))]
        public IEnumerable<Lazy<IRequestHandler, IRequestHandlerMetadata>> Handlers { get; set; }

        private IRequestHandler first = null;
        //Check for the handlers and call their Handle method

        //Get the first handler

        public RequestHandlerGateway()
        {
            //Call the methods to compose the handlers
            ComposeHandlers();

            //Find the first handler, hanlder that is not a successor of any other handler
            //Understand the use of Meta data here
            first = Handlers.First(handler => handler.Metadata.SuccessorOf == null).Value;


        }

        //Handle the request as it comes. Pass the request object and the name of the handler that will first process the request
        private bool TryHandle(IRequestHandler requestHandler, ILoanRequest request)
        {
            //Find if the curent request handler has got any Successor. If it has than assign the successor
            var firstOrDefault =
                Handlers.FirstOrDefault(handle => handle.Metadata.SuccessorOf == requestHandler.GetType());


            //Handle the request first. If it gets handled, nothing to do
            if (requestHandler.HandleRequestMEF(request))
            {
                return true;
            }
                //Reques did not get handled, try passing it on to the Successor
            else if (firstOrDefault != null)
            {
                requestHandler.Successor = firstOrDefault.Value;
                //Call the method again to process the request and set the successor, if there is any
                return TryHandle(requestHandler.Successor, request);
            }
            else
            {
                return false;
            }
        }


        //This method exposes the public interface for handling the request
        public bool HandleRequest(ILoanRequest request)
        {
            return TryHandle(first, request);
        }

        public void ComposeHandlers()
        {
             //A catalog that can aggregate other catalogs
            var aggrCatalog = new AggregateCatalog();
            //An assembly catalog to load information about part from this assembly
            var asmCatalog = new AssemblyCatalog(@"C:\Users\harioms\Documents\Visual Studio 2012\Projects\TeamWorkManagement\COR.Approvers\bin\Debug\COR.Approvers.dll");
            var asmCatalog1 = new AssemblyCatalog(@"C:\Users\harioms\Documents\Visual Studio 2012\Projects\TeamWorkManagement\COR.BusinessObjects\bin\Debug\COR.BusinessObjects.dll");
            aggrCatalog.Catalogs.Add(asmCatalog);
            aggrCatalog.Catalogs.Add(asmCatalog1);

            //Create a container
            var container = new CompositionContainer(aggrCatalog);
            //Composing the parts
            container.ComposeParts(this);
        }

    }
}
