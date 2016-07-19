using UnityEngine;
using System.Collections;

public class TheDestroyer : MonoBehaviour {

    public GameObject[] Grounds;
    public float Range;
    // Use this for initialization
    void Start () {
	
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
        GameObject tmp = Grounds[Random.Range(0, Grounds.Length-1)];
        Sprite sprite = tmp.GetComponent<SpriteRenderer>().sprite;
        Instantiate(tmp, new Vector3(transform.position.x + (3.5f * sprite.rect.width / 100), transform.position.y, transform.position.z), Quaternion.identity);
    }
}

