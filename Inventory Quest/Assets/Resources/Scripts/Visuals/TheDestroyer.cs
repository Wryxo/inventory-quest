using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TheDestroyer : MonoBehaviour {

    public GameObject[] Grounds;
    public Sprite[] StatsSprites;

    public CheatingBastard cheatbast;

    private string[] stats = new string[] { HelpFunctions.Attract, HelpFunctions.Jump, HelpFunctions.Run, HelpFunctions.Swim };
    private int[] difficulty = new int[4];
    private int freq;
    private int counter;
    private int itemsID = 10;

    private ArrayList passchance;

    // Use this for initialization
    void Start () {
        freq = counter = GameMaster.instance.Frequency;
        passchance = new ArrayList() { new Vector2(0, 0), new Vector2(4, .1f), new Vector2(5, .2f), new Vector2(10, 1) };
        cheatbast = GetComponentInChildren<CheatingBastard>();
        for (int i = 0; i < 8; i++)
        {
            GameObject tmp = Grounds[Random.Range(0, Grounds.Length)];
            Sprite sprite = tmp.GetComponent<SpriteRenderer>().sprite;
            var ground = Instantiate(tmp, new Vector3(transform.position.x + (i * sprite.rect.width / 100.0f), transform.position.y, transform.position.z), Quaternion.identity) as GameObject;
            if (i == (8 - freq))
            {
                var stat = PrepareObstacle(ground);
                PrepareReward(ground, stat);
            }
        }
    }
	
	// Update is called once per frame
	void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Ground")
        {
            SpawnGround();
        }
        Destroy(other.gameObject);

	}

    private void SpawnGround()
    {
        GameObject tmp = Grounds[Random.Range(0, Grounds.Length)];
        Sprite sprite = tmp.GetComponent<SpriteRenderer>().sprite;
        var ground = Instantiate(tmp, new Vector3(transform.position.x  + (8 * sprite.rect.width / 100.0f), transform.position.y, transform.position.z), Quaternion.identity) as GameObject;
        counter++;
        if (counter >= freq) {
            var stat = PrepareObstacle(ground);
            PrepareReward(ground, stat);
            counter = 0;
        }
    }

    private int PrepareObstacle(GameObject go)
    {
        var obs = go.transform.FindChild("obstacle").GetComponent<Obstacle>();
        obs.statChecks = new List<StatCheck>();
        int stat = Random.Range(0, stats.Length);
        difficulty[stat]++;
        passchance[3] = new Vector2(cheatbast.character.skills.LevelOf(stats[stat]),1);
        var check = HelpFunctions.FitStatCheck(stats[stat], passchance);
        int diff = check.baseDifficulty + check.sidesPerDie;
        obs.statChecks.Add(check);
        var text = go.transform.FindChild("text").GetComponent<TextMesh>();
        text.text = diff.ToString();
        var s = go.transform.FindChild("obstacle").GetComponent<SpriteRenderer>();
        s.sprite = StatsSprites[stat];
        return stat;
    }

    private void PrepareReward(GameObject go, int stat)
    {
        int choice = Random.Range(0, 8);
        Item res = new Item();
        var obs = go.transform.FindChild("obstacle").GetComponent<Obstacle>();
        switch (choice) {
            case 0:
                res = Item.Celenka(Random.Range(7 * (difficulty[stat] - 1), 13 * (difficulty[stat] + 1)));
                break;
            case 1:
                res = Item.Ciapka(Random.Range(7 * (difficulty[stat] - 1), 13 * (difficulty[stat] + 1)));
                break;
            case 2:
                res = Item.Pruziny(Random.Range(7 * (difficulty[stat] - 1), 13 * (difficulty[stat] + 1)));
                break;
            case 3:
                res = Item.Topanka(Random.Range(7 * (difficulty[stat] - 1), 13 * (difficulty[stat] + 1)));
                break;
            case 4:
                res = Item.Topanky(Random.Range(7 * (difficulty[stat] - 1), 13 * (difficulty[stat] + 1)));
                break;
            case 5:
                res = Item.Trysky(Random.Range(7 * (difficulty[stat] - 1), 13 * (difficulty[stat] + 1)));
                break;
            case 6:
                res = Item.Vesta(Random.Range(7 * (difficulty[stat] - 1), 13 * (difficulty[stat] + 1)));
                break;
            default:
                res = Item.Nasada(Random.Range(7 * (difficulty[stat] - 1), 13 * (difficulty[stat] + 1)));
                break;
        }
        for (int i = 0; i < 4; i++)
        {
            int chance = Random.Range(0, 10);
            if (chance < 6)
            {
                if (!res.stats.contents.Contains(stats[i]))
                {
                    res.AddStat(stats[i], new Skill(Random.Range(-7 * (difficulty[i] / 2), 7 * (difficulty[i] / 3))));
                }
            }
        }
        cheatbast.EquipItem(res);
        obs.reward = res;
    }
}

