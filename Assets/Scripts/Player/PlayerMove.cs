using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    public float speed;
    public float startupTime;
    public static PlayerMove instance;

    private void Awake()
    {
        //单例
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        if (GameManager.instance.gameMode == GameManager.GameMode.Normal)
        {
            if (GetComponent<PlayerAttack>().isAttack == false  &&
                GetComponent<PlayerGetHit>().isVertigo == false)
            {
                movement();
            }
        }
    }

    Vector2 velocity;
    //2D移动
    void movement()
    {
        var dt = MyTime.deltaTime;
        //移动
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            rb.velocity = new Vector2(Mathf.SmoothDamp(rb.velocity.x, speed, ref velocity.x, startupTime), rb.velocity.y);
            anim.SetBool("running", true);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            rb.velocity = new Vector2(Mathf.SmoothDamp(rb.velocity.x, -speed, ref velocity.x, startupTime), rb.velocity.y);
            anim.SetBool("running", true);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            anim.SetBool("running", false);
        }


        //转向
        float horizontalMove = Input.GetAxisRaw("Horizontal");
        if (horizontalMove != 0)
            transform.localScale = new Vector3(horizontalMove, 1, 1);
    }
}