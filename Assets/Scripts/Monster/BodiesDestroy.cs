using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodiesDestroy : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("DestroyThis", 10);
    }

    private void DestroyThis()
    {
        ObjectPool.Instance.PushObject(gameObject);
    }
}
