namespace astral_base.SCPRP;

public class SpawnComp : Component
{
	[Property]
	public string name = "1st One";
	
	[Property]
	public Team team { get; set; }
}
