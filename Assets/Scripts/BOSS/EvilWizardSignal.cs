using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilWizardSignal : MonoBehaviour
{
    public Animator blackLine;
    public Animator boss;
    public GameObject bossHpBar;

    public void Turn()
    {
        transform.localScale = new Vector3(-1, 1, 1);
    }

    public void TimeLineEnd()
    {
        blackLine.SetTrigger("end");
        GetComponent<EvilWizard>().enabled = true;
        GameManager.instance.gameMode = GameManager.GameMode.Normal;
        boss.SetBool("stage1", true);
        bossHpBar.SetActive(true);
    }
}
