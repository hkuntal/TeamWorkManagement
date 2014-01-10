using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using COR.Interfaces;

namespace COR.BusinessObjects
{
    public class LoanRequest:ILoanRequest
    {
        public decimal Amount { get; set; }
    }
}
