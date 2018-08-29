# GTA IV: Left Shift Sprint Fix Mod

This a tiny mod that fixes an oversight in Grand Theft Auto IV where the left
shift key *always* sprints, regardless of whether it's being held or pressed
repeatedly, unlike other keys.

This behavior is corrected through mod by making a call to a native function of
the game that disables sprinting for a given player, thus it is used when the
key is not being pressed repeatedly.

There is also a feature where you can use the caps lock to revert to vanilla
behavior, so you won't have hand cramps at the end of the day.

Be aware that this won't work well in case "sticky keys" is enabled in your
system. To check if it's enabled, simply hit the shift key a few times to see
if a window about "sticky keys" pops up, if it does then follow the instructions
in it to disable it.

## How to build

To build, simply use either the .NET CLI tools with the command ```dotnet build```
or through Visual Studio (though I haven't tested with it). Requires that you
place a copy or link to ```ScriptHookDotNet.dll``` in the project folder, which
is added as an assembly reference.

You must then change the extension of the output to ```.net.dll``` for the .NET
scripthook to recognize it. Then simply copy/link it and the .ini file to the
```scripts``` folder in your GTA IV folder.

## The future...

I'm planning to make an option to make sprinting behave like it does in GTA V,
but I need a way to make the game "think" you're pressing the shift. If anyone
knows how to do that, feel free to make an issue report or even a pull request.
