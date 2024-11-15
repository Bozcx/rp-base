using Sandbox.Diagnostics;
using Sandbox.Events;
using Sandbox.Network;

namespace astral_base.SCPRP;

// Few calls in this class need to re-done due to Obsolete methods.
public sealed class GameNetworkManager : SingletonComponent<GameNetworkManager>, Component.INetworkListener
{
	[Property]
	public GameObject PlayerPrefab { get; set; }

	[Property]
	public bool IsMultiplayer { get; set; } = true;

	protected override void OnStart() // Can be used to make sure only 1 server possibly?
	{
		if ( !IsMultiplayer )
		{
			Log.Info( $"Starting a local server." );
			OnActive( Connection.Local );
			return;
		}

		if ( !Networking.IsActive )
		{
			Log.Info( $"Starting a new lobby server." );
			Networking.CreateLobby();
		}
	}

	private Player GetPlayer( Connection channel = null )
	{
		// Checks if they are re-connecting or are new. If they are new this will have null.
		var player = Scene.GetAllComponents<Player>().FirstOrDefault( x => x.Connection is null && x.GetSteamID() == channel.SteamId );

		return player;
	}

	private Player CreatePlayer( Connection channel = null )
	{
		Assert.True( PlayerPrefab.IsValid(), "Could not spawn player entity as no PlayerPrefab assigned." );

		var playerPrefab = PlayerPrefab.Clone();
		playerPrefab.BreakFromPrefab();
		playerPrefab.Name = $"{channel.SteamId}";
		playerPrefab.Network.SetOrphanedMode( NetworkOrphaned.Destroy );
		playerPrefab.Components.Get<Player>().NetworkId = channel.Id;

		var player = playerPrefab.Components.Get<Player>();

		return player;
	}

	/// Called when somebody connects to the server.
	public void OnActive( Connection channel )
	{
		Log.Info( $"Player '{channel.DisplayName}' is beginning to initialize." );

		var player = CreatePlayer( channel );
		if ( !player.IsValid() ) {
			throw new Exception( $"Error Something went wrong whilst creating Player: {channel.DisplayName}" );
		}

		if ( !player.Network.Active ){
			player.GameObject.NetworkSpawn( channel );
		}

		Log.Info( $"Player '{channel.DisplayName}' Has Finished Initializing." );
	}
}
