using UnityEngine;
using System.Collections;

public class GroundMovement : MonoBehaviour {

    public float Speed;

    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void FixedUpdate () {
        //rb.velocity = new Vector2(Speed, 0);
        transform.position = new Vector3(transform.position.x + Speed * Time.deltaTime, transform.position.y, transform.position.z);
    }
}
