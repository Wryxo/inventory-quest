using UnityEngine;
using System.Collections;

public class ScollingBackground : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 offset = new Vector2(Time.fixedDeltaTime * (-GameMaster.instance.Speed / 100), 0);

		GetComponent<Renderer>().material.mainTextureOffset += offset;
	}
}
