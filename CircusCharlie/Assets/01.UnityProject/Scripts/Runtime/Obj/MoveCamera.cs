using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField]
    GameObject player = default;
    float cameraPosX = 0;

    void Update()
    {
        cameraPosX = Mathf.Clamp(player.GetRect().anchoredPosition.x + 350, 0, 14690);
        gameObject.GetRect().anchoredPosition = new Vector2(cameraPosX, Camera.main.gameObject.
            GetRect().anchoredPosition.y);
    }
}
