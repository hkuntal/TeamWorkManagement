using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using COR.Interfaces;
using COR.Utils;

namespace COR.Approvers
{
    [ExportHandler(SuccessorOf = typeof(Manager))]
    public class SuperManager:IRequestHandler
    {
        //There is no successor to the Super Manager
        public string HandleRequest(ILoanRequest request)
        {
            if (request.Amount <=100000)
            {
                return "Request approved by Super Manager";
            }
            else
            {
                throw new Exception("Bank can approve loan only uptill one lakh. Loan cannot be granted for amounts greater than 1 lac");
            }
        }

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
    }
}
