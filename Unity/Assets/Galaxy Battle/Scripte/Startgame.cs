using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startgame : MonoBehaviour
{
    public float Speed;
    public Sprite[] Sprite;
    private Manage Manage;
    private SpriteRenderer Spr;
    public GameObject[] Players;
    private GameObject Output;
    public GameObject[] Managelevel;
    public GameObject[] Fireparticle;

    // Start is called before the first frame update
    void Start()
    {
        Manage = GameObject.Find("Manage").GetComponent<Manage>();
        Manage.End = false;
        if (Manage.Sound == 0)
        {
            GameObject.Find("Levelaudio").GetComponent<AudioSource>().Play();
        }
        Spr = GetComponent<SpriteRenderer>();
        Manage.Gun = 1;
        Spr.sprite = Sprite[Manage.Player];
        Fireparticle[Manage.Player].SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, Speed * Time.deltaTime, 0);
        if (transform.position.y >= -5f)
        {
            Output = Instantiate(Managelevel[Manage.Level], new Vector3(transform.position.x, 11.5f, transform.position.z), Quaternion.identity) as GameObject;
            Output.name = "Managelevel";
            Output = Instantiate(Players[Manage.Player], new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity) as GameObject;
            Output.name = "Player";
            Destroy(gameObject);
        }
    }
}
