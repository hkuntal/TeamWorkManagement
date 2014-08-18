using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ActiveDirectoryAccess
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

            bool a = ValidateCredentials(@"IXLab\IXUser", "IXlogin#1", true);
                Console.WriteLine(a.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + Environment.NewLine + "Stack Trace: " + ex.StackTrace);
            }
            Console.WriteLine("Credentials successfully validated");
            Console.ReadLine();
        }

        public static bool ValidateCredentials(string userName, string password, bool domainUser)
        {
            var pc = domainUser
                         ? new PrincipalContext(ContextType.Domain, userName.Split('\\')[0])
                         : new PrincipalContext(ContextType.Machine, Dns.GetHostName());
            
            
            return pc.ValidateCredentials(userName, password);
            
        }
    }
}
