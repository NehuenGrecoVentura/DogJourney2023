using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfWorld : MonoBehaviour
{


    [SerializeField] private Transform SpawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.transform.position = SpawnPoint.position;
        }
    }
}
