namespace Amper.FPS;

public class EconItemGenerator
{
	public static IEconItem GenerateItemFromResource( EconItemDefinition itemDef, int quality )
	{
		var engineClass = itemDef.EngineClass;

		if ( string.IsNullOrEmpty( engineClass ) )
			return null;

		// Make sure the type we're working with exists.
		if ( TypeLibrary.GetDescription( engineClass ) == null )
			return null;

		var item = TypeLibrary.Create<IEconItem>( engineClass );
		if ( item == null )
			return null;

		item.ItemDefinition = itemDef;
		item.Quality = quality;

		return item;
	}
}
