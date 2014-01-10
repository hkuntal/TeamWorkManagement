using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using COR.Interfaces;
using COR.Utils;

namespace COR.Approvers
{
    [ExportHandler(SuccessorOf = typeof(Clerk))]
    public class Manager:IRequestHandler
    {
        //Successor will approve the amount if Clerrk cannot
        public IRequestHandler Successor { get; set; }
        public bool HandleRequestMEF(ILoanRequest request)
        {
             //A clerk can approve amount upto Rs 10,000
            if (request.Amount <= 1000)
            {
                Console.WriteLine("Your loan request for amount {0} has been approved by {1} ", request.Amount,
                                  this.GetType().ToString());
                return true;
            }
            else
            {
                return false;
            }
        }

        //Assigning next Successor through a costructor
        public Manager()
        {
            Successor = new SuperManager();
        }

        public string HandleRequest(ILoanRequest request)
        {
            //A Manager can approve amount upto Rs 50,000
            if (request.Amount <= 5000)
                return "Request approved by Manager";
            else
            {
                //Else Clerk cannot approve,so pass it to the next successor which is Super Manager
                //return Successor.HandleRequest(request);
                //Other than the above line we can also use
               return this.TrySuccessor(request);
            }
        }


    }
}
