using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giant3 : MonoBehaviour
{
    public GameObject Gun;
    public GameObject Gun2;
    public GameObject Bulletright;
    public GameObject Bulletleft;
    public GameObject Rocket;
    public GameObject Laser;

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
    private bool ShotLaser;
    private float Timer2;
    private float Timer3;
    private float barragetime;
    public GameObject Myparents;
    public int Once;
    private Barlife Showscore;
    private bool Onceshot;
    private GameObject Output;
    private bool Shotgun;
    private float Shotguntimer;
    void Start()
    {
        Manage = GameObject.Find("Manage").GetComponent<Manage>();
        Showscore = GameObject.Find("Barlife").GetComponent<Barlife>();
        Spr = GetComponent<SpriteRenderer>();
        transform.parent = null;

        Enemybox = Mymanage.GetComponent<Enemybox>();

    }
    private void LookAtTarget(Transform targetTranform)
    {
        // LookAt 2D
        Vector3 target = targetTranform.position;
        // get the angle
        Vector3 norTar = (target - transform.position).normalized;
        float angle = Mathf.Atan2(norTar.y, norTar.x) * Mathf.Rad2Deg;
        // rotate to angle
        Quaternion rotation = new Quaternion();
        rotation.eulerAngles = new Vector3(0, 0, angle);
        Gun.transform.rotation = rotation;
        if (Gun2 != null)
        {
            Gun2.transform.rotation = rotation;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= 7.5f && Manage.Myplayer != null && Collision < Life)
        {

            LookAtTarget(Manage.Myplayer.transform);


            timer += 1 * Time.deltaTime;
            if (timer >= Shottime)
            {
                Randomshot = Random.Range(0, 101);
                if (Randomshot <= 50)
                {
                    barrage = true;
                }
                else if (Randomshot > 50)
                {
                    Onceshot = false;
                    ShotLaser = true;
                }
                Timer2 = 0;
                timer = 0;

            }
            if (timer >= 2.5f)
            {
                Shotgun = true;
            }



            if (barrage == true)
            {
                Timer2 += 1 * Time.deltaTime;
                barragetime += 1 * Time.deltaTime;
                if (barragetime >= 0.15f)
                {
                    Instantiate(Bulletright, new Vector3(targets[1].transform.position.x +
                        (Random.Range(0.3f, -0.3f)), targets[1].transform.position.y, transform.position.z), Quaternion.identity);
                    Instantiate(Bulletleft, new Vector3(targets[2].transform.position.x +
                     (Random.Range(0.3f, -0.3f)), targets[2].transform.position.y, transform.position.z), Quaternion.identity);
                    barragetime = 0;
                }

                if (Timer2 >= 1.5f)
                {
                    barrage = false;
                }

            }
            else if (ShotLaser == true)
            {
                Timer2 += 1 * Time.deltaTime;


                if (Onceshot == false)
                {
                    Output = Instantiate(Laser, new Vector3(targets[0].transform.position.x
                       , targets[0].transform.position.y, transform.position.z), Quaternion.identity) as GameObject;
                    Output.transform.parent = gameObject.transform;
                    Onceshot = true;
                }

                if (Timer2 >= 1.5f)
                {

                    ShotLaser = false;
                }

            }
            if (Shotgun == true)
            {
                Shotguntimer += 1 * Time.deltaTime;
                Timer3 += 1 * Time.deltaTime;
                if (Timer3 >= 1.5f)
                {
                    Shotgun = false;
                }
                if (Shotguntimer >= 0.3f)
                {
                    Instantiate(Rocket, new Vector3(targets[3].transform.position.x
                       , targets[3].transform.position.y, transform.position.z), Quaternion.identity);

                    if (Gun2 != null)
                    {
                        Instantiate(Rocket, new Vector3(targets[5].transform.position.x
                  , targets[5].transform.position.y, transform.position.z), Quaternion.identity);
                    }
                    Shotguntimer = 0;
                }
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
                    Destroy(Output);
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
                    Destroy(Output);
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
                    Destroy(Output);
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

