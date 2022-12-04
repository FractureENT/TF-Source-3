using Sandbox;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Amper.FPS;

public class EconItemSchema
{
	private static Dictionary<Type, Dictionary<string, IEconDefinition>> DefinitionByName = new();
	private static Dictionary<Type, Dictionary<string, IEconDefinition>> DefinitionByPath = new();
	private static Dictionary<Type, Dictionary<int, IEconDefinition>> DefinitionByIndex = new();

	public static string NormalizeResourceName( string entryName )
	{
		// Trim the name to remove all the spaces.
		entryName = entryName.Trim();

		// Econ entities are always lowercase.
		entryName = entryName.ToLower();

		// Replace all spaces with underscores.
		entryName = entryName.Replace( " ", "_" );

		// Remove all unsupported characters.
		entryName = Regex.Replace( entryName, "[^a-z0-9_]", " " );
		return entryName;
	}

	public static T GetDefinitionByName<T>( string name ) where T : class, IEconDefinition
	{
		var type = typeof( T );
		if ( !DefinitionByName.ContainsKey( type ) )
			return null;

		name = NormalizeResourceName( name );
		if ( DefinitionByName[type].TryGetValue( name, out var value ) )
			return (T)value;

		return null;
	}

	public static T GetDefinitionByIndex<T>( int defId ) where T : class, IEconDefinition
	{
		var type = typeof( T );
		if ( !DefinitionByIndex.ContainsKey( type ) )
			return null;

		if ( DefinitionByIndex[type].TryGetValue( defId, out var value ) )
			return (T)value;

		return null;
	}

	public static T GetDefinitionByPath<T>( string path ) where T : class, IEconDefinition
	{
		var type = typeof( T );
		if ( !DefinitionByPath.ContainsKey( type ) )
			return null;

		if ( DefinitionByPath[type].TryGetValue( path, out var value ) )
			return (T)value;

		return null;
	}

	public static void RegisterDefinition<T>( T def ) where T: IEconDefinition
	{
		var type = typeof( T );

		{
			//
			// Register By Name
			//

			if ( !DefinitionByName.ContainsKey( type ) )
				DefinitionByName.Add( type, new() );

			var list = DefinitionByName[type];
			var name = NormalizeResourceName( def.ResourceName );
			list.Add( name, def );
		}

		{
			//
			// Register By Path
			//

			if ( !DefinitionByPath.ContainsKey( type ) )
				DefinitionByPath.Add( type, new() );

			var list = DefinitionByPath[type];
			list.Add( def.ResourcePath, def );
		}

		if ( def is IIndexedEconDefinition indexDef )
		{
			//
			// Register By Def Index
			//

			if ( !DefinitionByIndex.ContainsKey( type ) )
				DefinitionByIndex.Add( type, new() );

			var list = DefinitionByIndex[type];
			list.Add( indexDef.DefinitionIndex, def );
		}
	}

	[ConCmd.Server( "sv_dump_econ_entries" )]
	public static void Command_Dump()
	{
		foreach ( var typePair in DefinitionByName )
		{
			var type = typePair.Key;
			var list = typePair.Value;

			var typeDesc = TypeLibrary.GetDescription( type );
			var props = typeDesc.Properties;

			Log.Info( $"{type}:" );

			foreach ( var entryPair in list )
			{
				var name = entryPair.Key;
				var def = entryPair.Value;

				Log.Info( $"- \"{name}\":" );
				foreach ( var prop in props ) 
				{
					var value = prop.GetValue( def );
					Log.Info( $"  - {prop.Name} = {value}" );
				}
				Log.Info( "" );
			}
		}

	}
}

public class TwoKeyDictionary<K1, K2, T> 
{
	Dictionary<K1, T> DictK1 = new();
	Dictionary<K2, T> DictK2 = new();

	public void Add( K1 key1, K2 key2, T entry )
	{
		DictK1.Add( key1, entry );
		DictK2.Add( key2, entry );
	}

	public bool ContainsKey( K1 key ) => DictK1.ContainsKey( key );
	public bool ContainsKey( K2 key ) => DictK2.ContainsKey( key );

	public bool TryGetValue( K1 k1, out T entry ) => DictK1.TryGetValue( k1, out entry );
	public bool TryGetValue( K2 k2, out T entry ) => DictK2.TryGetValue( k2, out entry );
}
