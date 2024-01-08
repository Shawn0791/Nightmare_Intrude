using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : MonoBehaviour
{
    public float hp;
    public float maxHp;
    public float firstTime;
    public float waitTime;
    public GameObject fireColumnPrefab;
    public GameObject wormBodyPrefab;

    private float timer;
    private Animator anim;
    private SpriteRenderer sr;

    void Start()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        timer = firstTime;
    }

    void Update()
    {
        if (timer > 0)
            timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer = waitTime;
            anim.SetTrigger("attack");
        }
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
            anim.SetTrigger("dead");
        else
            anim.SetTrigger("hurt");
    }

    //受击闪白
    IEnumerator HurtShader()
    {
        sr.material.SetFloat("_FlashAmount", 1);
        yield return new WaitForSeconds(0.1f);
        sr.material.SetFloat("_FlashAmount", 0);
    }

    private void FireColumn()
    {
        GameObject fire = ObjectPool.Instance.GetObject(fireColumnPrefab);
        fire.transform.position = transform.position;
        fire.transform.rotation = transform.rotation;
    }

    private void Dead()
    {
        GameObject body = ObjectPool.Instance.GetObject(wormBodyPrefab);
        body.transform.position = transform.position;
        Destroy(gameObject);
    }
}
