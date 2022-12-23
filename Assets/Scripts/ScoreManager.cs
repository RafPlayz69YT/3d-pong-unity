using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{

    public TextMeshPro P1ScoreText;
    public TextMeshPro P2ScoreText;
    private static int p1Score = 0;
    private static int p2Score = 0;

    void Start()
    {
        p1Score = 0;
        p2Score = 0;
        RefreshText();
    }
     void RefreshText()
    {
        P1ScoreText.text = p1Score.ToString();
        P2ScoreText.text = p2Score.ToString();
    }

    public void RefreshAIVarsAndUpgrade()
    {
        float range = Mathf.Min(4f + (GameManager.curAILevel * 0.01f), 12f);
        float slowRangeMult = Mathf.Min(1.5f + (GameManager.curAILevel * 0.055f), 2f);
        float reactionTime = Mathf.Max(0.2f - (GameManager.curAILevel * 0.0055f), 0.005f);
        float mESC = 0.15f * Mathf.Min(GameManager.curAILevel / 1.5f, 0.8f);
        GameManager.ballSpeedScale = Mathf.Min(0.0003f * (Mathf.Floor(GameManager.curAILevel / 4) + 1), 0.005f);
        GameObject.Find("gaming contol").GetComponent<AIController>().setAIVars(range, slowRangeMult, reactionTime, mESC);
    }
    public void ChangeScore(int p1 = 0, int p2 = 0)
    {
        p1Score += p1;
        p2Score += p2;
        if (p1 != 0 && p1Score % 5 == 0 && !GameManager.in2PMode)
        {
            GameManager.increaseAILevel();
        }
        if ((p1Score >= 10 || p2Score >= 10) && !GameManager.infiniteMode)
        {
            GameObject.Find("gaming contol").GetComponent<GameManager>().win(p2Score >= 10);
        }
        RefreshText();
    }
}
