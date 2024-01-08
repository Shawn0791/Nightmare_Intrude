using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRefresh : MonoBehaviour
{
    public GameObject door;

    private GameObject monster;

    void Start()
    {
        monster = GameObject.FindGameObjectWithTag("Monster");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && door.GetComponent<Door>().noMonster == false) 
        {
            if (monster.transform.childCount == 0)
            {
                door.GetComponent<Door>().noMonster = true;
                door.GetComponent<Animator>().SetTrigger("open");

                SoundService.instance.Play("DoorOpen");
            }
            else
                CheckMonster();
        }
    }

    private void CheckMonster()
    {
        for (int i = 0; i < monster.transform.childCount; i++)
        {
            if (monster.transform.GetChild(i).gameObject.activeSelf == true)
                return;
            else
            {
                if (i == monster.transform.childCount - 1) 
                {
                    door.GetComponent<Door>().noMonster = true;
                    door.GetComponent<Animator>().SetTrigger("open");

                    SoundService.instance.Play("DoorOpen");
                }
            }
        }
    }
}
