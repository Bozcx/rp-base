
namespace astral_base.SCPRP;

[GameResource( "Item Base", "items", "", Category = "Items" )]
public class Item : GameResource
{
	public string Name { get; set; }
	public string Description { get; set; }

	[ImageAssetPath]
	public string Icon { get; set; }
	public string Class { get; set; }

	public virtual void Deploy() {}
	public virtual void UnDeploy() {}
	public virtual void FirePrimary() {}
}