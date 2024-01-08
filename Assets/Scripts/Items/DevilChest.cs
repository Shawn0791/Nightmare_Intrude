using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilChest : MonoBehaviour
{
    public GameObject[] rewards;
    public GameObject buttonTips;

    private bool canOpen;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(canOpen)
            OpenChest();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            buttonTips.SetActive(true);
            canOpen = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            buttonTips.SetActive(false);
            canOpen = false;
        }
    }

    private void OpenChest()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            anim.SetTrigger("open");
            GetComponent<BoxCollider2D>().enabled = false;

            SoundService.instance.Play("ChestOpen");
        }
    }

    public void CreateChestRewards()
    {
        for (int i = 0; i < rewards.Length; i++)
        {
            float rand1 = Random.Range(-1.5f, 1.5f);
            float rand2 = Random.Range(5f, 6f);
            GameObject body = ObjectPool.Instance.GetObject(rewards[i]);
            body.transform.position = new Vector2(transform.position.x, transform.position.y + 1);
            body.GetComponent<Rigidbody2D>().velocity = new Vector2(rand1, rand2);
        }
    }
}
