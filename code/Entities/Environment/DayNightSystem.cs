﻿using Sandbox;

namespace TFS2;

public enum TimeStage
{
	Dawn,
	Day,
	Dusk,
	Night
}

partial class DayNightSystem : Entity
{
	public static DayNightSystem Instance { get; protected set; }

	public delegate void TimeStageChanged( TimeStage stage );
	public event TimeStageChanged OnTimeStageChanged;

	[Net, Predicted]
	public TimeStage TimeStage { get; protected set; }

	[Net, Predicted]
	public float TimeOfDay { get; set; } = 10f;

	[ConVar.Replicated( "tf_daynight_speed", Help = "How fast the day night cycle is" )]
	public static float DayNightSpeed { get; set; } = 0.05f;

	public DayNightSystem()
	{
		Instance = this;
	}

	public override void Spawn()
	{
		base.Spawn();
		Transmit = TransmitType.Always;
	}

	public static TimeStage TimeToStage( float time )
	{
		if ( time > 5f && time <= 9f )
			return TimeStage.Dawn;
		if ( time > 9f && time <= 18f )
			return TimeStage.Day;
		if ( time > 18f && time <= 21f )
			return TimeStage.Dusk;

		return TimeStage.Night;
	}

	// Shared Tick
	[Event.Tick]
	protected void Tick()
	{
		TimeOfDay += DayNightSpeed * Time.Delta;

		if ( TimeOfDay >= 24f )
			TimeOfDay = 0f;

		var stage = TimeToStage( TimeOfDay );
		if ( stage != TimeStage )
		{
			TimeStage = stage;
			OnTimeStageChanged?.Invoke( stage );
		}
	}
}
