using System.Collections.Generic;
using System.Text.RegularExpressions;
using Sandbox;

namespace Amper.FPS;

public class Attributes
{
	private static Dictionary<StringToken, AttributeDefinition> DefinitionsByName = new();

	public static string NormalizeName( string attributeName )
	{
		// Trim the name to remove all the spaces.
		attributeName = attributeName.Trim();

		// Attribute names are always lowercase.
		attributeName = attributeName.ToLower();

		// Replace all spaces with underscores.
		attributeName = attributeName.Replace( " ", "_" );

		// Remove all unsupported characters.
		attributeName = Regex.Replace( attributeName, "[^a-z0-9_]", " " );
		return attributeName;
	}

	public static AttributeDefinition GetDefinitionByName( string name )
	{
		name = NormalizeName( name );
		if ( DefinitionsByName.TryGetValue( name, out AttributeDefinition attrDef ) )
			return attrDef;

		return null;
	}

	public static void RegisterDefinition( AttributeDefinition attrDef )
	{
		var name = NormalizeName( attrDef.ResourceName );
		DefinitionsByName.Add( name, attrDef );
	}

	public void Set( string attributeName, string value )
	{

	}

	[ConCmd.Server( "sv_test_attribute" )]
	public static void Command_TestAttribute( string name )
	{
		var def = GetDefinitionByName( name );
		if ( def == null )
		{
			Log.Info( "- Attribute not found." );
			return;
		}

		Log.Info( "- Attribute Found: " + def.Type );
	}
}
