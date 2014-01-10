using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using COR.Interfaces;
using COR.Utils;

namespace COR.Approvers
{
    [ExportHandler]
    public class Clerk:IRequestHandler
    {
        //Successor will approve the amount if Clerrk cannot
        public IRequestHandler Successor { get; set; }

        //Set the next Successor to Clerk in the Constructor. Successor can be set dynamically also.
        public Clerk()
        {
            Successor = new Manager();
        }

        public string HandleRequest(ILoanRequest request)
        {
            //A clerk can approve amount upto Rs 10,000
            if (request.Amount <= 1000)
                return "Amount Approved by Clerk";
            else
                Console.WriteLine("Loan request cannot be approved by ");
                //Else Clerk cannot approve,so pass it to the next successor
                return Successor.HandleRequest(request);
        }
        //This method will be called by Managed Extensibility Framework
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
    }
}
