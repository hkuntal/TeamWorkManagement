using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Serialization
{
    class Program
    {
        static void Main(string[] args)
        {
            SerializeObject();
            DeserializeObject();
            Console.ReadLine();
        }
        private static void SerializeObject()
        {
            Data obj = new Data();
            obj.n1 = 1;
            obj.n2 = 24;
            obj.str = "Hariom";
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("MyFile.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, obj);
            stream.Close();
        }
        private static void DeserializeObject()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("MyFile.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
            IData obj = (IData)formatter.Deserialize(stream);
            stream.Close();

            // Here's the proof.
            Console.WriteLine("n1: {0}", obj.n1);
            Console.WriteLine("n2: {0}", obj.n2);
            Console.WriteLine("str: {0}", obj.str);
        }
    }

    [Serializable]
    class Data:IData
    {

        public int n1 { get; set; }
        public int n2 { get; set; }
        public string str { get; set; }
    }

    internal interface IData
    {
        int n1 { get; set; }
        int n2 { get; set; }
        String str { get; set; }
    }
}
