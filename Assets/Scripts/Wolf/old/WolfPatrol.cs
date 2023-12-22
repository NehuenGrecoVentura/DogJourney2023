using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WolfPatrol : MonoBehaviour
{
    public Transform[] waypoints;
    public int speed;
    private int _index;
    private float _dist;
    public bool _playerDetected = false;
    public WolfPatrol patrol;

    void Start()
    {
        _index = 0;
        transform.LookAt(waypoints[_index].position);
    }

    void Update()
    {


        if (!_playerDetected)
        {
            _dist = Vector3.Distance(transform.position, waypoints[_index].position);
            if (_dist < 1f) AddIndex();
            Patrol();
        }
        else
        {
            patrol.enabled = false;
        }

    }

    private void Patrol()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void AddIndex()
    {
        _index++;
        if (_index >= waypoints.Length) _index = 0;
        transform.LookAt(waypoints[_index].position);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _playerDetected = true;
            print("El lobo detectó al jugador");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _playerDetected = true;
            print("El lobo detectó al jugador");
        }

    }



}
