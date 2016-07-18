using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public float Speed;
    public Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

	void FixedUpdate () {
        //transform.position = new Vector3(transform.position.x + Speed * Time.deltaTime, transform.position.y, transform.position.z);
        rb.velocity = new Vector2(Speed, 0);
    }
}
