namespace astral_base.SCPRP;


public partial class Player
{
    private TimeUntil _nextUpdateTime = 0f;

    protected override void OnFixedUpdate()
    {
        if (!Networking.IsHost) { return; }
		if (!Input.Pressed( "attack1" ) ){ return;  }
        if (this.Health > 0 ){ return;  }

        this.Respawn(Random.Shared.FromList(Spawners.SpawnList));
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
	}
}