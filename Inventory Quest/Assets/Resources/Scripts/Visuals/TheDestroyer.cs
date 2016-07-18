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
        Instantiate(Grounds[(int)(UnityEngine.Random.value * Grounds.Length)], new Vector3(transform.position.x + Range, transform.position.y, transform.position.z), Quaternion.identity);
    }
}

