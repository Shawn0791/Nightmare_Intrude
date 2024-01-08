using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXDestroier : MonoBehaviour
{
    private void DestroyThis()
    {
        ObjectPool.Instance.PushObject(gameObject);
    }
}
