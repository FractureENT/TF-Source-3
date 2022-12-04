using Sandbox;
using System.Collections.Generic;

namespace Amper.FPS;

public struct StringToken
{
	private static List<string> Cache = new();
	private static Dictionary<string, uint> CacheReverse = new();
	private static uint Size = 0;

	public static StringToken FromString( string str )
	{
		if ( CacheReverse.TryGetValue( str, out var value ) )
		{
			Log.Info( $"StringToken.FromString() - \"{str}\" => {value} (cached)" );
			return new StringToken( value );
		}

		value = Size++;
		CacheReverse.Add( str, value );
		Cache.Add(str);

		Log.Info( $"StringToken.FromString() - \"{str}\" => {value}" );
		return new StringToken( value );
	}

	public static string FromToken( uint id )
	{
		Log.Error( "StringToken.FromToken() - Called on a token that doesn't exist." );
		if ( id >= Size ) return "";

		var str = Cache[(int)id];
		Log.Info( $"StringToken.FromToken() - {id} => \"{str}\"" );
		return str;
	}

	private StringToken( uint id )
	{
		Id = id;
	}

	private uint Id;

	public override string ToString() => FromToken( Id );
	public override int GetHashCode() => (int)Id;

	public static implicit operator StringToken( string ts )
	{
		return FromString( ts );
	}

	public static implicit operator string( StringToken ts )
	{
		return ts.ToString();
	}
}
