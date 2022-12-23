using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public readonly static float defPaddleSpeed = 0.44f;
    public readonly static float defBallSpeed = 0.3f;

    public static float ballSpeedScale = 0.000275f;
    public static float paddleSpeed = defPaddleSpeed;
    public static float ballSpeed = defBallSpeed;
    public static float ballCheatingTolerance = 0.875f; // how much the ball is offset when calc for late hits

    public static bool isDebug = false;
    public static bool in2PMode = false;
    public static bool infiniteMode = false;

    public static bool loadedtheGame = false; // UNITY POSTPROCESSING IS A PAIN AND I HAVE TO DO THIS NONSENSE FNF TO GET IT WORK GRAHHHHHHHHHHHHHHH

    public static int curAILevel = 0;

    public bool paused = false;

    public GameObject topRoof;
    public GameObject bottomRoof;
    public GameObject pauseBG;
    public GameObject pauseTexts;
    public GameObject winText;
    public DepthOfField depthOfField;

    public RawImage ITSLOAD;

    // Start is called before the first frame update
    void Start() 
    {
        GameObject gameOBJ = GameObject.Find("gaming contol");
        if (!loadedtheGame)
        {
            loadedtheGame = true;
            ohseeya();
            return;
        }
        ITSLOAD.enabled= false;

        pauseBG.SetActive(false);
        pauseTexts.SetActive(false);
        winText.SetActive(false);

        ballSpeed = defBallSpeed;

        if (!in2PMode)
        {
            curAILevel = PlayerPrefs.GetInt("aiLvlC", 0);
            gameOBJ.GetComponent<ScoreManager>().RefreshAIVarsAndUpgrade();
        }
        bool roofsExist = PlayerPrefs.GetInt("roof", 0) == 1;
        topRoof.GetComponent<MeshRenderer>().enabled = roofsExist;
        bottomRoof.GetComponent<MeshRenderer>().enabled = roofsExist;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        { if (!paused)
                pause();
            else
                unpause();
        }
        if (Input.GetKeyDown(KeyCode.Return) && winText.activeSelf)
        {
            ohseeya();
        }
    }
    public static bool isPaused()
    {
        return GameObject.Find("gaming contol").GetComponent<GameManager>().pauseBG.activeSelf;
    }
    public static void increaseAILevel()
    {
        curAILevel++;
        if (PlayerPrefs.GetInt("aiLvl", 0) < curAILevel)
        {
            PlayerPrefs.SetInt("aiLvl", curAILevel);
            PlayerPrefs.Save();
        }
        CoolAILvlUpText.instance.CreateNew();
        GameObject.Find("gaming contol").GetComponent<ScoreManager>().RefreshAIVarsAndUpgrade();
    }
    public void pause()
    {
        paused = true;
        pauseBG.SetActive(true);
        pauseTexts.SetActive(true);
    }
    public void unpause()
    {
        paused = false;
        pauseBG.SetActive(false);
        pauseTexts.SetActive(false);
    }
    public void ohseeya()
    { 
            SceneManager.LoadScene("main menu gaming hd rtx on full quality");
    }
    public void win(bool is2P)
    {
        paused = true;
        pauseBG.SetActive(true);
        winText.SetActive(true);

        string player = "Player 1";
        if (is2P)
        {
            if (in2PMode)
                player = "Player 2";
            else
                player = "The AI";
        }
        winText.GetComponent<TextMeshProUGUI>().text = player + " has won the round!\n\nPress Enter to go back to the main menu!";
    }

    public static void print(string info)
    {
        if (!isDebug)
            return;
        Debug.Log(info);
    }
}
