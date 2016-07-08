using UnityEngine;
using System.Collections;

public class Obstacle {

    public StatCheck[] statChecks;
    public KeyCheck[] keyChecks;

    public bool Check(NPC guy)
    {
        foreach(StatCheck x in statChecks)
        {
            if (!x.Check(guy.skills)) return false;
        }
        foreach (KeyCheck x in keyChecks)
        {
            if (!x.Check(guy.inventory)) return false;
        }
        return true;
    }
}
