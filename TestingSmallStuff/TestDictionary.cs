using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestingSmallStuff
{
    [TestClass]
    class TestDictionary
    {
        [TestMethod]
        public void TestLoopingOnDictionaryKeys()
        {
            // Create a dictionary object
            // NOTE: Please note that you cannot modify dictionaries value when u r looping over them. it will 
            // throw an exception. Create a copy of the Keys new List(objDictionary.Keys), loop over the keys and
            // then modify the dict values
            IDictionary<string,string> obj = new Dictionary<string, string>();
            obj["A"] = "Apple";
            obj["B"] = "Ball";
            obj["C"] = "Cat";

            // Try looping on the dictionary
            foreach (var key in obj)
            {
                Console.WriteLine("Modifying Key: " + key);
                obj[key.ToString()] = obj[key.ToString()] + "Hariom";
            }

        }
    }
}
