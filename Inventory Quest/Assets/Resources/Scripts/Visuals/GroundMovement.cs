using UnityEngine;
using System.Collections;

public class GroundMovement : MonoBehaviour {
    // Update is called once per frame
    void FixedUpdate() {
        //rb.velocity = new Vector2(Speed, 0);
        transform.position = new Vector3(transform.position.x + GameMaster.instance.Speed * Time.deltaTime, transform.position.y, transform.position.z);
    }
}
