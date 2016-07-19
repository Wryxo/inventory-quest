﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class InventorySlot : MonoBehaviour {


    void Awake()
    {
        NPC.instance.inventory.Event_onInventoryChange += SetImage;
    }

	void SetImage()
    {
        int pos = Convert.ToInt32(name);
        int x = pos / 10;
        int y = pos % 10;
        Image tmp = GetComponent<Image>();
        var tmp2 = NPC.instance.inventory.ItemAt(x, y);
        Text tmp3 = GetComponentInChildren<Text>();
        if (tmp2 != null)
        {
            tmp.sprite = tmp2.imgs[NPC.instance.inventory.SpriteAt(x, y)];
            tmp.color = new Color(1, 1, 1, 1);
        }
        else
        {
            tmp.sprite = null;
            tmp.color = new Color(1, 1, 1, 0);
            tmp3.text = "";
            return;
        }
        var tmpp = NPC.instance.inventory.ItemAt(x+1, y);
        var tmpp2 = NPC.instance.inventory.ItemAt(x, y+1);
        var last = true;
        if (tmpp != null && tmpp.id == tmp2.id && NPC.instance.inventory.SpriteAt(x, y) < NPC.instance.inventory.SpriteAt(x + 1, y)) last = false;
        if (tmpp2 != null && tmpp2.id == tmp2.id && NPC.instance.inventory.SpriteAt(x, y) < NPC.instance.inventory.SpriteAt(x, y + 1)) last = false;
        if (tmp2.stack < 2 || !last)
        {
            tmp3.text = "";
        } else
        {
            tmp3.text = tmp2.stack.ToString();
        }
    }
}
