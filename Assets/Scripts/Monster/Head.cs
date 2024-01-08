using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    public int headInt;

    public void DestroyThis()
    {
        ObjectPool.Instance.PushObject(gameObject);
    }
}
