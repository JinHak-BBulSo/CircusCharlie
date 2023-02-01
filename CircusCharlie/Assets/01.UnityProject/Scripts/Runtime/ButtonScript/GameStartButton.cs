using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStartButton : MonoBehaviour
{
    TMP_Text GameStartTxt = default;
    [SerializeField]
    GameObject star = default;
    Animator starAni = default;
    void Start()
    {
        GameStartTxt = gameObject.FindChildObj("GameStartTxt").
            GetComponentMust<TMP_Text>();
        starAni = star.GetComponentMust<Animator>();
    }

    public void OnClickStart()
    {
        StartCoroutine(StartEffect());
    }

    private IEnumerator StartEffect()
    {
        GameStartTxt.color = new Color(255, 255, 255, 0);
        starAni.enabled = false;
        yield return new WaitForSeconds(0.5f);
        GameStartTxt.color = new Color(255, 255, 255, 255);
        GFunc.LoadScene("02.Stage1Scene");
    }
}
