using Sandbox.Utility;
using Sandbox.Events;

namespace astral_base.SCPRP;

public partial class Player
{
    [HostSync]
	[Property]
    private Team Team { get; set; }

	[Authority]
	[Broadcast( NetPermission.HostOnly )]
    public void SetTeam(Team team)
    {
        Log.Info( $"{this} has changed their team from {Team} to: {team}" );
        Team = team;
    }

    public Team GetTeam()
    {
        return this.Team;
    }
}