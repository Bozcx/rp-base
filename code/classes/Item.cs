namespace astral_base.SCPRP;

public class Item : Component
{
	[HostSync]
	[Property]
	public string EntityName { get; set; } = "Base Entity";

	[HostSync]
	 public Player? Owner { get; set; }

	protected override void OnStart()
	{
		base.OnStart();
		Log.Info( $"{EntityName} has been initialized." );
		SetupPhysics();
		GameObject.Tags.Add( "Interactable" );
	}

	protected virtual void OnDestroyed()
	{
		Log.Info( $"{EntityName} has been destroyed." );
		GameObject.Destroy();
	}

	private void SetupPhysics() {}

	public virtual void OnUse( Player player ) {}
}