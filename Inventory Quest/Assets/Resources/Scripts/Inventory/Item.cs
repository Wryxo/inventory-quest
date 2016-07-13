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

    public bool consumable;

    public Item()
    {
        stats = new Stats();
        compatibleSlots = new Hashtable();
        compatibleSlots.Add("chest", 1);
        compatibleSlots.Add("head", 1);
    }

    public override string ToString()
    {
        return string.Format("{0}\nStats: {1}\nCompatible slots: {2}\nSize: {3}x{4}\nAmount: {5}",name,stats,compatibleSlots,width,height,stack);
    }

}
