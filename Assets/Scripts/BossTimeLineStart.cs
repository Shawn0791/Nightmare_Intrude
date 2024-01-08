using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BossTimeLineStart : MonoBehaviour
{
    public PlayableDirector bossPlayable;
    public GameObject boss;
    public Animator blackLine;

    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.gameMode = GameManager.GameMode.TimeLine;
            //角色静止
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            player.GetComponent<Animator>().SetBool("running", false);
            player.GetComponent<Animator>().SetBool("rise", false);
            player.GetComponent<Animator>().SetBool("drop", false);
            player.GetComponent<Animator>().Play("Player_idle");
            //播放过场动画
            blackLine.SetTrigger("start");
            bossPlayable.Play();
            //仅触发一次
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
