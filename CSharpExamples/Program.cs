using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
namespace CSharpExamples
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //CheckStaticMethods();
            //Debug.WriteLine("Testing");
            //Collections.TestCollection();
            
            //ProcessFlatFiles();

            Console.WriteLine("Hariom");
            //Investigate ob = new Investigate();
            //ob.TestArray();
            //Console.WriteLine(ob.GenericMethod<int>("hariom"));
            Console.ReadLine();

            Debug.WriteLine("Harioms Debug Window");

        }

        private static void CheckStaticMethods()
        {
            Console.WriteLine("Trying to access static members");
            var a = StaticClass.StaticMember1;
            Console.WriteLine("Static Member 1" + a.ToString());

            //Call the method again
            //var b = StaticClass.StaticMember1;
            //var c = StaticClass.StaticMember1;

            Console.WriteLine("Calling the lazy method");
            var c = StaticClass.StaticMember4;
            Console.WriteLine(c.Value.ToString());
            
        }

        public static void ProcessFlatFiles()
        {
            Console.WriteLine("What od you want to do? \n 1. Process files simple way, or \n 2. Extract time and SopIds");
            var option = Console.ReadLine();
            if(option == "1")
            ProcessFile();
            else
            ProcessTheClientLogFile();
        }

        private static void ProcessFile()
        {
            try
            {
                Console.WriteLine("Enter the MetaData file path");
                var metaData = Console.ReadLine();

                Console.WriteLine("Enter the file path to be processed");
                var path = Console.ReadLine();
                //Console.WriteLine("Enter the output file path");
                //var outputPath = Console.ReadLine();

                Console.WriteLine("1. Remove $$ statements \n 2. Remove Spaces");

                var option = Console.ReadLine();

                //Console.WriteLine("Eneter the complete $$ prefix that you want to segregate");
                //var prefix = Console.ReadLine();

                Console.WriteLine("File is being processed");
                //var obj = new ProcessTheOutputFile(path, outputPath);
                var obj = new ProcessTheOutputFile(path, metaData);
                if (option == "1")
                {
                    obj.ProcessFile1();
                }
                else if (option == "2")
                {
                    obj.RemoveSpaces();
                }

                ProcessAgain();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An exception has occurred: " + ex.Message);
                ProcessAgain();
            }
        }

        private static void ProcessAgain()
        {
            Console.WriteLine("Processing Over ! Do you want to process another file (y/n)?");
            var a = Console.ReadLine();
            if (a != "y")
            {
                Console.WriteLine("press any key to exit");
                Console.ReadKey();
            }
            else
            {
                ProcessFile();
            }
        }

        private static void ProcessTheClientLogFile()
        {
            Console.WriteLine("Enter the file path to be processed");
            var path = Console.ReadLine();
            Console.WriteLine("Enter the output file path");
            var outputPath = Console.ReadLine();

            Console.WriteLine("1. Analyze the time taken for lossy images to load on the client side \n" +
                              "2. Analyze the time taken to download the lossless images on the client side");

            var option = Console.ReadLine();

            Console.WriteLine("File is being processed");
            var obj = new ProcessTheOutputFile(path, outputPath);
            obj.ProcessClientLogFile(option);
           

            Console.WriteLine("Processing Over ! Do you want to process another file (y/n)?");
            var a = Console.ReadLine();
            if (a != "y")
            {
                Console.WriteLine("press any key to exit");
                Console.ReadKey();
            }
            else
            {
                ProcessTheClientLogFile();
            }
        }
        private class Investigate
        {
            public string GenericMethod<T>(string parameter)
            {
                return Convert.ToString(parameter);
            }

            public void TestArray()
            {
                int[] a = new int[] {4, 5, 6};
                Console.WriteLine(a.Min());

                // byte[] b1 = new byte[] {0xA992,0x6};
                //new Random().NextBytes(b);

                DateTime dt = DateTime.Now;
                var time = dt.Millisecond;

                //byte[] b = new byte[20];
                //new Random().NextBytes(b);
                //short s1 = -16388;
                //int s2 = s1;
                //var p = s1 & 2;
                //int p = s1 << 3; 

                byte[] byteArray2 = {136};
                sbyte a5 = Convert.ToSByte(byteArray2[0]);
                sbyte s5 = (sbyte) (byteArray2[0]);
                ////Get the min  and max values
                //b.Min();
                byte[] byteArray1 = {0, 255};
                sbyte z = (sbyte) byteArray1[1];
                uint b = Convert.ToUInt32(z);
                //uint c = Convert.ToInt32((sbyte)byteArray1[0]);
                //var intermediateArray = new sbyte[byteArray1.Length];
                //Buffer.BlockCopy(byteArray1, 0, intermediateArray, 0, byteArray1.Length);
                //int a1 = (sbyte) byteArray1[0];
                //int a2 = (sbyte)byteArray1[1];

                byte[] byteArray = {32, 14};
                int aa = BitConverter.ToInt16(byteArray, 0);
                Console.WriteLine(aa);
            }
        }

        public class ProcessTheOutputFile
        {
            private string path;
            private string metaDataPath;
            private string outputPath;
            public ProcessTheOutputFile(string filepath,string outputPath)
            {
                path = filepath;
                this.metaDataPath = outputPath;
                this.outputPath = outputPath;
            }

            public void ProcessClientLogFile(string option)
            {
                Console.WriteLine("Is the browser IE 9 (y/n)?");
                var browser = Console.ReadLine();
                String lossyString;
                String losslessString;
                lossyString = (browser == "y")
                                      ? "LOG: $$ Time For downloading lossy image"
                                      : "$$ Time For downloading lossy image";

                losslessString = (browser == "y")
                                      ? "LOG: $$ Time For downloading lossless image"
                                      : "$$ Time For downloading lossy image";
                
                if (option == "1")
                {
                    //Process for lossy images
                    using (var reader = new StreamReader(path))
                    {
                        //read the required lines and write them into a new file
                        using (var writer = new StreamWriter(outputPath))
                        {
                            string line;
                            writer.WriteLine("This is the Lossy data being analyzed");
                            writer.WriteLine("************************************** \n");

                            while ((line = reader.ReadLine()) != null)
                            {
                                if (line.StartsWith(lossyString))
                                {
                                    var a = line.IndexOf("token");
                                    var b = a + 5;

                                    var i = line.IndexOf("is");
                                    i = i - 1;

                                    var ms =  line.IndexOf("ms");

                                    //Retrieve the token number
                                    string s = line.Substring(b, i - b) + "\t" + line.Substring(i + 3, ms - i - 3);
                                    writer.WriteLine(s);
                                }

                            }
                        }
                    }
                }
                else if (option == "2")
                {
                    //Process for lossless images
                    using (var reader = new StreamReader(path))
                    {
                        //read the required lines and write them into a new file
                        using (var writer = new StreamWriter(outputPath))
                        {
                            string line;
                            writer.WriteLine("This is the Lossless data being analyzed");
                            writer.WriteLine("**************************************** \n");
                            while ((line = reader.ReadLine()) != null)
                            {
                                if (line.StartsWith(losslessString))
                                {
                                    var a = line.IndexOf("token");
                                    var b = a + 5;

                                    var i = line.IndexOf("is");
                                    i = i - 1;

                                    var ms = line.IndexOf("ms");

                                    //Retrieve the token number
                                    string s = line.Substring(b, i - b) + "\t" + line.Substring(i + 3, ms - i - 3);
                                    writer.WriteLine(s);
                                }
                            }
                        }
                    }
                }
                
            }

            public void ProcessFile1()
            {
                //

                using (var metaReader = new StreamReader(metaDataPath))
                {
                    string metaLine;
                    while ((metaLine = metaReader.ReadLine()) != null)
                    {
                        if (string.IsNullOrEmpty(metaLine))
                            continue;
                        string[] arr = metaLine.Split('~');
                        string outputPath1 = Path.Combine(Path.GetDirectoryName(path),
                                                          Path.GetFileNameWithoutExtension(path) + arr[0] + ".txt");
                        var prefix = arr[1];

                        using (var reader = new StreamReader(path))
                        {
                            //read the required lines and write them into a new file
                            using (var writer = new StreamWriter(outputPath1))
                            {
                                string line;
                                //Console.WriteLine("Do you have a PID number, If yes please enter that number, else press 'n'");
                                //var pid = Console.ReadLine();
                                //if (pid != "n")
                                //{
                                //    while ((line = reader.ReadLine()) != null)
                                //    {
                                //        if (line.StartsWith("["+ pid + "] $$"))
                                //        {
                                //            writer.WriteLine(line.Substring(pid.Length + 2, line.Length - (pid.Length + 2)));
                                //        }
                                //    }
                                //}
                                //else
                                //{
                                while ((line = reader.ReadLine()) != null)
                                {
                                    foreach (var ab in prefix.Split(';'))
                                    {
                                        if (line.StartsWith(ab))
                                        {
                                            writer.WriteLine(line);
                                        }
                                    }
                                    
                                }
                                //}
                            }
                        }
                    }
                }
            }

            public void RemoveSpaces()
            {
                using (var reader = new StreamReader(path))
                {
                    //read the required lines and write them into a new file
                    using (var writer = new StreamWriter(outputPath))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (!string.IsNullOrEmpty(line))
                            {
                                writer.WriteLine(line);
                            }
                        }
                    }
                }
            }
        }
    }
}
