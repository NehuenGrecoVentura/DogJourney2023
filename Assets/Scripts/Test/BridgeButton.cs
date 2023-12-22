using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeButton : MonoBehaviour
{
    public GameObject bridge;
    public bool on;
    
    private void OnCollisionEnter(Collision other)
    {
        on = true;
    }

    private void Update()
    {
        if (on)
        {
            bridge.SetActive(true);
        }

        if (!on)
        {
            bridge.SetActive(false);
        }
            
    }
}
