using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeState : IState
{
    private FSM manager;
    private Parameter parameter;

    private AnimatorStateInfo info;

    public ChangeState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }

    public void OnEnter()
    {
        parameter.anim.Play("change");
        parameter.isChanging = true;

        SoundService.instance.Play("Zombie_change");
    }

    public void OnUpdate()
    {
        info = parameter.anim.GetCurrentAnimatorStateInfo(0);

        if (info.normalizedTime >= 0.95f)
        {
            manager.TransitionState(StateType.Chase);
            parameter.isChanging = false;
        }
    }

    public void OnExit()
    {
        if (parameter.type == Parameter.Type.red)
        {
            parameter.attack = 2;
            parameter.moveSpeed = 3;
            parameter.chaseSpeed = 3.5f;
        }
        else if (parameter.type == Parameter.Type.green)
        {
            parameter.attackDistance = 4;
            parameter.viewDistance = 4.5f;
            parameter.moveSpeed = 0.5f;
            parameter.chaseSpeed = 0.8f;
        }
    }
}
