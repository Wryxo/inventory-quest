using UnityEngine;
using System.Collections;

public class KeyCheck {

    public int itemID;
    public int amount;

    public bool Check(Inventory inventory)
    {
        return (inventory.CountItemsWithId(itemID) >= amount);
    }

    public bool Check(NPC guy)
    {
        int otherSources = 0;
        return (guy.CountItem(itemID) + otherSources >= amount);
    }

}
