using Sandbox.Utility;
using Sandbox.Events;

namespace astral_base.RPBASE;

public sealed class InitializeItems : GameObjectSystem<InitializeItems>, ISceneStartup
{
	public InitializeItems( Scene scene ) : base( scene ) {}

	[HostSync]
	public static Dictionary<Item, object> Items { get; set; } = new();

	void ISceneStartup.OnHostInitialize()
	{
		foreach ( var item in ResourceLibrary.GetAll<Item>() )
		{	
			Log.Info( $"Loading item: {item.Name} With Class: {item.Class}" );
			Type? itemclass = Type.GetType("astral_base.RPBASE." + item.Class); // get the class based off of the string.
			if (itemclass == null) {
				Log.Info($"Class '{item.Class}' not found.");
				continue;
			}

			object? instance = Activator.CreateInstance(itemclass); // Initiate the object. Add caching mby?
			Log.Info($"Added the class {instance} to the Item.");
			Items[item] = instance;
			
		}
	}
}