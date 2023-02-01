using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private float speed = 4f;
    private Rigidbody2D playerRigid = default;
    [SerializeField]
    private RectTransform joysticRect = default;
    private bool isClear = false;

    private void OnEnable()
    {
        MobileJoystick.OnPlayerMove -= Move;
        MobileJoystick.OnPlayerMove += Move;
    }
    void Start()
    {
        playerRigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
        if (isClear)
        {
            MobileJoystick.OnPlayerMove -= Move;
            return;
        }
    }

    void Move()
    {
        if(MobileJoystick.joysticPosX < 0)
        {
            playerRigid.velocity = Vector2.left * speed;
        }
        else if(MobileJoystick.joysticPosX > 0)
        {
            playerRigid.velocity = Vector2.right * speed;
        }
        else
        {
            playerRigid.velocity = Vector2.zero;
        }
    }
}
