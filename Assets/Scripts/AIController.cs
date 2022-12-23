using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public GameObject ball;
    public GameObject rightPaddle;

    private float paddleSpeed;

    // ai vars
    private float _aiDetectRange = 4f;// range in x 
    private float _aiSlowDetectMult = 1.5f;// multiples early detect
    private float _aiReactionTime = 0.2f; // secs
    private float _aiMovementErrorStoppingChance = 0.15f; // percent from 0-1, higher means it wont make as many movement errors

    // ai vars but dont touch
    private float elapsedTimeSinceDetected = 0f; // use to calc with _aiReactionTime
    private bool startingCountingForReaction = false; // prevent movement with this
    private bool detectedBall = false; // read the name lmao
    private AIKEY keyPressed;
    private AIKEY lastkeyPressed;


    public void setAIVars(float detectRange = 4f, float slowDetectmult = 1.5f, float reactionTime = 0.2f, float movementErrorStopChance = 0.15f)
    {
        _aiDetectRange = detectRange;
        _aiSlowDetectMult = slowDetectmult;
        _aiReactionTime = reactionTime;
        _aiMovementErrorStoppingChance = movementErrorStopChance;
    }

    void Start()
    {
        keyPressed = AIKEY.NONE;
        lastkeyPressed = AIKEY.NONE;
        paddleSpeed = GameManager.paddleSpeed;
        enabled = !GameManager.in2PMode;
    }

    void FixedUpdate()
    {
        if (detectedBall)
        {
            float ballY = ball.transform.position.z;
            float paddleY = rightPaddle.transform.position.z;
            if (ballY > paddleY)
            {
                    keyPressed = AIKEY.UP;
            }
            else if (ballY < paddleY)
            {
                    keyPressed = AIKEY.DOWN;
            }
            lastkeyPressed = keyPressed;
        }
        else
        {
            if (lastkeyPressed != AIKEY.NONE && Random.Range(0f, 1f) < _aiMovementErrorStoppingChance)
                keyPressed = AIKEY.NONE;
            lastkeyPressed = keyPressed;
        }
        handleKeyPress();
    }
    // Update is called once per frame
    void Update()
    {
        if ((ball.transform.position.x >= rightPaddle.transform.position.x - (_aiDetectRange * _aiSlowDetectMult)) && !startingCountingForReaction && !detectedBall && !ball.GetComponent<Ball>().rightPaddleRecent)
            startingCountingForReaction = true; // check if in range

        if (startingCountingForReaction)
        {
            elapsedTimeSinceDetected += Time.deltaTime;
            if (elapsedTimeSinceDetected > _aiReactionTime) // add time and if its good detect the ball
            {
                detectedBall = true;
                startingCountingForReaction = false;
            }
        }
    }

    public void resetDetection()
    {
        detectedBall = false;
        elapsedTimeSinceDetected = 0.0f;
    }


    void handleKeyPress()
    {
        if (GameManager.isPaused())
            return;
        paddleSpeed = GameManager.paddleSpeed;

        if (ball.transform.position.x >= rightPaddle.transform.position.x - _aiDetectRange)
        {
            if (keyPressed == AIKEY.UP)
                rightPaddle.transform.Translate(0f, 0f, Mathf.Min(-4.95f - rightPaddle.transform.position.z, paddleSpeed));
            if (keyPressed == AIKEY.DOWN)
                rightPaddle.transform.Translate(0f, 0f, Mathf.Max(-14.95f - rightPaddle.transform.position.z, -paddleSpeed));
        }
        else
        {
            if (keyPressed == AIKEY.UP)
                rightPaddle.transform.Translate(0f, 0f, Mathf.Min(-4.95f - rightPaddle.transform.position.z, paddleSpeed / 1.5f));
            if (keyPressed == AIKEY.DOWN)
                rightPaddle.transform.Translate(0f, 0f, Mathf.Max(-14.95f - rightPaddle.transform.position.z, -paddleSpeed / 1.5f));
        }
    }
}
enum AIKEY
{
    UP,
    NONE,
    DOWN
}