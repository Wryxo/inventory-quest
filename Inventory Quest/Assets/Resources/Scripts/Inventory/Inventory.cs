using UnityEngine;
using System.Collections;
using System;

public class Inventory : MonoBehaviour {

    public int width;
    public int height;

    Item[,] contents; //TODO: If needed, use a smarter solution; the Battleships algorithm seems stupid
    int[,] spriteOffsets; // Offsets for sprites broken down to separate slots

	// Use this for initialization
	void Awake () {
        contents = new Item[width, height];
        spriteOffsets = new int[width, height];

        // Testing mock up data
        var debugstats = new Stats();
        debugstats.Add("Strength", new Skill() { level = 3, baselevel = 0 });
        var tmp = new Item() { id = 47, width = 1, height = 1, stack = 2, maxStack = 5, img = Resources.Load("Sprites/AmplifiedPotion") as Sprite, imgs = Resources.LoadAll<Sprite>("Sprites/AmplifiedPotion") as Sprite[] };
        Debug.Log(InsertItem(tmp, 0, 0));
        tmp = new Item() { id = 49, width = 1, height = 1, stack = 2, maxStack = 5, img = Resources.Load("Sprites/Deaths_breath") as Sprite, imgs = Resources.LoadAll<Sprite>("Sprites/Deaths_breath") as Sprite[] };
        Debug.Log(InsertItem(tmp, 0, 1));
        /*tmp = new Item() { id = 48, width = 3, height = 2, stats = debugstats };
        Debug.Log(InsertItem(tmp, 2, 1));
        debugstats = new Stats();
        debugstats.Add("Strength", new Skill() { level = 2, baselevel = 0 });
        tmp = new Item() { id = 49, width = 1, height = 2, stats = debugstats };
        Debug.Log(InsertItem(tmp, 1, 2));*/
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
                    Debug.Log(xi + " " + yi);
                    contents[xi, yi] = what;
                    spriteOffsets[xi,yi] = so;
                    so++;
                }
            }
            return null;
        } else
        {
            //Try to stack same items if possible
            for (int yi = y; yi < y + h; yi++)
            {
                for (int xi = x; xi < x + w; xi++)
                {
                    var checkedItem = contents[xi, yi];
                    if(checkedItem != null && checkedItem.id == what.id && checkedItem.stack < checkedItem.maxStack)
                    {
                        if(checkedItem.stack + what.stack <= checkedItem.maxStack)
                        {
                            checkedItem.stack += what.stack;
                            return null;
                        }
                        else
                        {
                            checkedItem.stack = checkedItem.maxStack;
                            what.stack -= checkedItem.maxStack;
                            what.stack += checkedItem.stack;
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
                    Debug.Log(xi + " " + yi);
                    contents[xi, yi] = what;
                    spriteOffsets[xi, yi] = so;
                    so++;
                }
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
    }

    public int CountItemsWithId(int id)
    {
        int area = 1;
        int count = 0;
        for (int xi = 0; xi < width; xi++)
        {
            for (int yi = 0; yi < height; yi++)
            {
                var checkedItem = contents[xi, yi];
                if(checkedItem != null && checkedItem.id == id)
                {
                    count += checkedItem.stack;
                    if(area == 0) area = checkedItem.width * checkedItem.height;
                }
            }
        }
        return count/area;
    }

	// Update is called once per frame
	void Update () {
	
	}
}
