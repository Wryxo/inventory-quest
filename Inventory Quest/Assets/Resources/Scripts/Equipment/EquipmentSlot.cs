using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class EquipmentSlot : MonoBehaviour, IDropHandler
{
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
        NPC.instance.gear.Event_onEquipmentChange += SetItem;
    }

    void OnDestroy()
    {
        NPC.instance.gear.Event_onEquipmentChange -= SetItem;
    }

    void SetImage()
    {
        Image tmp = GetComponent<Image>();
        var tmp2 = NPC.instance.gear.ItemAt(name);
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

    void SetItem()
    {
        var tmp = NPC.instance.gear.ItemAt(name);
        foreach (Transform go in transform)
        {
            Destroy(go.gameObject);
        }
        if (tmp != null)
        {

            GetComponent<Image>().color = new Color(1, 1, 1, 0);
            var go = Instantiate(itemPrefab, transform.position, Quaternion.identity) as GameObject;
            go.transform.SetParent(transform);
        } else {
            GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
    }
}
