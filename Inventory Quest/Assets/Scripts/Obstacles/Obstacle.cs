using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

    public StatCheck[] statChecks;
    public KeyCheck[] keyChecks;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public bool Check(NPC guy)
    {
        foreach(StatCheck x in statChecks)
        {
            Debug.Log("Attempting " + x.statName + " check");
            if (!x.Check(guy.skills)) return false;
            Debug.Log("Passed " + x.statName + " check");
        }
        Debug.Log("Passed skill checks");
        foreach (KeyCheck x in keyChecks)
        {
            if (!x.Check(guy.inventory)) return false;
        }
        return true;
    }
}
