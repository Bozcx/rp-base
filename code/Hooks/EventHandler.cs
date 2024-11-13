using Sandbox.Utility;
using Sandbox.Events;

namespace astral_base.SCPRP;

public class EventHandler : Component, IGameEventHandler<PlayerTookDamage>, IGameEventHandler<PlayerTakeDamage>
{
	public bool OnGameEvent( PlayerTookDamage eventArgs )
	{
		var player = eventArgs.player;
		var damage = eventArgs.damage;

		Log.Info($"{player.SteamID} is took {damage.Amount} ammount of damage.");
		return false;
	}

	public bool OnGameEvent( PlayerTakeDamage eventArgs )
	{
		var player = eventArgs.player;
		var damage = eventArgs.damage;

		Log.Info($"{player.SteamID} is attempting to take {damage.Amount} ammount of damage.");

		player.ActualTakeDamage(damage);
		return false;
	}
}