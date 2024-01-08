using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IState
{
    private FSM manager;
    private Parameter parameter;
    private float timer;

    public ChaseState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        if (parameter.isChanged == false)
        {
            parameter.anim.Play("walk");
        }
        else
        {
            parameter.anim.Play("walk2");
        }
    }

    public void OnUpdate()
    {
        //朝向目标
        if (parameter.target != null) 
        {
            manager.FlipTo(parameter.target.position);
        }

        //检测到目标则切换为攻击状态
        float direction = manager.transform.localScale.x;
        Vector3 Dir = new Vector3(direction * parameter.attackDistance, 0, 0);
        RaycastHit2D attackRay = Physics2D.Raycast(parameter.attackPoint.position, Dir, parameter.attackDistance, parameter.targetLayer);

        if (attackRay.collider != null)
        {
            Debug.DrawLine(parameter.attackPoint.position, attackRay.point, Color.red);

            if (parameter.type == Parameter.Type.green &&
                parameter.isChanged == true) 
            {
                if (timer <= 0)
                {
                    timer = 3;
                    SoundService.instance.Play("Zombie_attack");
                    manager.TransitionState(StateType.Attack);
                }
            }
            else
            {
                SoundService.instance.Play("Zombie_attack");
                manager.TransitionState(StateType.Attack);
            }
        }
        else
        {
            Debug.DrawLine(parameter.attackPoint.position, parameter.attackPoint.position + Dir, Color.green);
        }

        //不在攻击范围内则向目标靠近
        if (parameter.target && attackRay.collider == null) 
        {
            manager.transform.position = Vector2.MoveTowards(manager.transform.position,
                parameter.target.position, parameter.chaseSpeed * Time.deltaTime);
        }

        //丢失目标恢复静止
        if (parameter.target == null ||
            manager.transform.position.x < parameter.chasePoints[0].position.x ||
            manager.transform.position.x > parameter.chasePoints[1].position.x)
        {
            manager.TransitionState(StateType.Idle);
        }

        if (timer > 0)
            timer -= Time.deltaTime;
    }

    public void OnExit()
    {
        
    }
}
