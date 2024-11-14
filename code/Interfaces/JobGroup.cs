using System.Drawing;

namespace astral_base.SCPRP;

[GameResource( "Job Group Definition", "group", "" )]
public class JobGroup : GameResource
{
	public string Name { get; set; } = null!;

	public Color Color { get; set; }
}