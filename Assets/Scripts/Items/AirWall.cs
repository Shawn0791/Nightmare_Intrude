using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirWall : MonoBehaviour
{
    public GameObject walker;

    private bool popupDone;

    private void Update()
    {
        if (walker.activeSelf == false && !popupDone) 
        {
            GetComponent<BoxCollider2D>().enabled = false;
            popupDone = true;
            Popup();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponent<BoxCollider2D>().enabled = false;
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
        data.title = "Execute";
        data.content = "When you use execution to kill an enemy, the enemy will be dismembered. \nYou can pick up the Head and use it." +
            "\n\nTap Anything To Continue";
        data.btnLeftTxt = "Why not";
        data.btnLeftAction = () =>
        {
            SoundService.instance.Play("Tap");
        };
        data.btnRightTxt = "Disgusting";
        data.btnRightAction = () =>
        {
            SoundService.instance.Play("Tap");
        };
        ConfirmboxBehaviour.instance.Setup(data);
        ConfirmboxBehaviour.instance.Show();
    }
}
