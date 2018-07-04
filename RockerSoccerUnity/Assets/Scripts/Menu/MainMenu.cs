﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour {

    public void PlayGame ()
    {
        PlayerPrefs.SetInt("nextScene", 2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame ()
    {
        Debug.Log ("QUIT");
        Application.Quit();
    }
}
