using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OOPSConcepts
{
    public class VirtualOverride
    {

    }
    public class VOParent
    {
        public virtual string Display()
        {
            return "VOParent method called";
        }
    }

    public class VOChild : VOParent
    {
        public override string Display()
        {
            return "VOChild method called";
        }
    }
    public class VOGrandChild:VOChild
    {
        public override string  Display()
        {
            return "VOGrandChild method called";
        }
    }
}
