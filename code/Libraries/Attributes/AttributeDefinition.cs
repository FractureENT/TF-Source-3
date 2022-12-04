using Sandbox;

namespace Amper.FPS;

public class AttributeDefinition : GameResource
{
	public AttributeType Type { get; set; }

	[HideIf( "Type", AttributeType.String )]
	public AttributeEffectType EffectType { get; set; }

	[HideIf( "Type", AttributeType.String )]
	[ShowIf( "EffectType", AttributeEffectType.PositiveOrNegative )]
	public string PositiveDescription { get; set; }

	[HideIf( "Type", AttributeType.String )]
	[ShowIf( "EffectType", AttributeEffectType.PositiveOrNegative )]
	public string NegativeDescription { get; set; }

	[HideIf( "Type", AttributeType.String )]
	[ShowIf( "EffectType", AttributeEffectType.Neutral )]
	[Title( "Description" )]
	public string NeutralDescription { get; set; }

	[ShowIf( "Type", AttributeEffectType.String )]
	[Title( "Description" )]
	public string StringDescription { get; set; }
}

public enum AttributeEffectType
{
	PositiveOrNegative,
	Neutral
}


public enum AttributeType
{
	/// <summary>
	/// Positive if 1 &lt; value
	/// </summary>
	Multiplier,
	/// <summary>
	/// 
	/// </summary>
	Additive,
	/// <summary>
	/// Attribute will contain a literal string. Always assumes <b>Neutral</b> effect type.
	/// </summary>
	String
}
