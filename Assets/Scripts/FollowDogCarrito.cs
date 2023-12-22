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
            _agent.destination = Dog.transform.position;
        } 
    }
}
