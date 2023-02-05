using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : GSingleton<GameManager>
{
    public delegate void StageClearHandler();
    public static event StageClearHandler GameClear;

    public delegate void GameOverHandler();
    public static event GameOverHandler GameOver;

    public delegate void GetScoreHandler();
    public static event GetScoreHandler GetScore;

    public int score = 0;
    private int savedScore = 0;

    private void OnEnable()
    {
        GameOver = default;
    }
    public void Clear()
    {
        savedScore = score;
        GameClear();
    }

    public void Over()
    {
        GameOver();
    }

    public void GameRestart()
    {
        if(GFunc.GetActiveScene().name == "02.Stage1Scene")
        {
            score = 0;
        }
        else if(GFunc.GetActiveScene().name == "03.Stage2Scene")
        {
            score = savedScore;
        }
        GFunc.LoadScene(GFunc.GetActiveScene().name);
    }

    public void Score()
    {
        GetScore();
    }
}
