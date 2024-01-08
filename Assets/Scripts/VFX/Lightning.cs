using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    public float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerGetHit>().GetHitBack(damage, Vector3.zero, 0);
            collision.GetComponent<PlayerGetHit>().Vertigo();
        }
    }

    public void AudioLight1()
    {
        SoundService.instance.Play("light1");
    }

    public void AudioLight2()
    {
        SoundService.instance.Play("light2");
    }
}
