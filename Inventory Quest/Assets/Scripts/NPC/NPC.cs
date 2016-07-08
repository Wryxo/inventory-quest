using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour {

    public Hashtable skills;
    public Inventory inventory;
    public Item hand;

   

    Obstacle debugObstacle; //Remove this later on

	// Use this for initialization
	void Start () {
        debugObstacle = new Obstacle();
        var tmp = new StatCheck[] { new StatCheck() {statName="Strength",baseDifficulty=0,nDice=2,sidesPerDie=2 },new StatCheck() { statName = "Agility", baseDifficulty = -1, nDice = 1, sidesPerDie = 2 } };
        var k = new KeyCheck[] { new KeyCheck() { itemID = 1, amount = 5 }, new KeyCheck() { itemID = 2, amount = 1 } };
        debugObstacle.statChecks = tmp;
        debugObstacle.keyChecks = k;
	}
	
	// Update is called once per frame
	void Update () {
        if(debugObstacle.Check(this)) Debug.Log("Win");
	}

}
