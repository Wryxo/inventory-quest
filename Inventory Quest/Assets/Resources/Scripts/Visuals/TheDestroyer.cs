using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class TheDestroyer : MonoBehaviour {

    public GameObject[] Grounds;
    public GameObject FinishGround;
    public Sprite[] StatsSprites;

    public CheatingBastard cheatbast;

    public LevelBuilder lb;

    private string[] stats = new string[] { HelpFunctions.Attract, HelpFunctions.Jump, HelpFunctions.Run, HelpFunctions.Swim };
    private int[] difficulty = new int[4];
    private int freq;
    private int counter;
    private bool finishSpawned = false;
    private Func<int, Item>[] itemFunkcie =
    {
        x => { return Item.Celenka(x); },
        x => { return Item.Ciapka(x); },
        x => { return Item.Nasada(x); },
        x => { return Item.Pruziny(x); },
        x => { return Item.Topanka(x); },
        x => { return Item.Topanky(x); },
        x => { return Item.Trysky(x); },
        x => { return Item.Vesta(x); },
        x => { return Item.Delfin(x); },
        x => { return Item.DelfinBota(x); },
        x => { return Item.DelfinBoty(x); },
        x => { return Item.Kridla(x); },
        x => { return Item.Magnet(x); },
        x => { return Item.Neopren(x); },
        x => { return Item.Plutva(x); },
        x => { return Item.Plutvy(x); },
        x => { return Item.Tryska(x); },
        x => { return Item.Trysky2(x); }
    };

    private ArrayList passchance;

    // Use this for initialization
    void Start () {
        freq = counter = GameMaster.instance.Frequency;
        passchance = new ArrayList() { new Vector2(0, 0), new Vector2(8, .1f), new Vector2(9, .2f), new Vector2(10, 1) };
        cheatbast = GetComponentInChildren<CheatingBastard>();
        lb = new LevelBuilder();
        lb.obstacles = MarkovChain.Clique(new ArrayList(){0,1,2,3});
        lb.items = MarkovChain.Clique(new ArrayList() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 });
        for (int i = 0; i < 8; i++)
        {
            int stat = (int)lb.obstacles.Random();
            GameObject tmp = Grounds[UnityEngine.Random.Range(0, 4)];
            if (stat == 1 && i == (8 - freq))
            {
                tmp = Grounds[4];
            }
            if (stat == 3 && i == (8 - freq))
            {
                tmp = Grounds[5];
            }
            if (stat == 0 && i == (8 - freq))
            {
                tmp = Grounds[6 + UnityEngine.Random.Range(0, 4)];
            }
            if (stat == 2 && i == (8 - freq))
            {
                tmp = Grounds[10];
            }
            Sprite sprite = tmp.GetComponent<SpriteRenderer>().sprite;
            var ground = Instantiate(tmp, new Vector3(transform.position.x + (i * sprite.rect.width / 100.0f), transform.position.y, transform.position.z), Quaternion.identity) as GameObject;
            if (i == (8 - freq))
            {
                PrepareObstacle(ground, stat);
                PrepareReward(ground, stat);
            }
        }
    }
	
	// Update is called once per frame
	void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Ground")
        {
            if (GameMaster.instance.Overtime && !finishSpawned)
            {
                finishSpawned = true;
                SpawnFinish();
            } else
            {
                SpawnGround();
            }
        }
        Destroy(other.gameObject);

	}

    private void SpawnFinish()
    {
        Sprite sprite = FinishGround.GetComponent<SpriteRenderer>().sprite;
        var ground = Instantiate(FinishGround, new Vector3(transform.position.x  + (7.9f * sprite.rect.width / 100.0f), transform.position.y, transform.position.z), Quaternion.identity) as GameObject;
    }

    private void SpawnGround()
    {
        int stat = (int)lb.obstacles.Random();
        GameObject tmp = Grounds[UnityEngine.Random.Range(0, 4)];
        counter++;
        if (!GameMaster.instance.Overtime) { 
            if (stat == 1 && counter >= freq)
            {
                tmp = Grounds[4];
            }
            if (stat == 3 && counter >= freq)
            {
                tmp = Grounds[5];
            }
            if (stat == 0 && counter >= freq)
            {
                tmp = Grounds[6 + UnityEngine.Random.Range(0, 4)];
            }
            if (stat == 2 && counter >= freq)
            {
                tmp = Grounds[10];
            }
        }
        Sprite sprite = tmp.GetComponent<SpriteRenderer>().sprite;
        var ground = Instantiate(tmp, new Vector3(transform.position.x  + (7.9f * sprite.rect.width / 100.0f), transform.position.y, transform.position.z), Quaternion.identity) as GameObject;
        if (counter >= freq && !GameMaster.instance.Overtime) {
            PrepareObstacle(ground, stat);
            PrepareReward(ground, stat);
            counter = 0;
        }
    }

    private void PrepareObstacle(GameObject go, int stat)
    {
        var obs = go.transform.FindChild("obstacle").GetComponent<Obstacle>();
        obs.statChecks = new List<StatCheck>();
        difficulty[stat]++;
        passchance[3] = new Vector2(cheatbast.character.skills.LevelOf(stats[stat]),1);
        var check = HelpFunctions.FitStatCheck(stats[stat], passchance);
        int diff = check.baseDifficulty + check.sidesPerDie;
        obs.statChecks.Add(check);
        var text = go.transform.FindChild("text").GetComponent<TextMesh>();
        text.text = diff.ToString();
        var s = go.transform.FindChild("obstacle").GetComponent<SpriteRenderer>();
        s.sprite = StatsSprites[stat];
    }

    private void PrepareReward(GameObject go, int stat)
    {
        int choice = (int)lb.items.Random();
        Item res = new Item();
        var obs = go.transform.FindChild("obstacle").GetComponent<Obstacle>();
        res = itemFunkcie[choice].Invoke(UnityEngine.Random.Range(7 * (difficulty[stat] - 1), 13 * (difficulty[stat] + 1)));
        for (int i = 0; i < 4; i++)
        {
            int chance = UnityEngine.Random.Range(0, 10);
            if (chance < 5)
            {
                if (!res.stats.contents.Contains(stats[i]))
                {
                    res.AddStat(stats[i], new Skill(UnityEngine.Random.Range(-7 * (difficulty[i] / 2), 7 * (difficulty[i] / 3))));
                }
            }
        }
        res.AddStat(HelpFunctions.Branches, new Skill(UnityEngine.Random.Range(-10,10)));
        cheatbast.EquipItem(res);
        obs.reward = res;
    }
}

