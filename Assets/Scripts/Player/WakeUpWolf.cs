using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WakeUpWolf : MonoBehaviour
{
    public SleepWolf sleepWolf; 

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Sleep Wolf")
        {
            sleepWolf.stealthActive = true;
            print("TOCADO");
        }
    }
}