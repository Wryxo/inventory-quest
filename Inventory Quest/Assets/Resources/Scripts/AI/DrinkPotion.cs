using UnityEngine;
using System.Collections;

public class DrinkPotion : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /*
     * Possible options:
     * Simplex method
     * 
     * */

    void PrepareForObstacle(Obstacle o,int smartness = -1)
    {

    }

    void ReachStats(Stats stats,int smartness = -1)//TODO: From relevant consumable items, select enough for each stat if possible
    {
        var inv = GetComponent<NPC>();
    }

    ArrayList GetRelevantConsumableItems()
    {
        return new ArrayList();
    }
}
