using Sandbox.Diagnostics;
using Sandbox.Events;

namespace astral_base.RPBASE;

public partial class Player
{
	public List<Item> Items { get; set; } = new();

	[Property]
	public WeaponComponent WeaponComponent { get; set; } // Weapon Component. Will be placed onto the player object & will be in charge of animations etc.
	private Item WeaponEquiped { get; set;}

	public void AddItem(Item item)
	{
		Items.Add(item);
		this.SetActiveItem(item);
	}

	public void RemoveItem(Item item)
	{
		Items.Single(x => x.Name == item.Name); // Remove if the name matches.
	}

	public void SetActiveItem(Item item)
	{
		if (this.WeaponEquiped.IsValid()) { // Kinda speaks for itself, Undeploy if we had a weapon equiped in the past. (Should always be the case but whatever for rn.)
			this.WeaponEquiped.UnDeploy();
		}
		item.Deploy();
		this.WeaponEquiped = item;
		WeaponComponent.item = item;
		// WeaponComponent // Call the WeaponComp and call the deploy function there. Item.Deploy is used for special cases.
	}
}