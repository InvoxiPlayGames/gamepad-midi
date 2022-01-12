using LibUsbDotNet;
using LibUsbDotNet.Main;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibPlasticInstrument
{
  public static class ControllerEnumerator
  {
    //static List<RefCount<Controller>> Controllers = null;
    /// <summary>
    /// Returns an list of controller capabilities.
    /// </summary>
    /// <returns></returns>
    public static List<Controller> EnumerateControllers()
    {
      var ret = new List<Controller>();
      for (uint i = 0; i < XInput.XUSER_MAX_COUNT; i++)
      {
        var caps = new XInput.Capabilities();
        if (0 == XInput.XInputGetCapabilities(i, 1, ref caps))
        {
          ret.Add(new Controller(i, caps));
        }
      }
      uint addedUsb = 0;
      foreach (UsbRegistry device in UsbDevice.AllDevices)
      {
        // USB\VID_1BAD&PID_3330
        if (device.Vid == 0x1BAD && device.Pid == 0x3330)
        {
          ret.Add(new Controller(addedUsb++, device.Device, ControllerType.Keytar));
        }
      }
      return ret;
    }
  }
}
