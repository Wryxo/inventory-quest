using UnityEngine;
using System.Collections;

public class RunObstacle : MonoBehaviour {

    public bool Run = false;
    public float Speed = 0.3f;
    public float TimeLeft;
	
	// Update is called once per frame
	void FixedUpdate () {
	    if (Run)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - Time.deltaTime * Speed);
        }

        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
	}
}
