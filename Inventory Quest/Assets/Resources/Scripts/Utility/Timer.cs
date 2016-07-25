using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour {
    float timeLeft;

	// Use this for initialization
	void Start () {
        timeLeft = 3.0f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0.001f)
        {
            SceneManager.LoadScene(0);
        }
        TimeSpan ts = TimeSpan.FromSeconds(timeLeft);
        GetComponent<Text>().text = ts.Minutes.ToString("D2") + ":" + ts.Seconds.ToString("D2") + "." + ts.Milliseconds.ToString("D3");
    }
}
