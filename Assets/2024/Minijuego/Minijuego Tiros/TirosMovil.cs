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
    [SerializeField] private ParticleSystem PS;
    [SerializeField] private BoxCollider BC;
    [SerializeField] private GameObject Decal;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Random();
        BC = GetComponent<BoxCollider>();
    }

    private void Reset()
    {
        rb.velocity = Vector3.zero;
        BC.enabled = true;
        Random();
        //PS.Stop();
    }

    private void Update()
    {
        move();
        if (PS.isEmitting)
        {
            PS.transform.position = new Vector3(spawnPoint.transform.position.x,-1,spawnPoint.transform.position.z);
            
        }
        else
        {
            PS.transform.position = new Vector3(transform.position.x,-1,transform.position.z);
        }
    }

    void move()
    {
        if (RandomWay >= 5)
        {
            rb.velocity = Vector3.left * speed;
            Decal.transform.rotation = new Quaternion(0f, 90f, 0f,0f);
        }
        else
        {
            rb.velocity = Vector3.right * speed;
            Decal.transform.rotation = new Quaternion(0f, 0f, 0f,0f);
        }
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        spawnPoint.position = transform.position;
        if (!Target)
        {
            Random();
        }
        if(other.gameObject.name == "OutBoxI")
        {
            Random();
            //transform.position = Derecha.position;
        }
        if(other.gameObject.name == "OutBoxD")
        {
            Random();
            
            //transform.position = Izquierda.position;
        }
    }

    public void gotHit()
    {
        spawnPoint.position = transform.position;
        if (Target)
        {
            PS.Play();
            _tirosCharacter.AddScore();
            BC.enabled = false;
            Reset();
            Random();

        }
        else
        {
            PS.Play();
            _tirosCharacter.RemoveLive();
            BC.enabled = false;
            Reset();
        }
    }
    void Random()
    {
        PS.Play();
        RandomWay = UnityEngine.Random.Range(0, 10);
        if (RandomWay >= 5)
        {
            transform.position = Izquierda.position;
        }
        else
        {
            transform.position = Derecha.position;
        }

        speed = UnityEngine.Random.Range(Minspeed, Maxspeed);
    }
    
}
