using Sandbox.Utility;
using Sandbox.Events;

namespace astral_base.SCPRP;

public class Debug: Component, IGameEventHandler<PlayerLoadedIn>, IGameEventHandler<PlayerTakeDamage>
{
    [After<Player>]
	public bool OnGameEvent( PlayerLoadedIn eventArgs )
	{
		var player = eventArgs.player;
		var damage = new Damage(25f, 1, player, false);

		player.TakeDamage(damage);

		return false;
	}

    [Before<EventHandler>]
	public bool OnGameEvent( PlayerTakeDamage eventArgs )
	{
		var player = eventArgs.player;
		var Damage = eventArgs.damage;

		return false;
	}
}