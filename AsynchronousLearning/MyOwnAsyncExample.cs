using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AsynchronousLearning
{
    class MyOwnAsyncExample
    {
        public static void Start()
        {
            Console.WriteLine("Start method called");
            // This call works it is able to call the method asynchronously
            //button1_Click();

            //This call also works perfectly well
            CallMethodAsyncWithOwnTaskObject();

        }

        private async static void CallMethodAsyncWithOwnTaskObject()
        {

            var s = await button2_Click();
            Console.WriteLine(s);
            Console.WriteLine("Start method call finished");
        }
        // Create a async method
        public void GetWebResponseAsync()
        {
            WebClient w = new WebClient();

            // Create a Task object

            //This will load the string synchronously
            string s = w.DownloadString("http://google.com");
            Console.WriteLine(s);

            //To downlaod the string synchronously
            
        }

        private async static void button1_Click()
        {

            var res = await Task.Run(() =>
                {
                    var i = 9990;
                    var answer = 9990 / 45;
                    return answer.ToString(CultureInfo.InvariantCulture);
                });
            
            Console.WriteLine(res);
        }

        private async static Task<string> button2_Click()
        {

            var t = new Task<string>(
                () =>
                    {
                        var i = 9990;
                        var answer = 9990 / 45;
                        return answer.ToString(CultureInfo.InvariantCulture);
                    }
                );
            t.Start();
            var res1 = await t;
            Console.WriteLine(res1);
            return res1;

        }
    }
}
