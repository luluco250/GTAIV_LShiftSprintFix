using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GTA;
using GTA.Native;

public class ModScript : Script {
	  //==========//
	 // Settings //
	//==========//

	readonly int tickInterval;
	readonly int sprintInterval;
	readonly bool useCapsLock;

	  //=======//
	 // State //
	//=======//

	int lastClickTime = Game.GameTime;
	bool isSprinting = false;

	  //=============//
	 // Constructor //
	//=============//

	public ModScript() {
		// Read settings...
		tickInterval = Settings.GetValueInteger("TickInterval", "Settings", 100);
		sprintInterval = Settings.GetValueInteger("SprintInterval", "Settings", 300);
		useCapsLock = Settings.GetValueBool("UseCapsLock", "Settings", true);

		// Setup events...
		KeyDown += new GTA.KeyEventHandler(OnKeyDown);

		Interval = tickInterval;
		Tick += new EventHandler(OnTick);
	}

	  //========//
	 // Events //
	//========//

	void OnTick(object sender, EventArgs args) {
		bool sprint = useCapsLock && Control.IsKeyLocked(Keys.CapsLock);
		sprint = sprint || ((Game.GameTime - lastClickTime) < sprintInterval);

		CanSprint(isSprinting && sprint);
		isSprinting = sprint;
	}

	void OnKeyDown(object sender, GTA.KeyEventArgs args) {
		if (args.Key == Keys.LShiftKey)
			lastClickTime = Game.GameTime;
	}

	  //=========//
	 // Helpers //
	//=========//

	void CanSprint(bool b) {
		Function.Call("DISABLE_PLAYER_SPRINT", Player.Index, !b);
	}
}
