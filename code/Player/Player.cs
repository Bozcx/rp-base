using Sandbox.Utility;
using Sandbox.Events;

namespace astral_base.SCPRP;

public sealed partial class Player : Component
{
	/// The current character controller for this player.
	[HostSync]
	[RequireComponent]
	[Property]
	private CharacterController CharacterController { get; set; } // Save this for easy acces to the player.

	[HostSync]
	[RequireComponent]
	[Property]
	public GameObject Body { get; set; } // Save this for easy acces to the player.

	[HostSync]
	[RequireComponent]
	[Property]
	public SkinnedModelRenderer RenderModel { get; set; } // Save this for easy acces to the player.

	[HostSync]
	[Property]
	private ulong SteamID { get; set; };

	public ulong GetSteamID()
	{
		return this.SteamID;
	}

	[HostSync]
	[Property]
	private string SteamName { get; set; };

	public string GetSteamName()
	{
		return this.SteamName;
	}

	[HostSync]
	[Property]
	public string DisplayName { get; set; } // Display Name, Use SteamName as fallback.

	[Broadcast]
	protected override void OnAwake()
    {
		this.SteamName = Steam.PersonaName;
		this.SteamID = Steam.SteamId;

		this.DisplayName = SteamName; // For now till we add functionality to this.

		this.Team = Teams.Default(); // This is a must. The player should never not have an ID.
		Teams.SetupSpawn(this, this.Team);

		Log.Info( $"Player object for {this.SteamID} has been initialized." );
		Scene.Dispatch( new PlayerLoadedIn( this ) );
    }

	protected override void OnPreRender()
	{
		if ( Scene.Camera is null )
			return;

		var hud = Scene.Camera.Hud;

		hud.DrawText( new TextRendering.Scope( this.Health.ToString(), Color.Red, 32 ), Screen.Width * 0.5f );
		hud.DrawText( new TextRendering.Scope( this.Armor.ToString(), Color.Red, 32 ), Screen.Width * 0.3f );
	}
}
