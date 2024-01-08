using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAppear : MonoBehaviour
{
    public GameObject bossHpBar;
    public GameObject boss;
    public GameObject vfx;

    private bool bossAppear;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !bossAppear) 
        {
            StartCoroutine(BossAppearing());
            vfx.SetActive(true);
            bossAppear = true;
        }
    }

    IEnumerator BossAppearing()
    {
        bossHpBar.SetActive(true);
        yield return new WaitForSeconds(2);
        boss.SetActive(true);
        vfx.SetActive(false);
    }
}
