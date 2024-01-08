using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light : MonoBehaviour
{
    private float timer;
    private float rotateZ;
    private float originZ;
    private bool turn;
    private bool createEnemy;

    public float rotateSpeed;
    public float waitTime;
    public GameObject enemy;
    public Transform createPos;

    void Start()
    {
        originZ = rotateZ = transform.rotation.eulerAngles.y;
    }

    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, rotateZ);

        if (timer > 0)
            timer -= Time.deltaTime;

        if (timer <= 0)
        {
            if (!turn && rotateZ > originZ - 30)
                rotateZ -= rotateSpeed;
            else if (turn && rotateZ < originZ + 30)
                rotateZ += rotateSpeed;
        }

        if (rotateZ < originZ - 30 && !turn)
        {
            timer = waitTime;
            turn = true;
        }
        else if (rotateZ > originZ + 30 && turn)
        {
            timer = waitTime;
            turn = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            if (!createEnemy)
            {
                GameManager.instance.CreateWalker(createPos.position);
                enemy.SetActive(true);

                createEnemy = true;
            }
            //眩晕主角
            collision.transform.GetComponent<PlayerGetHit>().Vertigo();
        }
    }
}
