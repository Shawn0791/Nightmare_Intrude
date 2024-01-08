using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RestartMenu : MonoBehaviour
{
    public GameObject player;
    public GameObject bloodOverlay;

    public void RestartGame()
    {
        RefreshPlayer();

        ObjectPoolDisable();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        gameObject.SetActive(false);
    }

    public void MainMenu()
    {
        RefreshPlayer();

        ObjectPoolDisable();
        GameManager.instance.HidePlayerAndUI();

        SceneManager.LoadScene(0);
        gameObject.SetActive(false);
    }

    private void RefreshPlayer()
    {
        //重置状态
        player.GetComponent<PlayerGetHit>().hp = player.GetComponent<PlayerGetHit>().maxHp;
        GameManager.instance.RefreshHp();
        bloodOverlay.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        player.GetComponent<Animator>().Play("Player_idle");
        player.GetComponent<Animator>().SetBool("running", false);
        player.GetComponent<Animator>().SetBool("rise", false);
        player.GetComponent<Animator>().SetBool("drop", false);
        player.layer = LayerMask.NameToLayer("Player");
        player.GetComponent<PlayerGetHit>().StopCoroutine("ContinuousBloodLoss");
        BuffIcon.instance.RefreshAllIcon();
        //恢复控制
        GameManager.instance.gameMode = GameManager.GameMode.Normal;
    }

    private void ObjectPoolDisable()
    {
        GameObject objectPool = GameObject.Find("ObjectPool");
        //对象池物体取消激活
        if (objectPool != null)
        {
            for (int i = 0; i < objectPool.transform.childCount; i++)
            {
                Transform child = objectPool.transform.GetChild(i);
                for (int a = 0; a < child.childCount; a++)
                {
                    child.GetChild(a).gameObject.SetActive(false);
                }
            }
        }
    }
}
