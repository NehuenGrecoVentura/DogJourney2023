using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    [SerializeField] private float LifeTime;
    [SerializeField] private float MaxLife;
    [SerializeField] private Rigidbody rb;
    [SerializeField] float Speed;

    private void Start()
    {
        
    }

    void Update()
    {

        LifeTime -= Time.deltaTime;
        if (LifeTime <= 0)
        {
            BalaFactory.Instance.ReturnBala(this);
        }

        Move();
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

    public static void TurnOn(Bala b)
    {
        b.Reset();
        b.gameObject.SetActive(true);
    }

    public static void TurnOff(Bala b)
    {
        b.gameObject.SetActive(false);
    }


    private void OnCollisionEnter(Collision other)
    {
        TirosMovil temp;
        temp = other.gameObject.GetComponent<TirosMovil>();
        if (temp != null)
        {
            temp.gotHit();
            BalaFactory.Instance.ReturnBala(this);
        }
    }
}
