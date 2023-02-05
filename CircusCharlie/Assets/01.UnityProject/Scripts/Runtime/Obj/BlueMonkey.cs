using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueMonkey : Monkey
{
    private bool isJump = false;
    private Animator blueMonkeyAni = default;
    protected override void Start()
    {
        moveSpeed = 10f;
        blueMonkeyAni = gameObject.GetComponentMust<Animator>();
        base.Start();
        offset = new Vector2(3600, 0);
    }

    protected override void Update()
    {
        base.Update();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "HitObject" && !isJump)
        {
            monkeyRigid.AddForce(Vector2.up * 6, ForceMode2D.Impulse);
            isJump = true;
            blueMonkeyAni.SetBool("isJump", true);
            monkeyRigid.gravityScale = 1;
        }
        else if(collision.tag == "Ground")
        {
            monkeyRigid.gravityScale = 0;
            monkeyRigid.velocity = Vector2.left * 6;
            blueMonkeyAni.SetBool("isJump", false);
            isJump = false;
        }
    }
}
