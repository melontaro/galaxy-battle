using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private Manage Manage;
    private BoxCollider2D Boxcolider;
   
    void Start()
    {
        Manage = GameObject.Find("Manage").GetComponent<Manage>();
        Manage.Targetposition = gameObject;
        Boxcolider = GetComponent<BoxCollider2D>();
        transform.position = new Vector3(0, 0, -6);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseDrag()
    {
        Manage.Move = true;
        Vector3 mospos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -6);
        Vector3 obj = Camera.main.ScreenToWorldPoint(mospos);

        transform.position = obj;
        Boxcolider.enabled = false;
    }




    void OnMouseUp()
    {
        Manage.Move = false;
        transform.position = new Vector3(0, 0, -6);
        Boxcolider.enabled = true;

    }



}
