using Sandbox.Diagnostics;
using Sandbox.Events;
using Sandbox.Network;

namespace astral_base.RPBASE;

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
			Networking.CreateLobby(new LobbyConfig() {
				MaxPlayers = 999999,
				Privacy = LobbyPrivacy.Public,
				Name = "Site-19"
			});
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
		channel.CanSpawnObjects = false; // Include this into the permission system. This is to disallow prop spawning etc. Honestly just do this properly, keep it like this.
		var player = CreatePlayer( channel );
		if ( !player.IsValid() ) {
			throw new Exception( $"Error Something went wrong whilst creating Player: {channel.DisplayName}" );
		}

		if ( !player.Network.Active ){
			player.GameObject.NetworkSpawn( channel );
		}

			var msg = new ChatMessage()
			{
				Text = $"{player.DisplayName} has joined the game.",
				Author = player.GetSteamID(),
				DisplayName = player.DisplayName,
				Time = DateTime.Now,
				IsActive = true,
				IsOOC = false,
				IsSytemMessage = true
			};

			Scene.Dispatch( new PlayerSendMessage( player, msg ) );

		Log.Info( $"Player '{channel.DisplayName}' Has Finished Initializing." );
	}
}
