using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Victory : MonoBehaviour {

    public Text DisplayText;

    // Use this for initialization
    void Start () {
        if (DisplayText != null)
        {
            DisplayText.text = NPC.instance.inventory.CountItemsWithId(0).ToString();
        }
    }
}
