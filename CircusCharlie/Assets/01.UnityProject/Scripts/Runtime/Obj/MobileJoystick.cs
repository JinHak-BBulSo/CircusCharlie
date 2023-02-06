using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class MobileJoystick : MonoBehaviour, IEndDragHandler, IDragHandler
{
    public delegate void playerMoveHandler();
    public static event playerMoveHandler OnPlayerMove;
    public static float joystickPosX = 0;

    private const float JOYSTICK_RANGE = 120;
    [SerializeField]
    Canvas canvas = default;
    [SerializeField]
    private RectTransform joysticRect;
    private bool isMove = false;
    private bool isOut = false;

    void Update()
    {
        if (isMove && OnPlayerMove != default) OnPlayerMove();
    }
    public void OnDrag(PointerEventData eventData)
    {
        isMove = true;
        if (isMove && !isOut)
        {
            joysticRect.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }

        if (joysticRect.anchoredPosition.magnitude > JOYSTICK_RANGE)
        {
            RangeOut();
        }
        joystickPosX = joysticRect.anchoredPosition.x;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        joystickPosX = 0;
        isMove = false;
        isOut = false;
        joysticRect.anchoredPosition = Vector2.zero;
        if (isMove && OnPlayerMove != default) OnPlayerMove();
    }

    public void RangeOut()
    {
        isOut = true;
        isMove = false;
        joysticRect.anchoredPosition = Vector2.zero;
        joystickPosX = 0;
    }
}
