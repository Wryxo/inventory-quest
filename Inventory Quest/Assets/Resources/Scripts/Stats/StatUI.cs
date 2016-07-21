using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatUI : MonoBehaviour {

    void Awake()
    {
        NPC.instance.Event_onStatsChange += SetValue;
    }

	void SetValue()
    {
        GetComponent<Text>().text = NPC.instance.skills.LevelOf(name).ToString();
    }
}
