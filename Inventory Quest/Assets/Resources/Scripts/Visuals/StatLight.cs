using UnityEngine;
using System.Collections;

public class StatLight : MonoBehaviour {

	public Vector3 pos1;
    public Vector3 pos2;
    public float speed = 1.0f;
	float random;
    
 
	void Start()
	{
	   random = Random.Range(0.0f, 65535.0f);
	}

    void Update() {
    	float noise = Mathf.PerlinNoise(random, Mathf.PingPong(Time.time*speed, 1.0f));
        transform.localPosition = Vector3.Lerp (pos1, pos2, noise);
    }
}
