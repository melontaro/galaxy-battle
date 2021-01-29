using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movedown : MonoBehaviour
{
    public float Speed;
    public bool Score;
    public GameObject Particle;
    void Start()
    {
        transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(0, Speed * Time.deltaTime, 0);
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player") {
            if (Score == true) {
                Instantiate(Particle, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                Destroy(gameObject);
            }

        }
    }
}
