using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class EquipmentSlot : MonoBehaviour {

	private Equipment equipment;

    void Awake()
    {
        equipment = GameObject.FindGameObjectWithTag("Player").GetComponent<NPC>().gear;
    }

    void SetImage()
    {
        Image tmp = GetComponent<Image>();
        var tmp2 = equipment.ItemAt(name);
        Text tmp3 = GetComponentInChildren<Text>();
        if (tmp2 != null)
        {
            tmp.sprite = tmp2.img;
            tmp.color = new Color(1, 1, 1, 1);
        }
        else
        {
            tmp.sprite = null;
            tmp.color = new Color(1, 1, 1, 0);
            tmp3.text = ""; 
            return;
        }
        if (tmp2.stack > 1)
        {
            tmp3.text = tmp2.stack.ToString();
        } else
        {
            tmp3.text = ""; 
        }
    }
}
