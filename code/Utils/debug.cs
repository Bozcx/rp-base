using Sandbox.Utility;
using Sandbox.Events;

namespace astral_base.RPBASE;

public class Debug: GameObjectSystem<Debug>, 
IGameEventHandler<PlayerLoadedIn>, 
IGameEventHandler<PlayerTakeDamage>
{
	public Debug( Scene scene ) : base( scene ) {}

    [After<Player>]
	public bool OnGameEvent( PlayerLoadedIn eventArgs )
	{
		// var player = eventArgs.player;
		// player.RenderModel.Model = Model();
		// var damage = new Damage(25f, 1, player, false);

		// player.TakeDamage(damage);

		return false;
	}

    [Before<EventHandler>]
	public bool OnGameEvent( PlayerTakeDamage eventArgs )
	{
		// var player = eventArgs.player;
		// var Damage = eventArgs.damage;

		return false;
	}
}