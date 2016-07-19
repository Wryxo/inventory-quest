using UnityEngine;
using System.Collections;
using System;

public class Equipment : MonoBehaviour {

    public Hashtable validSlots;
    Hashtable items;
    public delegate void del_onEquipmentChange();
    public del_onEquipmentChange Event_onEquipmentChange;

    public void Awake()
    {
        items = new Hashtable();
        validSlots = new Hashtable();
        AddSlot("chest");
        AddSlot("head");
    }

    public void Start()
    {
        if (Event_onEquipmentChange != null)
        {
            Event_onEquipmentChange();
        }
    }

    public void UI_getSlotItem(string slot)
    {
        NPC.instance.hand = EquipItem(NPC.instance.hand, slot);
    }

    public Item EquipItem(Item item, object slot = null) //TODO: Maybe fix the RuneScape quiver bug
    {
        if (item == null)
        {
            return Unequip(slot);
        }
        if (slot == null) 
        {
            foreach(DictionaryEntry de in validSlots)
            {
                if (item.compatibleSlots.Contains(de.Key))
                {
                    if (!items.Contains(de.Key))
                    {
                        items[de.Key] = item;
                        if (Event_onEquipmentChange != null)
                        {
                            Event_onEquipmentChange();
                        }
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
                    if (Event_onEquipmentChange != null)
                    {
                        Event_onEquipmentChange();
                    }
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
                if (Event_onEquipmentChange != null)
                {
                    Event_onEquipmentChange();
                }
                return tmp;
            }
            else return item;
        }
        else return item;
    }

    public Item Unequip(object slot)
    {
        if (slot == null) throw (new System.Exception("Error: Can't remove item from null slot")); //this should never happen
        var s = ItemAt(slot);
        if (s != null)
        {
            items.Remove(slot);
            if (Event_onEquipmentChange != null)
            {
                Event_onEquipmentChange();
            }
        }
        return s;
    }

    public Item ItemAt(object slot)
    {
        if (!items.Contains(slot)) return null;
        return (Item)items[slot];
    }

    public bool AddSlot(object slot)
    {
        if (validSlots.Contains(slot)) return false;
        validSlots.Add(slot, null);
        return true;
    }

    public bool RemoveSlot(object slot)
    {
        if (!validSlots.Contains(slot)) return false;
        validSlots.Remove(slot);
        return true; 
    }

    public int CountItem(int id)
    {
        var count = 0;
        foreach (DictionaryEntry de in items)
        {
            if (((Item)de.Value).id == id)
            {
                count += ((Item)de.Value).stack;
            }
        }
        return count;
    }

    public int SpendItem(int id, int amount) //TODO: Make this work
    {
        int debt = amount;
        foreach(DictionaryEntry de in items)
        {
            if(((Item)de.Value).id == id)
            {
                if(((Item)de.Value).stack > debt)
                {
                    ((Item)de.Value).stack -= debt;
                    return 0;
                }
                else
                {
                    debt -= ((Item)de.Value).stack;
                    Unequip(de.Key);
                }
            }
        }
        return debt;
    }
}
