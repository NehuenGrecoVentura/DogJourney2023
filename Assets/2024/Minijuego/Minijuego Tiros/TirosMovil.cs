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
    [SerializeField] private SpriteRenderer Decal;
    [SerializeField] private Animator _animator;
    [SerializeField] private bool Hit;
    [SerializeField] private float AnimTimer;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Random();
        BC = GetComponent<BoxCollider>();
        Hit = false;
        PS.Stop();
    }

    private void Reset()
    {
        PS.Stop();
        AnimTimer = 0;
        Hit = false;
        rb.velocity = Vector3.zero;
        BC.enabled = true;
        Random();
    }

    private void Update()
    {
        move();
        if (Hit)
        {
            speed = 0;
            AnimTimer += Time.deltaTime;
            if (AnimTimer >= 1f)
            {
                Hit = false;
                Reset();
            }
        }
    }

    void move()
    {
       if(RandomWay >= 5f)
        {
            rb.velocity = Vector3.left * speed;
            Decal.flipX = false;
        }
        else if (RandomWay < 5f)
        {
            rb.velocity = Vector3.right * speed;
            Decal.flipX = true;
        }
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        spawnPoint.position = transform.position;
       /* if (!Target)
        {
            Random();
        }*/
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
       Hit = true;
       PS.Play();
       _animator.SetTrigger("Hit");
       BC.enabled = false;
        if (Target)
        {
            _tirosCharacter.AddScore();
        }
        else
        {
            _tirosCharacter.RemoveLive();
        }
    }
    void Random()
    {
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
