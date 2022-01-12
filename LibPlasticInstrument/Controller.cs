using LibUsbDotNet;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibPlasticInstrument
{
  public enum ControllerType
  {
    Unknown,
    Keytar,
    Drums,
    ProGuitar
  }
  
  public enum ControllerPlatform
  {
    XInput,
    USB
  }
  
  public class Controller
  {
    public XInput.Capabilities Capabilities { get; }
    public ControllerType Type { get; }
    public ControllerPlatform Platform { get; }
    public uint Index { get; }
    public UsbDevice Device { get; }

    internal Controller(uint index, XInput.Capabilities caps)
    {
      Platform = ControllerPlatform.XInput;
      switch (caps.SubType)
      {
        case XInput.DevSubType.Keytar:
          Type = ControllerType.Keytar;
          break;
        case XInput.DevSubType.DrumKit:
          Type = ControllerType.Drums;
          break;
        case XInput.DevSubType.ProGuitar:
          Type = ControllerType.ProGuitar;
          break;
      }
      Capabilities = caps;
      Index = index;
    }
    internal Controller(uint index, UsbDevice device, ControllerType type)
    {
      Platform = ControllerPlatform.USB;
      Type = type;
      Index = index;
      Device = device;
    }

    public override string ToString()
    {
      return $"{Platform} {Index}: {Type}";
    }
  }
}
