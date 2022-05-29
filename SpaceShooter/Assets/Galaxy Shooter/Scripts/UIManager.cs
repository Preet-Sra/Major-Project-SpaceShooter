﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    public Sprite[] Lifes;
    public Image LifesImageDisplay;
    public Text ScoreText;
    public GameObject GalaxyShooterTitleImage;
    public int Score;
    public int BestScore = 0;
    public Text bestScoreText;
    private GameManager _gameManager;
    private int TemporaryScore = 0;
    public GameObject Boss;
    private void Start()
    {
        BestScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    public void UpdateLifes(int CurrentLifes)
    {
        LifesImageDisplay.sprite = Lifes[CurrentLifes];
    }

    public void UpdateScore()
    {
        Score += 10;
        TemporaryScore += 10;
        if (TemporaryScore >= 500)
        {
            TemporaryScore = 0;
            FindObjectOfType<GameManager>().BossSpawned = true;
            Instantiate(Boss, new Vector3(0,6,0), Quaternion.identity);
        }
        ScoreText.text = "Score: " + Score;
    }

    public void checkBestScore()
    {
        if (Score > BestScore)
        {
            BestScore = Score;
            PlayerPrefs.SetInt("HighScore", BestScore);
        }
        if(bestScoreText!=null)
        bestScoreText.text = "Best: " + BestScore;
    }

    public void ShowTitleScreen()
    {
        GalaxyShooterTitleImage.SetActive(true);
        checkBestScore();
    }

    public void HideTitleScreen()
    {
        GalaxyShooterTitleImage.SetActive(false);
        ScoreText.text = "Score: ";
        Score = 0;
    }

    public void pauseMenuResumeButton()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _gameManager.pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void pauseMenuBackToMainMenu()
    {
        SceneManager.LoadScene("Main_Menu", LoadSceneMode.Single);
    }

}
