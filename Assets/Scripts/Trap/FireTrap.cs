using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    public float timer;
    public GameObject fireColumn;
    
    private Animator anim;
    private bool isTriggering;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") &&
            isTriggering == false &&
            Vector2.Distance(collision.transform.position, transform.position) < 0.3f)  
        {
            isTriggering = true;
            Invoke("FireColumn", timer);
            anim.SetTrigger("trigger");
        }
    }

    private void FireColumn()
    {
        GameObject fire = ObjectPool.Instance.GetObject(fireColumn);
        fire.transform.position = transform.position;
        isTriggering = false;
    }
}
