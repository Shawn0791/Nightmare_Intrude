using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    private FSM manager;
    private Parameter parameter;

    private int patrolPosition;
    private int sign;

    public PatrolState(FSM manager)
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

        SoundService.instance.Play("Zombie_idle");
    }

    public void OnUpdate()
    {
        //持续朝向巡逻方向
        manager.FlipTo(parameter.patrolPoints[patrolPosition].position);

        //移动到巡逻点
        manager.transform.position = Vector2.MoveTowards(manager.transform.position,
            parameter.patrolPoints[patrolPosition].position, parameter.moveSpeed * Time.deltaTime);

        //接近目标切换为追击状态
        if (parameter.target != null )
        {
            manager.TransitionState(StateType.Chase);
        }

        //到达巡逻点
        if (Mathf.Abs(manager.transform.position.x - parameter.patrolPoints[patrolPosition].position.x) < 0.1f) 
        {
            //结束巡逻开始静止
            manager.TransitionState(StateType.Idle);
        }
    }

    public void OnExit()
    {
        //下一个巡逻点
        patrolPosition++;

        //防止超出数量
        if (patrolPosition >= parameter.patrolPoints.Length)
        {
            patrolPosition = 0;
        }
    }
}
