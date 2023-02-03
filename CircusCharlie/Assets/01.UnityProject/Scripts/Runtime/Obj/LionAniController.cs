using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LionAniController : MonoBehaviour
{
    private Animator lionAni = default;
    void Awake()
    {
        PlayerController.AnimationHandler -= AniControll;
        PlayerController.AnimationHandler += AniControll;
        lionAni = gameObject.GetComponentMust<Animator>();
    }

    void AniControll(string triggerName_)
    {
        lionAni.SetTrigger(triggerName_);
        PlayerController.AnimationHandler -= AniControll;
    }
}
