namespace astral_base.SCPRP;

public record struct Spawner( Transform Transform, IReadOnlyList<string> Tags )
{
	public Vector3 Position => Transform.Position;
	public Rotation Rotation => Transform.Rotation;
	public Team team { get; set; }
}