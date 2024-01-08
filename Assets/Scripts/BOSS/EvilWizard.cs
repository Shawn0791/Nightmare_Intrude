using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilWizard : MonoBehaviour
{
    [Header("Attack Data")]
    public Transform target;
    public Transform player;
    public float attack;
    public LayerMask targetLayer;
    public Transform viewPoint;
    public float viewDistance;
    public bool lostTarget;
    public bool isAttack;
    [Header("VFX")]
    public GameObject fireCreaterPrefab;
    public GameObject waterPrefab;
    public Transform waterPos;
    public GameObject lightningPrefab;
    public GameObject venomPrefab;
    public Transform venomPos;
    public GameObject tentPrefab;

    private float timer;
    private Animator anim;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = player;
    }

    void Update()
    {

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
        if (collision.CompareTag("Player") && isAttack) 
        {
            //计算方向
            Vector3 dir = transform.position - target.position;
            //传递伤害
            collision.GetComponent<PlayerGetHit>().GetHitBack(attack, dir, 150);
        }
        if (collision.CompareTag("Head") && !isAttack)
        {
            anim.SetTrigger("water");
        }
    }

    //发现目标
    private void FindTarget()
    {
        float direction = transform.localScale.x;
        Vector3 Dir = new Vector3(direction * viewDistance, 0, 0);
        RaycastHit2D viewRay = Physics2D.Raycast(viewPoint.position, Dir, viewDistance, targetLayer);

        if (viewRay.collider != null)
        {
            Debug.DrawLine(viewPoint.position, viewRay.point, Color.red);

            target = player;
            timer = 3;
        }
        else
        {
            Debug.DrawLine(viewPoint.position, viewPoint.position + Dir, Color.blue);
        }
        //丢失计时
        if (viewRay.collider == null &&
            timer > 0 &&
            GetComponent<EvilWizardGetHit>().isVertigo == false)
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0)
        {
            target = null;
            timer = 3;
        }
    }

    public void FireSkill()
    {
        StartCoroutine(CreateFire());
    }

    IEnumerator CreateFire()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject fireCreater = ObjectPool.Instance.GetObject(fireCreaterPrefab);
            fireCreater.transform.position = new Vector2(transform.position.x + transform.localScale.x * (i + 3), transform.position.y);
            yield return new WaitForSeconds(0.3f);
        }
    }

    public void WaterSkill()
    {
        GameObject wall = ObjectPool.Instance.GetObject(waterPrefab);
        wall.transform.position = waterPos.position;
    }

    public void LightningSkill()
    {
        StartCoroutine(CreateLightning());
    }

    IEnumerator CreateLightning()
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject lightning1 = ObjectPool.Instance.GetObject(lightningPrefab);
            lightning1.transform.position = new Vector2(transform.position.x + (i + 1), transform.position.y);
            GameObject lightning2 = ObjectPool.Instance.GetObject(lightningPrefab);
            lightning2.transform.position = new Vector2(transform.position.x - (i + 1), transform.position.y);
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void VenomSkill()
    {
        StartCoroutine(CreateVenom());
    }

    IEnumerator CreateVenom()
    {
        for (int i = 0; i < 8; i++)
        {
            GameObject venom = ObjectPool.Instance.GetObject(venomPrefab);
            venom.transform.position = venomPos.position;
            float randX = Random.Range(-3, 3);
            venom.GetComponent<Rigidbody2D>().velocity = new Vector2(randX,6);
            yield return new WaitForSeconds(0.3f);
        }
    }

    public void CreateTent()
    {
        GameObject tent = ObjectPool.Instance.GetObject(tentPrefab);
        tent.transform.position = transform.position;
    }
}
