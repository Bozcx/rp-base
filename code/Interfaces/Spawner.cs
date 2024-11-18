namespace astral_base.RPBASE;

public record struct Spawner( Transform Transform, IReadOnlyList<string> Tags )
{
	public Vector3 Position => Transform.Position;
	public Rotation Rotation => Transform.Rotation;
	public Job job { get; set; }
}