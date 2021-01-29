using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveenemy3 : MonoBehaviour
{
    public float Speed;
    public float Timer;
    private bool Moveright;
    private bool Moveleft;
    private bool Once;
    void Start()
    {
        transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (transform.position.y <= 2.2f)
        {
            if (Once == false)
            {
                if (transform.position.x > 0)
                {
                    Moveleft = true;
                }
               else if (transform.position.x < 0)
                {
                    Moveright = true;
                }
                StartCoroutine(Movedown());

                
                Once = true;
            }
        }
        if (Moveright == true && Moveleft == false)
        {
            transform.position += new Vector3(Speed * Time.deltaTime, -Speed * Time.deltaTime, 0);
        }
      else  if (Moveright == false && Moveleft == true)
        {
            transform.position -= new Vector3(Speed * Time.deltaTime, Speed * Time.deltaTime, 0);
        }
        else if (Moveright == false && Moveleft == false)
        {
            transform.position += new Vector3(0, -Speed * Time.deltaTime, 0);
        }
    }
    IEnumerator Movedown()
    {
        yield return new WaitForSeconds(Timer);
        Moveleft = false;
        Moveright = false;
    }
}
