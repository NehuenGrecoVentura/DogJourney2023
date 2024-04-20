using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class FishingZone : MonoBehaviour
{
    [SerializeField] private FishingMinigame fishing;
    [SerializeField] private BoxCollider zone;
    [SerializeField] private float CD;
    [SerializeField] private float MaxCD;
    [SerializeField] private float MinCD;
    [SerializeField] private Material CDMat;
    [SerializeField] private Transform outPoint;
    [SerializeField] private GameObject player;

    private void Update()
    {
        CD -= Time.deltaTime;
        if (CD > 0)
        {
            CDMat.color = Color.red;
        }
        else
        {
            CDMat.color = Color.green;
        }

    }

    private void OnTriggerEnter(Collider zone)
    {
        if (CD <= 0)
        {
            fishing.start = true;
            player.transform.position = outPoint.position;
            CD = Random.Range(MinCD, MaxCD);
        }
    }
    
}
