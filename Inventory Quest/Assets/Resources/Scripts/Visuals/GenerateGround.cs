using UnityEngine;
using System.Collections;
using System;

public class GenerateGround : MonoBehaviour {

    public GameObject[] Grounds;
    public float Interval;
    private float timeLeft;

	// Use this for initialization
	void Start () {
        timeLeft = 0.0f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0.0f)
        {
            SpawnGround();
            timeLeft = Interval;
        }
	}

    private void SpawnGround()
    {
        Instantiate(Grounds[(int)(UnityEngine.Random.value * Grounds.Length)], new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
    }
}
