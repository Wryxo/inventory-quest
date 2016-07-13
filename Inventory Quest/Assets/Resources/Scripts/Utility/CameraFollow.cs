using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{

    private Transform player;

    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

	void Update ()
    {
        transform.position = new Vector3(player.position.x + 8, 0, -10);
	}
}
