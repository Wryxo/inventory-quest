using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {

    NPC fakePlayer;
    NPC killerGM;

    public float evilness;
    public int intelligence;
    public float speed;

	// Use this for initialization
	void Start () {
	}

    public bool createObstacle()
    {
        // vytvor obstacle
        return false;
    }
}
