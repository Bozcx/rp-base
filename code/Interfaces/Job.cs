namespace astral_base.SCPRP;

[GameResource( "Job Definition", "job", "", Category = "Roleplay" )]
public class Job : GameResource
{
	public string Name { get; set; } = null!;
	public string Description { get; set; } = null!;
	public Color Color { get; set; }
	public JobGroup Group { get; set; } = null!;
	public int SortingLevel { get; set; }
    public float MaxHealth { get; set; }
    public float MaxArmor { get; set; }
	public List<Model> Models { get; set; } = null!;
	public float Salary { get; set; }
	public int MaxWorkers { get; set; }
	public List<Item> Equipment { get; set; } = new();
}