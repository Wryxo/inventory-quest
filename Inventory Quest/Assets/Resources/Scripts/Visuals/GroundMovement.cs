using UnityEngine;
using System.Collections;

public class GroundMovement : MonoBehaviour {

	public float Speed;
	public float deadX;
	public GameObject podlaha;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		var old = transform.position;
		transform.position = new Vector2(old.x - Speed*Time.deltaTime, old.y);
		if (transform.position.x < deadX) {
			Instantiate(podlaha, new Vector3(25.5f, 0.2f, 0), Quaternion.identity);
			Destroy(transform.gameObject);
		}
	}
}
