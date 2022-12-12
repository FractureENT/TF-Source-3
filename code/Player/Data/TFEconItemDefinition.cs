using Amper.FPS;
using Sandbox;
using System.Collections.Generic;

namespace TFS2;

[GameResource( "TF:S2 Item Definition", "tfitem", "", Icon = "category" )]
public class TFEconItemDefinition : EconItemDefinition
{
	public enum SelectMode
	{
		Globally,
		PerClass
	}

	[ResourceType( "tfitem" )]
	[Category( "Prefabs" )]
	public override List<string> Prefabs { get; set; }

	/// <summary>
	/// Literally all this does is defines whether item name will have "The" before it under certain cases.
	/// </summary>
	public bool ProperName { get; set; }


	[Category( "Team Fortress Specific" )]
	public List<string> UsedByClasses { get; set; }

	[Category( "Team Fortress Specific" )]
	public SelectMode DefineSlot { get; set; }

	[Category( "Team Fortress Specific" )]
	[ShowIf( "DefineSlot", SelectMode.Globally )]
	public TFWeaponSlot Slot { get; set; }

	[Category( "Team Fortress Specific" )]
	[ShowIf( "DefineSlot", SelectMode.PerClass )]
	[Title( "Slot" )]
	public Dictionary<TFPlayerClass, TFWeaponSlot> SlotPerClass { get; set; }

	//
	// Models
	//

	[Category( "Models" )]
	[ResourceType( "vmdl" )]
	public override string WorldModel { get; set; }

	[Category( "Models" )]
	public SelectMode DefineViewModel { get; set; }

	[Category( "Models" )]
	[ResourceType( "vmdl" )]
	[ShowIf( "DefineViewModel", SelectMode.Globally )]
	public override string ViewModel { get; set; }

	[Category( "Models" )]
	[ShowIf( "DefineViewModel", SelectMode.PerClass )]
	[Title( "View Model" )]
	public Dictionary<TFPlayerClass, string> ViewModelPerClass { get; set; }

	public TFWeaponSlot GetLoadoutSlotForClass( TFPlayerClass pClass )
	{
		if ( DefineSlot == SelectMode.PerClass )
		{
			if ( SlotPerClass.TryGetValue( pClass, out var slot ) )
				return slot;

			Log.Error( "TFEconItemDefinition.GetLoadoutSlotForClass() - Called on class that we don't support." );
			return TFWeaponSlot.Primary;
		}

		return Slot;
	}
}
