using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolSingleton : MonoBehaviour
{
    public static ObjectPoolSingleton instance;
    private void Awake()
    {
        //单例
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);
    }
}
