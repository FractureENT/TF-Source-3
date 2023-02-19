﻿using Sandbox;
using Sandbox.UI;
using System.Linq;

namespace TFS2.UI;

public partial class ArenaPlayerCount : Panel
{
	protected Label RedCount { get; set; }
	protected Label BlueCount { get; set; }
	public override void Tick()
	{
		SetClass( "visible", ShouldDraw() );

		if ( !IsVisible )
			return;

		var players = Entity.All.OfType<TFPlayer>().Where( x => x.IsAlive );
		RedCount.Text = players.Where( x => x.Team == TFTeam.Red ).Count().ToString();
		BlueCount.Text = players.Where( x => x.Team == TFTeam.Blue ).Count().ToString();

		// Move the arena player count down if in specator mode.
		SetClass( "if-spectator", ((TFPlayer)Sandbox.Game.LocalPawn).Team == TFTeam.Spectator );
	}

	public bool ShouldDraw()
	{
		return TFGameRules.Current.IsPlaying<Arena>();
	}
}
