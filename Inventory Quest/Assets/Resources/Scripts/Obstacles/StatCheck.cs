using UnityEngine;
using System.Collections;

public class StatCheck {

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
        }
        if (statLevel >= baseDifficulty + sum) return true;
        return false;
    }

    public bool Check(Stats stats)
    {
        if (stats == null) return Roll(0);
        return Roll(stats.LevelOf(statName));
    } 

}
