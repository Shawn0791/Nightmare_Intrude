using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilWizardGetHit : MonoBehaviour
{
    public float hp;
    public float maxHp;
    public GameObject[] bodies;
    public bool isVertigo;
    [Header("VFX")]
    public GameObject blood8Prefab;
    public GameObject blood4Prefab;
    public GameObject blood7Prefab;
    public GameObject blood9Prefab;
    public GameObject blood3Prefab;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    //受击掉血且击退
    public void GetHitBack(float damage, Vector3 dir, float force)
    {
        //造成伤害
        hp -= damage;
        //闪白
        StartCoroutine(HurtShader());
        //后退
        rb.AddForce(-dir * force);
        //判断死亡
        if (hp <= 0)
            Dead();
    }

    public void GetVertigo(float damage)
    {
        //掉血
        hp -= damage;
        //眩晕状态
        anim.SetTrigger("vertigo");
        isVertigo = true;
        //判断死亡
        if (hp <= 0)
            Dead();

        Debug.Log("vertigo");
    }

    public void VertigoEnd()
    {
        isVertigo = false;
    }

    public void GetExecute(float damage)
    {
        hp -= damage;
        BloodVFX1();

        //发现敌人
        GetComponent<EvilWizard>().target = GetComponent<EvilWizard>().player;

        Debug.Log("execute!!!");
    }

    //受击闪白
    IEnumerator HurtShader()
    {
        sr.material.SetFloat("_FlashAmount", 1);
        yield return new WaitForSeconds(0.1f);
        sr.material.SetFloat("_FlashAmount", 0);
    }

    public void Explode()
    {
        //判断死亡
        if (hp <= 0)
        {
            BloodVFX3();
            Dead();
        }
        else
        {
            BloodVFX3();
        }
    }

    public void BloodVFX1()
    {
        //喷血特效8
        GameObject blood8 = ObjectPool.Instance.GetObject(blood8Prefab);
        blood8.transform.position = new Vector2(transform.position.x, transform.position.y + 0.5f);
    }

    public void BloodVFX2()
    {
        //喷血特效4
        GameObject blood4 = ObjectPool.Instance.GetObject(blood4Prefab);
        blood4.transform.position = new Vector2(transform.position.x, transform.position.y + 0.5f);
        //喷血特效7
        GameObject blood7 = ObjectPool.Instance.GetObject(blood7Prefab);
        blood7.transform.position = new Vector2(transform.position.x, transform.position.y + 0.5f);
        //喷血特效9
        GameObject blood9 = ObjectPool.Instance.GetObject(blood9Prefab);
        blood9.transform.position = new Vector2(transform.position.x, transform.position.y + 0.5f);
    }

    public void BloodVFX3()
    {
        //喷血特效8
        GameObject blood3 = ObjectPool.Instance.GetObject(blood3Prefab);
        blood3.transform.position = new Vector2(transform.position.x, transform.position.y + 0.5f);

        SoundService.instance.Play("Player_executeblood");
    }

    private void Dead()
    {
        anim.SetTrigger("dead");
        transform.gameObject.layer = LayerMask.NameToLayer("Dead");

        if(anim.GetBool("stage3"))
            GameManager.instance.GameOver();
    }

    private void CreateBodies()
    {
        for (int i = 0; i < 6; i++)
        {
            //生成尸块
            GameObject body = ObjectPool.Instance.GetObject(bodies[i]);
            body.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
            //随机抛飞
            float rand1 = Random.Range(-1.7f, 1.7f);
            float rand2 = Random.Range(3, 5);
            body.GetComponent<Rigidbody2D>().velocity = new Vector2(rand1 * 1.5f, rand2);
        }
    }
}
