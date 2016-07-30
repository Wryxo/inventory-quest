using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class InventorySlot : MonoBehaviour, IDropHandler {
    public GameObject item
    {
        get
        {
            if (transform.childCount > 0)
            {
                return transform.GetChild(0).gameObject;
            }
            return null;
        }
    }

    public GameObject itemPrefab;

    public void OnDrop(PointerEventData eventData)
    {
        ItemUI.itemBeingDragged.transform.SetParent(transform);
    }

    void Awake()
    {
        NPC.instance.inventory.Event_onInventoryChange += SetItem;
    }

    void OnDestroy()
    {
        NPC.instance.inventory.Event_onInventoryChange -= SetItem;
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

    void SetItem()
    {
        int pos = Convert.ToInt32(name);
        int x = pos / 10;
        int y = pos % 10;
        var tmp = NPC.instance.inventory.ItemAt(x, y);
        foreach(Transform go in transform)
        {
            Destroy(go.gameObject);
        }
        if (tmp != null)
        {
            var go = Instantiate(itemPrefab, transform.position, Quaternion.identity) as GameObject;
            go.transform.SetParent(transform);
        }
    }
}
