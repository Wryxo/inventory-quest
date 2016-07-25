using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {

    NPC fakePlayer;
    NPC killerGM;

    public NPC player;

    public float evilness;
    public int intelligence;
    public float speed;

    CategoricDistribution events;
    ArrayList passChance;

    public GameObject BaseObstacle;

    int MAX_DICE = 1;


	// Use this for initialization
	void Start () {
        fakePlayer = Instantiate(player);
        killerGM = Instantiate(player);

        passChance = new ArrayList();
        passChance.Add(null);
        passChance.Add(null);
        passChance.Add(null);

        events = new CategoricDistribution();
        var bottomlessPit = Instantiate(BaseObstacle);
        Obstacle o = bottomlessPit.GetComponent<Obstacle>();
        o.statChecks = new StatCheck[1] { new StatCheck() { statName = "Jump", sidesPerDie = -1 } };
        events.AddCategory(bottomlessPit);
        var water = Instantiate(BaseObstacle);
        o = water.GetComponent<Obstacle>();
        o.statChecks = new StatCheck[1] { new StatCheck() { statName = "Swim", sidesPerDie = -1 } };
        events.AddCategory(water);
        createObstacle();
    }

    StatCheck FitStatCheck(string statName, ArrayList f) //May be replaced later on, it's really dumb
    {
        var x = new StatCheck();
        x.statName = statName;
        x.nDice = 1;
        float min = (float)((DictionaryEntry)f[0]).Value;
        float max = (float)((DictionaryEntry)f[f.Count - 1]).Value;
        float range = max - min;
        float mean = 0;
        float pquad = 0;
        float quad = 0;
        float pmean;
        for (int i = 1; i < f.Count-1; i++)
        {
            mean += (float)((DictionaryEntry)f[i]).Value * (float)((DictionaryEntry)f[i]).Key;
            quad += (float)((DictionaryEntry)f[i]).Value;
            pquad += (float)((DictionaryEntry)f[i]).Key;
        }
        if (quad > 0)
        {
            pmean = mean / pquad;
            mean /= quad;
        }
        else
        {
            mean = min + range / 2;
            pmean = .5f;
        }
        mean -= min;
        float a = Random.value;
        float b = Random.value;
        float c = Random.value * mean + Random.value * (range - mean);
        if (a > b) a = b;
        if(c/pmean < (range - c) / (1 - pmean))
        {
            min += c * (1 - a);
            range = c * a / pmean;
            x.baseDifficulty = (int)Mathf.Floor(min);
            x.sidesPerDie = (int)Mathf.Ceil(range);
        } else
        {
            max -= c * (1 - a);
            range = (range - c) * a / (1 - pmean);
            x.baseDifficulty = (int)Mathf.Floor(max - range);
            x.sidesPerDie = (int)Mathf.Ceil(range);
        }
        return x;
    }

    public void createObstacle()
    {
        // vytvor obstacle
        GameObject bo = (GameObject)events.Random();
        Obstacle o = bo.GetComponent<Obstacle>();
        if (o == null) throw new System.Exception("Not implemented yet");
        else
        {
            if(o.statChecks != null)
            {
                Debug.Log("Stat checks: ");
                for(int i=0;i<o.statChecks.Length;i++)
                {
                    StatCheck s = o.statChecks[i];
                    if(s.sidesPerDie < 0) {
                        passChance[0] = new DictionaryEntry(0.0f,0.0f);
                        passChance[1] = new DictionaryEntry((float)fakePlayer.skills.LevelOf(s.statName),1-evilness);
                        passChance[2] = new DictionaryEntry((float)killerGM.skills.LevelOf(s.statName),1f);
                        o.statChecks[i] = FitStatCheck(s.statName, passChance);
                        Debug.Log(o.statChecks[i].baseDifficulty);
                    }
                }
            }
        }
    }
}
