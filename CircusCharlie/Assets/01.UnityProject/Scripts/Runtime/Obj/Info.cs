using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Info : MonoBehaviour
{
    private GameObject scoreTxt;
    private GameObject bonusTxt;

    private bool bonusDecLoof = false;

    private int bonus = 5000;

    private void OnEnable()
    {
        GameManager.GetScore -= UpdateScore;
        GameManager.GetScore += UpdateScore;

        GameManager.GameClear -= GetBonus;
        GameManager.GameClear += GetBonus;
    }
    void Start()
    {
        scoreTxt = gameObject.FindChildObj("Text_Score");
        bonusTxt = gameObject.FindChildObj("Text_Bonus");
    }
    void Update()
    {
        if(!bonusDecLoof && bonus > 0)
        {
            bonusDecLoof = true;
            UpdateBonus();
            StartCoroutine(bonusDecrease());
        }
    }

    public void UpdateScore()
    {
        scoreTxt.SetTmpText("Score\n" + GameManager.Instance.score);
    }

    public void UpdateBonus()
    {
        bonusTxt.SetTmpText("BONUS - " + bonus);
    }

    IEnumerator bonusDecrease()
    {
        yield return new WaitForSeconds(0.5f);
        bonusDecLoof = false;
        bonus -= 10;
    }

    public void GetBonus()
    {
        GameManager.Instance.score += bonus;
        bonus = 0;
        UpdateBonus();
        GameManager.GameClear -= GetBonus;
    }
}
