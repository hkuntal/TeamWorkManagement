using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //http://www.codeproject.com/Articles/10649/An-Introduction-to-Socket-Programming-in-NET-using

            SynchronousSocketClient.StartClient();

            Console.WriteLine("Do you want to connect again to the Socket Server - Y/N");
            var ans = Console.ReadLine();
            if (ans.ToUpper().Equals("Y"))
            {
                //SynchronousSocketClient.StartClient();   
                Program.Main(null);
            }
            else
            {
                Console.ReadKey();
            }
        }
    }
}
