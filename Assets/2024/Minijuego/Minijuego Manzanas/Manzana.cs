using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manzana : MonoBehaviour
{
    [SerializeField] private Manzanas_Player Player;
    [SerializeField] private float LifeTime;
    [SerializeField] private float MaxLife;
    [SerializeField] private Rigidbody rb;
    [SerializeField] float Speed;

    private void Start()
    {
        Player = FindObjectOfType<Manzanas_Player>();
    }

    void Update()
    {

        LifeTime -= Time.deltaTime;
        if (LifeTime <= 0)
            ManzanaFactory.Instance.ReturnManzana(this);
        //Move();


    }

    private void Move()
    {
        transform.position += transform.forward * Speed * Time.deltaTime;
    }

    private void Reset()
    {
        LifeTime = MaxLife;
        rb.velocity = Vector3.zero;
    }

    public static void TurnOn(Manzana b)
    {
        b.Reset();
        b.gameObject.SetActive(true);
    }

    public static void TurnOff(Manzana b)
    {
        b.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "CatchBox")
        {
            Debug.Log("Atrapado");
            ManzanaFactory.Instance.ReturnManzana(this);
            Player.AddScore();
        }

        if (other.gameObject.name == "Suelo")
        {
            Player.RemoveLive();
            ManzanaFactory.Instance.ReturnManzana(this);
        }


        if (other.gameObject.name == "Side")
        {
            ManzanaFactory.Instance.ReturnManzana(this);
        }

    }
}