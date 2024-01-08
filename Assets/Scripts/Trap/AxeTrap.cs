using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeTrap : MonoBehaviour
{
    public float damage;
    public float fistTime;
    [Header("陷阱数据")]
    public float downSpeed;
    public float upSpeed;
    public float waitTime;
    public Transform target;

    private Vector2 originPos;
    private Animator anim;
    private bool down;
    private float timer;
    private float posY;
    private bool hurt;

    void Start()
    {
        anim = GetComponent<Animator>();
        originPos = transform.position;
        posY = originPos.y;
        down = true;
        timer = fistTime;
    }

    private void Update()
    {
        AxeMove();

        if (timer > 0)
            timer -= Time.deltaTime;
    }

    private void AxeMove()
    {
        transform.position = new Vector2(transform.position.x, posY);

        if (timer <= 0)
        {
            if (posY > target.position.y && down)
            {
                posY -= downSpeed;
                anim.SetBool("down", true);
            }
            else if (posY < originPos.y && !down) 
            {
                posY += upSpeed;
            }
        }

        if (posY <= target.position.y && down)
        {
            down = !down;
            timer = waitTime;

            SoundService.instance.Play("Axe");
        }
        else if (posY >= originPos.y && !down)
        {
            down = !down;
            timer = waitTime;
            anim.SetBool("down", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //角色在斧子里，取消碰撞
            GetComponent<EdgeCollider2D>().enabled = false;
            if (!hurt)
            {
                collision.GetComponent<PlayerGetHit>().GetHitBack(damage, Vector3.zero, 0);
                hurt = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //角色离开斧子，添加碰撞
            GetComponent<EdgeCollider2D>().enabled = true;
            hurt = false;
        }
    }
}
