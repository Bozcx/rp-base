using Sandbox.Utility;
using Sandbox.Events;

namespace astral_base.RPBASE;

public partial class Player
{
	public Connection Connection => GameObject.Network.Owner;
	public bool IsConnected => Connection is not null && (Connection.IsActive || Connection.IsHost);
}
