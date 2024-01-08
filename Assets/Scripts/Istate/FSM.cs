using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateType
{
    Idle,
    Patrol,
    Chase,
    Attack,
    Change,
    Vertigo,
    Dead
}

[Serializable]
public class Parameter
{
    public float attack;
    public float moveSpeed;
    public float chaseSpeed;
    public float idleTime;

    public Type type;
    public enum Type
    {
        normal,
        red,
        green,
    }

    [Header("Attack Data")]
    public Transform target;
    public LayerMask targetLayer;
    public Transform attackPoint;
    public float attackDistance;
    public Transform viewPoint;
    public float viewDistance;
    public bool lostTarget;

    [Header("DeBug")]
    public Animator anim;
    public Rigidbody2D rb;
    public SpriteRenderer sp;

    public Transform[] patrolPoints;
    public Transform[] chasePoints;

    public Transform player;
    public bool isChanged;
    public bool isChanging;
}

public class FSM : MonoBehaviour
{
    public Parameter parameter;

    private float timer;
    private IState currentState;
    private Dictionary<StateType, IState> states = new Dictionary<StateType, IState>();

    void Start()
    {
        states.Add(StateType.Idle, new IdleState(this));
        states.Add(StateType.Patrol, new PatrolState(this));
        states.Add(StateType.Chase, new ChaseState(this));
        states.Add(StateType.Attack, new AttackState(this));
        states.Add(StateType.Change, new ChangeState(this));
        states.Add(StateType.Vertigo, new VertigoState(this));
        states.Add(StateType.Dead, new DeadState(this));

        parameter.anim = GetComponent<Animator>();
        parameter.rb = GetComponent<Rigidbody2D>();
        parameter.sp = GetComponent<SpriteRenderer>();
        parameter.player = GameObject.FindGameObjectWithTag("Player").transform;

        TransitionState(StateType.Idle);

        timer = 3;
        //Physics2D.queriesStartInColliders = false;
    }

    void Update()
    {
        FindTarget();
        currentState.OnUpdate();
    }

    //切换状态
    public void TransitionState(StateType type)
    {
        if (currentState != null)
            currentState.OnExit();
        currentState = states[type];
        currentState.OnEnter();

        //Debug.Log(type);
    }

    //角色朝向
    public void FlipTo(Vector2 target)
    {
        if (target != null)
        {
            if (transform.position.x > target.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (transform.position.x < target.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //造成伤害
        if (collision.CompareTag("Player") && currentState == states[StateType.Attack]) 
        {
            //计算方向
            Vector3 dir = transform.position - parameter.target.position;
            //传递伤害
            collision.GetComponent<PlayerGetHit>().GetHitBack(parameter.attack, dir, 150);

            //音效
            if (parameter.isChanged == false)
                SoundService.instance.Play("Scratch_hurt");
            else if(parameter.isChanged==true&&parameter.type==Parameter.Type.red)
                SoundService.instance.Play("Red_fisthurt");

            //判断角色是否死亡
            if (collision.GetComponent<PlayerGetHit>().hp <= 0)
                parameter.target = null;
        }
    }
    //发现目标
    private void FindTarget()
    {
        float direction = transform.localScale.x;
        Vector3 Dir = new Vector3(direction * parameter.viewDistance, 0, 0);
        RaycastHit2D viewRay = Physics2D.Raycast(parameter.viewPoint.position, Dir, parameter.viewDistance, parameter.targetLayer);

        if (viewRay.collider != null)
        {
            Debug.DrawLine(parameter.viewPoint.position, viewRay.point, Color.red);

            parameter.target = parameter.player;
            timer = 3;
        }
        else
        {
            Debug.DrawLine(parameter.viewPoint.position, parameter.viewPoint.position + Dir, Color.blue);
        }
        //丢失计时
        if (viewRay.collider == null && 
            timer > 0 && 
            GetComponent<EnemyGetHit>().isVertigo == false) 
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0)
        {
            parameter.target = null;
            timer = 3;
        }
    }

    public void Change()
    {
        TransitionState(StateType.Change);
    }

    public void DestoryThis()
    {
        Destroy(transform.parent.gameObject);
    }

    public void AudioScratch()
    {
        SoundService.instance.Play("Zombie_scratch");
    }

    public void AudioFist()
    {
        SoundService.instance.Play("Red_attack");
    }
}
