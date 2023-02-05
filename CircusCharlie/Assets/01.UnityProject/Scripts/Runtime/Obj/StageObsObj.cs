using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageObsObj : MonoBehaviour
{
    private void Awake()
    {
        GameManager.GameOver -= CloseObj;
        GameManager.GameOver += CloseObj;
        GameManager.GameClear -= CloseObj;
        GameManager.GameClear += CloseObj;
    }

    void CloseObj()
    {
        GameManager.GameOver -= CloseObj;
        GameManager.GameClear -= CloseObj;
        gameObject.SetActive(false);
    }
}