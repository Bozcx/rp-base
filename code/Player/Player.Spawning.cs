namespace astral_base.RPBASE;


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
		SetupSpawn(this, this.GetJob());
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
	
	[Authority]
    public void SetupSpawn(Player player, Job job)
	{
		player.SetMaxHealth(job.MaxHealth);
		player.SetHealth(job.MaxHealth);
		player.SetMaxArmor(job.MaxArmor);
		player.SetArmor(job.MaxArmor);
		Log.Info($"{player.GetSteamID()} Initialized Job onto the player: {job}.");
	}
}