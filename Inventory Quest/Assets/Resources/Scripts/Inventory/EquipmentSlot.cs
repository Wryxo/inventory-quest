using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class EquipmentSlot : MonoBehaviour {

	public WornEquipment equipment;

	void SetImage()
    {
        int pos = Convert.ToInt32(name);
        int x = pos / 10;
        int y = pos % 10;
        
    }
}
