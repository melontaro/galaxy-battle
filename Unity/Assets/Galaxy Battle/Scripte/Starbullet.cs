using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starbullet : MonoBehaviour
{
    public float Speed;
    public GameObject Fire;
    public GameObject []Bullet;
    private GameObject Output;
    private AudioSource Audio;
    private Manage Manage;
    void Start()
    {
        Manage = GameObject.Find("Manage").GetComponent<Manage>();

        StartCoroutine(bullets());
        Audio = GetComponent<AudioSource>();

        if(Manage.Sound == 0){
            Audio.Play();
        }

    }
    IEnumerator bullets()
    {
        yield return new WaitForSeconds(0.25f);
        Instantiate(Fire, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        Instantiate(Bullet[0], new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        Instantiate(Bullet[1], new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        Instantiate(Bullet[2], new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        Instantiate(Bullet[3], new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        Instantiate(Bullet[4], new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);

        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, Speed * Time.deltaTime, 0);
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Enemy") {
            Instantiate(Fire, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            Instantiate(Bullet[0], new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            Instantiate(Bullet[1], new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            Instantiate(Bullet[2], new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            Instantiate(Bullet[3], new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            Instantiate(Bullet[4], new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
