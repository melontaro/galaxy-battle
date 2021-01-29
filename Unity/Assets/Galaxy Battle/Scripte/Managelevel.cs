using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managelevel : MonoBehaviour
{
    public int Level;
   
    public GameObject[] Enemybox;
    public GameObject[] Leftenemies;
    public GameObject[] Rightenemies;
    public GameObject[] Upenemies;
    public GameObject [] Bonus;
    private Manage Manage;
    private GameObject Output;
    private float Timer1;
    private float Timer2;
    private float Timer3;
    private float Timer4;
    private int Count1;
    private int Count2;
    private int Count3;
    public int chance;
    private int Checkchance;
    private bool instantiatRight;
    private bool instantiatLeft;
    private bool instantiatUp;
    public bool AutoBonus;
    public float AutoBonusTimer;
    void Start()
    {
        Manage = GameObject.Find("Manage").GetComponent<Manage>();
        instantiatebonus();
        Instantiate(Enemybox[Level], new Vector3(0, 11, 0), Quaternion.identity);
        if (Rightenemies[Level] != null)
        {
            instantiatRight = true;

        }
        if (Leftenemies[Level] != null)
        {
            instantiatLeft = true;

        }
        if (Upenemies[Level] != null)
        {
            instantiatUp = true;

        }
    }

    void resetrightenemies()
    {
        instantiatRight = false;
        Count1 = 0;
        Timer1 = 0;
    }
    void resetLeftenemies()
    {
        instantiatLeft = false;
        Count2 = 0;
        Timer2 = 0;
    }
    void resetUpenemies()
    {
        instantiatUp = false;
        Count3 = 0;
        Timer3 = 0;
    }
    void instantiatebonus()
    {
        Checkchance = Random.Range(0, 101);
        if (Checkchance <= chance)
        {
            Instantiate(Bonus[Random.Range(0,4)], new Vector3(Random.Range(-4f, 4), 11.5f, 0), Quaternion.identity);
        }
    }
    void Update()
    {

        if (AutoBonus == transform)
        {
            Timer4 += 1 * Time.deltaTime;
            if(Timer4>= AutoBonusTimer)
            {
                Checkchance = Random.Range(0, 101);
                  Instantiate(Bonus[Random.Range(0, 4)], new Vector3(Random.Range(-4f, 4), 11.5f, 0), Quaternion.identity);
               
                Timer4 = 0;
            }
        }


        //--------instantiatRight&&instantiatLeft&&instantiatup Enemies

        if (instantiatRight == true)
        {
            Timer1 += 1 * Time.deltaTime;
            if (Timer1 >= 0.5f)
            {
                Instantiate(Rightenemies[Level], new Vector3(-6f, -2f, 0), Quaternion.identity);
                Count1 += 1;
                if (Count1 >= 8)
                {
                    resetrightenemies();
                }
                Timer1 = 0;
            }
        }
        if (instantiatLeft == true)
        {
            Timer2 += 1 * Time.deltaTime;
            if (Timer2 >= 0.5f)
            {
                Instantiate(Leftenemies[Level], new Vector3(6f, -2f, 0), Quaternion.identity);
                Count2 += 1;
                if (Count2 >= 8)
                {
                    resetLeftenemies();
                }
                Timer2 = 0;
            }
        }
        if (instantiatUp == true)
        {
            Timer3 += 1 * Time.deltaTime;
            if (Timer3 >= 0.5f)
            {
                Instantiate(Upenemies[Level], new Vector3(0, 11f, 0), Quaternion.identity);
                Count3 += 1;
                if (Count3 >= 8)
                {
                    resetUpenemies();
                }
                Timer3 = 0;
            }
        }
    }
    public void Nextenemybox()
    {
        
        Level += 1;
        
        if (Enemybox[Level]!=null)
        {
             Instantiate(Enemybox[Level], new Vector3(0, 11, 0), Quaternion.identity);
            instantiatebonus();
        }
        if (Rightenemies[Level] != null)
        {
            instantiatRight = true;

        }
        if (Upenemies[Level] != null)
        {
            instantiatUp = true;

        }
        if (Leftenemies[Level] != null)
        {
            instantiatLeft = true;

        }
        if (Enemybox[Level] == null)
        {
            Manage.Endgame();
            Destroy(gameObject);

        }
    }
}
