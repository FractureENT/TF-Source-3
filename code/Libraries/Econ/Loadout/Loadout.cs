using Sandbox;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Amper.FPS;

public partial class Loadout
{
	/// <summary>
	/// Returns loadout of the local client.
	/// </summary>
	public static Loadout LocalLoadout => ForClient( Local.Client );
	private static Dictionary<Client, Loadout> _cache = new();

	/// <summary>
	/// Target client.
	/// </summary>
	public readonly Client Client;
	public readonly bool ClientSupportsLoadout;
	public LoadoutState State;

	DeserializedData CachedData;
	TimeSince TimeSinceDataUpdated;

	/// <summary>
	/// Retrieve loadout data for a client, both on server and client.
	/// </summary>
	public static Loadout ForClient( Client client )
	{
		if ( client == null )
			return null;

		if ( _cache.TryGetValue( client, out var loadout ) )
			return loadout;

		var el = new Loadout( client );
		_cache[client] = el;

		return el;
	}

	private Loadout( Client client )
	{
		Client = client;
		ClientSupportsLoadout = !client.IsBot;

		Invalidate();
	}

	public virtual EconItemDefinition GetDefaultLoadoutItem( int classIndex, int itemSlot ) => null;
	public virtual Task<EconItemDefinition> GetLoadoutItem( int classIndex, int itemSlot ) => null;

	/// <summary>
	/// Invalidates the loadout, we will request it again next time we need it.
	/// </summary>
	public void Invalidate()
	{
		State = LoadoutState.Invalid;
		CachedData = null;
	}

	/// <summary>
	/// Load the loadout from the appropriate source if not yet loaded.
	/// </summary>
	public async Task Load()
	{
		if ( ClientSupportsLoadout )
			return;

		// Don't try to load if we're already loading
		if ( State == LoadoutState.Loading )
			return;

		State = LoadoutState.Loading;
		var loadedData = await LoadData();

		if ( loadedData == null )
		{
			CachedData = null;
			State = LoadoutState.Invalid;
			return;
		}

		CachedData = loadedData;
		State = LoadoutState.Loaded;
	}

	public async Task<DeserializedData> LoadData()
	{
		if ( Host.IsClient )
			return LoadDataFromDisk();

		return await LoadDataFromClient();
	}

	public virtual bool IsUpToDate()
	{
		if ( State == LoadoutState.Loaded )
		{
			if ( TimeSinceDataUpdated > mp_loadout_max_cache_time )
				return false;

			return true;
		}

		return false;
	}

	[ConVar.Engine] public static float mp_loadout_max_cache_time { get; set; } = 60;
	public class DeserializedData : Dictionary<string, Dictionary<int, string>> { }
}

public enum LoadoutState
{
	/// <summary>
	/// Loadout is invalid, we can't use it, need to request data ASAP.
	/// </summary>
	Invalid,
	/// <summary>
	/// We are currently waiting from the client for input.
	/// </summary>
	Loading,
	/// <summary>
	/// Loadout is loaded, we can use it.
	/// </summary>
	Loaded
}
