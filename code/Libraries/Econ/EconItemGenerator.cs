namespace Amper.FPS;

public static class EconItemGenerator
{
	public static T GenerateItemFromName<T>( string name ) where T : class, IEconItem
	{
		var itemDef = EconItemSchema.GetDefinitionByName<EconItemDefinition>( name );
		if ( itemDef == null )
			return null;

		return GenerateItemFromResource<T>( itemDef );
	}
	public static T GenerateItemFromDefinitionIndex<T>( int defId ) where T : class, IEconItem
	{
		var itemDef = EconItemSchema.GetDefinitionByIndex<EconItemDefinition>( defId );
		if ( itemDef == null )
			return null;

		return GenerateItemFromResource<T>( itemDef );
	}

	public static T GenerateItemFromResource<T>( EconItemDefinition itemDef ) where T: class, IEconItem
	{
		var engineClass = itemDef.EngineClass;

		if ( string.IsNullOrEmpty( engineClass ) )
			return null;

		// Make sure the type we're working with exists.
		if ( TypeLibrary.GetDescription( engineClass ) == null )
			return null;

		var item = TypeLibrary.Create<T>( engineClass );
		if ( item == null )
			return null;

		item.ItemDefinition = itemDef;
		item.Attributes.CopyStaticFrom( itemDef );
		return item;
	}
}
