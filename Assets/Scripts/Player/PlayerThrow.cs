using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerThrow : MonoBehaviour
{
    public GameObject[] headBullets;
    public Transform throwPoint;
    public Vector2 throwSpeed;
    public Image skillIcon;

    private Animator anim;
    private float cd;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (GameManager.instance.gameMode == GameManager.GameMode.Normal)
        {
            ThrowHead();
            SkillData();
        }
    }

    private void ThrowHead()
    {
        if (Input.GetKeyDown(KeyCode.E) && cd <= 0) 
        {
            anim.SetTrigger("throw");
            cd = 3;
        }
    }

    public void CreateHead()
    {
        if(GetComponent<PlayerPickup>().headSlot[0] != null)
        {
            int num = GetComponent<PlayerPickup>().headSlot[0].GetComponent<Head>().headInt;
            GameObject head = ObjectPool.Instance.GetObject(headBullets[num]);
            float dir = transform.localScale.x;
            //生成投掷物
            head.transform.position = throwPoint.position;
            head.GetComponent<Rigidbody2D>().velocity = new Vector2(throwSpeed.x * dir, throwSpeed.y);
            //删除储存
            GetComponent<PlayerPickup>().headSlot[0] = null;
            GetComponent<PlayerPickup>().RefreshHead();
        }
    }

    private void SkillData()
    {
        //技能CD
        if (cd > 0) 
            cd -= Time.deltaTime;
        //技能CD显示
        skillIcon.fillAmount = 1 - cd / 3;
    }
}
