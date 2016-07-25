using UnityEngine;
using System.Collections;

public class GroundMovement : MonoBehaviour {

    private float speed;

    void Start()
    {
        speed = GameMaster.instance.Speed;
    }

    // Update is called once per frame
    void FixedUpdate() {
        //rb.velocity = new Vector2(Speed, 0);
        transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
    }
}
