using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public GameObject Bullet;
    public GameObject target;
    public int Life;
    private int Collision;
    public GameObject Fire;
   
   
    private Manage Manage;
   
    private SpriteRenderer Spr;
    public int Score;
    private float timer;
    private float Randomshot;
    public float Shottime;
    // Start is called before the first frame update
    private Barlife Showscore;
    void Start()
    {
        Showscore = GameObject.Find("Barlife").GetComponent<Barlife>();
        Spr = GetComponent<SpriteRenderer>();
        Manage = GameObject.Find("Manage").GetComponent<Manage>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= -11.5f)
        {
            Destroy(gameObject);
        }


        //--------Shot--------------
        if (transform.position.y <= 7.5f)
        {

            timer += 1 * Time.deltaTime;
            if (timer >= Shottime)
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



