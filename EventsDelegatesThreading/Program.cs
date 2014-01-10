using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            TestThreading();

            //TestExceptions();

            //TestParallelism();
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
            Console.WriteLine(args.SongName + " is now being played on " + System.Threading.Thread.CurrentThread.ManagedThreadId);
            return 5;
        }
        public int PlayTapeWithReturns2(object sender, TapeRecorderArgs args)
        {
            Console.WriteLine(args.SongName + " second part is now being played on " + System.Threading.Thread.CurrentThread.ManagedThreadId);
            return 10;
        }
        //public void PlayPause()
        //{
        //    Console.WriteLine("Tape recorder has now paused");
        //}
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

            int a = PlayReturns(this, new TapeRecorderArgs("Katrina returns"));

            Console.WriteLine(a.ToString());
            //Play("Pause");

            //Play("Fast Forward");

            //Play("Reverse");
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
