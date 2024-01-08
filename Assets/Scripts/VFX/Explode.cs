using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyGetHit>().GetVertigo(damage);
        }
        else if (collision.CompareTag("BOSS"))
        {
            collision.GetComponent<EvilWizardGetHit>().GetVertigo(damage);
        }
        else if (collision.CompareTag("Tent"))
        {
            collision.GetComponent<Tent>().GetHit(damage);
        }
        else if (collision.CompareTag("Corpses"))
        {
            collision.GetComponent<Corpses>().GetHit(damage);
        }
    }
}
