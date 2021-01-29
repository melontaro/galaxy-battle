using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Buttons : MonoBehaviour
{
    public int Playernumber;
    public GameObject Select;
    public GameObject Lock;
    private Manage Manage;
    public int Gem;
    private Menu Menu;
    public GameObject Mymenu;
    public bool Ckeckplayers;
    public bool Ckeckmylock;
    public Sprite[] Sprite;
    private Image Myimage;
    public GameObject Soundsprite;
    public bool Checksoundsprites;
    private AudioSource Clickaudio;


    void Start()
    {
        Manage = GameObject.Find("Manage").GetComponent<Manage>();
        Clickaudio = GameObject.Find("Bulletaudio").GetComponent<AudioSource>();
        if (Checksoundsprites == true)
        {
            Myimage = Soundsprite.GetComponent<Image>();
            Checksoundsprite();
        }
        if (Mymenu != null)
        {
            Menu = Mymenu.GetComponent<Menu>();
            if (Ckeckplayers == true)
            {
                Menu.ShowSelectplayer();
            }
        }
        if (Ckeckmylock == true)
        {
            CheckLocks();
        }
    }
    public void Playbutton()
    {
        if (Manage.Lock > 0)
        {
            if (Manage.Lock > 30)
            {
                Manage.Level = 30;
            }
            else if (Manage.Lock <= 30)
            {

                Manage.Level = Manage.Lock;
            }


        }
        else if (Manage.Lock <= 0)
        {
            Manage.Level = 1;
        }
        SceneManager.LoadScene("Play", LoadSceneMode.Single);
    }


    public void ShowIntrestitialAds()
    {

    }




    private void Checksoundsprite()
    {
        if (Manage.Sound == 0)
        {
            Myimage.sprite = Sprite[0];
            GameObject.Find("Menuaudio").GetComponent<AudioSource>().Play();
        }
        else if (Manage.Sound == 1)
        {
            Myimage.sprite = Sprite[1];
            GameObject.Find("Menuaudio").GetComponent<AudioSource>().Stop();
        }
    }
    public void Soundbutton()
    {
        Clickaudio.Play();
        Manage.Sound += 1;
        if (Manage.Sound >= 2)
        {
            Manage.Sound = 0;
        }
        Checksoundsprite();
    }
    public void Exitplayersmenu()
    {
        Manage.Items[0].SetActive(true);
        Manage.Items[1].SetActive(true);
        Manage.Items[2].SetActive(true);
        Manage.Items[3].SetActive(true);
        Manage.Items[4].SetActive(false);
        Manage.Items[5].SetActive(true);
        if (Manage.Sound == 0)
        {
            Clickaudio.Play();
        }

    }
    public void Selectplsyersbutton()
    {
        if (Manage.Sound == 0)
        {
            Clickaudio.Play();
        }
        Manage.Items[0] = GameObject.Find("Sound-button");
        Manage.Items[1] = GameObject.Find("Exit-button");
        Manage.Items[2] = GameObject.Find("Select-player-button");
        Manage.Items[3] = GameObject.Find("Levels-button");
        Manage.Items[4] = Mymenu;
        Manage.Items[5] = GameObject.Find("Playbutton");

        Manage.Playersmenu();

    }
    public void Levelsbutton()
    {
        if (Manage.Sound == 0)
        {
            Clickaudio.Play();
        }
        Manage.Items[0] = GameObject.Find("Sound-button");
        Manage.Items[1] = GameObject.Find("Exit-button");
        Manage.Items[2] = GameObject.Find("Select-player-button");
        Manage.Items[3] = GameObject.Find("Levels-button");
        Manage.Items[4] = Mymenu;
        Manage.Items[5] = GameObject.Find("Playbutton");
        Manage.Showlevelsmenu();

    }
    // Update is called once per frame
    void Update()
    {

    }
    public void Exitpausemenu()
    {
        if (Manage.Sound == 0)
        {
            Clickaudio.Play();
        }
        Time.timeScale = 1;
        Manage.Items[0].SetActive(true);
        Destroy(Mymenu);
    }
    public void Resumegame()
    {
        if (Manage.Sound == 0)
        {
            Clickaudio.Play();
        }
        Time.timeScale = 1;
        Manage.Items[0].SetActive(true);
        Destroy(Mymenu);
    }
    public void Pausebutton()
    {
        if (Manage.End == false)
        {
            if (Manage.Sound == 0)
            {
                Clickaudio.Play();
            }
            Manage.Items[0] = gameObject;
            Manage.Pausebutton();
        }


    }
    public void Nextlevelbutton()
    {

        if (Manage.Sound == 0)
        {
            Clickaudio.Play();
        }
        Manage.Level += 1;
        ShowIntrestitialAds();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
    public void Homebutton()
    {
        if (Manage.Sound == 0)
        {
            Clickaudio.Play();
        }
        Time.timeScale = 1;
        ShowIntrestitialAds();
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);

    }
    public void Againbutton()
    {
        if (Manage.Sound == 0)
        {
            Clickaudio.Play();
        }
        Time.timeScale = 1;
        ShowIntrestitialAds();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
    public void Exitgame()
    {
        if (Manage.Sound == 0)
        {
            Clickaudio.Play();
        }
        Application.Quit();
    }
    public void CheckLocks()
    {
        if (Playernumber == 2 && Manage.UnlockPlayer2 == 1)
        {
            Destroy(Lock);
        }
        else if (Playernumber == 3 && Manage.UnlockPlayer3 == 1)
        {
            Destroy(Lock);
        }
        else if (Playernumber == 4 && Manage.UnlockPlayer4 == 1)
        {
            Destroy(Lock);
        }
        else if (Playernumber == 5 && Manage.UnlockPlayer5 == 1)
        {
            Destroy(Lock);
        }

    }
    public void Selectplayer()
    {
        if (Manage.Sound == 0)
        {
            Clickaudio.Play();
        }
        if (Lock == null)
        {
            Manage.Player = Playernumber;
            Manage.Saveplayer();
            Menu.ShowSelectplayer();
        }
        else if (Lock != null)
        {
            if (Manage.Sound == 0)
            {
                Clickaudio.Play();
            }
            if (Manage.Gem >= Gem)
            {
                Manage.Gem -= Gem;

                Manage.Savegem();
                Menu.ShowGem();
                if (Playernumber == 2)
                {
                    Manage.UnlockPlayer2 = 1;
                }
                else if (Playernumber == 3)
                {
                    Manage.UnlockPlayer3 = 1;
                }
                else if (Playernumber == 4)
                {
                    Manage.UnlockPlayer4 = 1;
                }
                else if (Playernumber == 5)
                {
                    Manage.UnlockPlayer5 = 1;
                }
                Manage.Player = Playernumber;
                Menu.ShowSelectplayer();
                Manage.Saveplayers();
                CheckLocks();
            }
        }
    }
}
