using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowDogCarrito : MonoBehaviour
{
    private NavMeshAgent _agent;
    public bool DogFriend;
    [SerializeField] GameObject _player;
    [SerializeField] private GameObject Dog;

    [SerializeField] float _dist; 

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (!DogFriend)
        {
            _agent.destination = _player.transform.position;
        }
        else
        {

            //_agent.destination = Dog.transform.position;

            Vector3 targetPosition = Dog.transform.position - Dog.transform.forward * _dist;


            _agent.SetDestination(targetPosition);
        } 
    }
}
