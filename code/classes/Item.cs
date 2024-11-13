namespace astral_base.SCPRP;

public class Item : Component
{
	[Property] public string EntityName { get; set; } = "Base Entity";

	 public Player? Owner { get; set; }

	protected void OnStart()
	{
		base.OnStart();
		Log.Info( $"{EntityName} has been initialized." );
		SetupPhysics();

		// Ensure the entity has the interact tag to be recognized by the InteractionSystem
		GameObject.Tags.Add( "Interactable" );
	}

	protected virtual void OnDestroyed()
	{
		Log.Info( $"{EntityName} has been destroyed." );
		GameObject.Destroy();
	}

	public void OnKill( DamageInfo damageInfo )
	{
		OnDestroyed();
	}

	private void SetupPhysics()
	{
		// Setup physics, if necessary.
	}

	public virtual void OnUse( Player player ) {}
}