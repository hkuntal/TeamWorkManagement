using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using COR.Interfaces;

namespace COR.Utils
{
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ExportHandlerAttribute : ExportAttribute
    {
        public Type SuccessorOf { get; set; }

        public ExportHandlerAttribute()
            : base(typeof(IRequestHandler))
        {
        }

        public ExportHandlerAttribute(Type successorOf)
            : base(typeof(IRequestHandler))
        {
            this.SuccessorOf = successorOf;
        }
    } 
}
