using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Obstacle : MonoBehaviour{

    public List<StatCheck> statChecks;
    public List<KeyCheck> keyChecks;
    public Item reward;

    public void Start()
    {
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && (statChecks != null || keyChecks != null)) { 
            if (Check(NPC.instance))
            {
                GameMaster.instance.goodScore++;
            } else
            {
                GameMaster.instance.badScore++;
            }
            NPC.instance.lootbox.EmptyInventory();
            NPC.instance.lootbox.InsertItem(reward, 0, 0);
            Destroy(transform.parent.FindChild("text").gameObject);
            Destroy(gameObject);
        }
    }

    public bool Check(NPC guy)
    {
        if (statChecks != null) {
            foreach (StatCheck x in statChecks)
            {
                if (!x.Check(guy.skills)) return false;
            }
        }
        if(keyChecks != null) { 
            foreach (KeyCheck x in keyChecks)
            {
                if (!x.Check(guy)) return false;
            }
        }
        return true;
    }
}
