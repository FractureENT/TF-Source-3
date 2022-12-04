using Amper.FPS;
using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFS2;

[GameResource( "TF:S2 Item Definition", "tfitem", "", Icon = "category" )]
public class TFItemDefinition : ItemDefinition<TFPlayerClass>
{
	public enum ViewModelSelectMode
	{
		Globally,
		PerClass
	}

	[ResourceType( "tfitem" )]
	[Category( "Prefabs" )]
	public override List<string> Prefabs { get; set; }

	//
	// Models
	//

	[Category( "Models" )]
	[ResourceType( "vmdl" )]
	public override string WorldModel { get; set; }

	[Category( "Models" )]
	public ViewModelSelectMode DefineViewModel { get; set; }

	[Category( "Models" )]
	[ResourceType( "vmdl" )]
	[ShowIf( "DefineViewModel", ViewModelSelectMode.Globally )]
	public override string ViewModel { get; set; }

	[Category( "Models" )]
	[ShowIf( "DefineViewModel", ViewModelSelectMode.PerClass )]
	[Title( "View Model" )]
	public Dictionary<TFPlayerClass, string> ViewModelPerClass { get; set; }
}
