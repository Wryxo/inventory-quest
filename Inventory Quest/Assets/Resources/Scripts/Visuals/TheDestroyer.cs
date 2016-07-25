using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TheDestroyer : MonoBehaviour {

    public GameObject[] Grounds;
    public Sprite[] StatsSprites;

    private string[] stats = new string[] { HelpFunctions.Attract, HelpFunctions.Jump, HelpFunctions.Run, HelpFunctions.Swim };
    private int[] difficulty = new int[4];
    private int freq = 3;
    private int counter = 3;
    private int itemsID = 10;

    // Use this for initialization
    void Start () {
        for (int i = 0; i < 8; i++)
        {
            GameObject tmp = Grounds[Random.Range(0, Grounds.Length)];
            Sprite sprite = tmp.GetComponent<SpriteRenderer>().sprite;
            Instantiate(tmp, new Vector3(transform.position.x + (i * sprite.texture.width / 100.0f), transform.position.y, transform.position.z), Quaternion.identity);
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
        var ground = Instantiate(tmp, new Vector3(transform.position.x  + (8.0f * sprite.texture.width / 100.0f), transform.position.y, transform.position.z), Quaternion.identity) as GameObject;
        counter++;
        if (counter >= freq) {
            PrepareObstacle(ground);
            PrepareReward(ground);
            counter = 0;
        }
    }

    private void PrepareObstacle(GameObject go)
    {
        var obs = go.transform.FindChild("obstacle").GetComponent<Obstacle>();
        obs.statChecks = new List<StatCheck>();
        int stat = Random.Range(0, stats.Length);
        difficulty[stat]++;
        int diff = Random.Range(7 * difficulty[stat], 13 * difficulty[stat]);
        obs.statChecks.Add(new StatCheck() { statName = stats[stat], nDice = 1, sidesPerDie = 6, baseDifficulty = diff });
        var text = go.transform.FindChild("text").GetComponent<TextMesh>();
        text.text = diff.ToString();
        var s = go.transform.FindChild("obstacle").GetComponent<SpriteRenderer>();
        s.sprite = StatsSprites[stat];
    }

    private void PrepareReward(GameObject go)
    {
        int choice = Random.Range(0, 8);
        Item res = new Item();
        var obs = go.transform.FindChild("obstacle").GetComponent<Obstacle>();
        switch (choice) {
            case 0:
                res = Item.Celenka(Random.Range(7 * (difficulty[0] - 1), 13 * (difficulty[0] + 1)));
                break;
            case 1:
                res = Item.Ciapka(Random.Range(7 * (difficulty[0] - 1), 13 * (difficulty[0] + 1)));
                break;
            case 2:
                res = Item.Pruziny(Random.Range(7 * (difficulty[1] - 1), 13 * (difficulty[1] + 1)));
                break;
            case 3:
                res = Item.Topanka(Random.Range(7 * (difficulty[2] - 1), 13 * (difficulty[2] + 1)));
                break;
            case 4:
                res = Item.Topanky(Random.Range(7 * (difficulty[2] - 1), 13 * (difficulty[2] + 1)));
                break;
            case 5:
                res = Item.Trysky(Random.Range(7 * (difficulty[1] - 1), 13 * (difficulty[1] + 1)));
                break;
            case 6:
                res = Item.Trysky(Random.Range(7 * (difficulty[3] - 1), 13 * (difficulty[3] + 1)));
                break;
            default:
                res = Item.Nasada(Random.Range(7 * difficulty[Random.Range(0, 4)], 13 * difficulty[Random.Range(0, 4)]));
                break;
        }
        for (int i = 0; i < 4; i++)
        {
            int chance = Random.Range(0, 10);
            if (chance < 8)
            {
                if (!res.stats.contents.Contains(stats[i]))
                {
                    res.AddStat(stats[i], new Skill(Random.Range(-7 * (difficulty[i] / 2), 7 * (difficulty[i] / 3))));
                }
            }
        }
        obs.reward = res;
    }
}

