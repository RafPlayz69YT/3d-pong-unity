using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePaddle : MonoBehaviour
{
    public GameObject leftPaddle;
    private float paddleSpeed;

    void Start()
    {
        paddleSpeed = GameManager.paddleSpeed;
        leftPaddle.transform.position.Set(-14.5f, -15, -9.275f);
        enabled = !GameManager.in2PMode;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        paddleSpeed = GameManager.paddleSpeed;

        if (GameManager.isPaused())
            return;
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            leftPaddle.transform.Translate(0f,0f, Mathf.Min(-4.9f - leftPaddle.transform.position.z, paddleSpeed));
       if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            leftPaddle.transform.Translate(0f, 0f, Mathf.Max(-14.95f - leftPaddle.transform.position.z,-paddleSpeed));
    }
}
