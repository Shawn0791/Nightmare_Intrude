using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertigoState : IState
{
    private FSM manager;
    private Parameter parameter;
    private float timer;

    public VertigoState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        timer = 5f;
        parameter.target = parameter.player;
        if (parameter.isChanged == false)
        {
            parameter.anim.Play("vertigo");
        }
        else
        {
            parameter.anim.Play("vertigo2");
        }
    }

    public void OnExit()
    {
        manager.GetComponent<EnemyGetHit>().isVertigo = false;
    }

    public void OnUpdate()
    {
        if (timer > 0)
            timer -= Time.deltaTime;
        else
            manager.TransitionState(StateType.Idle);
    }
}
