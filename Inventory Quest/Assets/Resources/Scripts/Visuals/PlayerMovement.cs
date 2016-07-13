using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public float Speed;

	void FixedUpdate () {
        transform.position = new Vector3(transform.position.x + Speed * Time.deltaTime, transform.position.y, transform.position.z);
	}
}
