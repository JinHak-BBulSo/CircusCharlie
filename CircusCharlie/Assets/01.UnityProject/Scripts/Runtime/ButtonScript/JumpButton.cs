using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpButton : MonoBehaviour
{
    public static Action playerJump;

    public void OnClickJump()
    {
        playerJump();
    }
}
