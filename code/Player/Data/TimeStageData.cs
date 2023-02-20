using Sandbox;
using Editor;
using System.Collections.Generic;
using System.Linq;

namespace TFS2;

[GameResource( "TF:S2 Time Stage Data", "tftime", "Team Fortress: Source 2 day-night cycle system", Icon = "☀️", IconBgColor = "#c0eb34" )]
public partial class TimeStageData : GameResource
{
	public static List<TimeStageData> All { get; protected set; } = new();
	public static TimeStageData Get( string path ) => All.First( x => x.ResourcePath == path );

	[Category( "Lighting" )]
	public Color SkyColor { get; set; }
	[Category( "Lighting" )]
	public Color LightColor { get; set; }
	[Category( "Lighting" )]
	public Color AmbientLightColor { get; set; } = new Color( 0.1f, 0.1f, 0.1f );

	[Category( "Fog" )]
	public bool FogEnabled { get; set; } = true;
	[Category( "Fog" )]
	public float FogStartDistance { get; set; } = 0.0f;
	[Category( "Fog" )]
	public float FogEndDistance { get; set; } = 4000.0f;
	[Category( "Fog" )]
	public float FogStartHeight { get; set; } = 0.0f;
	[Category( "Fog" )]
	public float FogEndHeight { get; set; } = 200.0f;
	[Category( "Fog" )]
	public float FogMaximumOpacity { get; set; } = 0.5f;
	[Category( "Fog" )]
	public Color FogColor { get; set; } = Color.White;
	[Category( "Fog" )]
	public float FogStrength { get; set; } = 1.0f;
	[Category( "Fog" )]
	public float FogDistanceFalloffExponent { get; set; } = 2.0f;
	[Category( "Fog" )]
	public float FogVerticalFalloffExponent { get; set; } = 1.0f;

	protected override void PostLoad()
	{
		base.PostLoad();

		if ( !All.Contains( this ) )
			All.Add( this );
	}
}
