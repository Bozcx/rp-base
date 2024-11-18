using Sandbox.Utility;
using Sandbox.Events;

namespace astral_base.RPBASE;

public sealed class InitializeJobs : GameObjectSystem<InitializeJobs>, ISceneStartup
{
	public InitializeJobs( Scene scene ) : base( scene ) {}

	[HostSync]
	public static Dictionary<string, JobGroup> JobGroups { get; } = new();
	[HostSync]
	public static Dictionary<string, Job> Jobs { get; } = new();

	void ISceneStartup.OnHostInitialize()
	{
		Log.Info( "Loading job groups" );

		Jobs.Clear();
		JobGroups.Clear();
		
		// Get all JobGroup resources
		foreach ( var group in ResourceLibrary.GetAll<JobGroup>() )
		{
			Log.Info( $"Loading group: {group.Name}" );
			JobGroups[group.Name] = group;
		}

		Log.Info( "Loading jobs" );
		
		// Get all Job resources
		foreach ( var job in ResourceLibrary.GetAll<Job>() ) // Think about merging jobs into jobgroups. Jobgroups[Jobgroup][Job]
		{
			Log.Info( $"Loading job: {job.Name}" );
			Jobs[job.Name] = job;
		}
	}

	public static Job Default()
	{
		return ResourceLibrary.Get<Job>( "gameplay/jobs/default.job" );
	}

}