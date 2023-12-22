using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeMoveTest : MonoBehaviour
{
    public Transform pos1;
    public Transform pos2;
    private Transform actPos;
    public bool onPos1;
    public float timer;
    public float maxTimer;
    public float rotationForce1;
    public float rotationForce2;
    private void Start()
    {
        actPos = transform;
    }

    private void Update()
    {
        //  checks();
        //  move();
        RotateTree();
    }

    void RotateTree()
    {
        if (!onPos1)
        {
            transform.rotation = Quaternion.Euler(rotationForce1, 0, 0);
        }

        if (onPos1)
        {
            transform.rotation = Quaternion.Euler(rotationForce2, 0, 0);
        }
    }





    public void checks()
    {
        /* OLD CHECK, SE PUEDE PERFECCIONAR, SE TARDA MUCHO CUANDO LLEGA AL FINAL
        if (transform.position == pos1.position)
        {
            if (transform.rotation == pos1.rotation)
            {
                onPos1 = true;
                updatePos();
            }
        }
        
        if (transform.position == pos2.position)
        {
            if (transform.rotation == pos2.rotation)
            {
                onPos1 = false;
                updatePos();
            }
        }*/
        timer += Time.deltaTime;
        if (timer >= maxTimer)
        {
            timer = 0;
            updatePos();
            if (onPos1)
            {
                onPos1 = false;
            }
            else if (!onPos1)
            {
                onPos1 = true;
            }
            
            
        }

    }

    public void move()
    {
        if (!onPos1)
        {
            //transform.position = Vector3.Lerp(actPos.position,pos1.position,Time.deltaTime);
            transform.rotation = Quaternion.Lerp(actPos.rotation,pos1.rotation,Time.deltaTime);
            //transform.rotation = Quaternion.Euler(rotationForce1, 0, 0);
        }

        if (onPos1)
        {
            //transform.position = Vector3.Lerp(actPos.position,pos2.position,Time.deltaTime);
            transform.rotation = Quaternion.Lerp(actPos.rotation,pos2.rotation,Time.deltaTime);
            //transform.rotation = Quaternion.Euler(rotationForce2, 0, 0);
            //transform.rotation = Quaternion.Lerp(actPos.rotation, rotationForce2, Time.deltaTime);
        }
    }

    public void updatePos()
    {
        actPos.position = transform.position;
        actPos.rotation = transform.rotation;
    }

}
