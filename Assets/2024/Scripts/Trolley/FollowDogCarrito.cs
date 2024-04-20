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
    [SerializeField] float _speedRot = 5f; 

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

            //Vector3 targetPosition = Dog.transform.position - Dog.transform.forward * _dist;
            //_agent.SetDestination(targetPosition);



            // Obtener la posición detrás del personaje
            Vector3 targetPosition = Dog.transform.position - Dog.transform.forward * _dist;

            // Mover el carrito hacia esa posición
            _agent.SetDestination(targetPosition);

            // Calcular la dirección hacia la posición del personaje
            Vector3 direction = targetPosition - transform.position;
            direction.y = 0f;

            // Rotar suavemente el carrito hacia esa dirección
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _speedRot * Time.deltaTime);
            }
        } 
    }
}
