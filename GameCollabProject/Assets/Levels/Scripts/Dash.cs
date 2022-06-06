using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [SerializeField] private float dashDistance;
    private float doubleTapTimer;
    private KeyCode lastKeyCode;
    private Rigidbody2D body;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        //hethi partie taa dashing 
        //dashing right 
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {   //second time pressing
            if (doubleTapTimer > Time.time && lastKeyCode == KeyCode.RightArrow)
            {
                dash(1);// 1 is for the direction
                Debug.Log("dash");
            }
            //first time pressing 
            else
            {
                doubleTapTimer = Time.time + 0.4f; // after u press a key 3andk el 7a9 fi 0.5 secs to verify the condition fel if li 9ablha
            }
            lastKeyCode = KeyCode.RightArrow;
        }
        //dashing left
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {   //second time pressing
            if (doubleTapTimer > Time.time && lastKeyCode == KeyCode.LeftArrow)
            {
                dash(-1);// -1 is for the direction
            }
            //first time pressing 
            else
            {
                doubleTapTimer = Time.time + 0.4f;
            }
            lastKeyCode = KeyCode.LeftArrow;
        }
    }
    private void dash(int direction)
    {

        body.gravityScale = 0;
        body.AddForce(direction * Vector2.right * dashDistance, ForceMode2D.Impulse);//i straight up cant understand how to add a force in a specific direction in unity if u do know tell me 
        body.gravityScale = 2;                                                         //  body.gravityScale = 2;
    }
}
