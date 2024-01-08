using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGetHit : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    public float maxHp;
    public float hp;
    public bool isVertigo;
    [Header("VFX")]
    public GameObject blood8Prefab;
    public GameObject blood4Prefab;
    public GameObject blood7Prefab;
    public GameObject blood9Prefab;
    public GameObject blood3Prefab;
    [Header("Bodies")]
    public GameObject[] bodiesPrefab;
    public GameObject[] redBodiesPrefab;
    public GameObject[] greenBodiesPrefab;
    public GameObject walkerBodyPrefab;
    public GameObject redBodyPrefab;
    public GameObject greenBodyPrefab;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    //受击掉血且击退
    public void GetHitBack(float damage, Vector3 dir, float force)
    {
        //造成伤害
        hp -= damage;
        //进入受击状态

        //闪白
        StartCoroutine(HurtShader());
        //后退
        rb.AddForce(-dir * force);
        //判断死亡
        if (hp <= 0)
            Dead();

        //判断变身
        if (hp <= 0.5f * maxHp &&
            GetComponent<FSM>().parameter.isChanged == false &&
            GetComponent<FSM>().parameter.type != Parameter.Type.normal) 
        {

            GetComponent<FSM>().Change();
            GetComponent<FSM>().parameter.isChanged = true;
        }
    }

    public void GetVertigo(float damage)
    {
        //掉血
        hp -= damage;
        //闪白
        StartCoroutine(HurtShader());
        //没在变身可晕
        if (GetComponent<FSM>().parameter.isChanging == false)
        {
            //眩晕状态
            GetComponent<FSM>().TransitionState(StateType.Vertigo);
            isVertigo = true;
        }
        //判断死亡
        if (hp <= 0)
            Dead();
    }

    public void GetExecute(float damage)
    {
        hp -= damage;
        BloodVFX1();

        //发现敌人
        GetComponent<FSM>().parameter.target = GetComponent<FSM>().parameter.player;
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
    }

    //受击闪白
    IEnumerator HurtShader()
    {
        sr.material.SetFloat("_FlashAmount", 1);
        yield return new WaitForSeconds(0.1f);
        sr.material.SetFloat("_FlashAmount", 0);
    }

    void Dead()
    {
        GetComponent<FSM>().TransitionState(StateType.Dead);
        transform.gameObject.layer = LayerMask.NameToLayer("Dead");
    }

    public void Explode()
    {
        //判断死亡
        if (hp <= 0)
        {
            BloodVFX2();
            //生成尸块
            ChooseBodiesType();
            SoundService.instance.Play("Body_exeplode");
            ObjectPool.Instance.PushObject(transform.parent.gameObject);
        }
        else
        {
            BloodVFX3();
        }
    }

    private void ChooseBodiesType()
    {
        if (GetComponent<FSM>().parameter.type == Parameter.Type.normal ||
            GetComponent<FSM>().parameter.type == Parameter.Type.red &&
            GetComponent<FSM>().parameter.isChanged == false ||
            GetComponent<FSM>().parameter.type == Parameter.Type.green &&
            GetComponent<FSM>().parameter.isChanged == false) 
            CreateBodies(bodiesPrefab);
        else if (GetComponent<FSM>().parameter.type == Parameter.Type.red &&
            GetComponent<FSM>().parameter.isChanged == true) 
            CreateBodies(redBodiesPrefab);
        else if (GetComponent<FSM>().parameter.type == Parameter.Type.green &&
            GetComponent<FSM>().parameter.isChanged == true)
            CreateBodies(greenBodiesPrefab);
    }

    private void CreateBodies(GameObject[] prefab)
    {
        for (int i = 0; i < 6; i++)
        {
            //生成尸块
            GameObject body = ObjectPool.Instance.GetObject(prefab[i]);
            body.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
            //随机抛飞
            float rand1 = Random.Range(-2.5f, 2.5f);
            float rand2 = Random.Range(3, 5);
            body.GetComponent<Rigidbody2D>().velocity = new Vector2(rand1 , rand2);
        }
        //生成血蓝珠
        int rand3 = Random.Range(0, 5);
        Vector2 pos1 = new Vector2(transform.position.x ,transform.position.y+1.3f);
        GameManager.instance.CreateBloodPoint(pos1, rand3);
    }

    public void DeadBody()
    {
        ObjectPool.Instance.PushObject(transform.parent.gameObject);

        if (GetComponent<FSM>().parameter.type == Parameter.Type.normal)
        {
            GameObject body = ObjectPool.Instance.GetObject(walkerBodyPrefab);
            body.transform.position = transform.position;
            body.transform.localScale = transform.localScale;
        }
        else if (GetComponent<FSM>().parameter.type == Parameter.Type.red)
        {
            GameObject body = ObjectPool.Instance.GetObject(redBodyPrefab);
            body.transform.position = transform.position;
            body.transform.localScale = transform.localScale;
        }
        else if (GetComponent<FSM>().parameter.type == Parameter.Type.green)
        {
            GameObject body = ObjectPool.Instance.GetObject(greenBodyPrefab);
            body.transform.position = transform.position;
            body.transform.localScale = transform.localScale;
        }
    }
}
