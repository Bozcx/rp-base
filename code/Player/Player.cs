using Sandbox.Utility;
using Sandbox.Events;

namespace astral_base.SCPRP;

public sealed partial class Player : Component
{

	private static Player _local;

	public static Player Local // Method to get the Local Client! Get the local client using Player.Local
	{
		get
		{
			if ( _local.IsValid() ) return _local;
			_local = Game.ActiveScene.GetAllComponents<Player>().FirstOrDefault( x => !x.IsProxy );
			return _local;
		}
	}

	[HostSync]
	public Guid NetworkId { get; set; }

	[HostSync]
	[Property]
	public CharacterController CharacterController { get; set; }

	public CharacterController GetCharacterController()
	{
		return this.CharacterController;
	}

	[HostSync]
	[Property]
	public GameObject Body { get; set; }

	[HostSync]
	[Property]
	public SkinnedModelRenderer RenderModel { get; set; }

	[HostSync]
	[Property]
	private ulong SteamID { get; set; }

	public ulong GetSteamID()
	{
		return this.SteamID;
	}

	[HostSync]
	[Property]
	private string SteamName { get; set; }

	public string GetSteamName()
	{
		return this.SteamName;
	}

	[HostSync]
	[Property]
	public string DisplayName { get; set; } // Display Name, Use SteamName as fallback.

	[Authority]
	protected override void OnAwake()
    {
		this.SteamName = Steam.PersonaName;
		this.SteamID = Steam.SteamId;

		this.DisplayName = SteamName; // For now till we add functionality to this.

		this.SetJob(Jobs.Default()); // This is a must. The player should never not have an ID.
		this.SetupSpawn(this, this.Job);

		Log.Info( $"Player object for {this.SteamID} has been initialized." );
		Scene.Dispatch( new PlayerLoadedIn( this ) );
    }

	[Authority] // Aka only for that player.
	protected override void OnUpdate()
		{
			if ( Scene.Camera == null ) { return; };
			var hud = Scene.Camera.Hud;

			hud.DrawRect( new Rect( Screen.Width * 0.425f, 25, 200, 10 ), Color.Gray, new Vector4( 5, 5, 5, 5 ) );
			hud.DrawRect( new Rect( Screen.Width * 0.424f, 25, ((this.GetHealth() / this.GetMaxHealth()) * 202), 10 ), Color.Red, new Vector4( 5, 5, 5, 5 ) );
			hud.DrawRect( new Rect( Screen.Width * 0.425f, 40, 200, 10 ), Color.Gray, new Vector4( 5, 5, 5, 5 ) );
			hud.DrawRect( new Rect( Screen.Width * 0.424f, 40, ((this.GetArmor() / this.GetMaxArmor()) * 202), 10 ), Color.Blue, new Vector4( 5, 5, 5, 5 ) );
		}
}
