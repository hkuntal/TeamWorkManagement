using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 This is all about static classess
 1. Can a static class derive from another static class - NO. Because static classes are ingerently considered sealed and we cannot 
 * inherit from Sealed classes. This also means that we cannot derive instance class from Static classes.
 
 2. Can we derive static classes from Instance classes - No we can't even do that.
 * 
 */

namespace OOPSConcepts
{
    class AllAboutStaticClasses
    {

    }

    public class StaticParent
    {
        
    }

    public class StaticChild : StaticParent
    {
        
    }
}
