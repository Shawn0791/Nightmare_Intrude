using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : MonoBehaviour
{
    public float hp;
    public GameObject chipPrefab;
    public GameObject[] chips;

    private SpriteRenderer sr;
    private bool isDead;
    private GameObject player;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    //受击掉血
    public void GetHit(float damage)
    {
        //造成伤害
        hp -= damage;
        //闪白
        StartCoroutine(HurtShader());
        //判断死亡
        if (hp <= 0)
            Dead();
    }

    //受击闪白
    IEnumerator HurtShader()
    {
        sr.material.SetFloat("_FlashAmount", 1);
        yield return new WaitForSeconds(0.1f);
        sr.material.SetFloat("_FlashAmount", 0);
    }

    private void Dead()
    {
        if (hp <= 0 && !isDead)
        {
            isDead = true;
            CreateChips();
        }
    }

    private void CreateChips()
    {
        for (int i = 0; i < chips.Length; i++)
        {
            float randX = Random.Range(-0.4f, 0.4f);
            float randY = Random.Range(1, 2);
            float rand1 = Random.Range(-1.5f, 1.5f);
            float rand2 = Random.Range(3f, 8f);
            GameObject body = ObjectPool.Instance.GetObject(chips[i]);
            body.transform.position = new Vector2(transform.position.x + randX, transform.position.y + randY);
            body.GetComponent<Rigidbody2D>().velocity = new Vector2(rand1, rand2);
        }

        player.GetComponent<PlayerGetHit>().HealingBegin();

        DestroyThis();
    }

    private void DestroyThis()
    {
        SoundService.instance.Play("HealingExplode");

        GameObject chip = ObjectPool.Instance.GetObject(chipPrefab);
        chip.transform.position = transform.position;
        Destroy(gameObject);
    }
}
