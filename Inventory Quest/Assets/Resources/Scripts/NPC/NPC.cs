using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour {

    public Stats skills;
    public Inventory inventory;
    public Item hand {
        get { return _hand; }
        set {
            if (value != null)
            {
                Cursor.SetCursor((value.img == null ? null : HelpFunctions.spriteToTexture(value.imgs[0])), Vector2.zero, CursorMode.Auto);
            }
            else
            {
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            }
            _hand = value;
        }
    }
    private Item _hand;
    public WornEquipment gear;

    public ArrayList statusEffects;

    Obstacle debugObstacle; //Remove this later on

	// Use this for initialization
	void Start () {
        statusEffects = new ArrayList();
        skills = new Stats();
        gear = new WornEquipment();


        //DEBUG
        skills.Add("swag", new Skill(2));

        gear.AddSlot("Head");

        var h = new Item() {name="Hat of yolo swag", id = 50 };
        h.stats.Add("yolo", 1);
        h.stats.Add("swag", 2);
        h.compatibleSlots.Add("Head", null);
        Debug.Log("Swag level: " + skills.contents["swag"]);
        Debug.Log("Cool items in inventory: "+CountItem(50));
        EquipItem(h);
        Debug.Log("Swag level: " + skills.contents["swag"]);
        Debug.Log("Cool items in inventory: " + CountItem(50));
        UnequipItem("Head");
        Debug.Log("Swag level: " + skills.contents["swag"]);
        Debug.Log("Cool items in inventory: " + CountItem(50));

        debugObstacle = new Obstacle();
        var tmp = new StatCheck[] { new StatCheck() {statName="Strength",baseDifficulty=-0,nDice=2,sidesPerDie=2 },new StatCheck() { statName = "Agility", baseDifficulty = 1, nDice = 0, sidesPerDie = 2 } };
        var k = new KeyCheck[] { new KeyCheck() { itemID = 1, amount = 5 }, new KeyCheck() { itemID = 2, amount = 1 } };
        debugObstacle.statChecks = tmp;
        debugObstacle.keyChecks = k;
        
	}
	
	// Update is called once per frame
	void Update () {

	}

    void FixedUpdate()
    {
        foreach(var x in statusEffects)
        {
            ((StatusEffect)x).Update(Time.fixedDeltaTime);
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
        if(r != null) skills.Subtract(r.stats);
        if(item != null) skills.Add(item.stats);
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

}
