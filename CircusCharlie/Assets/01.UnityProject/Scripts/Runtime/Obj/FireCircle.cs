using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCircle : StageObsObj
{
    RectTransform cameraRect = default;
    RectTransform firecircleRect = default;

    private float moveSpeed = 30f;

    Vector2 offset = new Vector2(1500, 0);

    void Start()
    {
        cameraRect = Camera.main.gameObject.GetRect();
        firecircleRect = gameObject.GetRect();
    }

    void Update()
    {
        gameObject.AddLocalPos(moveSpeed * Time.deltaTime * (-1), 0, 0);
        if (cameraRect.anchoredPosition.x - firecircleRect.anchoredPosition.x > 700)
        {
            firecircleRect.anchoredPosition += offset;
        }
    }
}
