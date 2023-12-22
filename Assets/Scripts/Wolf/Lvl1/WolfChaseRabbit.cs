using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WolfChaseRabbit : MonoBehaviour
{
    [SerializeField] private Transform _rabbit;
    [SerializeField] private float _time;
    private NavMeshAgent _agent;
    public BoxCollider colliderRockAxe;
    public BoxCollider colliderDogRock;
    public GameObject target;

    private int _index;
    public Transform[] waypoints;

    private void Start()
    {
        colliderDogRock.enabled = false;
        _agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        Destroy(colliderRockAxe);
        colliderDogRock.enabled = true;
        /* Vector3 a = gameObject.transform.position;
         Vector3 b = _rabbit.position;
         transform.position = Vector3.Lerp(a, b, _time * Time.deltaTime);
         transform.LookAt(_rabbit);*/
        ChaseRabbit();
    }

    void ChaseRabbit()
    {
        /*  _agent.speed = 20f;
          target.transform.position = _rabbit.transform.position;
          _agent.destination = target.transform.position;*/
        Destroy(_agent);
        if (transform.position != waypoints[_index].position) transform.position = Vector3.MoveTowards(transform.position, waypoints[_index].position, _time * Time.deltaTime);
        else _index = (_index + 1) % waypoints.Length;
        transform.LookAt(waypoints[_index].position);



        if (transform.position.x < -39f) Destroy(gameObject);

    }
}