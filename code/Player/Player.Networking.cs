using Sandbox.Utility;
using Sandbox.Events;

namespace astral_base.SCPRP;

public partial class Player
{
	public Connection? Connection => Network.OwnerConnection;
	public bool IsConnected => Connection is not null && (Connection.IsActive || Connection.IsHost);
}