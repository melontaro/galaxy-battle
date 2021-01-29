using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movebullets : MonoBehaviour
{
    public bool Playerbullet;
    public bool Enemybullet;
    public float Speed;
    public GameObject Fire;
    public float Rotate;
    private AudioSource Audio;
    private AudioSource CollisionAudio;
    private Manage Manage;
    // Start is called before the first frame update
    void Start()
    {
        Manage = GameObject.Find("Manage").GetComponent<Manage>();
        Audio = GetComponent<AudioSource>();
        CollisionAudio= GameObject.Find("Collisionaudio").GetComponent<AudioSource>();
        if (Manage.Sound == 0)
        {
            Audio.Play();
        }
        Instantiate(Fire, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, Rotate);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(Speed * Time.deltaTime, 0, 0));
        if (transform.position.y >= 7.5f||transform.position.y<=-11f||transform.position.x>=5.5f||transform.position.x<=-5.5f)
        {
            Instantiate(Fire, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Enemy") {
            if (Playerbullet == true) {
            Instantiate(Fire, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                if (Manage.Sound == 0)
                {
                    CollisionAudio.Play();
                }
            Destroy(gameObject);
            }
        }
        if (coll.gameObject.tag == "Player")
        {
            if (Enemybullet == true) {
            Instantiate(Fire, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                if (Manage.Sound == 0)
                {
                    CollisionAudio.Play();
                }
                Destroy(gameObject);
            }
        }
    }
}
