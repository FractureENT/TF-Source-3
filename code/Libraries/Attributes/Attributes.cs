using System.Text.RegularExpressions;

namespace Amper.FPS;

public class Attributes
{
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
}
