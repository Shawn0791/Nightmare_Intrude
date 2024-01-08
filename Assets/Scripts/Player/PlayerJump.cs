using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private int jumpCount;
    private bool S;

    public GameObject jumpVFXPrefab;
    [Header("跳跃属性")]
    public bool isJumping;
    public bool isOnGround;
    public float jumpF;
    public float fallMultiplier;
    public float jumpMultiplier;
    public int jumpNum;

    [Header("地面检测")]
    public LayerMask groundLayerMask;
    public Transform leftLeg;
    public Transform rightLeg;
    public float distance;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(GameManager.instance.gameMode == GameManager.GameMode.Normal)
        {
            if (GetComponent<PlayerGetHit>().isVertigo == false &&
                GetComponent<PlayerAttack>().isAttack == false)
            {
                NormalJump();
            }
        }

        isOnGround = OnGround();
        PressKeyD();
        JumpOffPlatform();
    }

    private void NormalJump()
    {
        float dt = MyTime.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < jumpNum && !S)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpF);
            isJumping = true;
            jumpCount++;

            GameObject vfx = ObjectPool.Instance.GetObject(jumpVFXPrefab);
            vfx.transform.position = transform.position;
            SoundService.instance.Play("Player_jumpup");
        }
        if (isOnGround && Input.GetAxisRaw("Jump") == 0)
        {
            isJumping = false;
            jumpCount = 0;
            //设置动画
            anim.SetBool("drop", false);
            anim.SetBool("rise", false);
        }
        //玩家上升
        else if (rb.velocity.y > 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (jumpMultiplier - 1) * dt;
        }
        //玩家下坠
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * dt;
            isJumping = true;
        }
        //设置动画
        if (!isOnGround)
        {
            float latestY = transform.position.y;
            if (rb.velocity.y > 0)
            {
                //设置动画
                anim.SetBool("drop", false);
                anim.SetBool("rise", true);
            }
            else if (rb.velocity.y < 0)
            {
                //设置动画
                anim.SetBool("drop", true);
                anim.SetBool("rise", false);
            }
        }
    }
    //地面检测碰撞器
    bool OnGround()
    {
        RaycastHit2D leftRay = Physics2D.Raycast(leftLeg.position, Vector2.down, distance, groundLayerMask);
        RaycastHit2D rightRay = Physics2D.Raycast(rightLeg.position, Vector2.down, distance, groundLayerMask);
        if (leftRay.collider != null)
            Debug.DrawLine(leftLeg.position, leftRay.point, Color.red);
        else
            Debug.DrawLine(leftLeg.position, leftLeg.position + Vector3.down * distance, Color.green);
        if (rightRay.collider != null)
            Debug.DrawLine(rightLeg.position, rightRay.point, Color.red);
        else
            Debug.DrawLine(rightLeg.position, rightLeg.position + Vector3.down * distance, Color.green);

        if (leftRay.collider != null || rightRay.collider != null)
            return true;
        else
            return false;
    }

    private void PressKeyD()
    {
        if (Input.GetKeyDown(KeyCode.S))
            S = true;
        if (Input.GetKeyUp(KeyCode.S))
            S = false;
    }

    private void JumpOffPlatform()
    {
        RaycastHit2D DownRay = Physics2D.Raycast(leftLeg.position, Vector2.down, distance, groundLayerMask);
        if (DownRay.collider != null)
        {
            if (DownRay.collider.TryGetComponent<PlatformEffector2D>(out PlatformEffector2D platform2D))
            {
                GoDown();
            }
        }
    }

    private void GoDown()
    {
        if (Input.GetKey(KeyCode.S) && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(ColliderShutDown());
        }
    }

    IEnumerator ColliderShutDown()
    {
        transform.GetComponent<CapsuleCollider2D>().enabled = false;
        yield return new WaitForSeconds(0.3f);
        transform.GetComponent<CapsuleCollider2D>().enabled = true;
    }
}