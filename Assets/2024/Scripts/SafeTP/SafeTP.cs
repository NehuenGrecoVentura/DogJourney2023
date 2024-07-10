using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeTP : MonoBehaviour
{
    [SerializeField] private Transform PointForest;
    [SerializeField] private Transform PointSavana;
    [SerializeField] private Transform PointSnow;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Dog;
    [SerializeField] private GameObject Trolley;
    [SerializeField] private int Stage;

    public void SafeZone()
    {
        if (Stage == 0)
        {
            Player.transform.position = PointForest.position;
            Dog.transform.position = PointForest.position;
            Trolley.transform.position = PointForest.position;
        }
        if (Stage == 1)
        {
            Player.transform.position = PointSavana.position;
            Dog.transform.position = PointSavana.position;
            Trolley.transform.position = PointSavana.position;   
        }
        if (Stage == 2)
        {
            Player.transform.position = PointSnow.position;
            Dog.transform.position = PointSnow.position;
            Trolley.transform.position = PointSnow.position; 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.name == "Zone1")
        {
            Stage = 0;
        }
        if (other.gameObject.name == "Zone2")
        {
            Stage = 1;
        }
        if (other.gameObject.name == "Zone3")
        {
            Stage = 2;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            SafeZone();
        }
    }
}
