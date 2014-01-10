using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using COR.Interfaces;
using COR.BusinessObjects;
using COR.Approvers;

namespace ChainOfResponsibility
{
    class Program
    {
        static void Main(string[] args)
        {
            //Nomral Implementation
            Console.WriteLine("--------------------This is a normal implementation----------------------------");
            
            ConsoleRequestResponse.ProcessRequest.ExecuteRequest(
                () =>
                    {
                        //Create a Loan Request Object
                        ILoanRequest request = new LoanRequest();
                        //Take the amount from the user
                        Console.WriteLine("Enter the Loan amount for approval");
                        var s = Console.ReadLine();
                        request.Amount = Convert.ToDecimal(s);
                        //Pass the request object to the Approvers. Please note that automatic properties override the changes 
                        //done in the constructor
                        //IRequestHandler obj = new Clerk{Successor = new SuperManager()};
                        IRequestHandler obj = new Clerk();
                        Console.WriteLine(obj.HandleRequest(request));
                        
                        
                    }
                );

            Console.WriteLine("--------------------Normal Implementation Over----------------------------");

            Console.WriteLine("--------------------MEF implementation Start----------------------------");
            ConsoleRequestResponse.ProcessRequest.ExecuteRequest(
               () =>
               {
                   //Create a Loan Request Object
                   ILoanRequest request = new LoanRequest();
                   //Take the amount from the user
                   Console.WriteLine("Enter the Loan amount for approval");
                   var s = Console.ReadLine();
                   request.Amount = Convert.ToDecimal(s);
                   //Pass the request object to the Approvers. Please note that automatic properties override the changes 
                   //done in the constructor
                   //IRequestHandler obj = new Clerk{Successor = new SuperManager()};
                   IRequestHandler obj = new Clerk();
                   RequestHandlerGateway objRequestHandlerGateway = new RequestHandlerGateway();
                   if(!objRequestHandlerGateway.HandleRequest(request))
                       Console.WriteLine("Sorry! Your request for amount {0} has been disapproved.",request.Amount);

                   }
               );
            Console.WriteLine("--------------------MEF implementation End----------------------------");
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();

        }
    }
}
