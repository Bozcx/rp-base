namespace astral_base.SCPRP;

public static class Teams
{
	private static Dictionary<string, TeamGroup> JobGroups { get; } = new();
	private static Dictionary<string, Team> Jobs { get; } = new();

	// On Start load all jobs
	static Teams()
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

	public static void SetupSpawn(Player player, Team team)
	{
		player.SetMaxHealth(team.MaxHealth);
		player.SetHealth(team.MaxHealth);
		player.SetMaxArmor(team.MaxArmor);
		player.SetArmor(team.MaxArmor);
		Log.Info($"{player.GetSteamID()} has had their spawn initialized.");
	}
	
	// Get default job when player spawns
	public static Team Default()
	{
		return ResourceLibrary.Get<Team>( "gameplay/jobs/default.job" );
	}

	public static Team GetTeamByString(string teamName)
	{
		var RetrievedTeam = ResourceLibrary.Get<Team>($"gameplay/jobs/{teamName}.job");
		if ( RetrievedTeam.IsValid()) {
			return RetrievedTeam;
		}
		return null;
	}

	public static Dictionary<string, Team> GetAllJobs()
	{
		return Jobs;
	}

	public static Dictionary<string, TeamGroup> GetAllJobGroups()
	{
		return JobGroups;
	}
}