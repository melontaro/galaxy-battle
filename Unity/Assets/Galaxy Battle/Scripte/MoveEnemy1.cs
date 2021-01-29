using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy1 : MonoBehaviour
{
    public GameObject Bullet;
    public GameObject target;
    public int Life;
    private int Collision;
    public GameObject Fire;
    public GameObject[] Targetposition;
    private bool Moveout;
    private Manage Manage;
    public int direction;
    private SpriteRenderer Spr;
    public int Score;
    private float timer;
    private float Randomshot;
    private Barlife Showscore;
    void Start()
    {
        Showscore = GameObject.Find("Barlife").GetComponent<Barlife>();
        Spr = GetComponent<SpriteRenderer>();
        Manage = GameObject.Find("Manage").GetComponent<Manage>();
        Targetposition[0] = GameObject.Find("Targetposition11");
        Targetposition[1] = GameObject.Find("Targetposition2");
        Targetposition[2] = GameObject.Find("Targetposition10");

        Targetposition[3] = GameObject.Find("Targetposition15");
        Targetposition[4] = GameObject.Find("Targetposition4");
        Targetposition[5] = GameObject.Find("Targetposition6");



        Targetposition[6] = GameObject.Find("Targetposition21");
        Targetposition[7] = GameObject.Find("Targetposition22");
        Targetposition[8] = GameObject.Find("Targetposition23");
        Targetposition[9] = GameObject.Find("Targetposition24");

        if (transform.position.x < 0)
        {
            direction = 0;
            StartCoroutine(Changeparent1());

        }
        if (transform.position.x > 0)
        {
            direction = 3;
            StartCoroutine(Changeparent4());

        }

    }

    //---------Direction1----------
    IEnumerator Changeparent1()
    {
        yield return new WaitForSeconds(4.5f);
        StartCoroutine(Changeparent2());
        direction = 1;
    }
    IEnumerator Changeparent2()
    {
        yield return new WaitForSeconds(4.2f);
        StartCoroutine(Changeparent3());
        direction = 2;
    }
    IEnumerator Changeparent3()
    {
        yield return new WaitForSeconds(7f);
        direction = Random.Range(6, 10);
        Moveout = true;
    }

    //-------------------------------------------------Direction2----------------------
    IEnumerator Changeparent4()
    {
        yield return new WaitForSeconds(4.5f);
        direction = 4;
        StartCoroutine(Changeparent5());
    }
    IEnumerator Changeparent5()
    {
        yield return new WaitForSeconds(4.2f);
        StartCoroutine(Changeparent3());
        direction = 5;
    }
    ///--------------------------------------------------------------
    void Update()
    {
        if (transform.position.y <= -11.5f)
        {
            Destroy(gameObject);
        }

        transform.eulerAngles = new Vector3(0, 0, 0);
        if (Moveout == false)
        {
            transform.parent = Targetposition[direction].transform;
        }

        else if (Moveout == true)
        {
            transform.parent = null;

            transform.position = Vector3.Lerp(transform.position, Targetposition[direction].transform.position, Time.deltaTime * 0.5f);
        }





        //--------Shot--------------
        if (transform.position.y <= 7.5f)
        {

            timer += 1 * Time.deltaTime;
            if (timer >= 2.5)
            {
                Randomshot = Random.Range(0, 101);
                if (Randomshot <= 50)
                {

                    Instantiate(Bullet, new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z), Quaternion.identity);

                    timer = 0;

                }
            }
        }
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
                
                Manage.Score += Score;
                Showscore.ShowScore();
                Destroy(gameObject);
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


                Instantiate(Fire, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
               
                
                Manage.Score += Score;

                Showscore.ShowScore();
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

                Manage.Score += Score;
                Showscore.ShowScore();
                Destroy(gameObject);
            }
            StartCoroutine(changecolor());


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
}

    
