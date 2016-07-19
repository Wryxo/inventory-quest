using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour{

    public StatCheck[] statChecks;
    public KeyCheck[] keyChecks;

    public void Start()
    {
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
    }

    public bool Check(NPC guy)
    {
        if(statChecks != null)
        foreach(StatCheck x in statChecks)
        {
            if (!x.Check(guy.skills)) return false;
        }
        if(keyChecks != null)
        foreach (KeyCheck x in keyChecks)
        {
            if (!x.Check(guy)) return false;
        }
        return true;
    }
}
