using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public static Action<string> AnimationHandler;

    private float speed = 4f;
    private float jumpForce = 7.5f;

    private Rigidbody2D playerRigid = default;
    private Animator playerAni = default;

    public bool isGrounded = true;
    private bool isJump = false;
    private bool isDie = false;
    private bool isHit = false;

    private GameObject GameOverTxt = default;

    private int hp = 3;
    public int Hp
    {
        get { return hp; }

    }

    [SerializeField]
    private GameObject[] life = new GameObject[3] {default, default, default};

    void Awake()
    {
        playerAni = gameObject.FindChildObj("Player").GetComponentMust<Animator>();
        playerRigid = GetComponent<Rigidbody2D>();

        GameOverTxt = GFunc.GetRootObj("UiObjs").FindChildObj("GameOverTxt");
        GameOverTxt.SetActive(false);
    }

    private void OnEnable()
    {
        GameManager.GameClear -= Clear;
        GameManager.GameClear += Clear;

        GameManager.GameOver -= Die;
        GameManager.GameOver += Die;

        MobileJoystick.OnPlayerMove -= Move;
        MobileJoystick.OnPlayerMove += Move;

        JumpButton.playerJump -= Jump;
        JumpButton.playerJump += Jump;
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && isDie)
        {
            GameManager.Instance.GameRestart();
        }
        if(Input.GetKeyDown(KeyCode.Space) && !isDie)
        {
            Jump();
        }
    }

    void Move()
    {
        if (isGrounded && !isDie)
        {
            if (MobileJoystick.joystickPosX < 0)
            {
                playerRigid.velocity = Vector2.left * speed;
            }
            else if (MobileJoystick.joystickPosX > 0)
            {
                playerRigid.velocity = Vector2.right * speed;
            }
            else
            {
                playerRigid.velocity = Vector2.zero;
            }
        }
    }

    void Jump()
    {
        if (isGrounded && !isJump && !isDie)
        {
            isGrounded = false;
            isJump = true;
            playerRigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    public void Ground()
    {
        isGrounded = true;
        isJump = false;
    }

    public void Clear()
    {
        playerAni.SetTrigger("Clear");
        if(AnimationHandler != default)
            AnimationHandler("Clear");
        GameManager.GameOver -= Die;
        GameManager.GameClear -= Clear;
        MobileJoystick.OnPlayerMove -= Move;
    }

    void Hit()
    {
        if (!isHit)
        {
            isHit = true;
            hp--;
            Debug.Log(hp);
            life[hp].SetActive(false);

            if (hp == 0 && !isDie)
            {
                hp = 0;
                GameManager.Instance.Over();
            }
            else
            {
                StartCoroutine(HitDelay());
            }
        } 
    }

    void Die()
    {
        isDie = true;
        playerRigid.velocity = Vector2.zero;
        GameOverTxt.SetActive(true);
        playerAni.SetTrigger("Die");

        if (AnimationHandler != default)
            AnimationHandler("Die");

        GameManager.GameOver -= Die;
        GameManager.GameClear -= Clear;
        MobileJoystick.OnPlayerMove -= Move;
    }

    IEnumerator HitDelay()
    {
        yield return new WaitForSeconds(0.5f);
        isHit = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "HitObject" && !isDie)
        {
            Hit();
        }
        else if(collision.tag == "ScoreGet")
        {
            GameManager.Instance.score += 100;
            GameManager.Instance.Score();
        }
        else if(collision.tag == "Goal" && gameObject != null)
        {
            GameManager.Instance.Clear();
        }
    }
}
