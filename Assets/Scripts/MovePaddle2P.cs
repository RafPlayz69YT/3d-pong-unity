using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePaddle2P : MonoBehaviour
{
    public GameObject leftPaddle;
    public GameObject rightPaddle;
    private float paddleSpeed;

    void Start()
    {
        enabled = GameManager.in2PMode;
        paddleSpeed = GameManager.paddleSpeed;
        leftPaddle.transform.position.Set(-14.5f, -15, -9.275f);
        rightPaddle.transform.position.Set(14.5f, -15, -9.275f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        paddleSpeed = GameManager.paddleSpeed;
        if (GameManager.isPaused())
            return;
        // p1
        if (Input.GetKey(KeyCode.W))
            leftPaddle.transform.Translate(0f,0f, Mathf.Min(-4.95f - leftPaddle.transform.position.z, paddleSpeed));
       if (Input.GetKey(KeyCode.S))
            leftPaddle.transform.Translate(0f, 0f, Mathf.Max(-14.95f - leftPaddle.transform.position.z,-paddleSpeed));

       // p2
       if (Input.GetKey(KeyCode.UpArrow))
            rightPaddle.transform.Translate(0f, 0f, Mathf.Min(-4.95f - rightPaddle.transform.position.z, paddleSpeed));
       if (Input.GetKey(KeyCode.DownArrow))
            rightPaddle.transform.Translate(0f, 0f, Mathf.Max(-14.95f - rightPaddle.transform.position.z, -paddleSpeed));
    }
}
