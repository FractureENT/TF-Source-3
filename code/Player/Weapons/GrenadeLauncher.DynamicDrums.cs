using Sandbox;
using System.Runtime.CompilerServices;

namespace TFS2;

public partial class GrenadeLauncher
{

	public TimeSince TimeSinceReloadLoopCycle { get; set; }	//Time since we started this reload loop cycle
	public bool ReloadSetBodygroupReady { get; set; } = false;	//Are we ready to set bodygroups?
	public bool ReloadHasSetBodygroup { get; set; } = false;    //Have we set bodygroups this cycle?
	public float ReloadSetBodygroupTime = 0.26f;    // What time during reload animation should the ammo bodygroup be set?


	public virtual void DynamicDrumSimulate()
	{
		//If we've entered reload loop, toggle reload state
		//After a certain delay, set the bodygroup to the NEXT expected ammo. Workaround for bodygroup events not being dynamic

		if ( TimeSinceReloadLoopCycle >= ReloadSetBodygroupTime && ReloadSetBodygroupReady && !ReloadHasSetBodygroup )
		{
			SetLoadedBodygroup( Clip + 1 );
			ReloadHasSetBodygroup = true;
		}
	}
	#region Overrides
	public override void SetupAnimParameters()
	{
		base.SetupAnimParameters();
		UpdateViewmodelParams();
	}
	public override void SimulateReload()
	{
		base.SimulateReload();
		DynamicDrumSimulate();
	}
	public override void FinishedReloadCycle()
	{
		base.FinishedReloadCycle();
		UpdateViewmodelParams();
	}
	public override void SendAnimParametersOnAttack()
	{
		base.SendAnimParametersOnAttack();
		UpdateViewmodelParams();
	}
	public override void SendAnimParametersOnReloadInsert()
	{
		TimeSinceReloadLoopCycle = 0f;
		ReloadSetBodygroupReady = true; //We have started Reload state, so delayed bodygroup knows when to trigger
		ReloadHasSetBodygroup = false;
	}
	public override void SendAnimParametersOnReloadStop()
	{
		base.SendAnimParametersOnReloadStop();
		UpdateViewmodelParams();
		ReloadSetBodygroupReady = false;
		ReloadHasSetBodygroup = false;
	}
	#endregion
	public virtual void UpdateViewmodelParams()
	{

		Log.Info( $"Clip: {Clip}" );
		SetLoadedBodygroup( Clip );
		SendViewModelAnimParameter("f_drumangle", Clip * 90);
	}

	public virtual void SetLoadedBodygroup(int ammo)
	{
		ViewModel?.SetBodyGroup( "grenades_loaded", ammo );

	}
}

