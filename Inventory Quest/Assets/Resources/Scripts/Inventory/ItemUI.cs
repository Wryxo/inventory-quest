using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public static GameObject itemBeingDragged;
    public Item item;
    Transform startParent;
    Vector3 startPosition;

    void Start()
    {
        if (transform.parent.parent.name == "PlayerInventory")
        {
            int pos = Convert.ToInt32(transform.parent.name);
            int x = pos / 10;
            int y = pos % 10;
            item = NPC.instance.inventory.ItemAt(x, y);
            GetComponent<Image>().sprite = item.imgs[NPC.instance.inventory.SpriteAt(x, y)];
            var tmpp = NPC.instance.inventory.ItemAt(x + 1, y);
            var tmpp2 = NPC.instance.inventory.ItemAt(x, y + 1);
            var last = true;
            if (tmpp != null && tmpp.id == item.id && NPC.instance.inventory.SpriteAt(x, y) < NPC.instance.inventory.SpriteAt(x + 1, y)) last = false;
            if (tmpp2 != null && tmpp2.id == item.id && NPC.instance.inventory.SpriteAt(x, y) < NPC.instance.inventory.SpriteAt(x, y + 1)) last = false;
            if (item.stack < 2 || !last)
            {
                GetComponentInChildren<Text>().text = "";
            }
            else
            {
                GetComponentInChildren<Text>().text = item.stack.ToString();
            }
        }
        if (transform.parent.parent.name == "LootBox")
        {
            int pos = Convert.ToInt32(transform.parent.name);
            int x = pos / 10;
            int y = pos % 10;
            item = NPC.instance.lootbox.ItemAt(x, y);
            GetComponent<Image>().sprite = item.imgs[NPC.instance.lootbox.SpriteAt(x, y)];
            var tmpp = NPC.instance.lootbox.ItemAt(x + 1, y);
            var tmpp2 = NPC.instance.lootbox.ItemAt(x, y + 1);
            var last = true;
            if (tmpp != null && tmpp.id == item.id && NPC.instance.lootbox.SpriteAt(x, y) < NPC.instance.lootbox.SpriteAt(x + 1, y)) last = false;
            if (tmpp2 != null && tmpp2.id == item.id && NPC.instance.lootbox.SpriteAt(x, y) < NPC.instance.lootbox.SpriteAt(x, y + 1)) last = false;
            if (item.stack < 2 || !last)
            {
                GetComponentInChildren<Text>().text = "";
            }
            else
            {
                GetComponentInChildren<Text>().text = item.stack.ToString();
            }
        }
        if (transform.parent.parent.name == "EquipUI")
        {
            item = NPC.instance.gear.ItemAt(transform.parent.name);
            GetComponent<Image>().sprite = item.img;
        }
        transform.localScale = new Vector3(1, 1);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        itemBeingDragged = gameObject;
        startPosition = transform.position;
        startParent = transform.parent;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        int x = 0;
        int y = 0;
        string slot = null;
        if (transform.parent.parent.name != "EquipUI")
        {
            int pos = Convert.ToInt32(transform.parent.name);
            x = pos / 10;
            y = pos % 10;
        } else
        {
            slot = transform.parent.name;
        }
        transform.SetParent(transform.parent.parent);
        if (transform.parent.name == "PlayerInventory")
        {
            transform.parent.parent.SetAsLastSibling();
            NPC.instance.inventory.RemoveItem(x, y);
        }
        if (transform.parent.name == "LootBox")
        {
            transform.parent.parent.SetAsLastSibling();
            NPC.instance.lootbox.RemoveItem(x, y);
        }
        if (transform.parent.name == "EquipUI")
        {
            transform.parent.SetAsLastSibling();
            NPC.instance.UnequipItem(slot);
        }
        var img = GetComponent<Image>();
        img.sprite = item.img;
        transform.localScale = new Vector3(item.width, item.height);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        Canvas myCanvas = GameObject.Find("UserInterface").GetComponent<Canvas>();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas.transform as RectTransform, Input.mousePosition, myCanvas.worldCamera, out pos);
        transform.position = myCanvas.transform.TransformPoint(pos);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        bool notMoved = false;
        if (transform.parent == startParent.parent)
        {
            notMoved = true;
            transform.SetParent(startParent);
            transform.position = startPosition;
        }
        itemBeingDragged = null;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        transform.localScale = new Vector3(1.0f, 1.0f);
        Item swappedOut = null;
        if (transform.parent.parent.name == "PlayerInventory")
        {
            int pos = Convert.ToInt32(transform.parent.name);
            int x = pos / 10;
            int y = pos % 10;
            int newPos = NPC.instance.inventory.ItemFitHere(item, x, y);
            swappedOut = NPC.instance.inventory.InsertItem(item, newPos / 10, newPos % 10);
        }
        if (transform.parent.parent.name == "LootBox")
        {
            int pos = Convert.ToInt32(transform.parent.name);
            int x = pos / 10;
            int y = pos % 10;
            int newPos = NPC.instance.lootbox.ItemFitHere(item, x, y);
            swappedOut = NPC.instance.lootbox.InsertItem(item, newPos / 10, newPos % 10);
        }
        if (transform.parent.parent.name == "EquipUI")
        {
            swappedOut = NPC.instance.EquipItem(item);
        }
        if (swappedOut != null)
        {
            Item swappedOut2 = null;
            if (startParent.parent.name == "PlayerInventory")
            {
                int oldPos = Convert.ToInt32(startParent.name);
                int oldX = oldPos / 10;
                int oldY = oldPos % 10;
                swappedOut2 = NPC.instance.inventory.InsertItem(swappedOut, oldX, oldY);
            }
            if (startParent.parent.name == "LootBox")
            {
                int oldPos = Convert.ToInt32(startParent.name);
                int oldX = oldPos / 10;
                int oldY = oldPos % 10;
                swappedOut2 = NPC.instance.lootbox.InsertItem(swappedOut, oldX, oldY);
            }
            if (startParent.parent.name == "EquipUI")
            {
                swappedOut2 = NPC.instance.EquipItem(swappedOut);
            }
            if (swappedOut2 != null)
            {
                NPC.instance.hand = swappedOut2;
            }
        }
        if (!notMoved)
        {
            Destroy(transform.gameObject);
        }
    }
}
