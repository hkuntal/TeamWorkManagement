using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AsynchronousLearning
{
    class CustomAsync
    {
        public static CustomAawaitable DoSomethingAsync()
        {
            return new CustomAawaitable();
            
        }
    }

    class CustomAawaitable
    {
        public CustomAwaiter GetAwaiter()
        {
            return new CustomAwaiter();
        }
    }

    class CustomAwaiter:INotifyCompletion
    {
        public bool BeginAwait(Action callback)
        {
            return false;
        }
        public string EndAwait()
        {
            return "End wait called";
        }
        public bool IsCompleted
        {
            get { return false; }
        }

        public void OnCompleted(Action continuation)
        {
            
        }
    }
}
