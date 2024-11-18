using System.Drawing;

namespace astral_base.RPBASE;

[GameResource( "Job Group Definition", "group", "" )]
public class JobGroup : GameResource
{
	public string Name { get; set; } = null!;

	public Color Color { get; set; }
}