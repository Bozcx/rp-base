namespace astral_base.SCPRP;


public partial class Player
{
    protected override void OnFixedUpdate()
    {
        if (!Networking.IsHost) { return; }
		if (!Input.Pressed( "attack1" ) ){ return;  }
        if (this.Health > 0 ){ return;  }

        this.Respawn(Random.Shared.FromList(InitializeSpawners.SpawnList)); // Make the spawns job-specific.
    }

    private void Respawn( Spawner spawnPoint )
	{
		Teleport( spawnPoint.Position, spawnPoint.Rotation );
	}

	[Authority]
	public void Teleport( Vector3 position, Rotation rotation )
	{
        this.Body.Enabled = true;
		this.Transform.World = new Transform( position, rotation );
		Transform.ClearInterpolation();
        CharacterController.Velocity = Vector3.Zero;
        CharacterController.IsOnGround = true;
		Log.Info($"{this.GetSteamID()} has been respawned.");
	}

    public void SetupSpawn(Player player, Team team)
	{
		player.SetMaxHealth(team.MaxHealth);
		player.SetHealth(team.MaxHealth);
		player.SetMaxArmor(team.MaxArmor);
		player.SetArmor(team.MaxArmor);
		Log.Info($"{player.GetSteamID()} Initialized Job onto the player: {team}.");
	}
}