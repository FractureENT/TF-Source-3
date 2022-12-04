using Sandbox;
using System;
using System.Collections.Generic;

namespace Amper.FPS;

public abstract class EconItemDefinition : GameResource, IIndexedEconDefinition
{
	public int DefinitionIndex { get; set; }
	public virtual string Title { get; set; }
	public virtual string EngineClass { get; set; }

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

	protected override void PostLoad()
	{
		EconItemSchema.RegisterDefinition( this );
	}
}
