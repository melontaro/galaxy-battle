using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float Speed;
    public bool Playerrocket;
    public bool enemyrocket;
    private GameObject Target;
    private bool Direction;
    public float Blasttime;
    public GameObject LastFire;
    public GameObject firstfire;
    private AudioSource Audio;
    private AudioSource Audio2;
    private Manage Manage;
    void Start()
    {
        Manage = GameObject.Find("Manage").GetComponent<Manage>();
        Audio = GetComponent<AudioSource>();
        Audio2 = GameObject.Find("Rocketaudio").GetComponent<AudioSource>();
        if (Manage.Sound == 0)
        {
            Audio.Play();
        }
        Instantiate(firstfire, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
     
        StartCoroutine(Dest());
        if (Playerrocket == true && enemyrocket == false)
        {
            StartCoroutine(Changespeed());
            GameObject[] gameobjectArray = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject go in gameobjectArray)
            {

                Target = go.gameObject;
            }
            if (Target != null)
            {
                LookAtTarget(Target.transform);
            }
            else if (Target == null)
            {
                transform.eulerAngles = new Vector3(0, 0, 90);
            }
        }
        else if (Playerrocket == false && enemyrocket == true)
        {
            if (GameObject.Find("Player") != null)
            {
                Target = GameObject.Find("Player");
            }
            if (Target != null)
            {
                LookAtTarget(Target.transform);
            }
            else if (Target == null)
            {
                transform.eulerAngles = new Vector3(0, 0, 270);
            }
            StartCoroutine(Movedown());

            

        }
    }
    IEnumerator Changespeed()
    {
        yield return new WaitForSeconds(0.6f);
        if (Manage.Sound == 0)
        {
            Audio2.Play();
        }
        Instantiate(firstfire, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        Speed += 10;
        Direction = true;
    }
    IEnumerator Movedown()
    {
        yield return new WaitForSeconds(0.6f);
        if (Manage.Sound == 0)
        {
            Audio2.Play();
        }
        Instantiate(firstfire, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        Speed += 10;
        Direction = true;
    }
    IEnumerator Dest()
    {
        yield return new WaitForSeconds(Blasttime);
        Instantiate(LastFire, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        Destroy(gameObject);
    }
    void Update()
    {
        transform.Translate(new Vector3(Speed * Time.deltaTime, 0, 0));
        if (Playerrocket == true && enemyrocket == false)
        {
            if (Target != null)
            {
                LookAtTarget(Target.transform);
            }
        }

        else if (Playerrocket == false && enemyrocket == true)
        {
            if (Target != null&&Direction==false)
            {
                LookAtTarget(Target.transform);
            }
        }


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
        transform.rotation = rotation;
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            if (Playerrocket == true)
            {
                Instantiate(LastFire, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                Destroy(gameObject);
            }
        }

        if (coll.gameObject.tag == "Player")
        {
            if (enemyrocket == true)
            {
                Instantiate(LastFire, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}

