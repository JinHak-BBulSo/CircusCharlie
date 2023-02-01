using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCircle : MonoBehaviour
{
    [SerializeField]
    GameObject parent = default;
    private GameObject otehrFireCircle = default;
    private float moveSpeed = 30f;

    void Start()
    {
        if(gameObject.name == "FireCircle_1")
        {
            otehrFireCircle = parent.FindChildObj("FireCircle_2");
        }
        else
        {
            otehrFireCircle = parent.FindChildObj("FireCircle_1");
        }
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.AddLocalPos(moveSpeed * Time.deltaTime * (-1), 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {

        }
    }
}
