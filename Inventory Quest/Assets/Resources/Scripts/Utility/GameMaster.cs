using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour {

    NPC fakePlayer;
    NPC killerGM;

    public static GameMaster instance;
    public float evilness;
    public int intelligence;
    public float speed;

    public int goodScore;
    public int badScore;
    private bool alreadyPlayed;

	// Use this for initialization
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

	void Start () {
        alreadyPlayed = false;
    }

    public void GoToGame()
    {
        goodScore = 0;
        badScore = 0;
        alreadyPlayed = true;
        SceneManager.LoadScene(1);
    }

    void OnLevelWasLoaded(int level)
    {
        if (level == 0 && alreadyPlayed)
        {
            GameObject.FindGameObjectWithTag("Finish").SetActive(true);
            GameObject.FindGameObjectWithTag("Good").GetComponent<Text>().text = goodScore.ToString();
            GameObject.FindGameObjectWithTag("Bad").GetComponent<Text>().text = badScore.ToString();
        }
    }

    public bool createObstacle()
    {
        // vytvor obstacle
        return false;
    }
}
