using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TraceDebug
{
    class Program
    {
        static void Main(string[] args)
        {
            // Both the lines below write the data to the output debug window. For this to work we need to define Debug and Trace constants in the Project properties
            // These statements do not work in Release mode but only in debug mode after the constants are defined.
            Debug.WriteLine("Hello Debug");
            Trace.WriteLine("Hello Trace");
            Trace.WriteLine("adad","Information");
            // However trace might still work in Release mode if we have some trace listeners defined explictly
            //Trace.TraceInformation();

            //private static TraceSource traceSource = CreateTraceSource();
            //traceSource.TraceEvent(TraceEventType.Information, id, "PERF: " + performanceMessage, "Performance");
        }
    }
}
