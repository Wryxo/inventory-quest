using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour {

    public Stats skills;
    public Inventory inventory;
    public Inventory lootbox;
    public Equipment gear;
    public Item hand
    {
        get { return _hand; }
        set {
            _hand = value;
            if (_hand != null)
            {
                Texture2D x = HelpFunctions.spriteToTexture(_hand.img);
                Cursor.SetCursor(x, new Vector2(x.width/2, x.height/3), CursorMode.ForceSoftware);
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

    Obstacle debugObstacle; //Remove this later on

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        var tmp = new Item() { id = 48, width = 1, height = 2, stack = 1, maxStack = 1, img = Resources.Load<Sprite>("Sprites/Items/Gothic_Shield_mouse") as Sprite, imgs = Resources.LoadAll<Sprite>("Sprites/Items/Gothic_Shield") as Sprite[] };
        tmp.stats.Add("Branches", 5);
        tmp.stats.Add("Jump", -5);
        tmp.AddSlot("chest");
        hand = tmp;
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

    public void OnTriggerEnter2D(Collider2D other)
    {
    }

}
