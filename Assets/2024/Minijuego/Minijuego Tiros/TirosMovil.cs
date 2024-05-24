using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TirosMovil : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private TirosCharacter _tirosCharacter;
    [SerializeField] private float speed;
    [SerializeField] private float Maxspeed;
    [SerializeField] private float Minspeed;
    [SerializeField] private Transform Izquierda;
    [SerializeField] private Transform Derecha;
    [SerializeField] private int RandomWay;
    [SerializeField] private bool Target;
    [SerializeField] private Transform spawnPoint;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Random();
    }

    private void Reset()
    {
        rb.velocity = Vector3.zero;
        transform.position = spawnPoint.position;
    }

    private void Update()
    {
        move();
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

    public void gotHit()
    {
        if (Target)
        {
            _tirosCharacter.AddScore();
            Reset();
            Random();
        }
        else
        {
            _tirosCharacter.RemoveLive();
            Reset();
            Random();
        }
    }
    void Random()
    {
        var RandomX = UnityEngine.Random.Range(Izquierda.position.x, Derecha.position.x);
        spawnPoint.position = new Vector3(RandomX, spawnPoint.position.y, spawnPoint.position.z);
        RandomWay = UnityEngine.Random.Range(0, 10);
        speed = UnityEngine.Random.Range(Minspeed, Maxspeed);
    }
}
