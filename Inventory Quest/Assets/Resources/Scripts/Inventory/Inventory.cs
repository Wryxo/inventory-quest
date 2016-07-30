using UnityEngine;
using System.Collections;
using System;

public class Inventory : MonoBehaviour {

    public int width;
    public int height;

    Item[,] contents; //TODO: If needed, use a smarter solution; the Battleships algorithm seems stupid
    int[,] spriteOffsets; // Offsets for sprites broken down to separate slots

    public delegate void del_onInventoryChange();
    public del_onInventoryChange Event_onInventoryChange;

    

    // Use this for initialization
    void Awake () {
        contents = new Item[width, height];
        spriteOffsets = new int[width, height];

        // Testing mock up data
        //var debugstats = new Stats();
        //debugstats.Add("Strength", new Skill() { level = 3, baselevel = 0 });
        /*var tmp = new Item() { id = 47, width = 1, height = 1, stack = 2, maxStack = 5, img = Resources.Load<Sprite>("Sprites/Items/AmplifiedPotion_mouse") as Sprite, imgs = Resources.LoadAll<Sprite>("Sprites/Items/AmplifiedPotion") as Sprite[] };
        tmp.stats.Add("Attractivity", 5);
        tmp.AddSlot("head");
        InsertItem(tmp, 0, 0);
        tmp = new Item() { id = 49, width = 1, height = 1, stack = 2, maxStack = 5, img = Resources.Load<Sprite>("Sprites/Items/Deaths_breath_mouse") as Sprite, imgs = Resources.LoadAll<Sprite>("Sprites/Items/Deaths_breath") as Sprite[] };
        tmp.stats.Add("Attractivity", 10);
        tmp.AddSlot("head");
        InsertItem(tmp, 0, 1);
        tmp = new Item() { id = 49, width = 1, height = 1, stack = 2, maxStack = 5, img = Resources.Load<Sprite>("Sprites/Items/Deaths_breath_mouse") as Sprite, imgs = Resources.LoadAll<Sprite>("Sprites/Items/Deaths_breath") as Sprite[] };
        tmp.stats.Add("Attractivity", 10);
        tmp.AddSlot("head");
        InsertItem(tmp, 1, 1);
        tmp = new Item() { id = 48, width = 1, height = 2, stack = 1, maxStack = 1, img = Resources.Load<Sprite>("Sprites/Items/Gothic_Shield_mouse") as Sprite, imgs = Resources.LoadAll<Sprite>("Sprites/Items/Gothic_Shield") as Sprite[] };
        tmp.stats.Add("Branches", 5);
        tmp.stats.Add("Jump", -5);
        tmp.AddSlot("chest");
        InsertItem(tmp, 3, 1);*/
        
        if (Event_onInventoryChange != null)
        {
            Event_onInventoryChange();
        }
    }
	
    public Item ItemAt(int x,int y)
    {
        if(x >= 0 && x < width && y >= 0 && y < height)
        {
            return contents[x, y];
        }
        return null;
    }

    public int SpriteAt(int x, int y)
    {
        if (x >= 0 && x < width && y >= 0 && y < height)
        {
            return spriteOffsets[x, y];
        }
        return 0;
    }

    public bool IsEmpty(int x,int y, int w, int h)
    {
        if (x < 0 || y < 0 || x + w > width || y + h > height) return false; //out of bounds
        //TODO: Maybe use something smarter
        for (int xi = x; xi < x + w; xi++)
        {
            for (int yi =y; yi < y + h; yi++)
            {
                if (contents[xi, yi] != null) return false;
            }
        }
        return true;
    }

    public void EmptyInventory()
    {
        contents = new Item[width, height];
        spriteOffsets = new int[width, height];
        if (Event_onInventoryChange != null)
        {
            Event_onInventoryChange();
        }
    }

    public int ItemFitHere(Item what, int x, int y)
    {
        int w = what.width;
        int h = what.height;
        for (int i = y; i > y - h; i--)
        {
            for (int j =  x; j > x - w; j--)
            {
                if (IsEmpty(j, i, w, h))
                {
                    return j*10 + i;
                }
            }
        }
        return x*10 + y;
    }

    public Item InsertItem(Item what,int x,int y)
    {
        int w = what.width;
        int h = what.height;
        if (IsEmpty(x, y, w, h))
        {
            int so = 0;
            for (int yi = y; yi < y + h; yi++)
            {
                for (int xi = x; xi < x + w; xi++)
                {
                    contents[xi, yi] = what;
                    spriteOffsets[xi,yi] = so;
                    so++;
                }
            }
            if (Event_onInventoryChange != null)
            {
                Event_onInventoryChange();
            }
            return null;
        } else
        {
            //Try to stack same items if possible
            for (int yi = y; yi < y + h; yi++)
            {
                for (int xi = x; xi < x + w; xi++)
                {
                    if (yi >= height || xi >= width)
                    {
                        return what;
                    }
                    var checkedItem = contents[xi, yi];
                    if(checkedItem != null && checkedItem.id == what.id && checkedItem.stack < checkedItem.maxStack)
                    {
                        if(checkedItem.stack + what.stack <= checkedItem.maxStack)
                        {
                            checkedItem.stack += what.stack;
                            if (Event_onInventoryChange != null)
                            {
                                Event_onInventoryChange();
                            }
                            return null;
                        }
                        else
                        {
                            what.stack -= checkedItem.maxStack;
                            what.stack += checkedItem.stack;
                            checkedItem.stack = checkedItem.maxStack;
                            if (Event_onInventoryChange != null)
                            {
                                Event_onInventoryChange();
                            }
                            return what;
                        }
                    }
                    
                }
            }
            //Swapping: Step 1: Find the item to swap if possible
            Item tmp = null;
            for (int yi = y; yi < y + h; yi++)
            {
                for (int xi = x; xi < x + w; xi++)
                {
                    var checkedItem = contents[xi, yi];
                    if(checkedItem != null)
                    {
                        if(tmp == null)
                        {
                            tmp = checkedItem;
                        } else if(tmp != checkedItem)
                        {
                            return what;
                        }
                    }
                }
            }
            //Swapping: Step 2: Swap them
            RemoveItem(tmp);
            int so = 0;
            for (int yi = y; yi < y + h; yi++)
            {
                for (int xi = x; xi < x + w; xi++)
                {
                    contents[xi, yi] = what;
                    spriteOffsets[xi, yi] = so;
                    so++;
                }
            }
            if (Event_onInventoryChange != null)
            {
                Event_onInventoryChange();
            }
            return tmp;
        }
    }

    public int SpendItem(int id, int amount)
    {
        int debt = amount;
        for (int xi = 0; xi < width; xi++)
        {
            for (int yi = 0; yi < height; yi++)
            {
                var checkedItem = contents[xi, yi];
                if(checkedItem.id == id)
                {
                    if(debt < checkedItem.stack)
                    {
                        checkedItem.stack -= debt;
                        return 0;
                    } else
                    {
                        debt -= checkedItem.stack;
                        RemoveItem(checkedItem);
                    }
                }
            }
        }
        return debt;
    }

    public void RemoveItem(Item what)
    {
        for(int xi = 0; xi < width; xi++)
        {
            for(int yi = 0; yi < height; yi++)
            {
                if (contents[xi, yi] == what)
                {
                    contents[xi, yi] = null;
                    spriteOffsets[xi, yi] = 0;
                }
            }
        }
        if (Event_onInventoryChange != null)
        {
            Event_onInventoryChange();
        }
    }

    public void RemoveItem(int x, int y)
    {
        RemoveItem(ItemAt(x, y));
    }

    public int CountItemsWithId(int id)
    {
        int area = 0;
        int count = 0;
        for (int yi = 0; yi < height; yi++)
        {
            for (int xi = 0; xi < width; xi++)
            {
                var checkedItem = contents[xi, yi];
                if(checkedItem != null && checkedItem.id == id)
                {
                    count += checkedItem.stack;
                    if(area == 0) area = checkedItem.width * checkedItem.height;
                }
            }
        }
        return area > 0 ? count/area : count;
    }

    public void UI_getInventorySlot(int position)
    {

        // position / 10  = X coordinate
        // position % 10  = Y coordinate
        int x = position / 10;
        int y = position % 10;
        if (NPC.instance.hand == null)
        {
            var h = ItemAt(x, y);
            if (h != null)
            {
                RemoveItem(h);
            }
            NPC.instance.hand = h;
        }
        else
        {
            var newPos = ItemFitHere(NPC.instance.hand, x, y);
            var h = InsertItem(NPC.instance.hand, newPos / 10, newPos % 10);
            NPC.instance.hand = h;
        }
    }

    // Update is called once per frame
    void Update () {
	
	}
}
