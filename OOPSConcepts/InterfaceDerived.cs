using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OOPSConcepts
{
    class InterfaceDerived:IParent
    {
        public string TestMethod()
        {
            return "Interface method Test Method is being called";
        }
        public string NonInterfaceMethod()
        {
            return "Non Interface method is being called";
        }
    }
}
