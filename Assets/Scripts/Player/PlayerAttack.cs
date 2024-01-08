using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attack;
    public bool isAttack;

    private Animator anim;
    private Rigidbody2D rb;
    private int combo;
    private float timer;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (GameManager.instance.gameMode == GameManager.GameMode.Normal)
        {
            if (GetComponent<PlayerGetHit>().isVertigo == false)
            {
                Attack();
            }
        }
    }

    //传递攻击
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //造成伤害
        if (collision.CompareTag("Enemy") && isAttack) 
        {
            //计算方向
            Vector3 dir = transform.position - collision.transform.position;
            if (collision.GetComponent<FSM>().parameter.target == null &&
                collision.GetComponent<EnemyGetHit>().isVertigo == false) 
            {
                //击昏
                collision.GetComponent<EnemyGetHit>().GetVertigo(attack);
            }
            else
            {
                //传递伤害
                collision.GetComponent<EnemyGetHit>().GetHitBack(attack, dir, 100);
            }

            AudioHurt();
        }
        else if (collision.CompareTag("BOSS") && isAttack)
        {
            //计算方向
            Vector3 dir = transform.position - collision.transform.position;
            if (collision.GetComponent<EvilWizard>().target == null &&
                collision.GetComponent<EvilWizardGetHit>().isVertigo == false)
            {
                //击昏
                collision.GetComponent<EvilWizardGetHit>().GetVertigo(attack);
            }
            else
            {
                //传递伤害
                collision.GetComponent<EvilWizardGetHit>().GetHitBack(attack, dir, 50);
            }

            AudioHurt();
        }
        else if (collision.CompareTag("Tent") && isAttack)
        {
            //传递伤害
            collision.GetComponent<Tent>().GetHit(attack);

            AudioHurt();
        }
        else if (collision.CompareTag("Corpses") && isAttack)
        {
            //传递伤害
            collision.GetComponent<Corpses>().GetHit(attack);

            AudioHurt();
        }
        else if (collision.CompareTag("Healing") && isAttack)
        {
            //传递伤害
            collision.GetComponent<Healing>().GetHit(attack);

            AudioHurt();
        }
        else if (collision.CompareTag("Fireworm") && isAttack)
        {
            //传递伤害
            collision.GetComponent<Worm>().GetHit(attack);

            AudioHurt();
        }
    }

    //攻击
    void Attack()
    {
        //鼠标左键攻击
        if (Input.GetMouseButtonDown(0) &&
            !isAttack) //没在攻击状态
        {
            rb.velocity = Vector2.zero;//停止移动
            isAttack = true;
            //连击数
            combo++;
            if (combo > 2)
                combo = 1;
            timer = 1;

            anim.SetTrigger("attack");
            anim.SetInteger("combo", combo);
        }
        //延时连击
        if (timer != 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = 0;
                combo = 0;
            }
        }
    }

    public void AttackEnd()
    {
        isAttack = false;
    }

    private void AudioHurt()
    {
        int rand = Random.Range(0, 2);
        if (rand == 0)
            SoundService.instance.Play("fist1");
        else
            SoundService.instance.Play("fist2");
    }
}
