using Sandbox.Utility;
using Sandbox.Events;

namespace astral_base.SCPRP;

public class InitializeTeams : Component, ISceneStartup
{
	[HostSync]
	public static Dictionary<string, TeamGroup> JobGroups { get; } = new();
	[HostSync]
	public static Dictionary<string, Team> Jobs { get; } = new();

	[Broadcast]
	void ISceneStartup.OnHostInitialize()
	{
		Log.Info( "Loading groups..." );

		Jobs.Clear();
		JobGroups.Clear();
		
		// Get all JobGroup resources
		foreach ( var group in ResourceLibrary.GetAll<TeamGroup>() )
		{
			Log.Info( $"Loading group: {group.Name}" );
			JobGroups[group.Name] = group;
		}

		Log.Info( "Loading jobs..." );
		
		// Get all Job resources
		foreach ( var job in ResourceLibrary.GetAll<Team>() ) // Think about merging jobs into jobgroups. Jobgroups[Jobgroup][Job]
		{
			Log.Info( $"Loading job: {job.Name}" );
			Jobs[job.Name] = job;
		}
	}

	public static Team Default()
	{
		return ResourceLibrary.Get<Team>( "gameplay/jobs/default.job" );
	}

}