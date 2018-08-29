using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GTA;
using GTA.Native;

public class ModScript : Script {
	int tickInterval;
	int sprintInterval;

	int lastClickTime = Game.GameTime;
	bool isSprinting = false;

	public ModScript() {
		tickInterval = Settings.GetValueInteger("TickInterval", "Settings", 100);
		sprintInterval = Settings.GetValueInteger("SprintInterval", "Settings", 300);

		KeyDown += new GTA.KeyEventHandler(OnKeyDown);

		bool useCapsLock = Settings.GetValueBool("UseCapsLock", "Settings", true);
		Interval = tickInterval;

		if (Settings.GetValueBool("UseSimpleAlgorithm", "Settings", false)) {
			if (useCapsLock)
				Tick += new EventHandler(OnTick_Simple);
			else
				Tick += new EventHandler(OnTick_SimpleNoCapsLock);
		} else {
			if (useCapsLock)
				Tick += new EventHandler(OnTick);
			else
				Tick += new EventHandler(OnTick_NoCapsLock);
		}
	}

	void OnTick(object sender, EventArgs e) {
		if (Control.IsKeyLocked(Keys.CapsLock))
			return;
		
		OnTick_NoCapsLock(sender, e);
	}

	void OnTick_NoCapsLock(object sender, EventArgs e) {
		if ((Game.GameTime - lastClickTime) < sprintInterval) {
			// Don't sprint immediately
			if (isSprinting)
				CanSprint(true);
			
			isSprinting = true;
		} else {
			if (isSprinting)
				CanSprint(false);
			
			isSprinting = false;
		}
	}

	void OnTick_Simple(object sender, EventArgs e) {
		DoSimple(Control.IsKeyLocked(Keys.CapsLock));
	}

	void OnTick_SimpleNoCapsLock(object sender, EventArgs e) {
		DoSimple();
	}

	void DoSimple(bool caps = false) {
		CanSprint(caps || (Game.GameTime - lastClickTime) < sprintInterval);
	}

	void OnKeyDown(object sender, GTA.KeyEventArgs e) {
		if (e.Key == Keys.LShiftKey)
			lastClickTime = Game.GameTime;
	}

	void CanSprint(bool b) {
		Function.Call("DISABLE_PLAYER_SPRINT", Player.Index, !b);
	}
}
