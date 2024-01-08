using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilWizardStage2MoveBehaviour : StateMachineBehaviour
{
    private Transform player;
    private Transform wizard;
    private Rigidbody2D rb;
    private float timer;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = animator.GetComponent<EvilWizard>().player;
        wizard = animator.transform;
        rb = animator.GetComponent<Rigidbody2D>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetComponent<EvilWizard>().target != null)
        {
            Turn();

            if (Mathf.Abs(player.transform.position.x - wizard.position.x) <= 3 &&
                timer > 0)
            {
                MoveTime();
                //远离
                if (player.transform.position.x < wizard.position.x)
                    rb.velocity = new Vector2(10, 0);
                else
                    rb.velocity = new Vector2(-10, 0);
            }
            else if (Mathf.Abs(player.transform.position.x - wizard.position.x) > 5 &&
                timer > 0)
            {
                MoveTime();
                //接近
                if (player.transform.position.x > wizard.position.x)
                    rb.velocity = new Vector2(5, 0);
                else
                    rb.velocity = new Vector2(-5, 0);
            }
            else
            {
                animator.SetBool("running", false);
                rb.velocity = Vector2.zero;
                timer = 2;
            }
        }
        else
            animator.SetBool("running", false);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
    void MoveTime()
    {
        if (timer > 0)
            timer -= Time.deltaTime;
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
