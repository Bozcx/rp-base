using astral_base.SCPRP;
using Sandbox.Events;

namespace astral_base.SCPRP;

public static class Players
{
	/// All players in the game (includes disconnected players before expiration).
	public static IEnumerable<Player> GetAllPlayers()
	{
		return Game.ActiveScene.GetAllComponents<Player>();
	}
}