using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMvcam : MonoBehaviour
{
    public static CMvcam instance;
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
