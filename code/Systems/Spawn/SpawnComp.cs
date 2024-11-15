namespace astral_base.SCPRP;

public class SpawnComp : Component
{
	[Property]
	public string name { get; set; }
	
	[Property]
	public Job job { get; set; }
}
