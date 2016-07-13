using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class LootBoxSlot : MonoBehaviour {

    private Inventory lootbox;

    void Awake()
    {
        lootbox = GameObject.FindGameObjectWithTag("Player").GetComponent<NPC>().lootbox;
    }

    void SetImage()
    {
        int pos = Convert.ToInt32(name);
        int x = pos / 10;
        int y = pos % 10;
        Image tmp = GetComponent<Image>();
        var tmp2 = lootbox.ItemAt(x, y);
        Text tmp3 = GetComponentInChildren<Text>();
        if (tmp2 != null)
        {
            tmp.sprite = tmp2.imgs[lootbox.SpriteAt(x, y)];
            tmp.color = new Color(1, 1, 1, 1);
        }
        else
        {
            tmp.sprite = null;
            tmp.color = new Color(1, 1, 1, 0);
            tmp3.text = "";
            return;
        }
        var tmpp = lootbox.ItemAt(x + 1, y);
        var tmpp2 = lootbox.ItemAt(x, y + 1);
        var last = true;
        if (tmpp != null && tmpp.id == tmp2.id && lootbox.SpriteAt(x, y) < lootbox.SpriteAt(x + 1, y)) last = false;
        if (tmpp2 != null && tmpp2.id == tmp2.id && lootbox.SpriteAt(x, y) < lootbox.SpriteAt(x, y + 1)) last = false;
        if (tmp2.stack < 2 || !last)
        {
            tmp3.text = "";
        }
        else
        {
            tmp3.text = tmp2.stack.ToString();
        }
    }
}
