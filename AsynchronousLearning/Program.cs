using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AsynchronousLearning
{
    class Program
    {
        static void Main(string[] args)
        {
            //PerformAsynchronousOperationsusingEventBasedAPM();

            //PerformAsynchronousOperationsusingCallbackBasedAPM();

            //PerformAsynchronousOperationsusingAsyncAwait();

            //PerformAsynchronousOperationsusingAsyncAwaitAndTask();

            // Uncommenting this method will produce build error
            /*Refer to this url for wait/awaiter pattern implementation - http://weblogs.asp.net/dixin/understanding-c-async-await-2-awaitable-awaiter-pattern*/
            //PerformAsynchronousOperationsusingCustomAwayableTasks();

            MyOwnAsyncExample.Start();

            Console.ReadLine();
        }

        private static async void PerformAsynchronousOperationsusingCustomAwayableTasks()
        {
            //var result = await CustomAsync.DoSomethingAsync();

            //Console.WriteLine(result);
        }

        private static async void PerformAsynchronousOperationsusingAsyncAwaitAndTask()
        {
            var req = (HttpWebRequest) WebRequest.Create("http://google.com");
            //req.Method = "HEAD";
            // Create a task object. We are trying to simulate the task object that is returned by async functions as DownloadStringTaskAsync
            Task<WebResponse> tresp = Task.Factory.FromAsync<WebResponse>(req.BeginGetResponse,
                                                                                  req.EndGetResponse, null);

            var resp = (HttpWebResponse) await tresp;
            // After calling the above line the execution will return back to the calling method
            // The below code will be invoked once the await code operation completes.
            var headers = resp.Headers;
            var headerStrings = from header in headers.Keys.Cast<string>()
                                select string.Format("{0} {1} ", header, headers[header]);

            Console.WriteLine(string.Join(Environment.NewLine, headerStrings.ToList()));

        }

        private static async void PerformAsynchronousOperationsusingAsyncAwait()
        {
            try
            {
                // Using an async keyword in the method signatire indicates that await keyword will be used inside the method
                WebClient w = new WebClient();

                var t = await w.DownloadStringTaskAsync(new Uri("http://google1z0.com"));

                // The execution will go back to the calling method at this point. Execution will come back to this point
                // once the result is received at the above statement. Basically, anything after this point becomes a callback
                // when the asynchronous process completes, which is done by the compiler.
                Console.WriteLine(t);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void PerformAsynchronousOperationsusingCallbackBasedAPM()
        {
            /*
             But, every BeginXxx method has two additional parameters: userCallback and
stateObject. The userCallback parameter is of the AsyncCallback delegate type:
public delegate void AsyncCallback(IAsyncResult ar);
For this parameter, you pass the name of a method (or lambda expression) representing code
that you want to be executed by a thread pool thread when the asynchronous I/O operation
completes. The last parameter to a BeginXxx method, stateObject, is a reference to any
object you’d like forwarded on to your callback method. Inside your callback method, you
access your objectState by querying the IAsyncResult interface’s read-only AsyncState
property.
All BeginXxx methods return an object that implements the System.IAsyncResult interface.
When you call a BeginXxx method, it constructs an object that uniquely identifies your I/O
request, queues up the request to the Windows device driver, and returns to you a reference
to the IAsyncResult object. You can think of this object as your receipt. You can actually
ignore the object reference returned from BeginXxx because the CLR internally holds
a reference to the IAsyncResult object as well. When the operation completes, a thread
pool thread will invoke your callback method, passing to it a reference to the internally held
IAsyncResult object.
Inside your method, you’ll call the corresponding EndXxx method, passing it the
IAsyncResult object. The EndXxx method returns the same result that you would have
gotten if you had called the synchronous method. For example, FileStream’s Read method
returns an Int32 that indicates the number of bytes actually read from the stream.
FileStream’s EndRead method’s return value has the same meaning:
             */
            var req = (HttpWebRequest) WebRequest.Create("http://google.com");
            //req.Method = "HEAD";
            req.BeginGetResponse(aSyncResult =>
                {
                    //This delegate is invoked once the operation is completed
                    var resp = (HttpWebResponse) req.EndGetResponse(aSyncResult);
                    var headers = resp.Headers;
                    var headerStrings = from header in headers.Keys.Cast<string>()
                                        select string.Format("{0} {1} ", header, headers[header]);

                    Console.WriteLine(string.Join(Environment.NewLine,headerStrings.ToList()));
                }, null);
        }

        private static void PerformAsynchronousOperationsusingEventBasedAPM()
        {
            /*The suspension of an async method at an await expression doesn't constitute an exit from the method, and finally blocks don’t run.*
             * http://msdn.microsoft.com/en-us/library/hh191443.aspx/
             */
            WebClient w = new WebClient();

            //This will load the string synchronously
            //string s = w.DownloadString("http://google.com");
            //Console.WriteLine(s);

            //To downlaod the string synchronously
            w.DownloadStringCompleted += w_DownloadGoogleStringCompleted;
            w.DownloadStringAsync(new Uri("http://google.com"));

            DownloadStringInBackground2("http://www.contoso.com/GameScores.html");
        }

        private static void w_DownloadGoogleStringCompleted(Object o, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                Console.WriteLine("*********************************************************************************************************************");
                Console.WriteLine(e.Result);
            }
        }

        // Sample call : DownloadStringInBackground2 ("http://www.contoso.com/GameScores.html");
        public static void DownloadStringInBackground2(string address)
        {
            WebClient client = new WebClient();
            Uri uri = new Uri(address);

            // Specify that the DownloadStringCallback2 method gets called 
            // when the download completes.
            client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(w_DownloadGoogleStringCompleted);
            client.DownloadStringAsync(uri);
        }
    }
}
