using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Slot : MonoBehaviour {

    public Inventory inventory;

	void SetImage()
    {
        Debug.Log(name);
        int pos = Convert.ToInt32(name);
        int x = pos / 10;
        int y = pos % 10;
        Image tmp = GetComponent<Image>();
        var tmp2 = inventory.ItemAt(x, y);
        if (tmp2 != null)
        {
            tmp.sprite = tmp2.imgs[inventory.SpriteAt(x, y)];
            tmp.color = new Color(1, 1, 1, 1);
        }
        else
        {
            tmp.sprite = null;
            tmp.color = new Color(1, 1, 1, 0);
        }
    }
}
