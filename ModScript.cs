using System;
using System.Diagnostics;
using System.Windows.Forms;
using GTA;
using GTA.Native;

public class ModScript : Script {
	int tickInterval;
	int sprintInterval;
	bool useCapsLock;

	Stopwatch stopwatch = new Stopwatch();

	public ModScript() {
		tickInterval = Settings.GetValueInteger("tickInterval", "Settings", 100);
		sprintInterval = Settings.GetValueInteger("sprintInterval", "Settings", 300);
		useCapsLock = Settings.GetValueBool("useCapsLock", "Settings", true);

		stopwatch.Start();
		KeyDown += new GTA.KeyEventHandler(OnKeyDown);
		Interval = tickInterval;
		Tick += new EventHandler(OnTick);
	}

	void OnTick(object sender, EventArgs e) {
		//	Can sprint if caps is activated or
		//	repeatedly pressing left shift.
		CanSprint((useCapsLock && Control.IsKeyLocked(Keys.CapsLock))
		|| stopwatch.ElapsedMilliseconds < sprintInterval);
	}

	void OnKeyDown(object sender, GTA.KeyEventArgs e) {
		if (e.Key == Keys.LShiftKey)
			stopwatch.Restart();
	}

	void CanSprint(bool b) {
		Function.Call("DISABLE_PLAYER_SPRINT", Player.Index, !b);
	}
}
