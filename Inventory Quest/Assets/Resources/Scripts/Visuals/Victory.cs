using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Victory : MonoBehaviour {

    public Text GoodText;
    public Text BadText;

    // Use this for initialization
    void Start () {
        GoodText.text = GameMaster.instance.goodScore.ToString();
        BadText.text = GameMaster.instance.badScore.ToString();
    }
}
