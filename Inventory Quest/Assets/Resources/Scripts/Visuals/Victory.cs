using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Victory : MonoBehaviour {

    public Text GoodText;
    public Text BadText;

    // Use this for initialization
    void Start () {
        GoodText.text = NPC.instance.inventory.CountItemsWithId(0).ToString();
        BadText.text = GameMaster.instance.badScore.ToString();
    }
}
