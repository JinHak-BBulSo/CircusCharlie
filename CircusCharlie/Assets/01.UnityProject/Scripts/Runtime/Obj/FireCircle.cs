using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCircle : MonoBehaviour
{
    [SerializeField]
    GameObject parent = default;

    RectTransform cameraRect = default;
    RectTransform firecircleRect = default;

    private GameObject otehrFireCircle = default;
    private float moveSpeed = 30f;

    Vector2 offset = new Vector2(1500, 0);

    void Start()
    {
        GameManager.GameOver -= CloseObj;
        GameManager.GameOver += CloseObj;
        GameManager.GameClear -= CloseObj;
        GameManager.GameClear += CloseObj;

        if (gameObject.name == "FireCircle_1")
        {
            otehrFireCircle = parent.FindChildObj("FireCircle_2");
        }
        else
        {
            otehrFireCircle = parent.FindChildObj("FireCircle_1");
        }
        cameraRect = Camera.main.gameObject.GetComponentMust<RectTransform>();
        firecircleRect = gameObject.GetComponentMust<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.AddLocalPos(moveSpeed * Time.deltaTime * (-1), 0, 0);
        if (cameraRect.anchoredPosition.x - firecircleRect.anchoredPosition.x > 700)
        {
            firecircleRect.anchoredPosition = otehrFireCircle.GetComponentMust<RectTransform>().anchoredPosition + offset;
        }
    }

    void CloseObj()
    {
        GameManager.GameOver -= CloseObj;
        GameManager.GameClear -= CloseObj;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {

        }
    }
}
