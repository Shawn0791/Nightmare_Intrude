using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeTrap : MonoBehaviour
{
    public float speed;
    public float offset;

    private void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Sin(Time.time * speed + offset) * 60);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //额外施加力
            Vector2 dir = collision.transform.position - transform.position;
            collision.transform.GetComponent<PlayerGetHit>().GetHitBack(1, dir, 100);
            //眩晕主角
            collision.transform.GetComponent<PlayerGetHit>().Vertigo();
        }
    }
}
