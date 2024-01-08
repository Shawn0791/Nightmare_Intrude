using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    public GameObject[] headSlot;
    public GameObject[] allHeads;
    public GameObject pickupUI;
    public GameObject itemPoint;
    public GameObject headIcon;
    public int headInt;

    private GameObject head;
    private bool fistPick;

    void Start()
    {
        
    }

    void Update()
    {
        PickupHead();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Head"))
        {
            pickupUI.SetActive(true);
            headInt = collision.GetComponent<Head>().headInt;
            head = collision.gameObject;
        }
        else
        {
            pickupUI.SetActive(false);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Head"))
        {
            pickupUI.SetActive(false);
            headInt = -1;
        }
    }

    private void PickupHead()
    {
        if (Input.GetKeyDown(KeyCode.C) &&
            pickupUI.activeSelf == true) 
        {
            if (headSlot[0] == null)
            {
                headSlot[0] = allHeads[headInt];
                RefreshHead();
                head.GetComponent<Head>().DestroyThis();
            }
            else
            {
                DropHead();
                headSlot[0] = allHeads[headInt];
                RefreshHead();
                head.GetComponent<Head>().DestroyThis();
            }
            //首次拾取教学
            if (!fistPick)
            {
                Popup();
                fistPick = true;
            }

            SoundService.instance.Play("Player_pickup");
        }
    }

    private void DropHead()
    {
        Debug.Log("drop");
        GameObject head = ObjectPool.Instance.GetObject(headSlot[0]);
        head.transform.position = new Vector2(transform.position.x, transform.position.y + 0.5f);
    }

    public void RefreshHead()
    {
        for (int i = 0; i < itemPoint.transform.childCount; i++)
        {
            Transform item = itemPoint.transform.GetChild(i);
            if (item.GetComponent<Head>().headInt == headInt)
            {
                item.gameObject.SetActive(true);
                headIcon.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                item.gameObject.SetActive(false);
                headIcon.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    private void Popup()
    {
        SoundService.instance.Play("Popup");
        Time.timeScale = 0;

        var data = new ConfirmboxBehaviour.ConfirmBoxData();
        data.btnClose = false;
        data.btnBgClose = true;
        data.btnLeft = false;
        data.btnRight = false;
        data.title = "Throw Skill [E]";
        data.content = " You can throw the enemy's head by 'E' to dizzy an enemy or destroy LightTrap." +
            "\n\nTap Anything To Continue";
        data.btnLeftTxt = "Sure";
        data.btnLeftAction = () =>
        {
            SoundService.instance.Play("Tap");
        };
        data.btnRightTxt = "Can it talk?";
        data.btnRightAction = () =>
        {
            SoundService.instance.Play("Tap");
        };
        ConfirmboxBehaviour.instance.Setup(data);
        ConfirmboxBehaviour.instance.Show();
    }
}
