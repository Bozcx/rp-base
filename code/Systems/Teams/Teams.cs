using Sandbox.Utility;
using Sandbox.Events;

namespace astral_base.SCPRP;

public class Teams
{
	public static Dictionary<string, TeamGroup> GetAllJobGroups()
	{
		return InitializeTeams.JobGroups;
	}

    public static Dictionary<string, Team> GetAllJobs()
	{
		return InitializeTeams.Jobs;
	}

	public static Team Default()
	{
		return InitializeTeams.Default();
	}
}