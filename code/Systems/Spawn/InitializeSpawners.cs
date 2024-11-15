using Sandbox.Utility;
using Sandbox.Events;

namespace astral_base.SCPRP;

public sealed class InitializeSpawners : GameObjectSystem<InitializeSpawners>, ISceneStartup
{
	public InitializeSpawners( Scene scene ) : base( scene ) {}

	// public Spawners( Scene scene ) : base( scene ) {}
	[HostSync]
	public static List<Spawner> SpawnList { get; } = new();

	// Static initialization method
	void ISceneStartup.OnHostInitialize()
	{
		Log.Info("Loading spawn points");

		SpawnList.Clear();

		foreach (var spawnPoint in Scene.GetAllComponents<SpawnComp>())
		{
			var spawner = new Spawner(
				new Transform(spawnPoint.WorldPosition, spawnPoint.WorldRotation),
				Array.Empty<string>()
			)
			{
				job = spawnPoint.job
			};

			SpawnList.Add(spawner);
			Log.Info($"Initializing Spawner: {spawnPoint.name} for team: {spawnPoint.job}");
		}
	}
}
