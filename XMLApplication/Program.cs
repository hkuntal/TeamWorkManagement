using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace XMLApplication
{
    class Program
    {
        static void Main(string[] args)
        {

            FileStream fs = new FileStream("StudentEntity.xml", FileMode.Open);
            //XmlTextReader reader = new XmlTextReader("StudentEntity.xml");
            var x = XmlReader.Create(fs);
            x.Read();
            x.ReadInnerXml();
            string s =
                "<?xml version=\"1.0\"?><student><name>Hariom</name><addresses><address type=\"HOME\"><stAddr1>987</stAddr1>      <stAddr2>Kamothe</stAddr2>      <city>Mumbai</city>      <state>Maharashtra</state>      <zip>12345</zip>    </address>  </addresses>  <birthDate>2012-03-17</birthDate>  <gender>Male</gender>  <ssn>12345623</ssn>  <other>Other</other></student>";

            XMLUtils.VerifyXmlFile(s);

            Console.WriteLine("The xml has been validated successfully");

            Console.ReadKey();
        }
    }
}
