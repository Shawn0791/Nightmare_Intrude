using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingTips : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            Popup();
            Destroy(gameObject);
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
        data.title = "Healing statue";
        data.content = " Breaking the statue can get a recovery Buff." +
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
