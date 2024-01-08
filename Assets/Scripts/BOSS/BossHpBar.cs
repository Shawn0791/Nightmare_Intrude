using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHpBar : MonoBehaviour
{
    public Image hpBar;
    public GameObject boss;

    private float hp;
    private float maxHp;

    private void OnEnable()
    {
        GetComponent<Animator>().SetTrigger("appear");
    }

    void Update()
    {
        hp = boss.GetComponent<EvilWizardGetHit>().hp;
        maxHp = boss.GetComponent<EvilWizardGetHit>().maxHp;
        //血条随血量变动
        hpBar.fillAmount = hp / maxHp;
    }
}
