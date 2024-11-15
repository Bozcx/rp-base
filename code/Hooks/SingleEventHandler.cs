using Sandbox.Utility;
using Sandbox.Events;

namespace astral_base.SCPRP;

/* EVENT GUIDE.

The events below are structured as garry's mod hooks.
If you return true it will stop transmitting that event to the catches behind it.
The core event handlers get put in here. Aka the base behaviour.

This only works for events that don't get called every frame for now!
Possible idea to be able to properly manage all of the events per-frame (or other timestamps)
is creating a class that they can extend. The GameSystemManager of this sytem will collect all the classes/methods
and execute them. If possible you could add a sorting to this similar to how the OnGameEvent is structured.

*/

public class SingleEventHandler : GameObjectSystem<SingleEventHandler>, 
IGameEventHandler<PlayerTookDamage>, 
IGameEventHandler<PlayerTakeDamage>,
IGameEventHandler<PlayerChangeJob>,
IGameEventHandler<PlayerSendMessage>
{

	public SingleEventHandler(Scene scene) : base(scene) { }

	[Late]
	public bool OnGameEvent( PlayerTookDamage eventArgs )
	{
		var player = eventArgs.player;
		var damage = eventArgs.damage;

		Log.Info($"{player.GetSteamID()} is took {damage.Amount} ammount of damage.");
		return false;
	}

	[Late]
	public bool OnGameEvent( PlayerTakeDamage eventArgs )
	{
		var player = eventArgs.player;
		var damage = eventArgs.damage;

		Log.Info($"{player.GetSteamID()} is attempting to take {damage.Amount} ammount of damage.");

		player.ActualTakeDamage(damage);
		return false;
	}

	[Late]
	public bool OnGameEvent( PlayerChangeJob eventArgs )
	{
		var player = eventArgs.player;
		var job = eventArgs.job;

		Log.Info($"{player.GetSteamID()} is attempting to change to Job {job} ");

		player.SetJob(job);

		return false;
	}

	[Late]
	public bool OnGameEvent( PlayerSendMessage eventArgs )
	{
		var player = eventArgs.player;
		var message = eventArgs.message;

		Log.Info( $"> {message.DisplayName} : {message.Text}" );

		Log.Info( $"> {player} : {Player.Local}" );

		Chat.GetChat().AddMessage(message, player);

		return false;
	}
}