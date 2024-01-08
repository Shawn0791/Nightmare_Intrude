using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCreater : MonoBehaviour
{
    public GameObject fireColumnPrefab;

    public void CreateFire()
    {
        GameObject fire = ObjectPool.Instance.GetObject(fireColumnPrefab);
        fire.transform.position = transform.position;
        DestroyThis();
    }

    private void DestroyThis()
    {
        ObjectPool.Instance.PushObject(gameObject);
    }
}
