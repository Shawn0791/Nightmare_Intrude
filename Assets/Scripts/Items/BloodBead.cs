using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodBead : MonoBehaviour
{
    public GameObject disappearVFXPrefab;
    public float addHp;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            rb.velocity = Vector2.zero;
            collision.GetComponent<PlayerGetHit>().KillRewardHp(addHp);
            DisappearVFX();
            DestroyThis();
        }
    }

    private void DisappearVFX()
    {
        GameObject dis = ObjectPool.Instance.GetObject(disappearVFXPrefab);
        dis.transform.position = transform.position;
    }

    private void DestroyThis()
    {
        SoundService.instance.Play("BeadPick");
        ObjectPool.Instance.PushObject(gameObject);
    }
}
