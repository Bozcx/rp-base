namespace astral_base.SCPRP;

[GameResource( "Item Definition", "item", "" )]
public class Item : GameResource
{
	public string Name { get; set; } = null!;
	public string Description { get; set; } = null!;

	// For now don't add weapons etc. Wait till a proper base gets released & possibly use that as a base.
}