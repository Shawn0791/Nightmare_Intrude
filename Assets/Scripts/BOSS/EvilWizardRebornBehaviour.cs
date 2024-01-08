using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilWizardRebornBehaviour : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //取消攻击状态
        animator.GetComponent<EvilWizard>().isAttack = false;

        if (animator.GetBool("stage2") == false)
        {
            animator.SetBool("stage2", true);
            animator.GetComponent<EvilWizardGetHit>().maxHp = 20;
        }
        else if(animator.GetBool("stage2") == true)
        {
            animator.SetBool("stage3", true);
            animator.GetComponent<EvilWizardGetHit>().maxHp = 30;
        }

        SoundService.instance.Play("BOSS_kneel");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetComponent<EvilWizardGetHit>().hp < animator.GetComponent<EvilWizardGetHit>().maxHp)
            animator.GetComponent<EvilWizardGetHit>().hp += 0.1f;
        else if (animator.GetComponent<EvilWizardGetHit>().hp >= animator.GetComponent<EvilWizardGetHit>().maxHp)
            animator.GetComponent<EvilWizardGetHit>().hp = animator.GetComponent<EvilWizardGetHit>().maxHp;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //重新可被攻击
        animator.gameObject.layer = LayerMask.NameToLayer("Enemy");
    }
}
