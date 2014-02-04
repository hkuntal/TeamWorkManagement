using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EventsDelegatesThreading
{
    public delegate void TapeRecorderControl (object sender, TapeRecorderArgs args);

    public delegate int TapeRecorderControlReturns(object sender, TapeRecorderArgs args);

    public class Program
    {
        static void Main(string[] args)
        {
            //TestEvents();

            //TestDelegates();

            //TestThreading();

            //CancellationDemo.Go();

            //TestThreading();

            //TestExceptions();

            //TestParallelism();

            TestEventsWithThreading();

            Console.ReadLine();

            
        }

        private static void TestExceptions()
        {
            CheckExceptionHandling obj = new CheckExceptionHandling();
            obj.Method1();
        }

        private static void TestEvents()
        {
//Create an object of class tape recorder
            TapeRecorder objTapeRecorder = new TapeRecorder();

            PlayTapeRecorder objPlayTapeRecorder = new PlayTapeRecorder();
            objTapeRecorder.Play += objPlayTapeRecorder.PlayTape;
            objTapeRecorder.PlayReturns += objPlayTapeRecorder.PlayTapeWithReturns;
            objTapeRecorder.PlayReturns += objPlayTapeRecorder.PlayTapeWithReturns2;

            objTapeRecorder.Pause += objPlayTapeRecorder.PlayTape;
            objTapeRecorder.FastForward += objPlayTapeRecorder.PlayTape;
            objTapeRecorder.Reverse += objPlayTapeRecorder.PlayTape;

            //Invoke the events
            objTapeRecorder.RaiseTheEvents();

            Console.ReadLine();
        }

        private static void TestEventsWithThreading()
        {
            Console.WriteLine("Registering events on the managed thread: " + System.Threading.Thread.CurrentThread.ManagedThreadId);
            //The idea here is to register the events on one thread and raise the events on other thread and check which thread executes the handler
            //FINDING: The thread on which the event is raised is the one that actually calls the event handler as well.
            TapeRecorder objTapeRecorder = new TapeRecorder();

            PlayTapeRecorder objPlayTapeRecorder = new PlayTapeRecorder();
            objTapeRecorder.Pause += objPlayTapeRecorder.PlayPause;
            objTapeRecorder.Play += objPlayTapeRecorder.PlayTape;
            
            //objTapeRecorder.PlayReturns += objPlayTapeRecorder.PlayTapeWithReturns;
            //objTapeRecorder.PlayReturns += objPlayTapeRecorder.PlayTapeWithReturns2;

            //objTapeRecorder.Pause += objPlayTapeRecorder.PlayTape;
            //objTapeRecorder.FastForward += objPlayTapeRecorder.PlayTape;
            //objTapeRecorder.Reverse += objPlayTapeRecorder.PlayTape;

            //Invoke the events
            objTapeRecorder.RaiseTheEventsInOtherThread();

            }

        private static void TestDelegates()
        {
            DelegateTest  objDelegateTest = new DelegateTest();
            objDelegateTest.Calculator(objDelegateTest.Sum);
            Console.ReadLine();
        }
        private static void TestThreading()
        {
            //ThreadingTest objThreadingTest = new ThreadingTest();
            //objThreadingTest.TestThreadPool();

            //TestTask.CreateAndTestATask();

            var objThreadingTest = new ThreadingTest();
            //objThreadingTest.CreateANewThreadAndTest();

            //TestTask.CreateParentChildTasks();

            //TestTask.TestTaskAndExceptions();

            //TestTask.TestTaskFactory();

            TestTask.TestTaskFactoryCreationAndExceptionHandling();

            Console.ReadLine();
        }
        private static void TestParallelism()
        {
            Parallelism.InvokeMethodsParallely();

            Console.ReadLine();
        }
    }

    public class PlayTapeRecorder
    {
        public void PlayTape(object sender, TapeRecorderArgs args)
        {
            Console.WriteLine(args.SongName + " is now being played on " + System.Threading.Thread.CurrentThread.ManagedThreadId);
        }
        public int PlayTapeWithReturns(object sender, TapeRecorderArgs args)
        {
            //The event handlers are called on the same thread  on which the events are raised. If there are multiple event handlers registered with the event
            //then they are called sequentially in the order of registration.
            Console.WriteLine(args.SongName + " is now being played on " + System.Threading.Thread.CurrentThread.ManagedThreadId);
            return 5;
        }
        public int PlayTapeWithReturns2(object sender, TapeRecorderArgs args)
        {
            Console.WriteLine(args.SongName + " second part is now being played on " + System.Threading.Thread.CurrentThread.ManagedThreadId);
            return 10;
        }
        public void PlayPause(object sender, TapeRecorderArgs args)
        {
            Console.WriteLine(args.SongName + " is now paused on " + System.Threading.Thread.CurrentThread.ManagedThreadId);
        }
        //public void PlayFastForward()
        //{
        //    Console.WriteLine("Fast forwarding the tape");
        //}
        //public void PlayReverse()
        //{
        //    Console.WriteLine("Reversing the tape");
        //}
    }

    public class TapeRecorder
    {
        public event TapeRecorderControl Play;

        public event TapeRecorderControlReturns PlayReturns;

        public event TapeRecorderControl Pause;

        public event TapeRecorderControl FastForward;

        public event TapeRecorderControl Reverse;

        public void RaiseTheEvents()
        {
            //Raise all the events
            Play(this, new TapeRecorderArgs("Tere Naam"));

            //If an event handler resturns a parameter then the paramweter returned by the last event hanlder gets captured.
            int a = PlayReturns(this, new TapeRecorderArgs("Katrina returns"));

            Console.WriteLine(a.ToString());
            //Play("Pause");

            //Play("Fast Forward");

            //Play("Reverse");
        }
        public void RaiseTheEventsInOtherThread()
        {
            //Create a new thread
            System.Threading.Thread t = new Thread(RaiseEvents);
            t.Start();
        }

        private void RaiseEvents()
        {
            //Raise all the events
            Console.WriteLine("Raising event on thread: " + System.Threading.Thread.CurrentThread.ManagedThreadId);

            Play(this, new TapeRecorderArgs("Tere Naam"));

            Pause(this, new TapeRecorderArgs("Tere Naam"));


        }
    }

    public class TapeRecorderArgs:EventArgs
    {
        public TapeRecorderArgs(string songName)
        {
            this.SongName = songName;
        }

        public string SongName { get; set; }
    }
}
