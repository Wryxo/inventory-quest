using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour {

    public Stats skills;
    public Inventory inventory;
    public Inventory lootbox;
    public Equipment gear;
    public Animator animator;
    public Item hand
    {
        get { return _hand; }
        set {
            _hand = value;
            if (_hand != null)
            {
                Texture2D x = HelpFunctions.spriteToTexture(_hand.img);
                Cursor.SetCursor(x, new Vector2(x.width/2, x.height/3), CursorMode.Auto);
            }
            else
            {
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            }
        }
    }
    private Item _hand;

    public ArrayList statusEffects;

    public static NPC instance;

    public delegate void del_onStatsChange();
    public del_onStatsChange Event_onStatsChange;

    public delegate void del_onItemHover(Item item);
    public del_onItemHover Event_onItemHover;

    public delegate void del_onItemExit();
    public del_onItemExit Event_onItemExit;

    float runDuration;
    bool running;
    bool jumping = false;
    float jumpDuration;

    void Awake()
    {
        if (instance == null && gameObject.tag == "Player")
        {
            instance = this;
        }
        skills = new Stats();
        skills.Add("Attractivity", 10);
        skills.Add("Jump", 10);
        skills.Add("Swim", 10);
        skills.Add("Branches", 10);
        skills.Add("Run", 10);
    }

    // Use this for initialization
    void Start () {
        if (Event_onStatsChange != null)
        {
            Event_onStatsChange();
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            GameMaster.instance.ShowGameMenu(false);
        }
        if (Input.GetButtonDown("Kill"))
        {
            GameMaster.instance.StartDefeat();
        }
    }

    void FixedUpdate()
    {
        if (transform.position.x > -15 && transform.position.y < 4.2f)
        {
            transform.position = new Vector3(transform.position.x - (Time.deltaTime * 3), transform.position.y, transform.position.z);
        }
        if (jumping)
        {
            jumpDuration -= Time.deltaTime;
        }
        if (jumping && jumpDuration < 0)
        {
            animator.SetBool("Jump", false);
            jumping = false;
        }
        if (running)
        {
            runDuration -= Time.deltaTime;
        }
        if (running && runDuration < 0)
        {
            running = false;
            GameMaster.instance.Speed /= 1.5f;
            SetRun(0);
        }
    }

    public void AddStatusEffect(StatusEffect e)
    {
        e.owner = this;
        e.index = statusEffects.Count;
        statusEffects.Add(e);
        skills.Add(e.stats);
        e.Start();
    }

    public void ExpireStatusEffectById(int id)
    {
        for(int i = 0; i < statusEffects.Count; i++)
        {
            if (((StatusEffect)statusEffects[i]).id == id) ((StatusEffect)statusEffects[i]).expire();
        }
    }

    public Item EquipItem(Item item, object slot = null)
    {
        var r = gear.EquipItem(item, slot);
        if (r != null)
        {
            skills.Subtract(r.stats);
        }
        if (item != null)
        {
            skills.Add(item.stats);
        }
        if (Event_onStatsChange != null)
        {
            Event_onStatsChange();
        }
        return r;
    }

    public Item UnequipItem(object slot)
    {
        return EquipItem(null, slot);
    }

    public int CountItem(int id)//Checks inventory and equipment for item
    {
        return gear.CountItem(id)+inventory.CountItemsWithId(id);
    }

    public int SpendItem(int id,int amount) //Tries to spend the prescribed amount of items with id if possible, returns debt, first checks inventory, then equipment
    {
        var debt = amount;
        debt = inventory.SpendItem(id, debt);
        debt = gear.SpendItem(id, debt);
        return debt;
    }

    public int GetEquippedDifference(Item item, object stat)
    {
        if (item == null) return 0;
        foreach (DictionaryEntry de in item.compatibleSlots)
        {
            var r = gear.ItemAt(de.Key);
            if (r == null)
            {
                return item.stats.LevelOf(stat);
            } else
            {
                return item.stats.LevelOf(stat) - r.stats.LevelOf(stat);
            }
        }
        return 0;
    }

    public void OnInventoryItemHover(int position)
    {
        var item = inventory.ItemAt(position / 10, position % 10);
        if (hand != null)
        {
            if (Event_onItemHover != null)
            {
                Event_onItemHover(hand);
            }
        }
        else if (ItemUI.itemBeingDragged != null)
        {
            if (Event_onItemHover != null)
            {
                Event_onItemHover(ItemUI.itemBeingDragged.GetComponent<ItemUI>().item);
            }
        }
        else 
        {
            if (item != null && Event_onItemHover != null)
            {
                Event_onItemHover(item);
            }
        }
    }

    public void OnLootBoxItemHover(int position)
    {
        var item = lootbox.ItemAt(position / 10, position % 10);
        if (hand != null)
        {
            if (Event_onItemHover != null)
            {
                Event_onItemHover(hand);
            }
        }
        else if (ItemUI.itemBeingDragged != null)
        {
            if (Event_onItemHover != null)
            {
                Event_onItemHover(ItemUI.itemBeingDragged.GetComponent<ItemUI>().item);
            }
        }
        else
        {
            if (item != null && Event_onItemHover != null)
            {
                Event_onItemHover(item);
            }
        }
    }

    public void OnEquippedItemHover(string position)
    {
        var item = gear.ItemAt(position);
        if (hand != null)
        {
            if (Event_onItemHover != null)
            {
                Event_onItemHover(hand);
            }
        }
        else if (ItemUI.itemBeingDragged != null)
        {
            if (Event_onItemHover != null)
            {
                Event_onItemHover(ItemUI.itemBeingDragged.GetComponent<ItemUI>().item);
            }
        }
        else
        {
            if (item != null && Event_onItemHover != null)
            {
                Event_onItemHover(item);
            }
        }
    }

    public void OnEquipClick()
    {
        if (hand != null)
        {
            if (Event_onItemHover != null)
            {
                Event_onItemHover(hand);
            }
        }
        else if (ItemUI.itemBeingDragged != null)
        {
            if (Event_onItemHover != null)
            {
                Event_onItemHover(ItemUI.itemBeingDragged.GetComponent<ItemUI>().item);
            }
        }
        else
        {
            if (Event_onItemExit != null)
            {
                Event_onItemExit();
            }
        }
    }

    public void OnItemExit()
    {
        if (hand == null && Event_onItemExit != null && ItemUI.itemBeingDragged == null) { 
            Event_onItemExit();
        }
    }

    public void Jump(float x, float y)
    {
        //GetComponent<Rigidbody2D>().AddForce(new Vector2(0,5),ForceMode2D.Impulse);
        jumping = true;
        animator.SetBool("Jump", true);
        jumpDuration = 3.0f;
    }

    public void SetRun(float duration)
    {
        if (duration > 0) { 
            runDuration = duration;
            running = true;
        }
        if (GameMaster.instance.Speed <= -5.5f)
        {
            transform.localScale = new Vector3(1.3f, 1.3f);
            animator.SetBool("Run", true);
        }
        if (GameMaster.instance.Speed > -5.5f)
        {
            animator.SetBool("Run", false);
            transform.localScale = new Vector3(2.0f, 2.0f);
        }
    }

    public void SetVictory()
    {
        animator.SetBool("Victory", true);
        transform.localScale = new Vector3(1.3f, 1.3f);
        GetComponent<BoxCollider2D>().offset = new Vector2(-2, -2);
        transform.position = new Vector3(transform.position.x - 1f, transform.position.y + 1);
    }

    public void SetDefeat()
    {
        animator.SetBool("Defeat", true);
        transform.localScale = new Vector3(1.3f, 1.3f);
        GetComponent<BoxCollider2D>().offset = new Vector2(-2, -2);
        //transform.position = new Vector3(transform.position.x, transform.position.y + 2);
    }
}
