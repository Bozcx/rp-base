using Sandbox.Utility;
using Sandbox.Events;

namespace astral_base.SCPRP;

public class Spawners : Component, ISceneStartup
{

	// public Spawners( Scene scene ) : base( scene ) {}
	public static List<Spawner> SpawnList { get; set; } = new();

	// Static initialization method
	void ISceneStartup.OnHostInitialize()
	{
		Log.Info("Loading spawn points...");

		SpawnList.Clear();

		foreach (var spawnPoint in Scene.GetAllComponents<SpawnComp>())
		{
			var spawner = new Spawner(
				new Transform(spawnPoint.Transform.Position, spawnPoint.Transform.Rotation),
				Array.Empty<string>()
			)
			{
				team = spawnPoint.team
			};

			SpawnList.Add(spawner);
			Log.Info($"Initializing Spawner: {spawnPoint.name} for team: {spawnPoint.team}");
		}
	}
}