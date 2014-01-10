using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COR.Interfaces
{
    //The metadata to tie a handler to next successor
    public interface IRequestHandlerMetadata
    {
        Type SuccessorOf { get; }
    }
}
