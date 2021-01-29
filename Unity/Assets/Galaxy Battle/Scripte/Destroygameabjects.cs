using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroygameabjects : MonoBehaviour
{
    public float Destroythis;
    public bool Sound;
    private AudioSource Audio;
    private Manage Manage;
    // Start is called before the first frame update
    void Start()
    {
        Manage = GameObject.Find("Manage").GetComponent<Manage>();
      
        if (Sound == true&&Manage.Sound==0)
        {
            Audio = GetComponent<AudioSource>();
            Audio.Play();
        }
        StartCoroutine(Dest());

       
    }
    IEnumerator Dest()
    {
        yield return new WaitForSeconds(Destroythis);
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
