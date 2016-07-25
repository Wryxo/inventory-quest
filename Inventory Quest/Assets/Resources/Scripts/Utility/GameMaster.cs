using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class GameMaster : MonoBehaviour {

    NPC fakePlayer;
    NPC killerGM;

    public static GameMaster instance;
    static Infinario.Infinario infinario;
    public float evilness;
    public int intelligence;
    public float speed;

    public int goodScore;
    public int badScore;

    public float Speed;
    public int Frequency;
    public float TimeLimit;

    public GameObject GameMenu, VictoryGO;
    private bool menu;
    private GameObject ui;

    // Use this for initialization
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        menu = true;
    }

	void Start () {
        infinario = Infinario.Infinario.GetInstance();
        infinario.Initialize("7b3b027c-5245-11e6-8785-b083fedeed2e");
        infinario.TrackSessionStart();
    }

    public void Track(string name, Dictionary<string, object> value)
    {
        infinario.Track(name, value);
    }

    public void GoToGame()
    {
        goodScore = 0;
        badScore = 0;
        instance.Speed = -Convert.ToSingle(GameObject.Find("SValue").GetComponent<Text>().text);
        instance.Frequency = Convert.ToInt32(GameObject.Find("DValue").GetComponent<Text>().text);
        instance.TimeLimit = Convert.ToSingle(GameObject.Find("TValue").GetComponent<Text>().text);
        string name = GameObject.Find("Meno").GetComponent<Text>().text;
        Debug.Log(infinario + " " + name);
        infinario.Identify(name);
        Dictionary<string, object> props = new Dictionary<string, object>();
        props.Add("Speed", -instance.Speed);
        props.Add("Difficulty", instance.Frequency);
        props.Add("TimeLimit", instance.TimeLimit);
        props.Add("Restart", false);
        infinario.Track("level_start", props);
        SceneManager.LoadScene(1);
    }

    void OnLevelWasLoaded(int level)
    {
        switch (level)
        {
            case 0:
                menu = true;
                break;
            case 1:
                menu = false;
                break;
        }

    }

    void OnApplicationQuit()
    {
        infinario.TrackSessionEnd();
    }

    public void ShowGameMenu(bool end)
    {
        ui = GameObject.Find("UserInterface");
        if (GameObject.FindGameObjectWithTag("GameMenu") == null)
        {
            PauseGame(true);
            var go = Instantiate(GameMenu) as GameObject;
            go.transform.SetParent(ui.transform, false);
            if (end)
            {
                GameObject.Find("BackButtonT").GetComponent<Button>().interactable = false;
            }
            else
            {
                GameObject.Find("BackButtonT").GetComponent<Button>().interactable = true;
                infinario.Track("game_paused");
            }
        }
        //GameUI.SetActive(false);
    }

    public void TrackFeedback()
    {
        Text tmp = GameObject.Find("FeedbackText").GetComponent<Text>();
        Dictionary<string, object> props = new Dictionary<string, object>();
        props.Add("Speed", -instance.Speed);
        props.Add("Difficulty", instance.Frequency);
        props.Add("TimeLimit", instance.TimeLimit);
        props.Add("GoodScore", instance.goodScore);
        props.Add("BadScore", instance.badScore);
        props.Add("Text", tmp.text);
        infinario.Track("feedback", props);
    }

    public void HideGameMenu()
    {
        Destroy(GameObject.FindGameObjectWithTag("GameMenu"));
        //GameUI.SetActive(true);
        PauseGame(false);
        infinario.Track("game_resumed");
    }

    public void RestartLevel()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Dictionary<string, object> props = new Dictionary<string, object>();
        props.Add("Speed", -instance.Speed);
        props.Add("Difficulty", instance.Frequency);
        props.Add("TimeLimit", instance.TimeLimit);
        props.Add("Restart", true);
        infinario.Track("level_start", props);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1.0f;
        Dictionary<string, object> props = new Dictionary<string, object>();
        props.Add("Speed", -instance.Speed);
        props.Add("Difficulty", instance.Frequency);
        props.Add("TimeLimit", instance.TimeLimit);
        props.Add("GoodScore", instance.goodScore);
        props.Add("BadScore", instance.badScore);
        infinario.Track("main_menu", props);
        SceneManager.LoadScene(0);
    }

    void PauseGame(bool pause)
    {
        if (pause)
        {
            Time.timeScale = 0.0f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }
    }

    public void Victory()
    {
        ui = GameObject.Find("UserInterface");
        var go = Instantiate(VictoryGO) as GameObject;
        go.transform.SetParent(ui.transform, false);
        ShowGameMenu(true);
        Dictionary<string, object> props = new Dictionary<string, object>();
        props.Add("Speed", -instance.Speed);
        props.Add("Difficulty", instance.Frequency);
        props.Add("TimeLimit", instance.TimeLimit);
        props.Add("GoodScore", instance.goodScore);
        props.Add("BadScore", instance.badScore);
        infinario.Track("level_end", props);
    }

    public bool createObstacle()
    {
        // vytvor obstacle
        return false;
    }
}
