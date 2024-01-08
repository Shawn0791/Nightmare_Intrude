using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirWall2 : MonoBehaviour
{
    public GameObject walker;

    private void Update()
    {
        if (walker.activeSelf == false)
        {
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
