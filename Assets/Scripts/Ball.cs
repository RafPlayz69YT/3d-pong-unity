using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private int stoodStill;

    private float velX;
    private float velY;
    public bool leftPaddleRecent = false;
    public bool rightPaddleRecent = false;

    public AudioClip paddle;
    public AudioClip wall;
    public AudioClip score;
    private AudioSource audioPlayer;

    void Start()
    {
        audioPlayer = GameObject.Find("camera").GetComponent<AudioSource>();
        resetBall();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameObject.transform.position.z > -3 || gameObject.transform.position.z < -20 || Mathf.Round(gameObject.transform.position.y * 10) / 10 != -14.5f)
            resetBall(Mathf.Round(gameObject.transform.position.y * 10) / 10 != -14.5f);
        else if (!GameManager.isPaused())
        {
            controlVel();
            Vector3 lastPos = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y , gameObject.transform.position.x);
            gameObject.transform.Translate(velX * GameManager.ballSpeed, 0, velY * GameManager.ballSpeed);
            GameManager.ballSpeed = Mathf.Min(GameManager.ballSpeed + GameManager.ballSpeedScale, 0.475f);
            if (lastPos.x == gameObject.transform.position.x  && lastPos.z ==gameObject.transform.position.z )
            {
                if (stoodStill > 5)
                {
                    stoodStill = 0;
                    resetBall();
                }
                stoodStill++;
            }
            else
            {
              stoodStill = 0;
            }
        }
    }
    void OnCollisionEnter(Collision col)
    {
        string colName = col.gameObject.name;
        if (colName.Contains("reset"))
        {
            resetBall();
            playSound(paddle, transform.position.x);
            return;
        }
        if (colName.Contains("roof"))
        {
            bool topRoof = colName.Contains("top");
            playSound(wall, transform.position.x);
            velY *= -1 + Random.Range(-0.1f, 0.1f);
            if (velY == 0f)
                velY = 0.5f;
            if ((topRoof && velY > 0) || (!topRoof && velY < 0))
                velY *= -1;
            return;
        }
        if (colName.Contains("paddle"))
        {
            GameManager.print("ball pos: " + gameObject.transform.localPosition.x.ToString() + ", paddle pos: " + col.transform.localPosition.x.ToString());
            bool leftPaddle = colName.Contains("left");
            if (leftPaddle) // this code looks bad but so we dont dual collide with same paddle
            {
                if (leftPaddleRecent || col.transform.localPosition.x  >= (gameObject.transform.localPosition.x - GameManager.ballCheatingTolerance))
                {
                    return;
                }
                else
                {
                    leftPaddleRecent = true;
                    rightPaddleRecent = false;
                }
            }
            else
            {
                if (rightPaddleRecent || col.transform.localPosition.x <= (gameObject.transform.localPosition.x + GameManager.ballCheatingTolerance))
                {
                    return;
                }
                else
                {
                    leftPaddleRecent = false;
                    rightPaddleRecent = true;
                }
            }
            playSound(paddle, transform.position.x);
            velX *= -1 + Random.Range(-0.1f, 0.1f);
            if (velX == 0f)
                velX = 0.5f;
            if ((rightPaddleRecent && velX > 0) || (leftPaddleRecent && velX < 0))
                velX *= -1;
        }
        else if (colName.Contains("collide"))
        {
            if (colName.Contains("left"))
            {
                GameObject.Find("gaming contol").GetComponent<ScoreManager>().ChangeScore(0, 1);
            }
            else
            {
                GameObject.Find("gaming contol").GetComponent<ScoreManager>().ChangeScore(1, 0);
            }
            GameManager.ballSpeed = Mathf.Max(GameManager.ballSpeed - 0.0625f, 0.16f);
            resetBall();
            leftPaddleRecent = false;
            rightPaddleRecent = false;
        }
        if (!GameManager.in2PMode)
            GameObject.Find("gaming contol").GetComponent<AIController>().resetDetection();
    }
    public void playSound(AudioClip audio, float ballX = 1000f)
    {
        if (ballX == 1000f)
        {
            audioPlayer.panStereo = 0;
        }
        else
        {
            if (ballX < 0)
            {
                audioPlayer.panStereo = -0.25f;
                if (ballX <= -5)
                    audioPlayer.panStereo = -0.3f;
            } 
            else
            {
                audioPlayer.panStereo =0.25f;
                if (ballX >= 5)
                    audioPlayer.panStereo = 0.3f;
            }
        }
        audioPlayer.PlayOneShot(audio);
    }
    public void controlVel()
    {
        if (velX < 0)
            Mathf.Max(-1.5f, velX);
        else if (velX > 0)
            Mathf.Min(1.5f, velX);
        if (velY < 0)
            Mathf.Max(-1.5f, velY);
        else if (velY > 0)
            Mathf.Min(1.5f, velY);

    }
    public void resetBall(bool justY = false)
    {
        controlVel();
        if (justY)
        {
            gameObject.transform.SetPositionAndRotation(new Vector3(gameObject.transform.position.x, -14.5f, gameObject.transform.position.z), gameObject.transform.rotation);
            return;
        }
        velX = 1; // random
        velY = 1;
        if (Random.Range(0, 2) == 1)
            velX *= -1;
        if (Random.Range(0, 2) == 1)
            velY *= -1;
        gameObject.transform.SetPositionAndRotation(new Vector3(0, -14.5f, -9.725f), gameObject.transform.rotation);
    }
}
