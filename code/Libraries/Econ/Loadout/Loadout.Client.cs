using Sandbox;
using System.Text.Json;

namespace Amper.FPS;

partial class Loadout
{
	/// <summary>
	/// Server requested us to send them loadout.
	/// </summary>
	[ClientRpc]
	public static void OnServerRequest()
	{
		Host.AssertClient();
		LocalLoadout.SendDataToServer();
	}

	public async void SendDataToServer()
	{
		Host.AssertClient();

		// Make sure our loadout is loaded before we access it.
		await Load();

		if ( !IsUpToDate() )
		{
			// our data is not valid, so send an empty response
			// because we still need to send something.
			OnClientTransmit( "" );
			return;
		}

		// Serialize our data
		var json = JsonSerializer.Serialize( CachedData );

		// compress it
		json = json.Compress();

		// Send to server.
		OnClientTransmit( json );
	}

	public DeserializedData LoadDataFromDisk()
	{
		Host.AssertClient();

		if ( FileSystem.Data.FileExists( "loadout.json" ) )
		{
			try
			{
				// Try to read from file storage.
				return FileSystem.Data.ReadJson<DeserializedData>( "loadout.json" );
			}
			catch { }
		}

		// If there is an error, or file doesn't exist, create a new one and return it.
		var data = new DeserializedData();
		FileSystem.Data.WriteJson( "loadout.json", data );
		return data;
	}

	public bool SetLoadoutItem( int classIndex, int slotIndex, EconItemDefinition itemDef )
	{
		Host.AssertClient();

		if ( itemDef == null )
			return false;

		WriteDataToDisk();
		SendDataToServer();
		return true;
	}

	public void WriteDataToDisk()
	{
		Host.AssertClient();

		if ( CachedData == null )
			return;

		var json = JsonSerializer.Serialize( CachedData );
		FileSystem.Data.WriteAllText( "loadout.json", json );
	}
}
