using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TheDestroyer : MonoBehaviour {

    public GameObject[] Grounds;
    public Sprite[] Stats;

    private string[] stats = new string[] { "Attractivity", "Jump", "Run", "Swim" };
    private int freq = 3;
    private int counter = 3;

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
            var obs = ground.transform.FindChild("obstacle").GetComponent<Obstacle>();
            obs.statChecks = new List<StatCheck>();
            int diff = Random.Range(8, 15);
            int stat = Random.Range(0, stats.Length);
            obs.statChecks.Add(new StatCheck() { statName = stats[stat], nDice = 0, sidesPerDie = 1, baseDifficulty = diff });
            var text = ground.transform.FindChild("text").GetComponent<TextMesh>();
            text.text = diff.ToString();
            var s = ground.transform.FindChild("obstacle").GetComponent<SpriteRenderer>();
            s.sprite = Stats[stat];
            counter = 0;
        }
    }
}

