using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Menu : MonoBehaviour
{
    public GameObject[] Selects;
    public GameObject[] Gems;
    public Text Levelscore;
    public Text Gemscore;
    private Manage Manage;
    public GameObject Nextlevelbutton;
    public GameObject Againbutton;
    public Text Levelcomplete;
    void Start()
    {
        Manage = GameObject.Find("Manage").GetComponent<Manage>();
      
        transform.SetParent(GameObject.Find("Canvas").transform, false);
        transform.localScale = Vector3.one;
        if (Levelcomplete != null)
        {
            Levelcomplete.text = "Level " + (int)Manage.Level+" complete";
        }
        if (Levelscore != null)
        {
            Levelscore.text = "Score : " + (int)Manage.Score;
        }
        if (Gemscore != null)
        {
            Gemscore.text = "Gem Score : " + (int)Manage.Gem;
        }
        if (Manage.Level >= 30)
        {
            if (Nextlevelbutton != null && Againbutton != null)
            {
                Againbutton.transform.position = new Vector3(Nextlevelbutton.transform.position.x, Nextlevelbutton.transform.position.y,
                    Nextlevelbutton.transform.position.z);
                Destroy(Nextlevelbutton);
            }
        }
    }
    public void ShowGem()
    {
        Gemscore.text = "Gem Score : " + (int)Manage.Gem;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowSelectplayer()
    {
        Manage = GameObject.Find("Manage").GetComponent<Manage>();

        if (Manage.Player == 1)
        {
            Selects[0].SetActive(true);
            Selects[1].SetActive(false);
            Selects[2].SetActive(false);
            Selects[3].SetActive(false);
            Selects[4].SetActive(false);
        }
        else if (Manage.Player == 2)
        {
            Selects[0].SetActive(false);
            Selects[1].SetActive(true);
            Selects[2].SetActive(false);
            Selects[3].SetActive(false);
            Selects[4].SetActive(false);
        }
        else if (Manage.Player == 3)
        {
            Selects[0].SetActive(false);
            Selects[1].SetActive(false);
            Selects[2].SetActive(true);
            Selects[3].SetActive(false);
            Selects[4].SetActive(false);
        }
        else if (Manage.Player == 4)
        {
            Selects[0].SetActive(false);
            Selects[1].SetActive(false);
            Selects[2].SetActive(false);
            Selects[3].SetActive(true);
            Selects[4].SetActive(false);
        }
        else if (Manage.Player == 5)
        {
            Selects[0].SetActive(false);
            Selects[1].SetActive(false);
            Selects[2].SetActive(false);
            Selects[3].SetActive(false);
            Selects[4].SetActive(true);
        }
        if (Manage.UnlockPlayer2 == 1)
        {
            Destroy(Gems[2]);
        }
        if (Manage.UnlockPlayer3 == 1)
        {
            Destroy(Gems[3]);
        }
        if (Manage.UnlockPlayer4 == 1)
        {
            Destroy(Gems[4]);
        }
        if (Manage.UnlockPlayer5 == 1)
        {
            Destroy(Gems[5]);
        }
    }
}
