using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monkey : StageObsObj
{
    RectTransform cameraRect = default;

    protected Rigidbody2D monkeyRigid = default;
    private RectTransform monkeyRect = default;
    protected float moveSpeed = 2f;

    protected Vector2 offset = new Vector2(4800, 0);
    protected virtual void Start()
    {
        cameraRect = Camera.main.gameObject.GetRect();
        monkeyRect = gameObject.GetRect();
        monkeyRigid = gameObject.GetComponentMust<Rigidbody2D>();

        monkeyRigid.velocity = Vector2.left * moveSpeed;
    }

    protected virtual void Update()
    {
        if (cameraRect.anchoredPosition.x - monkeyRect.anchoredPosition.x > 700)
        {
            monkeyRect.anchoredPosition += offset;
        }
    }
}
