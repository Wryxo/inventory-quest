using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour {

    public int width = 7;
    public int height = 4;

    Item[,] contents; //TODO: If needed, use a smarter solution; the Battleships algorithm seems stupid

    

	// Use this for initialization
	void Start () {
        contents = new Item[width, height];
	}
	
    public Item ItemAt(int x,int y)
    {
        if(x >= 0 && x < width && y >= 0 && y < height)
        {
            return contents[x, y];
        }
        return null;
    }

    public bool IsEmpty(int x,int y, int w, int h)
    {
        if (x < 0 || y < 0 || x + w > width || y + h > height) return false; //out of bounds
        //TODO: Maybe use something smarter
        for (int xi = x;xi < x+w; ++xi)
        {
            for (int yi =y;yi < y + w; ++yi)
            {
                if (contents[x, y]) return false;
            }
        }
        return true;
    }

    public bool InsertItem(Item what,int x,int y)
    {
        int w = what.width;
        int h = what.height;
        if (IsEmpty(x, y, w, h))
        {
            for (int xi = x; xi < x + w; ++xi)
            {
                for (int yi = y; yi < y + w; ++yi)
                {
                    contents[x, y] = what;
                }
            }
            return true;
        } else
        {
            //TODO: Allow items to stack
            return false;
        }
    }

    public void RemoveItem(Item what)
    {
        for(int xi = 0; xi < width; ++xi)
        {
            for(int yi = 0; yi < height; ++yi)
            {
                if (contents[xi, yi] == what) contents[xi, yi] = null;
            }
        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}
