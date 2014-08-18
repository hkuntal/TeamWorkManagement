using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelPLinqAndTasks
{
    internal class Parallels
    {
        private static Int64 DirectoryBytes(String path, String searchPattern,
                                            SearchOption searchOption )
        {
            var files = Directory.EnumerateFiles(path, searchPattern, searchOption);
            Int64 masterTotal = 0;
            ParallelLoopResult result = Parallel.ForEach<String, Int64>(
                files,
                () =>
                    {
                        // localInit: Invoked once per task at start
                        // Initialize that this task has seen 0 bytes
                        return 0; // Set taskLocalTotal initial value to 0
                    },
                (file, loopState, index, taskLocalTotal) =>
                    {
                        // body: Invoked once per work item
                        // Get this file's size and add it to this task's running total
                        Int64 fileLength = 0;
                        FileStream fs = null;
                        try
                        {
                            fs = File.OpenRead(file);
                            fileLength = fs.Length;
                        }
                        catch (IOException)
                        {
                            /* Ignore any files we can't access */
                        }
                        finally
                        {
                            if (fs != null) fs.Dispose();
                        }
                        return taskLocalTotal + fileLength;
                    },
                taskLocalTotal =>
                    {
                        // localFinally: Invoked once per task at end
                        // Atomically add this task's total to the "master" total
                        Interlocked.Add(ref masterTotal, taskLocalTotal);
                    });
            return masterTotal;
        }

        public static void CheckExceptionWithParallelForEach()
        {
            try
            {
                Parallels.CheckExceptionInForEachLoop();
            }
            catch (AggregateException ex)
            {
                foreach (var e in ex.InnerExceptions)
                {
                    Console.WriteLine(e.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }

        public static void CheckExceptionInForEachLoop()
        {
            //create a temp collection
            IList<int> coll = new List<int>();
            //Add digits to the  collection
            for (int i = 9000; i <= 999999; i++)
            {
                coll.Add(i);
            }

            //call for each loop on the collection
            Parallel.ForEach(coll, j =>
                {
                    Console.WriteLine(j*j);
                    if (j > 99999)
                    {
                        Console.WriteLine(j/(j-j));
                    }
                });
        }
    }
}
