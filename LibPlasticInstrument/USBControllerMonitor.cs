using LibUsbDotNet;
using LibUsbDotNet.Main;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace LibPlasticInstrument
{
    public class USBControllerMonitor : ControllerMonitor
    {
        private UsbDevice device;
        private UsbEndpointReader endpointReader;

        public delegate void USBControllerEvent(byte[] bytes);
        public event USBControllerEvent OnUSBStateChanged;

        public bool Connected { get; private set; }

        private byte lastState;
        private int bytesRead;
        private byte[] readBuffer = new byte[27];

        public USBControllerMonitor(Controller c)
        {
            if (c.Platform != ControllerPlatform.USB)
            {
                throw new Exception($"Attempted to intialise USBControllerMonitor with {c.Platform} device");
            }
            if (!c.Device.IsOpen)
            {
                throw new Exception($"Controller {c.Index} is not connected");
            }
            device = c.Device;
            endpointReader = device.OpenEndpointReader(ReadEndpointID.Ep01);
            endpointReader.Read(readBuffer, 27, out bytesRead);
            lastState = readBuffer[25];
            cancel = false;
            Connected = true;
            thread = new Thread(PollThread) { Name = $"USB Controller {c.Index} poll thread" };
            thread.Start();
        }
        private void PollThread()
        {
            var sleepTime = TimeSpan.FromMilliseconds(0.5);
            while (!cancel)
            {
                if (!Connected)
                {
                    // Do less while waiting for the controller to come back online.
                    Thread.Sleep(250);
                    continue;
                }
                if (!device.IsOpen || !device.UsbRegistryInfo.IsAlive)
                {
                    Connected = false;
                    RaiseDisconnect();
                    continue;
                }
                endpointReader.Read(readBuffer, 27, out bytesRead);
                if (readBuffer[25] != lastState)
                {
                    lastState = readBuffer[25];
                    OnUSBStateChanged.ThreadSafeInvoke(readBuffer);
                }
                Thread.Sleep(sleepTime);
            }
        }
    }
}
