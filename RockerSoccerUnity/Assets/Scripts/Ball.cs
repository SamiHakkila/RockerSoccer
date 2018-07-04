﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    public Rigidbody2D ball;
    public Transform ballT;
    public Transform ballStart;

   
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "LeftGoal")
        {
            GameObject.Find("Game_Manager").GetComponent<Game_Manager>().score_Team2 += 1;
           
            GameObject.Find("Game_Manager").SendMessage("ScoreDelay");
        }

        if (other.gameObject.tag == "RightGoal")
        {
            GameObject.Find("Game_Manager").GetComponent<Game_Manager>().score_Team1 += 1;
            
            
            GameObject.Find("Game_Manager").SendMessage("ScoreDelay");
        }

    }

    public void DisableBall()
    {
        GameObject.Find("Player_Manager").GetComponent<Player_Manager>().isBallDisabled = true;
        GameObject.Find("Ai_Manager").GetComponent<Ai_Manager>().isBallDisabled = true;

        GameObject.Find("Player_1").SendMessage("DisableBall");
        GameObject.Find("Player_2").SendMessage("DisableBall");
        GameObject.Find("Player_3").SendMessage("DisableBall");
        GameObject.Find("Player_4").SendMessage("DisableBall");
        GameObject.Find("Player_5").SendMessage("DisableBall");

        GameObject.Find("Ai_1").SendMessage("DisableBall");
        GameObject.Find("Ai_2").SendMessage("DisableBall");
        GameObject.Find("Ai_3").SendMessage("DisableBall");
        GameObject.Find("Ai_4").SendMessage("DisableBall");
        GameObject.Find("Ai_5").SendMessage("DisableBall");


        Physics2D.IgnoreLayerCollision(9, 11, true);

        Physics2D.IgnoreLayerCollision(9, 12, true);
    }
    public void EnableBall()
    {
        GameObject.Find("Player_Manager").GetComponent<Player_Manager>().isBallDisabled = false;
        GameObject.Find("Ai_Manager").GetComponent<Ai_Manager>().isBallDisabled = false;

        GameObject.Find("Player_1").SendMessage("EnableBall");
        GameObject.Find("Player_2").SendMessage("EnableBall");
        GameObject.Find("Player_3").SendMessage("EnableBall");
        GameObject.Find("Player_4").SendMessage("EnableBall");
        GameObject.Find("Player_5").SendMessage("EnableBall");

        GameObject.Find("Ai_1").SendMessage("EnableBall");
        GameObject.Find("Ai_2").SendMessage("EnableBall");
        GameObject.Find("Ai_3").SendMessage("EnableBall");
        GameObject.Find("Ai_4").SendMessage("EnableBall");
        GameObject.Find("Ai_5").SendMessage("EnableBall");

        Physics2D.IgnoreLayerCollision(9, 11, false);

        Physics2D.IgnoreLayerCollision(9, 12, false);
    }

    public void StopBall()
    {
       ball.velocity = new Vector2(0, 0);

    }

}
