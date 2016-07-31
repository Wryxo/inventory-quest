using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class GameMaster : MonoBehaviour {

    NPC fakePlayer;
    NPC killerGM;
    
    public NPC player;
    
    public static GameMaster instance;
    static Infinario.Infinario infinario;
    public float evilness;
    public int intelligence;
    public float speed;
    
    CategoricDistribution events;
    ArrayList passChance;

    public GameObject BaseObstacle;

    public int goodScore;
    public int badScore;
    public bool Overtime = false;

    public float Speed;
    public int Frequency;
    public float TimeLimit;

    public GameObject GameMenu, VictoryGO, DefeatGO;
    private bool menu;
    private GameObject ui;
    private float _speed;

    public delegate void InvokedFunction();
    public IEnumerator WaitAndInvoke(float secondsToWait, InvokedFunction func)
    {
        yield return new WaitForSeconds(secondsToWait);
        func();
    }

    // Use this for initialization
    void Start () {

        infinario = Infinario.Infinario.GetInstance();
        infinario.Initialize("7b3b027c-5245-11e6-8785-b083fedeed2e");
        //infinario.TrackSessionStart();
    }

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

    public void Track(string name, Dictionary<string, object> value)
    {
        //infinario.Track(name, value);
    }

    public void GoToGame()
    {
        goodScore = 0;
        badScore = 0;
        //instance.Speed = -Convert.ToSingle(GameObject.Find("SValue").GetComponent<Text>().text);
        //instance.Frequency = Convert.ToInt32(GameObject.Find("DValue").GetComponent<Text>().text);
        //instance.TimeLimit = Convert.ToSingle(GameObject.Find("TValue").GetComponent<Text>().text);
        instance.Speed = -5;
        instance.Frequency = 3;
        instance.TimeLimit = 90;
        string name = GameObject.Find("Meno").GetComponent<Text>().text;
        infinario.Identify(name);
        Dictionary<string, object> props = new Dictionary<string, object>();
        props.Add("Speed", -instance.Speed);
        props.Add("Difficulty", instance.Frequency);
        props.Add("TimeLimit", instance.TimeLimit);
        props.Add("Restart", false);
        //infinario.Track("level_start", props);
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
        //infinario.TrackSessionEnd();
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
                //infinario.Track("game_paused");
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
        //infinario.Track("feedback", props);
    }

    public void HideGameMenu()
    {
        Destroy(GameObject.FindGameObjectWithTag("GameMenu"));
        //GameUI.SetActive(true);
        PauseGame(false);
        //infinario.Track("game_resumed");
    }

    public void RestartLevel()
    {
        Time.timeScale = 1.0f;
        Speed = 5.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Dictionary<string, object> props = new Dictionary<string, object>();
        props.Add("Speed", -instance.Speed);
        props.Add("Difficulty", instance.Frequency);
        props.Add("TimeLimit", instance.TimeLimit);
        props.Add("Restart", true);
        //infinario.Track("level_start", props);
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
        //infinario.Track("main_menu", props);
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

    void StopPlayer()
    {
        Speed = 0;
    }

    public void StartVictory()
    {
        StopPlayer();
        StartCoroutine(WaitAndInvoke(2, Victory));
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
        props.Add("GoodScore", NPC.instance.inventory.CountItemsWithId(0));
        props.Add("Result", true);
        //infinario.Track("level_end", props);
    }

    public void StartDefeat()
    {
        StartCoroutine(WaitAndInvoke(1, StopPlayer));
        StartCoroutine(WaitAndInvoke(2, Defeat));
    }

    public void Defeat()
    {
        ui = GameObject.Find("UserInterface");
        var go = Instantiate(DefeatGO) as GameObject;
        go.transform.SetParent(ui.transform, false);
        ShowGameMenu(true);
        Dictionary<string, object> props = new Dictionary<string, object>();
        props.Add("Speed", -instance.Speed);
        props.Add("Difficulty", instance.Frequency);
        props.Add("TimeLimit", instance.TimeLimit);
        props.Add("GoodScore", NPC.instance.inventory.CountItemsWithId(0));
        props.Add("Result", false);
        //infinario.Track("level_end", props);
    }

    public void createObstacle()
    {
        // vytvor obstacle
 /*       GameObject bo = (GameObject)events.Random();
        Obstacle o = bo.GetComponent<Obstacle>();
        if (o == null) throw new System.Exception("Not implemented yet");
        else
        {
            if(o.statChecks != null)
            {
                Debug.Log("Stat checks: ");
                foreach(StatCheck s in o.statChecks)
                {
                    if(s.sidesPerDie < 0) {
                        passChance[0] = new DictionaryEntry(0.0f,0.0f);
                        passChance[1] = new DictionaryEntry((float)fakePlayer.skills.LevelOf(s.statName),1-evilness);
                        passChance[2] = new DictionaryEntry((float)killerGM.skills.LevelOf(s.statName),1f);
                        Debug.Log(FitStatCheck(s.statName, passChance));
                    }
                }
            }
        }*/
    }
}
