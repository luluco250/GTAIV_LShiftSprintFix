# GTA IV: Left Shift Sprint Fix Mod

This a tiny mod that fixes an oversight in Grand Theft Auto IV where the left
shift key *always* sprints, regardless of whether it's being held of pressed
repeatedly, unlike other keys.

This behavior is corrected in this mod by making a call to a native function of
the game that disables sprinting for a given player, thus it is used when the
key is not being pressed repeatedly.

There is also a feature where you can use the caps lock to revert to vanilla
behavior, so you won't have hand cramps at the end of the day.
