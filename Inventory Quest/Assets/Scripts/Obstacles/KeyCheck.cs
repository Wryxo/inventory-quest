using UnityEngine;
using System.Collections;

public class KeyCheck : MonoBehaviour {

    public int itemID;
    public int amount;

    public bool Check(Inventory inventory)
    {
        return (inventory.CountItemsWithId(itemID) >= amount);
    }

    public bool Check(NPC guy)
    {
        int otherSources = 0;
        return (guy.inventory.CountItemsWithId(itemID) + otherSources >= amount);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
