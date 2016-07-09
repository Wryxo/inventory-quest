using UnityEngine;
using System.Collections;

public class WornEquipment {

    Hashtable validSlots;
    Hashtable items;

    Item EquipItem(Item item, object slot = null) //TODO: Maybe fix the RuneScape quiver bug
    {
        if (slot == null) 
        {
            foreach(DictionaryEntry de in validSlots)
            {
                if (item.compatibleSlots.Contains(de.Key))
                {
                    if (!items.Contains(de.Key))
                    {
                        items[de.Key] = item;
                        return null;
                    }
                }
            }
            foreach (DictionaryEntry de in validSlots)
            {
                if (item.compatibleSlots.Contains(de.Key))
                {
                    var tmp = (Item)items[de.Key];
                    items[de.Key] = item;
                    return tmp;
                }
            }
            return item;
        }
        else if (validSlots.Contains(slot)) 
        {
            if (item.compatibleSlots.Contains(slot))
            {
                var tmp = (Item)items[slot];
                items[slot] = item;
                return tmp;
            }
            else return item;
        }
        else return item;
    }

    Item Unequip(object slot)
    {
        var s = ItemAt(slot);
        if(s != null) items.Remove(slot);
        return s;
    }

    Item ItemAt(object slot)
    {
        if (!items.Contains(slot)) return null;
        return (Item)items[slot];
    }

    bool AddSlot(object slot)
    {
        if (validSlots.Contains(slot)) return false;
        validSlots.Add(slot, null);
        return true;
    }

    bool RemoveSlot(object slot)
    {
        if (!validSlots.Contains(slot)) return false;
        validSlots.Remove(slot);
        return true; 
    }

}
