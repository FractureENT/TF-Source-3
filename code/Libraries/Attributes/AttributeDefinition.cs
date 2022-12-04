using Sandbox;

namespace Amper.FPS;

public class AttributeDefinition : GameResource
{
	/// <summary>
	/// Defines how this attribute will affect the undelying value.<br/>
	/// <br/>
	/// - <b>Scalar</b>: value assigned to the attribute will multiply the original value. <small>(i.e. 50 * 1.5 = 75)</small><br/>
	/// <br/>
	/// - <b>Additive</b>: value assigned to the attribute will be added to the origin value. <small>(i.e. 50 + 15 = 65)</small><br/> 
	/// </summary>
	public AttributeType Type { get; set; }

	/// <summary>
	/// Defines the effect type of this attribute. Aka can this attribute be positive or negative or it's neutral.
	/// </summary>
	public AttributeEffectType EffectType { get; set; }

	/// <summary>
	/// By default positivity of the attribute is calculated from the value assigned to the attribute.<br/>
	/// <br/>
	/// If <b>Scalar</b>, attributes will be positive if their value is &gt;= 1.00.<br/>
	/// If <b>Additive</b>, attributes will be positive if their value is &gt;= 0.<br/>
	/// <br/>
	/// If you want to inverse this condition and, for example, make scalar attributes positive if value is &lt; 1.00, tick this checkbox.
	/// </summary>
	[ShowIf( "EffectType", AttributeEffectType.PositiveOrNegative )]
	public bool InversePositivity { get; set; }

	/// <summary>
	/// Description of the attribute that will display if it is positive.<br/>
	/// <b>Note:</b>&nbsp;<i>%value%</i> will be replaced with the formatted attribute value.
	/// </summary>
	[ShowIf( "EffectType", AttributeEffectType.PositiveOrNegative )]
	public string PositiveDescription { get; set; }

	/// <summary>
	/// Description of the attribute that will display if it is negative.
	/// <b>Note:</b>&nbsp;<i>%value%</i> will be replaced with the formatted attribute value.
	/// </summary>
	[ShowIf( "EffectType", AttributeEffectType.PositiveOrNegative )]
	public string NegativeDescription { get; set; }

	/// <summary>
	/// Description of the attribute.
	/// <b>Note:</b>&nbsp;<i>%value%</i> will be replaced with the formatted attribute value.
	/// </summary>
	[HideIf( "EffectType", AttributeEffectType.PositiveOrNegative )]
	public string Description { get; set; }

	public AttributeFormatType DisplayValueAs { get; set; }

	protected override void PostLoad()
	{
		Attributes.RegisterDefinition( this );
	}
}

public enum AttributeType
{
	Scalar,
	Additive
}

public enum AttributeEffectType
{
	Neutral,
	PositiveOrNegative
}


public enum AttributeFormatType
{
	Unmodified,
	UnusualEffect
}
