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

    static int maxId = 0;

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
        } else
        {
            Skill tmp = (Skill)stats.contents[name];
            stats.contents[name] = new Skill(tmp.baselevel + value.baselevel);
        }
    }

    public override string ToString()
    {
        return string.Format("Size: {0}x{1}\n",width,height);
    }

    public static Item Celenka(int val)
    {
        Item res = new Item()
        {
            id = maxId++,
            height = 2,
            width = 1,
            maxStack = 1,
            stack = 1,
            img = Resources.Load<Sprite>("Sprites/Items/mouse_celenka") as Sprite,
            imgs = Resources.LoadAll<Sprite>("Sprites/Items/inv_celenka") as Sprite[]
        };
        res.AddSlot("head");
        res.AddStat(HelpFunctions.Attract, new Skill(val));
        return res;
    }

    public static Item Ciapka(int val)
    {
        Item res = new Item()
        {
            id = maxId++,
            height = 1,
            width = 1,
            maxStack = 1,
            stack = 1,
            img = Resources.Load<Sprite>("Sprites/Items/mouse_ciapka") as Sprite,
            imgs = Resources.LoadAll<Sprite>("Sprites/Items/inv_ciapka") as Sprite[]
        };
        res.AddSlot("head");
        res.AddStat(HelpFunctions.Attract, new Skill(val));
        return res;
    }

    public static Item Nasada(int val)
    {
        Item res = new Item()
        {
            id = maxId++,
            height = 1,
            width = 2,
            maxStack = 1,
            stack = 1,
            img = Resources.Load<Sprite>("Sprites/Items/mouse_nasada_na_zobak") as Sprite,
            imgs = Resources.LoadAll<Sprite>("Sprites/Items/inv_nasada_na_zobak") as Sprite[]
        };
        res.AddSlot("head");
        res.AddStat(HelpFunctions.Swim, new Skill(val));
        return res;
    }

    public static Item Pruziny(int val)
    {
        Item res = new Item()
        {
            id = maxId++,
            height = 1,
            width = 2,
            maxStack = 1,
            stack = 1,
            img = Resources.Load<Sprite>("Sprites/Items/mouse_pruziny") as Sprite,
            imgs = Resources.LoadAll<Sprite>("Sprites/Items/inv_pruziny") as Sprite[]
        };
        res.AddSlot("feet");
        res.AddStat(HelpFunctions.Jump, new Skill(val));
        return res;
    }

    public static Item Topanka(int val)
    {
        Item res = new Item()
        {
            id = maxId++,
            height = 1,
            width = 1,
            maxStack = 1,
            stack = 1,
            img = Resources.Load<Sprite>("Sprites/Items/mouse_topanka") as Sprite,
            imgs = Resources.LoadAll<Sprite>("Sprites/Items/inv_topanka") as Sprite[]
        };
        res.AddSlot("feet");
        res.AddStat(HelpFunctions.Run, new Skill(val));
        return res;
    }

    public static Item Topanky(int val)
    {
        Item res = new Item()
        {
            id = maxId++,
            height = 1,
            width = 2,
            maxStack = 1,
            stack = 1,
            img = Resources.Load<Sprite>("Sprites/Items/mouse_topanky") as Sprite,
            imgs = Resources.LoadAll<Sprite>("Sprites/Items/inv_topanky") as Sprite[]
        };
        res.AddSlot("feet");
        res.AddStat(HelpFunctions.Run, new Skill(val));
        return res;
    }

    public static Item Trysky(int val)
    {
        Item res = new Item()
        {
            id = maxId++,
            height = 3,
            width = 2,
            maxStack = 1,
            stack = 1,
            img = Resources.Load<Sprite>("Sprites/Items/mouse_trysky") as Sprite,
            imgs = Resources.LoadAll<Sprite>("Sprites/Items/inv_trysky") as Sprite[]
        };
        res.AddSlot("chest");
        res.AddStat(HelpFunctions.Jump, new Skill(val));
        return res;
    }

    public static Item Vesta(int val)
    {
        Item res = new Item()
        {
            id = maxId++,
            height = 2,
            width = 2,
            maxStack = 1,
            stack = 1,
            img = Resources.Load<Sprite>("Sprites/Items/mouse_vesta") as Sprite,
            imgs = Resources.LoadAll<Sprite>("Sprites/Items/inv_vesta") as Sprite[]
        };
        res.AddSlot("chest");
        res.AddStat(HelpFunctions.Swim, new Skill(val));
        return res;
    }
}
