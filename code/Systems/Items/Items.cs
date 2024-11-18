using Sandbox.Utility;
using Sandbox.Events;

namespace astral_base.SCPRP;

public class Items
{
	public static Dictionary<Item, object> GetAllItems()
	{
		return InitializeItems.Items;
	}

    public static KeyValuePair<Item, object>? GetItemByName(string name)
	{
        var list = GetAllItems();

        foreach (Item key in list.Keys) {
            if (!(key.Name == name)) { continue; }
            return new KeyValuePair<Item, object>(key, list[key]);
        }

        return null;
	}

    public static KeyValuePair<Item, object>? GetItem(Item item)
	{
        if (InitializeItems.Items.TryGetValue(item, out var value))
        {
            // Return the key-value pair
            return new KeyValuePair<Item, object>(item, value);
        }

        return null;
	}
}