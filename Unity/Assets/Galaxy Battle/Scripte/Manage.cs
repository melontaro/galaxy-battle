using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Manage : MonoBehaviour
{
    public GameObject Myplayer;
    public bool Move;
    public int Level;
    public GameObject Targetposition;
    public int Score;
    public float Life;
    public int Gun;
    public int Player;
    public int Gem;
    public GameObject[] Items;
    public int Sound;
    public int Lock;
    public int UnlockPlayer2;
    public int UnlockPlayer3;
    public int UnlockPlayer4;
    public int UnlockPlayer5;
    public GameObject Pausemenu;
    public GameObject Winmenu;
    public GameObject Losemenu;
    public bool End;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        StartCoroutine(Logo());



        if (Player == 0)
        {
            Player = 1;
        }
        Lock = PlayerPrefs.GetInt("Lock", Lock);
        Player = PlayerPrefs.GetInt("Player", Player);
        UnlockPlayer2 = PlayerPrefs.GetInt("UnlockPlayer2", UnlockPlayer2);
        UnlockPlayer3 = PlayerPrefs.GetInt("UnlockPlayer3", UnlockPlayer3);
        UnlockPlayer4 = PlayerPrefs.GetInt("UnlockPlayer4", UnlockPlayer4);
        UnlockPlayer5 = PlayerPrefs.GetInt("UnlockPlayer5", UnlockPlayer5);
        Gem = PlayerPrefs.GetInt("Gem", Gem);

    }
    IEnumerator Logo()
    {
        yield return new WaitForSeconds(3.5f);
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    public void Saveplayer()
    {
        PlayerPrefs.SetInt("Player", Player);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Saveplayers()
    {
        PlayerPrefs.SetInt("UnlockPlayer2", UnlockPlayer2);
        PlayerPrefs.SetInt("UnlockPlayer3", UnlockPlayer3);
        PlayerPrefs.SetInt("UnlockPlayer4", UnlockPlayer4);
        PlayerPrefs.SetInt("UnlockPlayer5", UnlockPlayer5);
    }
    public void Savegem()
    {
        PlayerPrefs.SetInt("Gem", Gem);

    }
    public void Savelock()
    {
        PlayerPrefs.SetInt("Lock", Lock);

    }
    public void Playersmenu()
    {
        Items[0].SetActive(false);
        Items[1].SetActive(false);
        Items[2].SetActive(false);
        Items[3].SetActive(false);
        Items[4].SetActive(true);
        Items[5].SetActive(false);
    }
    public void Showlevelsmenu()
    {
        Items[0].SetActive(false);
        Items[5].SetActive(false);
        Items[1].SetActive(false);
        Items[2].SetActive(false);
        Items[3].SetActive(false);
        Items[4].SetActive(true);

    }
    public void Pausebutton()
    {
        Items[0].SetActive(false);
        Instantiate(Pausemenu, new Vector3(0, 0, transform.position.z), Quaternion.identity);
        Time.timeScale = 0;
    }
    public void Endgame()
    {
        if (Myplayer != null)
        {
            StartCoroutine(Instantiatewinmenu());
            Myplayer.GetComponent<PlanePlayer>().Moveup = true;
        }



    }
    public void Losegame()
    {
        End = true;
        StartCoroutine(Instantiatelosemenu());

    }
    IEnumerator Instantiatelosemenu()
    {
        yield return new WaitForSeconds(1.5f);
        if (End == true)
        {
            Instantiate(Losemenu, new Vector3(0, 0, transform.position.z), Quaternion.identity);
            Time.timeScale = 0;
        }


    }
    IEnumerator Instantiatewinmenu()
    {
        yield return new WaitForSeconds(3f);
        if (Myplayer != null)
        {
            SaveLock();
            Instantiate(Winmenu, new Vector3(0, 0, transform.position.z), Quaternion.identity);

        }

    }
    public void SaveLock()
    {
        if (Lock > Level)
        {
            Gem += 5;

        }
        else if (Lock <= Level)
        {
            Gem += 25;
            Lock = Level + 1;
        }

        PlayerPrefs.SetInt("Lock", Lock);
        PlayerPrefs.SetInt("Gem", Gem);
    }

}
