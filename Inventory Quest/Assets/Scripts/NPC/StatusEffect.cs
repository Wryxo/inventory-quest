using UnityEngine;
using System.Collections;

public class StatusEffect {

    public Stats stats;
    public float duration;
    public float timeleft;
    public int id;
    

    bool remove;
    public NPC owner;
    public int index;

    public delegate void OnExpire();
    
	// Use this for initialization
	public void Start () {
        timeleft = duration;
        remove = false;
	}
	
	// Update is called once per frame
	public void Update (float dt) {
        if (duration > 0)
        {
            timeleft -= dt;
            if (timeleft <= 0) expire();
        }
    }

    public void expire()
    {
        if (!remove)
        {
            remove = true;
            owner.statusEffects[index] = owner.statusEffects[owner.statusEffects.Count - 1];
            ((StatusEffect)owner.statusEffects[index]).index = index;
            owner.skills.Subtract(stats);
        }
    }

}
