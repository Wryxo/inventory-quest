using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class EquipmentUI : MonoBehaviour {

	public WornEquipment equipment;
    private const string equipmentImageCallback = "SetImage";

	// Use this for initialization
	void Start () {
    	BroadcastMessage(equipmentImageCallback);
	}

	public void getSlotItem(string slot)
    {
    	equipment.EquipItem(equipment.GetComponent<NPC>().hand, slot);
    	BroadcastMessage(equipmentImageCallback);
    }
}
