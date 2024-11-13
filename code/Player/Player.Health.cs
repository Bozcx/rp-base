using Sandbox.Utility;
using Sandbox.Events;

namespace astral_base.SCPRP;

public partial class Player
{

	/*
		[HostSync]
		use [Authority] for functions which are only relevant to the player. (Show something clientside.)

		Are both ways to make sure that a variable/function can only be called/edited from the serverside.
		Make sure to always do this for things outside of clientside code as to keep security.
	*/

	[HostSync]
	private float ArmorSoak { get; set; } = 100f; // In percentages

	[HostSync]
	[Property]
	private bool GodMode { get; set; } = false;

	[HostSync]
	[Property]
	private float MaxHealth { get; set; } = 100;
	
	[HostSync]
	[Property]
	private float Health { get; set; } = 100;

    [HostSync]
	[Property]
	private float MaxArmor { get; set; } = 100;

	[HostSync]
	[Property]
	private float Armor { get; set; } = 100;

	[Broadcast( NetPermission.HostOnly )]
	public void SetHealth( int NewHealth )
    {
		if (NewHealth <= MaxHealth) {
			MaxHealth = Math.Max(NewHealth, 0);
		}
	}

	[Broadcast( NetPermission.HostOnly )]
	public void SetMaxHealth( int NewHealth )
    {
		MaxHealth = Math.Max(NewHealth, 0);
	}

	[Broadcast( NetPermission.HostOnly )]
	public void SetArmor( int NewArmor )
    {
		if (NewArmor <= MaxArmor) {
			Armor = Math.Max(NewArmor, 0);
		}
	}

	[Broadcast( NetPermission.HostOnly )]
	public void SetMaxArmor( int NewArmor )
    {
		MaxArmor = Math.Max(NewArmor, 0);
	}

	public void TakeDamage( Damage damage )
	{
		Scene.Dispatch( new PlayerTakeDamage( this, damage ) );
	}

	public void ActualTakeDamage( Damage damage )
    {
		if ( !Rpc.Caller.IsHost ) return;

		if ( this.GodMode ) return;

		float DamageAmount = damage.Amount;

		float damageToArmor = (ArmorSoak / 100f) * DamageAmount;
		float damageToHP = (1f - (ArmorSoak / 100f)) * DamageAmount;

		if (damage.Forced) { // Force ignores armor.
			this.Health = Math.Max(this.Health -= DamageAmount, 0);
		} else {
			this.Health = Math.Max(this.Health - damageToHP, 0);
			this.Armor = Math.Max(this.Armor - damageToArmor, 0);
		}
		
		Scene.Dispatch( new PlayerTookDamage( this, damage ) );
	}
}