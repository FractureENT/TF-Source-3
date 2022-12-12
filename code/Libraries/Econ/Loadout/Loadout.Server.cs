using Sandbox;
using System.Text.Json;
using System.Threading.Tasks;

namespace Amper.FPS;

partial class Loadout
{
	private delegate void ClientUpdateCallback( DeserializedData data );
	private event ClientUpdateCallback OnClientUpdateCallback;

	/// <summary>
	/// We received loadout data from the client.
	/// </summary>
	[ConCmd.Server( "send_loadout" )]
	public static void OnClientTransmit( string rawData )
	{
		Host.AssertServer();

		var client = ConsoleSystem.Caller;
		if ( client == null )
			return;

		rawData = rawData.Decompress();

		var loadout = ForClient( client );

		// if what data that we recieved is not valid.
		if ( !string.IsNullOrEmpty( rawData ) )
		{
			// deserialize loadout data.
			var data = JsonSerializer.Deserialize<DeserializedData>( rawData );
			if ( data != null )
			{
				loadout.OnClientUpdateCallback?.Invoke( data );
				loadout.OnClientUpdateCallback = null;
			}
		}

		loadout.OnClientUpdateCallback?.Invoke( null );
		loadout.OnClientUpdateCallback = null;
	}

	public Task<DeserializedData> LoadDataFromClient()
	{
		return GameTask.RunInThreadAsync( () =>
		{
			var t = new TaskCompletionSource<DeserializedData>();
			RequestDataFromClient( ( data ) => t.TrySetResult( data ) );
			return t.Task;
		} );
	}

	/// <summary>
	/// Request loadout information from client.
	/// </summary>
	void RequestDataFromClient( ClientUpdateCallback callback )
	{
		Host.AssertServer();

		OnClientUpdateCallback += callback;
		OnServerRequest( To.Single( Client ) );
	}
}
