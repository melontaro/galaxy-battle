using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanePlayer : MonoBehaviour
{

    public GameObject Myrocket;
    public GameObject Starbullet;
    public GameObject[] Bullet;
    private Manage Manage;
    private GameObject Targetposition;
    public float Speed;
    public float Damage1;
    public float Damage2;
    public float Shottime;
    private SpriteRenderer Mysprite;
    public Sprite[] Playersprite;
    public GameObject[] Targets;
    private float Timer;

    private Barlife Barlife;
    public GameObject Fire;
    private bool Star;
    private bool Rocket;
    private bool barrage;
    private float ScoreTimer;
    private SpriteRenderer Spr;
    private bool Collision;
    public bool Moveup;
    private bool Once;
    void Start()
    {
        Manage = GameObject.Find("Manage").GetComponent<Manage>();
        Manage.Myplayer = gameObject;
        Mysprite = GetComponent<SpriteRenderer>();
        Barlife = GameObject.Find("Barlife").GetComponent<Barlife>();
        Spr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Moveup == false && Time.timeScale == 1)
        {
            Timer += 1 * Time.deltaTime;
            if (Star == false && barrage == false && Rocket == false)
            {
                if (Timer >= Shottime)
                {
                    if (Manage.Gun == 1)
                    {
                        Instantiate(Bullet[0], new Vector3(Targets[1].transform.position.x, Targets[1].transform.position.y, transform.position.z), Quaternion.identity);
                    }
                    else if (Manage.Gun == 2)
                    {
                        Instantiate(Bullet[0], new Vector3(Targets[0].transform.position.x, Targets[0].transform.position.y, transform.position.z), Quaternion.identity);
                        Instantiate(Bullet[0], new Vector3(Targets[2].transform.position.x, Targets[2].transform.position.y, transform.position.z), Quaternion.identity);
                    }
                    else if (Manage.Gun == 3)
                    {
                        Instantiate(Bullet[1], new Vector3(Targets[0].transform.position.x, Targets[0].transform.position.y, transform.position.z), Quaternion.identity);
                        Instantiate(Bullet[0], new Vector3(Targets[1].transform.position.x, Targets[1].transform.position.y, transform.position.z), Quaternion.identity);
                        Instantiate(Bullet[2], new Vector3(Targets[2].transform.position.x, Targets[2].transform.position.y, transform.position.z), Quaternion.identity);
                    }
                    Timer = 0;
                }
            }
            else if (Star == true && barrage == false && Rocket == false)
            {
                ScoreTimer += 1 * Time.deltaTime;
                if (ScoreTimer >= 10f)
                {

                    Star = false;
                }
                if (Timer >= Shottime + 0.15f)
                {
                    Instantiate(Starbullet, new Vector3(Targets[1].transform.position.x, Targets[1].transform.position.y, transform.position.z), Quaternion.identity);
                    Timer = 0;
                }


            }
            else if (Star == false && barrage == true && Rocket == false)
            {
                ScoreTimer += 1 * Time.deltaTime;
                if (ScoreTimer >= 10f)
                {

                    barrage = false;
                }
                if (Timer >= 0.1f)
                {
                    Instantiate(Bullet[0], new Vector3(Targets[1].transform.position.x + (Random.Range(-0.5f, 0.5f)), Targets[1].transform.position.y, transform.position.z), Quaternion.identity);
                    Timer = 0;
                }


            }
            else if (Star == false && barrage == false && Rocket == true)
            {
                ScoreTimer += 1 * Time.deltaTime;
                if (ScoreTimer >= 10f)
                {

                    Rocket = false;
                }
                if (Timer >= Shottime + 0.3f)
                {
                    Instantiate(Myrocket, new Vector3(Targets[1].transform.position.x + (Random.Range(-0.5f, 0.5f)), Targets[1].transform.position.y, transform.position.z), Quaternion.identity);
                    Timer = 0;
                }


            }
            //----------------
            if (Manage.Targetposition != null && Targetposition == null)
            {
                Targetposition = Manage.Targetposition;

            }
            else if (Targetposition != null)
            {
                if (Manage.Move == true)
                {
                    transform.position = Vector3.Lerp(transform.position, transform.position =
                        new Vector3(Targetposition.transform.position.x, Targetposition.transform.position.y + 1f, -5f), Time.deltaTime * Speed);
                    ///---------Change player sprite-----------

                    if (transform.position.x > Targetposition.transform.position.x + 1)
                    {
                        Mysprite.sprite = Playersprite[1];
                        transform.localScale = new Vector3(1, 1, 1);
                    }
                    else if (transform.position.x < Targetposition.transform.position.x - 1)
                    {
                        Mysprite.sprite = Playersprite[1];
                        transform.localScale = new Vector3(-1, 1, 1);
                    }
                    else if (transform.position.x >= Targetposition.transform.position.x - 1 && transform.position.x <= Targetposition.transform.position.x + 1)
                    {
                        Mysprite.sprite = Playersprite[0];
                        transform.localScale = new Vector3(1, 1, 1);
                    }
                }
                else if (Manage.Move == false)
                {
                    Mysprite.sprite = Playersprite[0];
                    transform.localScale = new Vector3(1, 1, 1);
                    transform.position = new Vector3(transform.position.x, transform.position.y, -5f);

                }


            }


        }
        else if (Moveup == true)
        {
            Mysprite.sprite = Playersprite[0];
            transform.position += new Vector3(0, 2.5f * Time.deltaTime, 0);
        }

    }

    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Enemylaser")
        {


            Color color = Spr.material.color;
            color.a = 10;
            color.r = 1;
            color.g = 1f;
            color.b = 1;
            Spr.material.SetColor("_Color", color);
            Manage.Life -= 5f * Time.deltaTime;
            Barlife.Showbarlife();
            if (Manage.Life <= 0)
            {
                if (Once == false)
                {
                    Instantiate(Fire, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                    Manage.Losegame();

                    Once = true;
                }
                Destroy(gameObject);
            }
        }
    }
    void OnTriggerExit2D(Collider2D coll)
    {

        if (Collision == false)
        {

            Color color = Spr.material.color;
            color.a = 1;
            color.r = 1;
            color.g = 1f;
            color.b = 1;
            Spr.material.SetColor("_Color", color);
        }


    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            Collision = true;
            Color color = Spr.material.color;
            color.a = 10;
            color.r = 1;
            color.g = 1f;
            color.b = 1;
            Spr.material.SetColor("_Color", color);
            StartCoroutine(Changecolor());

            Manage.Life -= Damage1 + 5f;
            Barlife.Showbarlife();
            if (Manage.Life <= 0)
            {
                Instantiate(Fire, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                Manage.Losegame();
                Destroy(gameObject);
            }
        }
        if (coll.gameObject.tag == "Enemybullet1")
        {
            Manage.Life -= Damage1;
            Collision = true;
            Color color = Spr.material.color;
            color.a = 10;
            color.r = 1;
            color.g = 1f;
            color.b = 1;
            Spr.material.SetColor("_Color", color);
            StartCoroutine(Changecolor());
            Barlife.Showbarlife();
            if (Manage.Life <= 0)
            {
                Instantiate(Fire, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                Manage.Losegame();
                Destroy(gameObject);
            }
        }
        if (coll.gameObject.tag == "Enemybullet2" || coll.gameObject.tag == "Enemyrocket")
        {
            Manage.Life -= Damage2;
            Collision = true;
            Color color = Spr.material.color;
            color.a = 10;
            color.r = 1;
            color.g = 1f;
            color.b = 1;
            Spr.material.SetColor("_Color", color);
            StartCoroutine(Changecolor());
            Barlife.Showbarlife();
            if (Manage.Life <= 0)
            {
                Instantiate(Fire, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                Manage.Losegame();
                Destroy(gameObject);
            }
        }
        if (coll.gameObject.tag == "Star")
        {
            ScoreTimer = 0;
            Star = true;
            barrage = false;
            Rocket = false;
        }
        if (coll.gameObject.tag == "Life")
        {
            Manage.Life += 25f;
            if (Manage.Life >= 100)
            {
                Manage.Life = 100;
            }
            Barlife.Showbarlife();

        }
        if (coll.gameObject.tag == "Gun")
        {
            Manage.Gun += 1;
            if (Manage.Gun >= 3)
            {
                Manage.Gun = 3;
            }
            ScoreTimer = 0;
            barrage = true;
            Star = false;
            Rocket = false;
        }
        if (coll.gameObject.tag == "Rocket")
        {


            ScoreTimer = 0;
            barrage = false;
            Star = false;
            Rocket = true;

        }
    }
    IEnumerator Changecolor()
    {
        yield return new WaitForSeconds(0.1f);
        Color color = Spr.material.color;
        color.a = 1;
        color.r = 1;
        color.g = 1f;
        color.b = 1;
        Spr.material.SetColor("_Color", color);
        Collision = false;
    }

}
