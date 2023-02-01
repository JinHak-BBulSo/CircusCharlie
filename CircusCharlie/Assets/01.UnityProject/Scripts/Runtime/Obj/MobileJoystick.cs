using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class MobileJoystick : MonoBehaviour, IEndDragHandler, IDragHandler
{
    public delegate void playerMoveHandler();
    public static event playerMoveHandler OnPlayerMove;
    public static float joysticPosX = 0;

    private const float JOYSTICK_RANGE = 72;
    [SerializeField]
    Canvas canvas = default;
    [SerializeField]
    private RectTransform joysticRect;

    private void Awake()
    {

    }
    public void OnDrag(PointerEventData eventData)
    {
        joysticRect.anchoredPosition += eventData.delta / canvas.scaleFactor;

        if (joysticRect.anchoredPosition.magnitude > JOYSTICK_RANGE)
        {
            joysticRect.anchoredPosition = Vector2.zero;
        }
        joysticPosX = joysticRect.anchoredPosition.x;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        joysticPosX = 0;
        joysticRect.anchoredPosition = Vector2.zero;
    }
}
