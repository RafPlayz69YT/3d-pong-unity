using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public int curAILevel = 0; 
    public GameObject credits;
    public GameObject p1Extra;
    public GameObject p2Extra;

    public GameObject p1Text;
    public GameObject p2Text;

    public TextMeshProUGUI p1aiLvel;
    public Toggle shouldRoof;

    public bool p1ExtraVisible = false;
    public bool p2ExtraVisible = false;
    // Start is called before the first frame update
    void Start()
    {
        curAILevel = PlayerPrefs.GetInt("aiLvl", 0);
        shouldRoof.isOn = PlayerPrefs.GetInt("roof", 0) == 1;
        refreshAIText();
        credits.SetActive(false);
        p1Extra.SetActive(false);
        p2Extra.SetActive(false);
    }
    public void toogle()
    {
        PlayerPrefs.SetInt("roof", 1 - PlayerPrefs.GetInt("roof", 0));
        PlayerPrefs.Save();
        shouldRoof.isOn = PlayerPrefs.GetInt("roof", 0) == 1;
    }
    void playSound()
    {
        GameObject.Find("camera").GetComponent<AudioSource>().Play();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            changeCurAiLevel(-1 * (Input.GetKey(KeyCode.LeftShift) ? 5 : 1));  
        } else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            changeCurAiLevel(1 * (Input.GetKey(KeyCode.LeftShift) ? 5 : 1));       
        }
    }
    public void p2gone()
    {
        if (!p2ExtraVisible)
            return;
        p2Extra.SetActive(false);
        p2ExtraVisible = false;
        p2Text.GetComponent<Button>().enabled = true;
    }
    public void p1Extraexistent()
    {
        p2gone();
        playSound();
        p1Extra.SetActive(true);
        p1ExtraVisible = true;
        p1Text.GetComponent<Button>().enabled = false;
    }
    public void p2Extraexistent()
    {
        playSound();
        if (p1ExtraVisible)
        {
            p1Extra.SetActive(false);
            p1ExtraVisible = false;
            p1Text.GetComponent<Button>().enabled = true;
        }
        p2Extra.SetActive(true);
        p2ExtraVisible = true;
        p2Text.GetComponent<Button>().enabled = false;
    }
    public void changeCurAiLevel(int change = 0)
    {
        if (!p1ExtraVisible)
            return;
        int maxAILevel = PlayerPrefs.GetInt("aiLvl", 0);
        curAILevel += change;
        if (curAILevel < 0)
            curAILevel = maxAILevel;
        if (curAILevel > maxAILevel)
            curAILevel = 0;
        playSound();
        refreshAIText();
    }

    public void refreshAIText()
    {
        p1aiLvel.text = curAILevel.ToString();
    }

    public void OpenCredits()
    {
        p2gone();
        if (p1ExtraVisible)
        {
            p1Extra.SetActive(false);
            p1ExtraVisible = false;
            p1Text.GetComponent<Button>().enabled = true;
        }
        playSound();
        credits.SetActive(true);
    }
    public void CloseCredits()
    {
        p2gone();
        if (p1ExtraVisible)
        {
            p1Extra.SetActive(false);
            p1ExtraVisible = false;
            p1Text.GetComponent<Button>().enabled = true;
        }
        playSound();
        credits.SetActive(false);
    }

    public void Quit()
    {
        playSound();
        Application.Quit(0);
    }
    public void Raf()
    {
        playSound();
        Application.OpenURL("https://www.youtube.com/channel/UCmXh1HTaH_KRwisl0892KLA");
    }
    public void bruj()
    {
        playSound();
        Application.OpenURL("https://www.youtube.com/@bruj_");
    }
    public void SpecialThanks()
    {
        playSound();
        Application.OpenURL("https://docs.google.com/document/d/1JP8hemKV2otvkHu2cgH9nQ_w-stt1Zv_MxSa9NM5s08/edit?usp=sharing");
    }

    public void p1Inf()
    {
        GameManager.in2PMode = false;
        GameManager.infiniteMode = true;
        PlayerPrefs.SetInt("aiLvlC", curAILevel);
        PlayerPrefs.Save();
        SceneManager.LoadScene("pong time");
    }
    public void p1NonInf()
    {
        GameManager.in2PMode = false;
        GameManager.infiniteMode = false;
        PlayerPrefs.SetInt("aiLvlC", curAILevel);
        PlayerPrefs.Save();
        SceneManager.LoadScene("pong time");
    }
    public void p2Inf()
    {
        GameManager.in2PMode = true;
        GameManager.infiniteMode = true;
        SceneManager.LoadScene("pong time");
    }
    public void p2NonInf()
    {
        GameManager.in2PMode = true;
        GameManager.infiniteMode = false;
        SceneManager.LoadScene("pong time");
    }
}
