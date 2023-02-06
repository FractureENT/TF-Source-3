using Sandbox;

namespace TFS2;

public partial class GrenadeLauncher
{
	//Time since we started this reload loop cycle
	public TimeSince TimeSinceReloadLoopCycle { get; set; }

	public bool ReloadLoopStarted { get; set; }

	// What time during reload animation should the ammo bodygroup be set?
	public float ReloadSetBodygroupTime = 0.26f;

	//Are we ready to set bodygroups?
	public bool ReloadSetBodygroupReady { get; set; } = false;

	//Have we set bodygroups this cycle?
	public bool ReloadHasSetBodygroup { get; set; } = false;

	public override void SendAnimParametersOnReloadStop()
	{
		ReloadSetBodygroupReady = false;
		ReloadHasSetBodygroup = false;

		base.SendAnimParametersOnReloadStop();
	}
	public override void SimulateReload()
	{
		base.SimulateReload();
		DynamicDrumSimulate();
	}
	public virtual void DynamicDrumSimulate()
	{
		//base.SimulateReload();

		//If we've entered reload loop, toggle reload state
		//b_reload_loop is a standard bool, while b_reload is an Auto-reset bool. This is done to prevent flickering due to slight delays in animation playing

		if ( TimeSinceReloadLoopCycle >= ReloadSetBodygroupTime && ReloadSetBodygroupReady && !ReloadHasSetBodygroup )
		{
			SetLoadedBodygroup( Clip + 1 );
			ReloadHasSetBodygroup = true;
		}
	}
	public override void FinishedReloadCycle()
	{
		UpdateViewmodelParams();
		base.FinishedReloadCycle();
	}

	public override void SendAnimParametersOnReloadInsert()
	{
		SetLoadedBodygroup( Clip );
		ReloadLoopStarted = true;
		if ( !ReloadSetBodygroupReady )
		{
			TimeSinceReloadLoopCycle = 0f;
		}
		ReloadSetBodygroupReady = true;
	}

	public virtual void UpdateViewmodelParams()
	{
		SetLoadedBodygroup( Clip );
		SendViewModelAnimParameter("f_drumangle", Clip * 90);
	}

	public virtual void SetLoadedBodygroup(int ammo)
	{
		switch(ammo)
		{ 
			case 0:
				SendViewModelAnimParameter( "grenade_loaded1", false );
				SendViewModelAnimParameter( "grenade_loaded2", false );
				SendViewModelAnimParameter( "grenade_loaded3", false );
				SendViewModelAnimParameter( "grenade_loaded4", false );
				break;
			case 1:
				SendViewModelAnimParameter( "grenade_loaded1", true );
				SendViewModelAnimParameter( "grenade_loaded2", false );
				SendViewModelAnimParameter( "grenade_loaded3", false );
				SendViewModelAnimParameter( "grenade_loaded4", false );
				break;
			case 2:
				SendViewModelAnimParameter( "grenade_loaded1", true );
				SendViewModelAnimParameter( "grenade_loaded2", true );
				SendViewModelAnimParameter( "grenade_loaded3", false );
				SendViewModelAnimParameter( "grenade_loaded4", false );
				break;
			case 3:
				SendViewModelAnimParameter( "grenade_loaded1", true );
				SendViewModelAnimParameter( "grenade_loaded2", true );
				SendViewModelAnimParameter( "grenade_loaded3", true );
				SendViewModelAnimParameter( "grenade_loaded4", false );
				break;
			case 4:
				SendViewModelAnimParameter( "grenade_loaded1", true );
				SendViewModelAnimParameter( "grenade_loaded2", true );
				SendViewModelAnimParameter( "grenade_loaded3", true );
				SendViewModelAnimParameter( "grenade_loaded4", true );
				break;
		}
	}
}

