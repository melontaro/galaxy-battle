using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audiopitch : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource Audio;
    void Start()
    {
        Audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0)
        {
            Audio.pitch = 0;
        }
      else  if (Time.timeScale == 1)
        {
            Audio.pitch = 1;
        }
    }
}
