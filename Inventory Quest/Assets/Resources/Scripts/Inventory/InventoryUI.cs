using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour {

    private Inventory inventory;
    private Equipment equipment;
    private NPC player;
    StatCheck lu; //Debug
    private const string inventoryImageCallback = "SetImage";

    public delegate void del_onInventorySlot(int num);
    public del_onInventorySlot Event_OnInventorySlot;
    public static InventoryUI instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<NPC>();
        inventory = player.inventory;
        equipment = player.gear;
        //testing, debug data
        lu = new StatCheck() { statName = "Strength", baseDifficulty = 0, nDice = 2, sidesPerDie = 2};
        BroadcastMessage(inventoryImageCallback);
        if (Event_OnInventorySlot != null)
        {
            Event_OnInventorySlot(11);
        }
    }

    public void getInventorySlot(int position)
    {
        
        // position / 10  = X coordinate
        // position % 10  = Y coordinate
        int x = position / 10;
        int y = position % 10;
        if (player.hand == null)
        {
            var h = inventory.ItemAt(x, y);
            if (h != null)
            {
                inventory.RemoveItem(h);
            }
            player.hand = h;
        }
        else
        {
            var h = inventory.InsertItem(inventory.GetComponent<NPC>().hand, x, y);
            player.hand = h;
        }
        BroadcastMessage(inventoryImageCallback);
    }

    public void quickEquipInventorySlot(int position)
    {
        int x = position / 10;
        int y = position % 10;
        if (player.hand != null)
        {
            return;
            
        }
        var h = inventory.ItemAt(x, y);
        if (h != null)
        {
            inventory.RemoveItem(h);
        }
        player.hand = equipment.EquipItem(h);
        if (player.hand != null)
        {

        }
        BroadcastMessage(inventoryImageCallback);
    }
}
