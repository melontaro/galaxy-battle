using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy2 : MonoBehaviour
{
    public GameObject[] Targetposition;
    private bool Move;
    private bool Moveout;
    private Manage Manage;
    public int direction;
    private float Timer;
    public float Timetomove;
    public float Resettimer;
    public float Timetomoveout;
    void Start()
    {
        Manage = GameObject.Find("Manage").GetComponent<Manage>();
        Targetposition[1] = GameObject.Find("Targetposition1");
        Targetposition[2] = GameObject.Find("Targetposition2");
        Targetposition[3] = GameObject.Find("Targetposition3");
        Targetposition[4] = GameObject.Find("Targetposition4");
        Targetposition[5] = GameObject.Find("Targetposition5");
        Targetposition[6] = GameObject.Find("Targetposition6");
        Targetposition[7] = GameObject.Find("Targetposition7");
        Targetposition[8] = GameObject.Find("Targetposition8");
        Targetposition[9] = GameObject.Find("Targetposition9");
        Targetposition[10] = GameObject.Find("Targetposition10");
        Targetposition[11] = GameObject.Find("Targetposition11");
        Targetposition[12] = GameObject.Find("Targetposition12");
        Targetposition[13] = GameObject.Find("Targetposition13");
        Targetposition[14] = GameObject.Find("Targetposition14");
        Targetposition[15] = GameObject.Find("Targetposition15");
        Targetposition[16] = GameObject.Find("Targetposition16");
        Targetposition[17] = GameObject.Find("Targetposition17");
        Targetposition[18] = GameObject.Find("Targetposition18");
        Targetposition[19] = GameObject.Find("Targetposition19");
        Targetposition[20] = GameObject.Find("Targetposition20");
        Targetposition[21] = GameObject.Find("Targetposition21");
        Targetposition[22] = GameObject.Find("Targetposition22");
        Targetposition[23] = GameObject.Find("Targetposition23");
        Targetposition[24] = GameObject.Find("Targetposition24");

        StartCoroutine(Moveoutnow());

        if (Timetomove != 0)
        {
            StartCoroutine(Movenow());
           
        }
      else  if (Timetomove == 0)
        {
            Move = true;
            transform.parent = null;
            direction = Random.Range(1, 21);
        }

    }
    IEnumerator Movenow()
    {
        yield return new WaitForSeconds(Timetomove);
        direction = Random.Range(1, 21);
        transform.parent = null;

        Move = true;
    }
    IEnumerator Moveoutnow()
    {
        yield return new WaitForSeconds(Timetomoveout);
        direction = Random.Range(21, 25);
        Moveout = true;
    }
   
    void Update()
    {
        if (Move == true&&Moveout==false)
        {
            transform.position = Vector3.Lerp(transform.position, Targetposition[direction].transform.position, Time.deltaTime * 0.5f);
            Timer += 1 * Time.deltaTime;
            if (Timer >= Resettimer)
            {
                direction = Random.Range(1, 21);
                Timer = 0;
            }
        }
      else  if (Move == true && Moveout == true)
        {
            transform.position = Vector3.Lerp(transform.position, Targetposition[direction].transform.position, Time.deltaTime * 1f);
           
        }

       

    }
}
