﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class Game_Manager : MonoBehaviour {
   

    // Use this for initialization
    public enum GameState
    {
        STARTGAME,
        BEGIN,
        END,
        SCORE,
        GOALKICK,
        SLOWMO,
        PLAY
    };

    GameState gameState;

    GameObject StartButton;

   
    
    private bool startClicked = false; //onko painettu start
    private float xp = 0; //saavutettu xp
    private int roundedxp; //saavutettu xp pyöristettynä, mikä tallennetaan
    private float startClickedTime; //aikaleima kun start on painettu

    public bool enableBall;
    private float currentTime;
    private float roundTime;
    public float startTime = 20.0f;


    public Text UiTime;
  
    public Text UiScoreTeam1;
    public Text UiScoreTeam2;
    public Text GameEnded_text;

    public GameObject leftGoal;
    public GameObject rightGoal;

    public Transform ballLocation;
    public Transform ball;

    public bool releaseBall = false;
    //  public GameObject ballObject;
    //  public GameObject PlayerObject;

    public int score_Team1 = 0;
    public int score_Team2 = 0;

    private bool startClock = false;

    public bool SlowMo = false;


    private void Awake()
    {
        
        
    }

    void Start() {
        // Application.targetFrameRate = 60;

        
             StartCoroutine(WaitForStartClicked());
             StartButton = GameObject.Find("StartButton");


        //  StartCoroutine(ScoreDelay());
        //  Instantiate(ballObject, ballLocation.position, transform.rotation);
        //  Instantiate(PlayerObject, new Vector3(0.0f, 0.0f, 0.0f), transform.rotation);
    }


    
    //Odotetaan, että pelaaja klikkaa START
    IEnumerator WaitForStartClicked()
    {
        yield return new WaitUntil(() => startClicked == true); 
        gameState = GameState.STARTGAME;
        EnableBall();
        StartCoroutine(StartDelay());
    }


    // Update is called once per frame
    void Update() {
      
            
        
            if (releaseBall == false)
            {
                ball.transform.position = ballLocation.transform.position;
            }

        
        
        

            if (startClock == true)
            {

            
            
                currentTime = Time.time;
            
                //  Debug.Log(roundTime);

                if (gameState != GameState.END)
                {
                    roundTime = startTime - currentTime + startClickedTime;
                }



                if (roundTime <= 0.0f)
                {
                    gameState = GameState.END;
                    StartCoroutine(EndGame());
                    roundTime = 0.0f;

                }

            }
      
        UiTime.text = roundTime.ToString("0");
            UiScoreTeam1.text = score_Team1.ToString();
            UiScoreTeam2.text = score_Team2.ToString();

            if (TouchControl.inputState == TouchControl.InputState.InputStart && SlowMo == true && gameState != GameState.END)
            {
                Time.timeScale = 0.5f;
                Time.fixedDeltaTime = 0.02F * Time.timeScale;

            }
            else
            {
                Time.timeScale = 1.0f;
                Time.fixedDeltaTime = 0.02F * Time.timeScale;

            }
        
    }

   
   public IEnumerator ScoreDelay()
    {
        GameObject.Find("Ball").SendMessage("DisableBall");
        yield return new WaitForSeconds(5.0f);
        ball.transform.position = ballLocation.transform.position;
        GameObject.Find("Ball").SendMessage("EnableBall");
        StartCoroutine(StartDelay());
       

    }
    public IEnumerator StartDelay()
    {
        
        releaseBall = false;
        yield return new WaitForSeconds(1.0f);
        releaseBall = true;
        if (startClicked == true)
        {
            startClock = true;
        }

    }




    void EnableBall()
    {
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

    }

    IEnumerator EndGame()
    {
        GameObject.Find("Ball").SendMessage("DisableBall");
        releaseBall = false;
        GameEnded_text.text = ("Game Over");
        calculateXp(); //Lasketaan xp pelin päätyttyä
        yield return new WaitForSeconds(5.0f);
        Debug.Log("Game ended!!!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

    }

    public void SlowMoActivate()
    {
        SlowMo = true;

    }
    public void SlowMoDeActivate()
    {
        SlowMo = false;

    }

    //Algoritmi xp:n laskemiseen ja tallentamiseen
    public void calculateXp() 
    {
        int scoreDifference = score_Team1 - score_Team2; //maaliero
        
        if (scoreDifference > 0) //pelaajavoittaa
        {
            xp = (this.startTime * 1.5f) + (Mathf.Log10(scoreDifference)) * 100;
        }
        else if(scoreDifference < 0) //tietokonevoittaa
        {
            scoreDifference = -1 * scoreDifference;
            xp = (this.startTime * 1.5f) - ((Mathf.Log10(scoreDifference)) * 20); 
           
        }
        else if (scoreDifference == 0) //tasapeli
        {
            xp = this.startTime * 0.5f;
        }
        roundedxp = (int)Mathf.Round(xp);
        Debug.Log(roundedxp);
        if (roundedxp < 0) //tallennetaan 0xp
        {
            PlayerPrefs.SetInt("lastRoundXp", 0);
        }
        else if(roundedxp >= 0) //tallennetaan saatu xp
        {
            PlayerPrefs.SetInt("lastRoundXp", roundedxp);
        }
      

        
    }

    //Kutsutaan kun startbuttonia painetaan
    public void clickStart()
    {
        startClicked = true;
        startClickedTime = Time.time;
        StartButton.SetActive(false);
        
    }

    public void ReturnToMainMenu()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
