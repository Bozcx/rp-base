using Sandbox.Utility;
using Sandbox.Events;

namespace astral_base.RPBASE;

public partial class Player
{

	/*
		[HostSync]
		use [Authority] for functions which are only relevant to the player. (Show something clientside.)

		Are both ways to make sure that a variable/function can only be called/edited from the serverside.
		Make sure to always do this for things outside of clientside code as to keep security.
	*/

	private float ArmorSoak { get; set; } = 100f; // In percentages

	[HostSync]
	[Property]
	private bool GodMode { get; set; } = false;

	[HostSync]
	[Property]
	private float MaxHealth { get; set; } = 100f;
	
	[HostSync]
	[Property]
	private float Health { get; set; } = 100f;

    [HostSync]
	[Property]
	private float MaxArmor { get; set; } = 100f;

	[HostSync]
	[Property]
	private float Armor { get; set; } = 100f;

	public float GetHealth() // Weird networking error when applying sum to this.
    {
		return this.Health;
	}

	public float GetMaxHealth() // Weird networking error when applying sum to this.
    {
		return this.MaxHealth;
	}

	public float GetArmor() // Weird networking error when applying sum to this.
    {
		return this.Armor;
	}

	public float GetMaxArmor() // Weird networking error when applying sum to this.
    {
		return this.MaxArmor;
	}


	[Authority]
	[Broadcast( NetPermission.HostOnly )]
	public void SetHealth( float NewHealth )
    {
		if (NewHealth <= MaxHealth) {
			this.Health = MathF.Max(NewHealth, 0);
		}
	}

	[Authority]
	[Broadcast( NetPermission.HostOnly )]
	public void SetMaxHealth( float NewHealth )
    {
		this.MaxHealth = MathF.Max(NewHealth, 0);
	}

	[Authority]
	[Broadcast( NetPermission.HostOnly )]
	public void SetArmor( float NewArmor )
    {
		if (NewArmor <= MaxArmor) {
			this.Armor = MathF.Max(NewArmor, 0);
		}
	}

	[Authority]
	[Broadcast( NetPermission.OwnerOnly )]
	public void SetMaxArmor( float NewArmor )
    {
		this.MaxArmor = MathF.Max(NewArmor, 0);
	}

	[Authority]
	[Broadcast( NetPermission.OwnerOnly )]
	public void TakeDamage( Damage damage )
	{
		Scene.Dispatch( new PlayerTakeDamage( this, damage ) );
	}

	public void ActualTakeDamage( Damage damage )
    {
		if ( !Rpc.Caller.IsHost ) return;

		if ( this.GodMode ) return;

		var DamageAmount = damage.Amount;

		var damageToArmor = (ArmorSoak / 100f) * DamageAmount;
		var damageToHP = (1f - (ArmorSoak / 100f)) * DamageAmount;

		if (damage.Forced) { // Force ignores armor.
			this.Health = MathF.Max(this.Health -= DamageAmount, 0);
		} else {
			this.Health = MathF.Max(this.Health - damageToHP, 0);
			this.Armor = MathF.Max(this.Armor - damageToArmor, 0);
		}
		
		Scene.Dispatch( new PlayerTookDamage( this, damage ) );
	}
}