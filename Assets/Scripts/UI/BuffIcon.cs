using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffIcon : MonoBehaviour
{
    public static BuffIcon instance;
    public GameObject[] icons;

    private int number;
    private float time;

    private void Awake()
    {
        //单例
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public void StartBuff(int num,float time)
    {
        number = num;
        this.time = time;

        switch (num)
        {
            case 0:
                if (icons[num].activeSelf == true)
                    StopCoroutine("BuffIcon0Reveal");
                StartCoroutine("BuffIcon0Reveal");
                break;
            case 1:
                if (icons[num].activeSelf == true)
                    StopCoroutine("BuffIcon1Reveal");
                StartCoroutine("BuffIcon1Reveal");
                break;
            case 2:
                if (icons[num].activeSelf == true)
                    StopCoroutine("BuffIcon2Reveal");
                StartCoroutine("BuffIcon2Reveal");
                break;
            case 3:
                if (icons[num].activeSelf == true)
                    StopCoroutine("BuffIcon3Reveal");
                StartCoroutine("BuffIcon3Reveal");
                break;
            case 4:
                if (icons[num].activeSelf == true)
                    StopCoroutine("BuffIcon4Reveal");
                StartCoroutine("BuffIcon4Reveal");
                break;
        }
    }

    IEnumerator BuffIcon0Reveal()
    {
        icons[0].SetActive(true);
        yield return new WaitForSeconds(time);
        icons[0].SetActive(false);
    }

    IEnumerator BuffIcon1Reveal()
    {
        icons[1].SetActive(true);
        yield return new WaitForSeconds(time);
        icons[1].SetActive(false);
    }

    IEnumerator BuffIcon2Reveal()
    {
        icons[2].SetActive(true);
        yield return new WaitForSeconds(time);
        icons[2].SetActive(false);
    }

    IEnumerator BuffIcon3Reveal()
    {
        icons[3].SetActive(true);
        yield return new WaitForSeconds(time);
        icons[3].SetActive(false);
    }

    IEnumerator BuffIcon4Reveal()
    {
        icons[4].SetActive(true);
        yield return new WaitForSeconds(time);
        icons[4].SetActive(false);
    }

    public void RefreshAllIcon()
    {
        for (int i = 0; i < icons.Length; i++)
        {
            icons[i].SetActive(false);
        }
    }
}
