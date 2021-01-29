using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giant1 : MonoBehaviour
{
   
    public GameObject Bullet;
    public GameObject Rocket;
    public GameObject[] targets;
   
    public int Life;
    private int Collision;
    public GameObject Fire;
    public GameObject Fire2;
    public int Score;
    private SpriteRenderer Spr;
    private float timer;
    public float Shottime;
    private float Randomshot;
    private Enemybox Enemybox;
    public GameObject Mymanage;
    private Manage Manage;
    private bool barrage;
    private bool ShotRockets;
    private float Timer2;
    private float barragetime;
    public GameObject Myparents;
    public int Once;
    private Barlife Showscore;
  
    void Start()
    {
        Manage = GameObject.Find("Manage").GetComponent<Manage>();
        Showscore = GameObject.Find("Barlife").GetComponent<Barlife>();
        Spr = GetComponent<SpriteRenderer>();
        transform.parent = null;

        Enemybox = Mymanage.GetComponent<Enemybox>();

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= 7.5f&&Manage.Myplayer!=null&&Collision<Life)
        {

            timer += 1 * Time.deltaTime;
            if (timer >= Shottime)
            {
                Randomshot = Random.Range(0, 101);
                if (Randomshot <= 50)
                {
                    barrage = true;
                }
               else if (Randomshot >50)
                {
                    ShotRockets = true;
                }
                Timer2 = 0;
                timer = 0;
               
            }
        }


        if (barrage == true)
        {
            Timer2 += 1 * Time.deltaTime;
            barragetime += 1 * Time.deltaTime;
            if (barragetime >= 0.15f)
            {
                Instantiate(Bullet, new Vector3(targets[0].transform.position.x +
                    (Random.Range(0.3f, -0.3f)), targets[0].transform.position.y, transform.position.z), Quaternion.identity);
                barragetime = 0;
            }

            if (Timer2 >= 1.5f)
            {
                barrage = false;
            }

        }
      else  if (ShotRockets == true)
        {
            Timer2 += 1 * Time.deltaTime;
            barragetime += 1 * Time.deltaTime;
            if (barragetime >= 0.3f&&Manage.Myplayer!=null)
            {
                if (Manage.Myplayer.transform.position.y >= transform.position.y) { 

                Instantiate(Rocket, new Vector3(targets[Random.Range(3,5)].transform.position.x 
                   , targets[3].transform.position.y, transform.position.z), Quaternion.identity);
                }
                else if (Manage.Myplayer.transform.position.y < transform.position.y)
                {

                    Instantiate(Rocket, new Vector3(targets[Random.Range(1, 3)].transform.position.x 
                       , targets[1].transform.position.y, transform.position.z), Quaternion.identity);
                }
                barragetime = 0;
            }

            if (Timer2 >= 1.5f)
            {
                ShotRockets = false;
            }

        }
    }


            IEnumerator changecolor()
            {
                yield return new WaitForSeconds(0.15f);
                Color color = Spr.material.color;
                color.a = 1;
                color.r = 1;
                color.g = 1f;
                color.b = 1;
                Spr.material.SetColor("_Color", color);
            }
            void OnTriggerEnter2D(Collider2D coll)
            {
                if (coll.gameObject.tag == "Player")
                {
                    Collision += 1;
                    Color color = Spr.material.color;
                    color.a = 10f;
                    color.r = 1;
                    color.g = 1f;
                    color.b = 1;
                    Spr.material.SetColor("_Color", color);
                    if (Collision >= Life)
                    {
                        

                if (Once == 0)
                        {
                    Spr.enabled = false;
                    Myparents.SetActive(false);
                    Instantiate(Fire, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                    StartCoroutine(instantiatefire1());
                    Once += 1;
                        }


                       
                    }
                    StartCoroutine(changecolor());


                }
                if (coll.gameObject.tag == "Playerbullet1")
                {
                    Collision += 1;
                    Color color = Spr.material.color;
                    color.a = 10f;
                    color.r = 1;
                    color.g = 1f;
                    color.b = 1;
                    Spr.material.SetColor("_Color", color);
                    if (Collision >= Life)
                    {


                       
                if (Once == 0)
                        {
                    Spr.enabled = false;
                    Myparents.SetActive(false);
                    Instantiate(Fire, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                    StartCoroutine(instantiatefire1());
                    Once += 1;
                        }



                    }
                    StartCoroutine(changecolor());


                }
                if (coll.gameObject.tag == "Playerrocket")
                {
                    Collision += 3;
                    Color color = Spr.material.color;
                    color.a = 10f;
                    color.r = 1;
                    color.g = 1f;
                    color.b = 1;
                    Spr.material.SetColor("_Color", color);
                    if (Collision >= Life)
                    {

                        
              

                if (Once == 0)
                        {
                    Spr.enabled = false;
                    Myparents.SetActive(false);
                    Instantiate(Fire, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                    StartCoroutine(instantiatefire1());
                    Once += 1;

                  
                        }
                       
                    }
                    StartCoroutine(changecolor());


                }

            }
    IEnumerator instantiatefire1()
    {
        yield return new WaitForSeconds(0.1f);
      

        Instantiate(Fire2, new Vector3(targets[1].transform.position.x, targets[1].transform.position.y, transform.position.z), Quaternion.identity);
        StartCoroutine(instantiatefire2());
    }
    IEnumerator instantiatefire2()
    {
        yield return new WaitForSeconds(0.15f);
      
        Instantiate(Fire2, new Vector3(targets[3].transform.position.x, targets[3].transform.position.y, transform.position.z), Quaternion.identity);
        StartCoroutine(instantiatefire3());
    }
    IEnumerator instantiatefire3()
    {
        yield return new WaitForSeconds(0.15f);
    
        Instantiate(Fire2, new Vector3(targets[2].transform.position.x, targets[2].transform.position.y, transform.position.z), Quaternion.identity);
        StartCoroutine(instantiatefire4());
    }
    IEnumerator instantiatefire4()
    {
        yield return new WaitForSeconds(0.2f);
     
        Instantiate(Fire2, new Vector3(targets[4].transform.position.x, targets[4].transform.position.y, transform.position.z), Quaternion.identity);
        Manage.Score += Score;
        Enemybox.Checkmyenemy();
        Showscore.ShowScore();
        Destroy(gameObject);
    }
}


