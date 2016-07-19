using UnityEngine;
using System.Collections;

public class Item {

    public int width = 2;
    public int height = 2;

    public int stack = 1;
    public int maxStack = 1;

    public Sprite img;
    public Sprite[] imgs;

    public int id;

    public string name;

    public Stats stats;

    public Hashtable compatibleSlots;

    public float consumableBonus;
    public float wornStatBonus; 

    public Item()
    {
        stats = new Stats();
        compatibleSlots = new Hashtable();
    }

    public void AddSlot(string name)
    {
        if (!compatibleSlots.Contains(name))
        { 
            compatibleSlots.Add(name, 1);
        }
    }

    public void AddStat(string name, Skill value)
    {
        if (!stats.contents.Contains(name)) { 
            stats.Add(name, value);
        }
    }

    public override string ToString()
    {
        return string.Format("{0}\nStats: {1}\nCompatible slots: {2}\nSize: {3}x{4}\nAmount: {5}",name,stats,compatibleSlots,width,height,stack);
    }

}
