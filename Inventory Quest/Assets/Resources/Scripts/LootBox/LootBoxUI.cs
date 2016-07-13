using UnityEngine;
using System.Collections;

public class LootBoxUI : MonoBehaviour {

    private Inventory lootbox;
    private NPC player;
    private const string lootboxImageCallback = "SetImage";

    // Use this for initialization
    void Start()
    {
        //testing, debug data
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<NPC>();
        lootbox = player.lootbox;
        BroadcastMessage(lootboxImageCallback);
    }

    public void getLootboxSlot(int position)
    {
        // position / 10  = X coordinate
        // position % 10  = Y coordinate
        int x = position / 10;
        int y = position % 10;
        if (player.hand == null)
        {
            var h = lootbox.ItemAt(x, y);
            if (h != null)
            {
                lootbox.RemoveItem(h);
            }
            player.hand = h;
        }
        else
        {
            var h = lootbox.InsertItem(player.hand, x, y);
            player.hand = h;
        }
        BroadcastMessage(lootboxImageCallback);
    }
}
