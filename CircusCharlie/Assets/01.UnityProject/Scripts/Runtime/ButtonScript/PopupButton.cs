using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupButton : MonoBehaviour
{
    GameObject exitBtn = default;
    bool isBtnClick = false;

    void Start()
    {
        exitBtn = gameObject.FindChildObj("ExitBtn");
        exitBtn.SetActive(false);
        DontDestroyOnLoad(this);
    }

    public void OnClickPopupBtn()
    {
        if (!isBtnClick)
        {
            exitBtn.SetActive(true);
            Time.timeScale = 0;
            isBtnClick = true;
        }
        else
        {
            exitBtn.SetActive(false);
            Time.timeScale = 1;
            isBtnClick = false;
        }
    }
}
