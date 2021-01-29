using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guidedbullet : MonoBehaviour
{
    public GameObject firstfire;
    public GameObject Lastfire;
    public float Speed;
    private GameObject Target;
    public bool enemybullet;
    public bool playerbullet;
    private AudioSource Audio;
    private Manage Manage;
    void Start()
    {
        Manage = GameObject.Find("Manage").GetComponent<Manage>();
        Audio = GetComponent<AudioSource>();
        if (Manage.Sound == 0)
        {
            Audio.Play();
        }
        if (enemybullet == true && playerbullet == false)
        {
            if (GameObject.Find("Player") != null)
            {
                Target = GameObject.Find("Player");
                LookAtTarget(Target.transform);
            }
            else if (GameObject.Find("Player") == null)
            {
                transform.eulerAngles = new Vector3(0, 0, 270);
            }
        }
        else if (playerbullet == true && enemybullet == false)
        {

        }

        Instantiate(firstfire, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
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

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(Speed * Time.deltaTime, 0, 0));
        if (transform.position.x >= 5.5f || transform.position.x <= -5.5f || transform.position.y >= 12f || transform.position.y <= -11f)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            if (playerbullet == true)
            {
                Instantiate(Lastfire, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                Destroy(gameObject);

            }
        }
            if (coll.gameObject.tag == "Player")
            {
                if (enemybullet == true)
                {
                    Instantiate(Lastfire, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                    Destroy(gameObject);
                }
            }
        }
    }

