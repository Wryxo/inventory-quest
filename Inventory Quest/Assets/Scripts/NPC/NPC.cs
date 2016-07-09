using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour {

    public Stats skills;
    public Inventory inventory;
    public Item hand;
    public WornEquipment gear;

    public ArrayList statusEffects;

    Obstacle debugObstacle; //Remove this later on

	// Use this for initialization
	void Start () {
        statusEffects = new ArrayList();
        skills = new Stats();

        //DEBUG
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

}
