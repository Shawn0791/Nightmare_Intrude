using System.Collections;
using UnityEngine;

public class PlayerGetHit : MonoBehaviour
{
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private Animator anim;
    private bool hpIncreasing;
    private float debuffDamage;

    public float maxHp;
    public float hp;
    public bool isVertigo;
    public GameObject bloodOverlay;
    public GameObject RestartMenu;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {

    }

    //受击掉血且击退
    public void GetHitBack(float damage, Vector3 dir, float force)
    {
        hp -= damage;
        GameManager.instance.RefreshHp();
        //闪白
        StartCoroutine(HurtShader());
        //后退
        rb.AddForce(-dir * force);
        //修正状态
        GetComponent<PlayerAttack>().isAttack = false;
        GetComponent<PlayerExecute>().isExecute = false;
        isVertigo = false;
        
        //判断死亡
        if (hp <= 0)
            Dead();
        else
        {
            anim.SetTrigger("hurt");
            SoundService.instance.Play("Player_hurt");
        }
    }

    //受击闪白
    IEnumerator HurtShader()
    {
        sr.material.SetFloat("_FlashAmount", 1);
        yield return new WaitForSeconds(0.1f);
        sr.material.SetFloat("_FlashAmount", 0);
    }

    public void HealingBegin()
    {
        StopCoroutine("ContinuousBloodReturn");
        StartCoroutine("ContinuousBloodReturn");
        BuffIcon.instance.StartBuff(4, 5);
    }

    IEnumerator ContinuousBloodReturn()
    {
        for (int i = 0; i < 10; i++)
        {
            if (hp < maxHp)
                hp += 0.2f;
            if (hp > maxHp)
                hp = maxHp;
            GameManager.instance.RefreshHp();
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void Vertigo()
    {
        if (hp > 0)
        {
            isVertigo = true;
            rb.velocity = Vector2.zero;
            anim.SetTrigger("vertigo");
            BuffIcon.instance.StartBuff(0, 1);
        }
    }

    public void VertigoEnd()
    {
        isVertigo = false;
    }

    public void DebuffBloodLoss(float damage,int num)
    {
        StopCoroutine("ContinuousBloodLoss");
        debuffDamage = damage;
        StartCoroutine("ContinuousBloodLoss");
        BuffIcon.instance.StartBuff(num, 5);
    }

    IEnumerator ContinuousBloodLoss()
    {
        for (int i = 0; i < 5; i++)
        {
            hp -= debuffDamage;
            GameManager.instance.RefreshHp();
            yield return new WaitForSeconds(1);
        }
    }

    public void KillRewardHp(float _hp)
    {
        if (hp < maxHp)
        {
            hp += _hp;
            GameManager.instance.RefreshHp();
        }
        if (hp > maxHp)
        {
            hp = maxHp;
            GameManager.instance.RefreshHp();
        }
    }

    public void Dead()
    {
        anim.SetTrigger("dead");
        transform.gameObject.layer = LayerMask.NameToLayer("Dead");
        rb.velocity = Vector2.zero;

        GameManager.instance.gameMode = GameManager.GameMode.Dead;
        SoundService.instance.Play("Player_dead");
        RestartMenu.SetActive(true);
    }
}
