using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour {

    public Inventory inventory;
    StatCheck lu; //Debug
    private const string inventoryImageCallback = "SetImage";

    // Use this for initialization
    void Start () {

        //testing, debug data
	    inventory.GetComponent<NPC>().hand = new Item() { id = 48, width = 1, height = 2, stack = 1, maxStack = 1, img = Resources.Load<Sprite>("Sprites/Gothic_Shield_mouse") as Sprite, imgs = Resources.LoadAll<Sprite>("Sprites/Gothic_Shield") as Sprite[] };
        lu = new StatCheck() { statName = "Strength", baseDifficulty = 0, nDice = 2, sidesPerDie = 2};
        BroadcastMessage(inventoryImageCallback);
    }

    public void getSlotItem(int position)
    {
        // position / 10  = X coordinate
        // position % 10  = Y coordinate
        int x = position / 10;
        int y = position % 10;
        if (inventory.GetComponent<NPC>().hand == null)
        {
            var h = inventory.ItemAt(x, y);
            if (h != null)
            {
                inventory.RemoveItem(h);
            } 
            inventory.GetComponent<NPC>().hand = h;
        }
        else
        {
            var h = inventory.InsertItem(inventory.GetComponent<NPC>().hand, x, y);
            inventory.GetComponent<NPC>().hand = h;
        }
        BroadcastMessage(inventoryImageCallback);
    }
}
