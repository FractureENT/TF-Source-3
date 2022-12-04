using Sandbox;
using System;
using System.Collections.Generic;

namespace Amper.FPS;

public abstract class ItemDefinition<Classes> : GameResource where Classes : Enum
{
	public virtual string Title { get; set; }
	public virtual string EngineClass { get; set; }
	public virtual bool IsPrefabOnly { get; set; }
	public virtual List<Classes> UsedByClasses { get; set; }

	[ResourceType( "png" )]
	public virtual string InventoryImage { get; set; }

	[Category( "Models" )]
	[ResourceType( "vmdl" )]
	public virtual string WorldModel { get; set; }

	[Category( "Models" )]
	[ResourceType( "vmdl" )]
	public virtual string ViewModel { get; set; }

	[Category( "Attributes" )]
	public virtual Dictionary<string, string> Attributes { get; set; }

	[Category( "Prefabs" )]
	public virtual List<string> Prefabs { get; set; }
}
