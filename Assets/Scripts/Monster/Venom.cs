using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Venom : MonoBehaviour
{
    public GameObject explodePrefab;

    private Rigidbody2D rb;
    private float angle;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        SoundService.instance.Play("venom");
    }

    void Update()
    {
        angle = Vector2.SignedAngle(new Vector2(1, 0), rb.velocity);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") ||
            collision.CompareTag("Ground"))   
        {
            GameObject explode = ObjectPool.Instance.GetObject(explodePrefab);
            explode.transform.position = transform.position;
            DestroyThis();
        }
    }

    private void DestroyThis()
    {
        ObjectPool.Instance.PushObject(gameObject);
    }
}
