using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject Laser;
    public GameObject Bullet;
    public GameObject Bullet2;
    public GameObject Bullet3;
    public GameObject []targets;
    public bool Randomfire;
    public int Life;
    private  int Collision;
    public GameObject Fire;
    public int Score;
    private SpriteRenderer Spr;
    private float timer;
    public float Shottime;
    private float Randomshot;
    private Enemybox Enemybox;
    public GameObject Mymanage;
    private Manage Manage;
    private bool barrage;
    private float Timer2;
    private float Barragetime;
    public int Once;
    private Barlife Showscore;
    private GameObject Output;
    void Start()
    {
        Manage = GameObject.Find("Manage").GetComponent<Manage>();
        Showscore = GameObject.Find("Barlife").GetComponent<Barlife>();
        Spr = GetComponent<SpriteRenderer>();
        if (Randomfire == true)
        {
            Shottime = Random.Range(2.5f, 4f);
        }
        
        Enemybox = Mymanage.GetComponent<Enemybox>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= 7.5f) { 
        if (Randomfire == true)
        {
                timer += 1 * Time.deltaTime;
                if (timer >= Shottime)
                {
                    Randomshot = Random.Range(0, 101);
                    if (Randomshot <= 50)
                    {
                        if (Bullet != null&&Bullet2==null && Bullet3 == null) { 
                        Instantiate(Bullet, new Vector3(targets[0].transform.position.x, targets[0].transform.position.y, transform.position.z), Quaternion.identity);
                        }
                       else if (Bullet != null && Bullet2 != null && Bullet3 == null)
                        {
                            Instantiate(Bullet, new Vector3(targets[0].transform.position.x, targets[0].transform.position.y, transform.position.z), Quaternion.identity);
                            Instantiate(Bullet2, new Vector3(targets[1].transform.position.x, targets[1].transform.position.y, transform.position.z), Quaternion.identity);
                        }
                        else if (Bullet != null && Bullet2 != null && Bullet3 != null)
                        {
                            Instantiate(Bullet, new Vector3(targets[0].transform.position.x, targets[0].transform.position.y, transform.position.z), Quaternion.identity);
                            Instantiate(Bullet2, new Vector3(targets[1].transform.position.x, targets[1].transform.position.y, transform.position.z), Quaternion.identity);
                            Instantiate(Bullet3, new Vector3(targets[2].transform.position.x, targets[2].transform.position.y, transform.position.z), Quaternion.identity);
                        }
                        else if (Bullet == null && Bullet2 != null && Bullet3 == null)
                        {
                            Instantiate(Bullet2, new Vector3(targets[0].transform.position.x, targets[0].transform.position.y, transform.position.z), Quaternion.identity);
                            StartCoroutine(Shot2());

                            
                        }
                        else if (Bullet == null && Bullet2 == null && Bullet3 != null)
                        {
                            barrage = true;
                            Timer2 = 0;

                        }
                        else if (Bullet == null && Bullet2 == null && Bullet3 == null)
                        {
                            if (Laser != null)
                            {
                                Output=Instantiate(Laser, new Vector3(targets[0].transform.position.x, targets[0].transform.position.y, transform.position.z), Quaternion.identity)as GameObject;
                                Output.transform.parent=gameObject.transform;
                            }
                      

                        }
                    }
                    timer = 0;
                }
        }
          else  if (Randomfire == false)
            {
                timer += 1 * Time.deltaTime;
                if (timer >= Shottime)
                {
                    Randomshot = Random.Range(0, 101);
                    if (Randomshot <= 50)
                    {
                        if (Bullet != null && Bullet2 == null && Bullet3 == null)
                        {
                            Instantiate(Bullet, new Vector3(targets[0].transform.position.x, targets[0].transform.position.y, transform.position.z), Quaternion.identity);
                        }
                        else if (Bullet != null && Bullet2 != null && Bullet3 == null)
                        {
                            Instantiate(Bullet, new Vector3(targets[0].transform.position.x, targets[0].transform.position.y, transform.position.z), Quaternion.identity);
                            Instantiate(Bullet2, new Vector3(targets[1].transform.position.x, targets[1].transform.position.y, transform.position.z), Quaternion.identity);
                        }
                        else if (Bullet != null && Bullet2 != null && Bullet3 != null)
                        {
                            Instantiate(Bullet, new Vector3(targets[0].transform.position.x, targets[0].transform.position.y, transform.position.z), Quaternion.identity);
                            Instantiate(Bullet2, new Vector3(targets[1].transform.position.x, targets[1].transform.position.y, transform.position.z), Quaternion.identity);
                            Instantiate(Bullet3, new Vector3(targets[2].transform.position.x, targets[2].transform.position.y, transform.position.z), Quaternion.identity);
                        }
                        else if (Bullet == null && Bullet2 != null && Bullet3 == null)
                        {
                            Instantiate(Bullet2, new Vector3(targets[0].transform.position.x, targets[0].transform.position.y, transform.position.z), Quaternion.identity);
                            StartCoroutine(Shot2());


                        }
                        else if (Bullet == null && Bullet2 == null && Bullet3 != null)
                        {
                            barrage = true;
                            Timer2 = 0;

                        }
                        else if (Bullet == null && Bullet2 == null && Bullet3 == null)
                        {
                            if (Laser != null)
                            {
                                Output = Instantiate(Laser, new Vector3(targets[0].transform.position.x, targets[0].transform.position.y, transform.position.z), Quaternion.identity) as GameObject;
                                Output.transform.parent = gameObject.transform;
                            }


                        }
                    }
                    timer = 0;
                }
            }
        }
        if (barrage == true)
        {
            Timer2 += 1 * Time.deltaTime;
            Barragetime += 1 * Time.deltaTime;
            if (Barragetime >= 0.2f)
            {
                Instantiate(Bullet3, new Vector3(targets[0].transform.position.x+(Random.Range(-0.3f,0.3f)), 
                    targets[0].transform.position.y + (Random.Range(-0.05f, 0.2f)), transform.position.z), Quaternion.identity);
                Barragetime = 0;
            }
            if (Timer2 >= Shottime-1f)
            {
                barrage = false;
            }
        }
        if (transform.position.y <= -11.5f)
        {
           
            Enemybox.Checkmyenemy();
            Destroy(gameObject);
        }
    }
    IEnumerator Shot2()
    {
        yield return new WaitForSeconds(0.2f);
        Instantiate(Bullet2, new Vector3(targets[0].transform.position.x, targets[0].transform.position.y, transform.position.z), Quaternion.identity);
       
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
            Collision += 4;
            Color color = Spr.material.color;
            color.a = 10f;
            color.r = 1;
            color.g = 1f;
            color.b = 1;
            Spr.material.SetColor("_Color", color);
            if (Collision >= Life)
            {
                Instantiate(Fire, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                if (Once == 0)
                {
                    Manage.Score += Score;
                    Enemybox.Checkmyenemy();
                    Showscore.ShowScore();
                    Once += 1;
                }
               
               
                Destroy(gameObject);
            }
            StartCoroutine(changecolor());


        }
        if (coll.gameObject.tag == "Playerbullet1") {
            Collision += 1;
            Color color = Spr.material.color;
            color.a = 10f;
            color.r = 1;
            color.g = 1f;
            color.b = 1;
            Spr.material.SetColor("_Color", color);
            if (Collision >= Life)
            {

                
                Instantiate(Fire, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                if (Once == 0)
                {
                    Manage.Score += Score;
                    Showscore.ShowScore();
                    Enemybox.Checkmyenemy();
                    Once += 1;
                }
               
                
               
                Destroy(gameObject);
               
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
                
                    Instantiate(Fire, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);

                   
                if (Once == 0)
                {
                    Manage.Score += Score;
                    Enemybox.Checkmyenemy();
                    Showscore.ShowScore();
                    Once += 1;

                }
                Destroy(gameObject);
            }
            StartCoroutine(changecolor());


        }

    }
}
