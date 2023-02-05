using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public static Action<string> AnimationHandler;

    private float speed = 4f;
    private float jumpForce = 8f;

    private Rigidbody2D playerRigid = default;
    private Animator playerAni = default;
    private AudioSource playerAudio = default;

    public bool isGrounded = true;
    private bool isJump = false;
    private bool isDie = false;
    private bool isHit = false;

    private GameObject GameOverObjs = default;

    private int hp = 3;
    public int Hp
    {
        get { return hp; }

    }

    [SerializeField]
    private GameObject[] life = new GameObject[3] {default, default, default};

    void Awake()
    {
        if (gameObject.name != "Player")
        {
            playerAni = gameObject.FindChildObj("Player").GetComponentMust<Animator>();   
        }
        else
        {
            playerAni = gameObject.GetComponentMust<Animator>();
        }

        playerRigid = GetComponent<Rigidbody2D>();
        GameOverObjs = GFunc.GetRootObj("UiObjs").FindChildObj("GameOverObjs");
        playerAudio = GetComponent<AudioSource>();
        GameOverObjs.SetActive(false);
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
        if (Input.GetMouseButtonDown(0) && isDie)
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
            playerAudio.Play();
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
        GameOverObjs.SetActive(true);
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
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Goal" && gameObject != null)
        {
            GameManager.Instance.Clear();
        }
        else if (collision.gameObject.tag == "Ground")
        {
            Ground();
        }
    }
}
