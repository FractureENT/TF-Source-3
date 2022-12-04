namespace Amper.FPS;

public interface IEconItem : IHasAttributes
{
	public int Quality { get; set; }
	public EconItemDefinition ItemDefinition { get; set; }
}

public interface IEconDefinition
{
	public string ResourceName { get; }
	public string ResourcePath { get; }
}

public interface IIndexedEconDefinition : IEconDefinition
{
	/// <summary>
	/// Index that is used to identify this definition in the backend.<br/>
	/// <b>NEVER CHANGE THIS VALUE AFTER THE DEFINITION WENT PUBLIC</b> 
	/// </summary>
	public int DefinitionIndex { get; }
}
