using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Barlife : MonoBehaviour
{
    public Slider Myslider;
    public Text Scorelevel;
    private Manage Manage;

    void Start()
    {
        Manage = GameObject.Find("Manage").GetComponent<Manage>();
        Myslider = GetComponent<Slider>();
        Manage.Life = 100000000000;
        Manage.Score = 0;
        Showbarlife();
        ShowScore();

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Showbarlife()
    {
        Myslider.value = Manage.Life / 100;
    }
    public void ShowScore()
    {
        Scorelevel.text = "SCORE : " + (int)Manage.Score;
    }
}
