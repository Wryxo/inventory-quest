using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DiffUI : MonoBehaviour {

    void Awake()
    {
        NPC.instance.Event_onItemHover += ShowDifference;
        NPC.instance.Event_onItemExit += HideDifference;
    }

    void OnDestroy()
    {
        NPC.instance.Event_onItemHover -= ShowDifference;
        NPC.instance.Event_onItemExit -= HideDifference;
    }

    void ShowDifference(Item item)
    {
        int val = NPC.instance.GetEquippedDifference(item, transform.parent.name);
        Text text = GetComponent<Text>();
        if (val > 0)
        {
            text.text = "+" + val.ToString();
            text.color = new Color(0, 1, 0, 1);
        } else if (val < 0)
        {
            text.text = val.ToString();
            text.color = new Color(1, 0, 0, 1);
        } else
        {
            text.text = val.ToString();
            text.color = new Color(1, 1, 0, 1);
        }

    }

    void HideDifference()
    {
        GetComponent<Text>().color = new Color(1, 1, 0, 0);
    }
}
