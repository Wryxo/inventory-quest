using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TooltipUI : MonoBehaviour {

	public GameObject Name;
	public GameObject Type;
	public GameObject Attract;
	public GameObject Run;
	public GameObject Swim;
	public GameObject Jump;
	public GameObject Branches;

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
        GetComponent<Image>().color = new Color(1, 1, 1, 1);
        Name.SetActive(true);
        Name.GetComponent<Text>().text = item.name;
		Type.SetActive(true);
		if (item.compatibleSlots.Contains("head")) {
        	Type.GetComponent<Text>().text = "head";
		} else if (item.compatibleSlots.Contains("chest")) {
        	Type.GetComponent<Text>().text = "chest";
		} else {
        	Type.GetComponent<Text>().text = "feet";
		}

		if (item.stats.contents.Contains(HelpFunctions.Attract)) {
        	int val = NPC.instance.GetEquippedDifference(item, HelpFunctions.Attract);
			Attract.SetActive(true);
			Attract.GetComponentInChildren<Text>().text = item.stats.LevelOf(HelpFunctions.Attract) + (val > 0 ? " (+" : " (") + val + ")";
		}
		if (item.stats.contents.Contains(HelpFunctions.Run)) {
        	int val = NPC.instance.GetEquippedDifference(item, HelpFunctions.Run);
			Run.SetActive(true);
			Run.GetComponentInChildren<Text>().text = item.stats.LevelOf(HelpFunctions.Run) + (val > 0 ? " (+" : " (") + val + ")";
		}
		if (item.stats.contents.Contains(HelpFunctions.Swim)) {
        	int val = NPC.instance.GetEquippedDifference(item, HelpFunctions.Swim);
			Swim.SetActive(true);
			Swim.GetComponentInChildren<Text>().text = item.stats.LevelOf(HelpFunctions.Swim) + (val > 0 ? " (+" : " (") + val + ")";
		}
		if (item.stats.contents.Contains(HelpFunctions.Jump)) {
        	int val = NPC.instance.GetEquippedDifference(item, HelpFunctions.Jump);
			Jump.SetActive(true);
			Jump.GetComponentInChildren<Text>().text = item.stats.LevelOf(HelpFunctions.Jump) + (val > 0 ? " (+" : " (") + val + ")";
		}
		if (item.stats.contents.Contains(HelpFunctions.Branches)) {
        	int val = NPC.instance.GetEquippedDifference(item, HelpFunctions.Branches);
			Branches.SetActive(true);
			Branches.GetComponentInChildren<Text>().text = item.stats.LevelOf(HelpFunctions.Branches) + (val > 0 ? " (+" : " (") + val + ")";
		}
    }

    void HideDifference()
    {
        GetComponent<Image>().color = new Color(1, 1, 1, 0);
		Name.SetActive(false);
		Type.SetActive(false);
		Attract.SetActive(false);
		Jump.SetActive(false);
		Branches.SetActive(false);
		Run.SetActive(false);
		Swim.SetActive(false);
    }
}
