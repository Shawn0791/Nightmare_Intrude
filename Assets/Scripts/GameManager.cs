using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject walkerPrefab;
    public GameObject walker_greenPrefab;
    public GameObject walker_redPrefab;
    public GameObject player;
    public GameObject UI;
    public Image hpBar;
    public GameObject bloodPointPrefab;
    public GameObject bloodOverlay;
    public GameObject winMenu;
    public GameObject pauseMenu;
    public GameObject testMenu;

    private float hp;
    private float maxHp;
    private float mp;
    private float maxMp;
    private Animator anim;
    private bool blood;
    private bool pause;
    private bool test;

    public GameMode gameMode;
    public enum GameMode
    {
        Normal,
        Wait,
        Dead,
        TimeLine,
    }

    private void Awake()
    {
        //单例
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        anim = player.GetComponent<Animator>();
    }

    void Update()
    {
        PauseMenu();
        TestMenu();
    }

    public void CreateWalker(Vector3 pos)
    {
        int rand = Random.Range(0, 3);
        switch (rand)
        {
            case 0:
                GameObject walker = ObjectPool.Instance.GetObject(walkerPrefab);
                walker.transform.position = pos;
                break;
            case 1:
                GameObject walker1 = ObjectPool.Instance.GetObject(walker_greenPrefab);
                walker1.transform.position = pos;
                break;
            case 2:
                GameObject walker2 = ObjectPool.Instance.GetObject(walker_redPrefab);
                walker2.transform.position = pos;
                break;
        }
    }

    public void RefreshHp()
    {
        hp = player.GetComponent<PlayerGetHit>().hp;
        maxHp = player.GetComponent<PlayerGetHit>().maxHp;
        //血条随血量变动
        hpBar.fillAmount = hp / maxHp;
        //红屏
        if (hp <= 0.25 * maxHp)
        {
            bloodOverlay.GetComponent<Animator>().SetBool("blooding", true);
            blood = true;
        }
        else if (blood && hp > 0.25 * maxHp)
        {
            bloodOverlay.GetComponent<Animator>().SetBool("blooding", false);
            blood = false;
        }
    }

    public void SlowDownTime()
    {
        Time.timeScale = 0.5f;
        MyTime.timescale = 2;
        player.GetComponent<PlayerMove>().speed = 10;
        player.GetComponent<PlayerJump>().jumpF = 18;
        player.GetComponent<PlayerJump>().fallMultiplier = 3.8f;
        player.GetComponent<PlayerJump>().jumpMultiplier = 3;
        player.GetComponent<Rigidbody2D>().gravityScale = 4;
        SetAnimatorSpeed(anim, 2);

        Camera.main.GetComponent<PostProcessVolume>().enabled = true;
        Debug.Log("SlowDownTime_0.5x");
    }

    //public void SlowDownTime()
    //{
    //    Time.timeScale = 0.2f;
    //    MyTime.timescale = 5;
    //    player.GetComponent<PlayerMove>().speed = 25;
    //    player.GetComponent<PlayerJump>().jumpF = 38;
    //    player.GetComponent<PlayerJump>().fallMultiplier = 9.5f;
    //    player.GetComponent<PlayerJump>().jumpMultiplier = 7.5f;
    //    player.GetComponent<Rigidbody2D>().gravityScale = 10;
    //    SetAnimatorSpeed(anim, 2);
    //    Debug.Log("SlowDownTime_0.2x");
    //}

    public void RecoveryTime()
    {
        Time.timeScale = 1;
        MyTime.timescale = 1;
        player.GetComponent<PlayerMove>().speed = 5;
        player.GetComponent<PlayerJump>().jumpF = 10;
        player.GetComponent<PlayerJump>().fallMultiplier = 1.9f;
        player.GetComponent<PlayerJump>().jumpMultiplier = 1.5f;
        player.GetComponent<Rigidbody2D>().gravityScale = 2;
        SetAnimatorSpeed(anim, 1);

        Camera.main.GetComponent<PostProcessVolume>().enabled = false;
        Debug.Log("RecoveryTime_1");
    }

    private void SetAnimatorSpeed(Animator anim, float speed)
    {
        if (null == anim) return;
        anim.speed = speed;
    }

    public void CreateBloodPoint(Vector3 pos, int num)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject bead = ObjectPool.Instance.GetObject(bloodPointPrefab);
            bead.transform.position = pos;
            float rand1 = Random.Range(-1.5f, 1.5f);
            float rand2 = Random.Range(3, 6);
            bead.GetComponent<Rigidbody2D>().velocity = new Vector2(rand1, rand2);
        }
    }

    public void HidePlayerAndUI()
    {
        player.SetActive(false);
        UI.SetActive(false);
    }

    public void ShowPlayerAndUI()
    {
        player.SetActive(true);
        UI.SetActive(true);
    }

    public void GameOver()
    {
        winMenu.SetActive(true);
    }

    private void PauseMenu()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0) 
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !pause &&
                gameMode == GameMode.Normal) 
            {
                pauseMenu.SetActive(true);
                gameMode = GameMode.Wait;
                pause = true;
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && pause)
            {
                pauseMenu.SetActive(false);
                gameMode = GameMode.Normal;
                pause = false;
            }
        }
    }

    private void TestMenu()
    {
        if (Input.GetKeyDown(KeyCode.F12) && !test)
        {
            testMenu.SetActive(true);
            test = true;
        }
        else if (Input.GetKeyDown(KeyCode.F12) && test)
        {
            testMenu.SetActive(false);
            test = false;
        }
    }
}

public class MyTime
{
    public static float timescale = 1;
    public static float deltaTime
    {
        get
        {
            return Time.deltaTime * timescale;
        }
    }
}
