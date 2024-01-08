using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterPos : MonoBehaviour
{
    void Start()
    {
        PlayerMove.instance.transform.position = transform.position;

    }
}
