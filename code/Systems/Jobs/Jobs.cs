using Sandbox.Utility;
using Sandbox.Events;

namespace astral_base.RPBASE;

public class Jobs
{
	public static Dictionary<string, JobGroup> GetAllJobGroups()
	{
		return InitializeJobs.JobGroups;
	}

    public static Dictionary<string, Job> GetAllJobs()
	{
		return InitializeJobs.Jobs;
	}

	public static Job Default()
	{
		return InitializeJobs.Default();
	}
}