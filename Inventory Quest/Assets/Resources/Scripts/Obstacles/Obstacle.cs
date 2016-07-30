using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Obstacle : MonoBehaviour{

    public List<StatCheck> statChecks;
    public List<KeyCheck> keyChecks;
    public Item reward;
    public RunObstacle runObstacle;
    public ParticleSystem ps;

    public void Start()
    {
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && (statChecks != null || keyChecks != null)) { 
            Dictionary<string, object> props = new Dictionary<string, object>();
            props.Add("p_" + HelpFunctions.Attract, ((Skill)NPC.instance.skills.contents[HelpFunctions.Attract]).level);
            props.Add("p_" + HelpFunctions.Branches, ((Skill)NPC.instance.skills.contents[HelpFunctions.Branches]).level);
            props.Add("p_" + HelpFunctions.Jump, ((Skill)NPC.instance.skills.contents[HelpFunctions.Jump]).level);
            props.Add("p_" + HelpFunctions.Run, ((Skill)NPC.instance.skills.contents[HelpFunctions.Run]).level);
            props.Add("p_" + HelpFunctions.Swim, ((Skill)NPC.instance.skills.contents[HelpFunctions.Swim]).level);
            if (statChecks != null)
            {
                foreach (StatCheck x in statChecks)
                {
                    props.Add("o_" + x.statName, x.baseDifficulty);
                }
            }
            if (Check(NPC.instance))
            {
                GameMaster.instance.goodScore++;
                props.Add("result", true);
                foreach (StatCheck x in statChecks)
                {
                    if (x.statName == HelpFunctions.Jump)
                    {
                        NPC.instance.Jump();
                    }
                    if (x.statName == HelpFunctions.Attract)
                    {
                        Attract();
                    }
                    if (x.statName == HelpFunctions.Run)
                    {
                        Invoke("Run", 1.0f);
                    }
                }
            } else
            {
                GameMaster.instance.badScore++;
                props.Add("result", false);
            }
            NPC.instance.lootbox.EmptyInventory();
            NPC.instance.lootbox.InsertItem(reward, 0, 0);
            NPC.instance.lootbox.InsertItem(Item.Vetvicky(((Skill)NPC.instance.skills.contents[HelpFunctions.Branches]).level), 2, 0);
            GameMaster.instance.Track("obstacle", props);
            Destroy(transform.parent.FindChild("text").gameObject);
            Destroy(gameObject);
        }
    }

    void Run()
    {
        Debug.Log("run forest run");
        if (runObstacle != null)
        {
            runObstacle.Run = true;
        }
    }

    void Attract()
    {
        if (ps != null) { 
            ps.Play();
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
