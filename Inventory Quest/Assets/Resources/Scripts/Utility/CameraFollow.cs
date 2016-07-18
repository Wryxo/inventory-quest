using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{

    private Transform player;
    public float Range;

    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

	void Update ()
    {
        transform.position = new Vector3(player.position.x + Range, transform.position.y, transform.position.z);
	}
}
