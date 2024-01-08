using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilWizardStage1Behaviour : StateMachineBehaviour
{
    private Transform player;
    private Transform wizard;
    private int rand;
    private float waitTimer;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = animator.GetComponent<EvilWizard>().player;
        wizard = animator.transform;
        //取消攻击状态
        animator.GetComponent<EvilWizard>().isAttack = false;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        WaitTime();

        if (animator.GetComponent<EvilWizard>().target != null)
        {
            Turn();

            if (waitTimer <= 0)
            {
                if (Mathf.Abs(player.transform.position.x - wizard.position.x) <= 3)
                {
                    rand = Random.Range(0, 2);

                    if (rand == 0)
                    {
                        animator.SetTrigger("attack1");
                        animator.GetComponent<EvilWizard>().isAttack = true;
                    }
                    else
                    {
                        animator.SetTrigger("attack2");
                        animator.GetComponent<EvilWizard>().isAttack = true;
                    }
                }
                else
                {
                    //接近
                    animator.SetBool("running", true);
                }
            }
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        waitTimer = 1;
    }

    void WaitTime()
    {
        if (waitTimer > 0)
            waitTimer -= Time.deltaTime;
    }

    //转向
    void Turn()
    {
        if (player.transform.position.x > wizard.position.x)
        {
            wizard.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            wizard.localScale = new Vector3(-1, 1, 1);
        }
    }
}
