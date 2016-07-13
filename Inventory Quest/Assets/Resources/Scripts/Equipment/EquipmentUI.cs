using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class EquipmentUI : MonoBehaviour {

	private Equipment equipment;
    private const string equipmentImageCallback = "SetImage";

	// Use this for initialization
	void Start () {
        equipment = GameObject.FindGameObjectWithTag("Player").GetComponent<NPC>().gear;
        BroadcastMessage(equipmentImageCallback);
    }

    public void getSlotItem(string slot)
    {
        equipment.GetComponent<NPC>().hand = equipment.EquipItem(equipment.GetComponent<NPC>().hand, slot);
    	BroadcastMessage(equipmentImageCallback);
    }
}
