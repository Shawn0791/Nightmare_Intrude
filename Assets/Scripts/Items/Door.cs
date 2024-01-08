using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public bool noMonster;

    private bool canEnter;
    private GameObject objectPool;
    
    void Update()
    {
        if (canEnter && noMonster) 
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                //对象池物体取消激活
                for (int i = 0; i < objectPool.transform.childCount; i++)
                {
                    Transform child = objectPool.transform.GetChild(i);
                    for (int a = 0; a < child.childCount; a++)
                    {
                        child.GetChild(a).gameObject.SetActive(false);
                    }
                }
                //进入下一个场景
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canEnter = true;
            objectPool = GameObject.Find("ObjectPool");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canEnter = false;
        }
    }
}
