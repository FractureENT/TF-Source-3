using Sandbox;
using System.Collections.Generic;

namespace Amper.FPS;

public class Attributes
{
	private Dictionary<StringToken, EconAttributeAssignment> StaticAttributes = new();
	private Dictionary<StringToken, EconAttributeAssignment> DynamicAttributes = new();

	public void CopyStaticFrom( EconItemDefinition itemDef )
	{
		StaticAttributes.Clear();

		// Copy over the static attributes from the definition.
		foreach ( var pair in itemDef.Attributes ) 
			StaticAttributes.Add( pair.Key, new( pair.Value ) );
	}

	public void AddAttribute( string attribName, string value )
	{
		DynamicAttributes[attribName] = new( value );
	}

	public static void HookValue<T>( IHasAttributes target, string attribName, ref float var )
	{
	}
}

public struct EconAttributeAssignment
{
	public int IntValue;
	public float FloatValue;
	public string StringValue;

	public EconAttributeAssignment( string value )
	{
		StringValue = value;
		FloatValue = value.ToFloat();
		IntValue = value.ToInt();
	}
}
