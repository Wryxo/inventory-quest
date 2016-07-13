using UnityEngine;
using System.Collections;

public class GroundMovement : MonoBehaviour {

    private Transform player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.position = new Vector3(player.position.x + 8, 0.2f, 0);
	}
}
