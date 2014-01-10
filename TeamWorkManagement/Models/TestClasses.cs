using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamWorkManagement.Models
{
    public class TestClasses
    {
        public TestClass1 ObjTestClass1 { get; set; }
        public string submit { get; set; }
    }
    public class TestClass1
    {
        public TestClass2 ObjTestClass2 { get; set; }
    }
    public class TestClass2
    {
        public string submit { get; set; }
    }
}