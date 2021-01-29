using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemybox : MonoBehaviour
{
 
    private Manage Manage;
    private Managelevel Managelevel;
    private bool Movedown;
    private bool Left;
    private bool Right;
    public float PositionX;
    private float SpeedRight;
    private float SpeedLeft;
    public float Speed;
    private int Number;
    public int numberofenemies;
    public float Speeddown;
    void Start()
    {
        Manage = GameObject.Find("Manage").GetComponent<Manage>();
        Managelevel = GameObject.Find("Managelevel").GetComponent<Managelevel>();
        Movedown = true;

        Left = true;
        Right = false;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (transform.position.y < -3)
        {
            Movedown = false;
        }
   if (Movedown == true)
        {
            transform.position -= new Vector3(0, Speeddown * Time.deltaTime, 0);
        }
        else if (Movedown == false)
        {
            if (transform.position.x >= PositionX)
            {
                Left = true;
                Right = false;
            }
            if (transform.position.x <= -PositionX)
            {
                Left = false;
                Right = true;
            }
            if (Right == true)
            {
                SpeedRight += Speed * Time.deltaTime;
                if (SpeedRight >= 5.5f)
                {
                    SpeedRight = 5.5f;
                }
                transform.position += new Vector3(SpeedRight * Time.deltaTime, 0, 0);

            }
            if (Right == false)
            {
                SpeedRight -= 5.5f * Time.deltaTime;
                if (SpeedRight <= 0)
                {
                    SpeedRight = 0;
                }
                transform.position += new Vector3(SpeedRight * Time.deltaTime, 0, 0);


            }
            ///---------------------------moveleft


            if (Left == true)
            {
                SpeedLeft += Speed * Time.deltaTime;
                if (SpeedLeft >= 5.5f)
                {
                    SpeedLeft = 5.5f;
                }
                transform.position -= new Vector3(SpeedLeft * Time.deltaTime, 0, 0);

            }
            if (Left == false)
            {
                SpeedLeft -= 5.5f * Time.deltaTime;
                if (SpeedLeft <= 0f)
                {
                    SpeedLeft = 0f;
                }
                transform.position -= new Vector3(SpeedLeft * Time.deltaTime, 0, 0);
            }
        }
    }
    public void Checkmyenemy()
    {
        Number += 1;
        if (Number == numberofenemies)
        {
            Managelevel.Nextenemybox();
            Destroy(gameObject);
        }
        
        
    }
}

