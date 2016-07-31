using UnityEngine;
using System.Collections;

public class AttractObstacle : MonoBehaviour {

    public ParticleSystem ps;

    private bool attracting = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if(attracting)
        {
        }
        if (attracting && transform.localPosition.y < 3.5f)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + Time.deltaTime);
        }
        if (!attracting && transform.localPosition.y > 1.1f)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - Time.deltaTime);
        }
        if (attracting && transform.localPosition.y >= 3.5f)
        {
            attracting = false;
        }

    }

    public void Attract()
    {
        attracting = true;
        ps.Play();
    }
}
