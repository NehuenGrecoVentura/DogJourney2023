using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WolfPatrolLvl2 : MonoBehaviour
{
    public GameObject target;
    public Transform[] savedPoint;
    public NavMeshAgent _Agent;
    public bool onSight;
    public int Index;
    public float TargetRadius;
    public Vector3 TargetDistance;
    public GameObject player;
    public DogOrders _player;
    public AudioSource _Audio;
    public GameObject iconAlert;

    DogTest[] _dogs;
    public GameObject iconDetectedDog;

    private void Start()
    {
        iconAlert.gameObject.SetActive(false);
        _dogs = FindObjectsOfType<DogTest>();
        iconDetectedDog.gameObject.SetActive(false);
    }

    void Update()
    {
        if (onSight)
        {
            chase();
        }
        if (!onSight)
        {
            patrol();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            iconDetectedDog.gameObject.SetActive(true);
            iconAlert.gameObject.SetActive(true);
            onSight = true;
            _Audio.Play();
            print("El lobo detectó al jugador");
            foreach (var dog in _dogs)
            {
                dog.Scared = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            iconDetectedDog.gameObject.SetActive(false);
            iconAlert.gameObject.SetActive(false);
            onSight = false;
            print("El lobo detectó al jugador");
            foreach (var dog in _dogs)
            {
                dog.Scared = false;
            }
        }
    }

    public void patrol()
    {
        target.transform.position = savedPoint[Index].position;
        _Agent.destination = target.transform.position;
        TargetDistance = target.transform.position - this.transform.position;
        if (TargetDistance.magnitude <= TargetRadius)
        {
            if (Index >= savedPoint.Length - 1)
            {
                Index = 0;
            }
            else
            {
                Index++;
            }
        }
    }

    public void chase()
    {
        target.transform.position = player.transform.position;
        _Agent.destination = target.transform.position;
    }
}
