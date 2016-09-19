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

    static int maxId = 1;

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

    public static Item Vetvicky(int val)
    {
        Item res = new Item()
        {
            name = "Vetva",
            id = 0,
            height = 2,
            width = 1,
            maxStack = 999,
            stack = (val > 0) ? val : 1,
            img = Resources.Load<Sprite>("Sprites/Items/mouse_konar") as Sprite,
            imgs = Resources.LoadAll<Sprite>("Sprites/Items/inv_konar") as Sprite[]
        };
        res.AddSlot("head");
        return res;
    }
    public static Item Celenka(int val)
    {
        Item res = new Item()
        {
            id = maxId++,
            name = "Celenka Of Doom",
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
            name = "Santova Ciapka",
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
            name = "Nasada Na Zobak",
            height = 1,
            width = 2,
            maxStack = 1,
            stack = 1,
            img = Resources.Load<Sprite>("Sprites/Items/mouse_nasada_na_zobak") as Sprite,
            imgs = Resources.LoadAll<Sprite>("Sprites/Items/inv_nasada_na_zobak") as Sprite[]
        };
        res.AddSlot("head");
        res.AddStat(HelpFunctions.Branches, new Skill(val));
        return res;
    }
    public static Item Pruziny(int val)
    {
        Item res = new Item()
        {
            id = maxId++,
            name = "Super Pruziny",
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
            name = "Piratske Topanky",
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
            name = "Tenisky",
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
            name = "SpaceX",
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
            name = "Perova Vesta",
            height = 2,
            width = 2,
            maxStack = 1,
            stack = 1,
            img = Resources.Load<Sprite>("Sprites/Items/mouse_vesta") as Sprite,
            imgs = Resources.LoadAll<Sprite>("Sprites/Items/inv_vesta") as Sprite[]
        };
        res.AddSlot("chest");
        res.AddStat(HelpFunctions.Attract, new Skill(val));
        return res;
    }
    public static Item Delfin(int val)
    {
        Item res = new Item()
        {
            id = maxId++,
            name = "Willy",
            height = 2,
            width = 2,
            maxStack = 1,
            stack = 1,
            img = Resources.Load<Sprite>("Sprites/Items/mouse_delfin") as Sprite,
            imgs = Resources.LoadAll<Sprite>("Sprites/Items/inv_delfin") as Sprite[]
        };
        res.AddSlot("chest");
        res.AddStat(HelpFunctions.Swim, new Skill(val));
        return res;
    }
    public static Item DelfinBota(int val)
    {
        Item res = new Item()
        {
            id = maxId++,
            name = "Mrtvy Willy",
            height = 1,
            width = 1,
            maxStack = 1,
            stack = 1,
            img = Resources.Load<Sprite>("Sprites/Items/mouse_delfinBota") as Sprite,
            imgs = Resources.LoadAll<Sprite>("Sprites/Items/inv_delfinBota") as Sprite[]
        };
        res.AddSlot("feet");
        res.AddStat(HelpFunctions.Swim, new Skill(val));
        return res;
    }
    public static Item DelfinBoty(int val)
    {
        Item res = new Item()
        {
            id = maxId++,
            name = "Uz Su Dvaja",
            height = 1,
            width = 2,
            maxStack = 1,
            stack = 1,
            img = Resources.Load<Sprite>("Sprites/Items/mouse_delfinBoty") as Sprite,
            imgs = Resources.LoadAll<Sprite>("Sprites/Items/inv_delfinBoty") as Sprite[]
        };
        res.AddSlot("feet");
        res.AddStat(HelpFunctions.Swim, new Skill(val));
        return res;
    }
    public static Item Kridla(int val)
    {
        Item res = new Item()
        {
            id = maxId++,
            name = "Angel Wings",
            height = 2,
            width = 3,
            maxStack = 1,
            stack = 1,
            img = Resources.Load<Sprite>("Sprites/Items/mouse_kridla") as Sprite,
            imgs = Resources.LoadAll<Sprite>("Sprites/Items/inv_kridla") as Sprite[]
        };
        res.AddSlot("chest");
        res.AddStat(HelpFunctions.Jump, new Skill(val));
        return res;
    }
    public static Item Magnet(int val)
    {
        Item res = new Item()
        {
            id = maxId++,
            name = "Wood Magnet",
            height = 1,
            width = 1,
            maxStack = 1,
            stack = 1,
            img = Resources.Load<Sprite>("Sprites/Items/mouse_magnet") as Sprite,
            imgs = Resources.LoadAll<Sprite>("Sprites/Items/inv_magnet") as Sprite[]
        };
        res.AddSlot("head");
        res.AddStat(HelpFunctions.Branches, new Skill(val));
        return res;
    }
    public static Item Neopren(int val)
    {
        Item res = new Item()
        {
            id = maxId++,
            name = "Chudnuci Pas",
            height = 2,
            width = 2,
            maxStack = 1,
            stack = 1,
            img = Resources.Load<Sprite>("Sprites/Items/mouse_neopren") as Sprite,
            imgs = Resources.LoadAll<Sprite>("Sprites/Items/inv_neopren") as Sprite[]
        };
        res.AddSlot("chest");
        res.AddStat(HelpFunctions.Swim, new Skill(val));
        return res;
    }
    public static Item Plutva(int val)
    {
        Item res = new Item()
        {
            id = maxId++,
            name = "Plutva Vznasania",
            height = 1,
            width = 1,
            maxStack = 1,
            stack = 1,
            img = Resources.Load<Sprite>("Sprites/Items/mouse_plutva") as Sprite,
            imgs = Resources.LoadAll<Sprite>("Sprites/Items/inv_plutva") as Sprite[]
        };
        res.AddSlot("feet");
        res.AddStat(HelpFunctions.Swim, new Skill(val));
        return res;
    }
    public static Item Plutvy(int val)
    {
        Item res = new Item()
        {
            id = maxId++,
            name = "Plutvy Rychlosti",
            height = 1,
            width = 2,
            maxStack = 1,
            stack = 1,
            img = Resources.Load<Sprite>("Sprites/Items/mouse_plutvy") as Sprite,
            imgs = Resources.LoadAll<Sprite>("Sprites/Items/inv_plutvy") as Sprite[]
        };
        res.AddSlot("feet");
        res.AddStat(HelpFunctions.Swim, new Skill(val));
        return res;
    }
    public static Item Tryska(int val)
    {
        Item res = new Item()
        {
            id = maxId++,
            name = "Maly SpaceX",
            height = 3,
            width = 1,
            maxStack = 1,
            stack = 1,
            img = Resources.Load<Sprite>("Sprites/Items/mouse_tryska") as Sprite,
            imgs = Resources.LoadAll<Sprite>("Sprites/Items/inv_tryska") as Sprite[]
        };
        res.AddSlot("chest");
        res.AddStat(HelpFunctions.Jump, new Skill(val));
        return res;
    }
    public static Item Trysky2(int val)
    {
        Item res = new Item()
        {
            id = maxId++,
            name = "Hyper SpaceX",
            height = 3,
            width = 2,
            maxStack = 1,
            stack = 1,
            img = Resources.Load<Sprite>("Sprites/Items/mouse_trysky_2") as Sprite,
            imgs = Resources.LoadAll<Sprite>("Sprites/Items/inv_trysky_2") as Sprite[]
        };
        res.AddSlot("chest");
        res.AddStat(HelpFunctions.Jump, new Skill(val));
        return res;
    }
}
