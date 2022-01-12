# GamepadMidi

[![Windows Build](https://ci.appveyor.com/api/projects/status/5w8y2qrnh7rj4q50?svg=true)](https://ci.appveyor.com/project/InvoxiPlayGames/gamepad-midi/build/artifacts)

Use your Rock Band 3 Keytar or Drum Kit as a MIDI controller - wirelessly!
Connect a controller, start the program, select the controller
and the output MIDI device and start shredding.

I suggest installing [LoopBe1](https://www.nerds.de/en/download.html) if you want to use this as a midi controller for e.g. a DAW.

## Keytar

Use left and right on the D-pad to switch octaves.

## Drum kit

The drums are output on MIDI channel 10. Currently the MIDI notes are fixed and assume you have the Pro Cymbals attached.

## Support

- For Xbox 360 instruments, use an Xbox 360 wireless controller adapter to connect the controllers.
- For Wii instruments (Keytar support only right now), plug in the dongle, install the WinUSB driver for the dongle using a tool like [Zadig](https://zadig.akeo.ie/)
- PS3 instrument support is unavailable, but is planned.

## TODO

- Use Keytar touch strip data for pitch bend/modulation
- Support PS3 instruments and other Wii instruments
- Finish Pro Guitar support

## Screenshot
![screenshot](https://i.imgur.com/eJGkYzU.png)
