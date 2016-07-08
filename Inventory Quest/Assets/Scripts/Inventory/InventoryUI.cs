using UnityEngine;
using System.Collections;

public class InventoryUI : MonoBehaviour {

    public Inventory inventory;
    public float tileWidth;
    public float tileHeight;
    public Rect position;
    public Texture2D inventoryImage;
    public Texture2D slotImage;

    Item hand; //Debug, please remove this after this class is programmed

    // Use this for initialization
    void Start () {
	    hand = new Item() { id = 1, width = 1, height = 1, stack = 2, maxStack = 5 };
    }
	
	void OnGUI()
    {
        // Fit sizes to screen size
        position.width = Screen.width;
        position.height = Screen.height / 2;
        position.y = Screen.height / 2;
        tileWidth = (3 * position.width / 4 - 40) / inventory.width;
        tileHeight = tileWidth;
        GUI.DrawTexture(position, inventoryImage);
        // This is called sooner than inventory is populated => not all slots that should be filled are filled --- TODO: fix it!
        drawSlots();
    }

    // Draw graphics for slots according to inventory
    void drawSlots()
    {
        Rect slotPosition = new Rect(position.width / 4 + 20, position.y + 20, tileWidth, tileHeight);
        for (int i = 0; i < inventory.height; i++)
        {
            for (int j = 0; j < inventory.width; j++)
            {
                // Draw image, slotImage depends on item in that slot
                GUI.DrawTexture(slotPosition, slotImage);
                // Put invisible button over image --- TODO: is it responsive enough ?
                if (GUI.Button(slotPosition, "", new GUIStyle()))
                {
                    if (!inventory.IsEmpty(j, i, 1, 1)) {
                        Debug.Log("ITEM " + inventory.ItemAt(j, i).id);
                        Debug.Log("AMOUNT " + inventory.ItemAt(j, i).stack);
                    } else
                    {
                        Debug.Log("EMPTY");
                    }
                    var h = inventory.InsertItem(hand, j, i);
                    if(h == null)
                    {
                        hand = new Item() { id = 1, width = 1, height = 1, stack = 2, maxStack = 5 };
                    }
                    else
                    {
                        hand = h;
                    }
                    Debug.Log("ITEM ON HAND: " + hand.id);
                    Debug.Log("AMOUNT: " + hand.stack);
                }
                slotPosition.x += tileWidth;
            }
            slotPosition.x = position.width / 4 + 20;
            slotPosition.y += tileHeight; 
        }
    }
}
