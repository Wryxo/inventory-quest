using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour {
    private float timeLeft;

    void Start()
    {
        timeLeft = GameMaster.instance.TimeLimit;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0.001f)
        {
            GameMaster.instance.Victory();
        }
        TimeSpan ts = TimeSpan.FromSeconds(timeLeft);
        GetComponent<Text>().text = ts.Minutes.ToString("D2") + ":" + ts.Seconds.ToString("D2") + "." + ts.Milliseconds.ToString("D3");
    }

}
