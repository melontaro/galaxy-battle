using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveleftandright : MonoBehaviour
{
    public float Speed;
    public float SpeedDown;
    public bool Left;
    public bool Right;
    private float SpeedRight;
    private float SpeedLeft;
    private float PositionX;
    public float Return;
    
    void Start()
    {
        
        PositionX = transform.position.x;
       
        
     
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(0, SpeedDown * Time.deltaTime, 0);

        if (transform.position.x >= PositionX+ Return)
        {
            Left = true;
            Right = false;
        }
      else  if (transform.position.x <= PositionX - Return)
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
     else   if (Right == false)
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
     else   if (Left == false)
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
