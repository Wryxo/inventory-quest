using UnityEngine;
using System.Collections;

public class StatCheck : MonoBehaviour {

    public string statName;
    public int baseDifficulty;
    public int nDice;
    public int sidesPerDie;

    bool Roll(int statLevel)
    {
        if (statLevel < baseDifficulty + nDice) return false;
        if (statLevel >= baseDifficulty + nDice * sidesPerDie) return true;
        int sum = nDice;
        for(int i = 0; i < nDice; i++)
        {
            int r = Mathf.FloorToInt(Random.value * sidesPerDie);
            sum += r;
            Debug.Log("Die number: " + i + ", Rolled: " + (r+1));
        }
        Debug.Log("Sum: " + sum);
        if (statLevel >= baseDifficulty + sum) return true;
        return false;
    }

    public bool Check(Hashtable stats)
    {
        if (stats != null && stats.Contains(statName))
        {
            return Roll(((Skill)stats[statName]).level);
        } else
        {
            return Roll(0);
        }
    } 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
