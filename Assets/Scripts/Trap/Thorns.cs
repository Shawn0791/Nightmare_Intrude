using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorns : MonoBehaviour
{
    public float damage;
    public float debuffDam;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerGetHit>().GetHitBack(damage, Vector3.zero, 0);
            collision.GetComponent<PlayerGetHit>().DebuffBloodLoss(debuffDam, 2);
        }
    }
}
