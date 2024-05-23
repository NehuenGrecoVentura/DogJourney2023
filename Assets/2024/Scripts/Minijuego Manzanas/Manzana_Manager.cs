using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manzana_Manager : MonoBehaviour
{
    [SerializeField] private float SpawnerTimer;
    [SerializeField] private float SpawnerTimerMax;
    [SerializeField] private float SpawnerTimerMin;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private float Maxspeed;
    [SerializeField] private float Minspeed;
    [SerializeField] private Transform Izquierda;
    [SerializeField] private Transform Derecha;
    [SerializeField] private int RandomWay;
    void Spawn()
    {
        var m = ManzanaFactory.Instance.pool.GetObject();
        Random();

        m.transform.position = transform.position + transform.forward;
        m.transform.forward = transform.forward;
    }

    void move()
    {
        if (RandomWay >= 5)
        {
            rb.velocity = Vector3.left * speed;
        }
        else
        {
            rb.velocity = Vector3.right * speed;
        }
        
    }

    void Random()
    {
        RandomWay = UnityEngine.Random.Range(0, 10);
        speed = UnityEngine.Random.Range(Minspeed, Maxspeed);
        SpawnerTimer = UnityEngine.Random.Range(SpawnerTimerMin, SpawnerTimerMax);
    }

    private void Update()
    {
        move();
        SpawnerTimer -= Time.deltaTime;
        if (SpawnerTimer <= 0)
        {
            
            Spawn();
            Random();
        }

        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "OutBoxI")
        {
            transform.position = Derecha.position;
        }
        if(other.gameObject.name == "OutBoxD")
        {
            transform.position = Izquierda.position;
        }
    }
}
