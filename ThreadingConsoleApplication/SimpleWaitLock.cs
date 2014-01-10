using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ThreadingConsoleApplication
{
    internal sealed class SimpleWaitLock : IDisposable
    {
        private AutoResetEvent m_ResourceFree = new AutoResetEvent(true); // Initially free
        public void Enter()
        {
            // Block efficiently in the kernel for the resource to be free, then return
            bool b = m_ResourceFree.WaitOne();
        }
        public void Leave()
        {
            m_ResourceFree.Set();// Mark the resource as Free
        }
        public void Dispose() { m_ResourceFree.Dispose(); }
    }
}
