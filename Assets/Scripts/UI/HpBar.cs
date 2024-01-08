using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public GameObject obj;
    public Image hpBg;
    public Image hp_fill;

    protected private float alpha;
    protected private float hp;
    protected private float maxHp;
    protected private float lastData;
    protected private float timer;

    protected virtual void Start()
    {
        lastData = hp;
        GetComponent<Canvas>().worldCamera = Camera.main;
    }

    protected virtual void Update()
    {
        HideBar();

        hp = obj.GetComponent<EnemyGetHit>().hp;
        maxHp = obj.GetComponent<EnemyGetHit>().maxHp;
        //血条随血量变动
        hp_fill.fillAmount = hp / maxHp;
        //不随主体翻转
        transform.localScale = obj.transform.localScale;

        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }

    protected virtual void HideBar()
    {
        hpBg.color = new Color(1, 1, 1, alpha);
        hp_fill.color = new Color(0.5094f, 0.5094f, 0.5094f, alpha);

        if (hp != lastData)//血量发生变化
        {
            //血条可见
            alpha = 1;
            //更正数值
            lastData = hp;
            timer = 2;
        }
        else if (timer <= 0 && alpha > 0)
        {
            //血条逐渐透明
            alpha -= 0.01f;
        }
    }
}

