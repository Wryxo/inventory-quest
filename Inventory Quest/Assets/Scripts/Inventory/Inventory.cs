using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour {

    public int width;
    public int height;

    Item[,] contents; //TODO: If needed, use a smarter solution; the Battleships algorithm seems stupid

    

	// Use this for initialization
	void Start () {
        contents = new Item[width, height];

        // Testing mock up data
        var tmp = new Item() { id = 1, width = 2, height = 2 };
        Debug.Log(InsertItem(tmp, 0, 0));
        /*tmp = new Item() { id = 2, width = 3, height = 2 };
        Debug.Log(InsertItem(tmp, 2, 1));
        tmp = new Item() { id = 3, width = 1, height = 2 };
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
            for (int xi = x; xi < x + w; xi++)
            {
                for (int yi = y; yi < y + h; yi++)
                {
                    Debug.Log(xi + " " + yi);
                    contents[xi, yi] = what;
                }
            }
            return null;
        } else
        {
            //TODO: Allow items to stack
            return what;
        }
    }

    public void RemoveItem(Item what)
    {
        for(int xi = 0; xi < width; xi++)
        {
            for(int yi = 0; yi < height; yi++)
            {
                if (contents[xi, yi] == what) contents[xi, yi] = null;
            }
        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}
