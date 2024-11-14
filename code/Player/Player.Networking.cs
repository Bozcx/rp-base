using Sandbox.Utility;
using Sandbox.Events;

namespace astral_base.SCPRP;

public partial class Player
{
	[Property]
	[RequireComponent]
	public GameObject PlayerPrefab { get; set; } = null!;

	public Connection? Connection => Network.OwnerConnection;
	public bool IsConnected => Connection is not null && (Connection.IsActive || Connection.IsHost);
}