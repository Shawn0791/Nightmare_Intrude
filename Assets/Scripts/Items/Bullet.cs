using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public GameObject blood0;

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

        if (collision.CompareTag("Ground") ||
            collision.CompareTag("Enemy") ||
            collision.CompareTag("BOSS") ||
            collision.CompareTag("VFX") ||
            collision.CompareTag("Tent") ||
            collision.CompareTag("Corpses"))   
        {
            if(GetComponent<Head>().headInt==3)
                SoundService.instance.Play("explode");
            else
                SoundService.instance.Play("Head_attack");

            GameObject blood = ObjectPool.Instance.GetObject(blood0);
            blood.transform.position = transform.position;
            DestroyThis();
        }
    }

    private void DestroyThis()
    {
        ObjectPool.Instance.PushObject(gameObject);
    }
}
