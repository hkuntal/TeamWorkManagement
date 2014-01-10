using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COR.Interfaces
{
    public interface IRequestHandler
    {
        string HandleRequest(ILoanRequest obj);

        //We can implement the Successor here as well, because every successor will eventually be a IRequestHandler
        IRequestHandler Successor { get; set; }

        bool HandleRequestMEF(ILoanRequest request);
    }

    //Create an extension method on IRequestHandler
    public static class ExtensionMethods
    {
        public static string TrySuccessor(this IRequestHandler current, ILoanRequest request)
        {
            if (current.Successor == null)
            {
                return "There is no next successor";
            }
            else
            {
                Console.WriteLine("Cannot process the request. Passing it onto the next successor");
                return current.Successor.HandleRequest(request);
            }
        }
    }

    
}
