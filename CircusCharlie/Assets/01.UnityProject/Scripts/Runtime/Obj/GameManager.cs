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

    private void OnEnable()
    {
        GameOver = default;
    }
    public void Clear()
    {
        GameClear();
    }

    public void Over()
    {
        GameOver();
    }

    public void GameRestart()
    {
        GFunc.LoadScene(GFunc.GetActiveScene().name);
    }

    public void Score()
    {
        GetScore();
    }
}
