using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace LibPlasticInstrument
{
    public class ControllerMonitor : IDisposable
    {
        public Thread thread;
        public bool cancel;

        public delegate void DisconnectEvent();
        public event DisconnectEvent OnDisconnect;

        public ControllerMonitor()
        {
            // no code
        }
        public ControllerMonitor(Controller c)
        {
            // no code
        }


        public void Dispose()
        {
            cancel = true;
            if (thread != null) thread.Join();
        }

        public void RaiseDisconnect()
        {
            OnDisconnect?.ThreadSafeInvoke();
        }
    }
}
