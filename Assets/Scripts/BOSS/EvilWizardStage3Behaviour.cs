using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilWizardStage3Behaviour : StateMachineBehaviour
{
    private Transform player;
    private Transform wizard;
    private int rand;
    private float waitTimer;
    private float tentTimer;

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

        if (animator.GetComponent<EvilWizard>().target != null ) 
        {
            Turn();

            if (tentTimer <= 0)
            {
                animator.SetTrigger("tent");
                tentTimer = 120;
            }

            if (waitTimer <= 0)
            {
                if (Mathf.Abs(player.transform.position.x - wizard.position.x) <= 3)
                {
                    rand = Random.Range(0, 8);
                    if (rand == 0)
                        animator.SetTrigger("lightning");
                    else if (rand == 1)
                        animator.SetTrigger("venom");
                    else if (rand == 2)
                    {
                        animator.SetTrigger("attack1");
                        animator.GetComponent<EvilWizard>().isAttack = true;
                    }
                    else if (rand == 3)
                    {
                        animator.SetTrigger("attack2");
                        animator.GetComponent<EvilWizard>().isAttack = true;
                    }
                    else
                    {
                        //远离
                        animator.SetBool("running", true);
                    }
                }
                else if (Mathf.Abs(player.transform.position.x - wizard.position.x) > 3 &&
                    Mathf.Abs(player.transform.position.x - wizard.position.x) <= 6)
                {
                    rand = Random.Range(0, 3);
                    if (rand == 0)
                        animator.SetTrigger("lightning");
                    else if (rand == 1)
                        animator.SetTrigger("venom");
                    else
                        animator.SetTrigger("fire");
                }
                else
                {
                    rand = Random.Range(0, 2);
                    if (rand == 0)
                        animator.SetTrigger("fire");
                    else
                    {
                        //接近
                        animator.SetBool("running", true);
                    }
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

        if (tentTimer > 0)
            tentTimer -= Time.deltaTime;
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
