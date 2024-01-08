using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tent : MonoBehaviour
{
    public float time;
    public float hp;

    private SpriteRenderer sr;
    private Animator anim;
    private bool isDead;
    private float timer;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        timer = 1;
    }

    void Update()
    {
        if (timer > 0)
            timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Vector2 pos = new Vector2(transform.position.x, transform.position.y + 1);
            GameManager.instance.CreateWalker(transform.position);
            timer = time;
        }
    }

    private void OnEnable()
    {
        //TryToCreate();
        SoundService.instance.Play("Tent");
    }

    private void TryToCreate()
    {
        StartCoroutine(CreateMonster());
    }

    IEnumerator CreateMonster()
    {
        yield return new WaitForSeconds(1);
        Vector2 pos = new Vector2(transform.position.x, transform.position.y + 1);
        GameManager.instance.CreateWalker(transform.position);
        yield return new WaitForSeconds(time);
        TryToCreate();
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
            anim.SetTrigger("dead");
            isDead = true;
        }
    }

    private void DestroyThis()
    {
        ObjectPool.Instance.PushObject(gameObject);
    }
}
