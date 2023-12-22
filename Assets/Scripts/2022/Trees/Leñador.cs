using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leñador : MonoBehaviour
{

    [SerializeField] private int Logs;
    [SerializeField] private int maxLogs;
    [SerializeField] private int Money;
    [SerializeField] private KeyCode buttonPickUp = KeyCode.F;
    [SerializeField] private KeyCode buttonDrop = KeyCode.G;
    
    public bool getLogs()
    {
        if (Logs < maxLogs)
        {
            Logs++;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void sellLogs()
    {
        if (Logs > 0)
        {
            Logs--;
            Money += 5;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        NewAxe temp;
        temp = other.GetComponent<NewAxe>();
        if (temp != null)
        {
            temp.ChangeInteract(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        NewAxe temp;
        temp = other.GetComponent<NewAxe>();
        if (temp != null)
        {
            if (Input.GetKeyDown(buttonPickUp))
            {
                temp.GetPicked();
            }

            if (Input.GetKeyDown(buttonDrop))
            {
                temp.GetDroped(transform);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        NewAxe temp;
        temp = other.GetComponent<NewAxe>();
        if (temp != null)
        {
            temp.ChangeInteract(false);
        } 
    }
}
