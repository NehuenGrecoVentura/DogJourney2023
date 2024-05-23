using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manzanas_Player : MonoBehaviour
{
    [SerializeField] GameObject Box;
    [SerializeField] int Lifes;
    [SerializeField] int Score;
    [SerializeField] Rigidbody _rb;
    [SerializeField] float speed;
    [SerializeField] private float baseSpeed;
    [SerializeField] Transform Izquierda;
    [SerializeField] Transform Derecha;
    [SerializeField] float Energy;
    [SerializeField] float MaxEnergy;
   // [SerializeField] Image bar;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void AddScore()
    {
        Score++;
    }

    public void RemoveLife()
    {
        Lifes--;
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift) == true)
        {
            Run(true);
        }
        else
        {
            Run(false);
        }
        var hor = Input.GetAxis("Horizontal");
        if (hor != 0)
        {
            Move(new Vector3(-hor, 2.85f, 0));
        }
    }

    public void Move(Vector3 dir)
    {
        dir.y = 2.85f;
        dir.z = 0;
        _rb.velocity = dir * speed;
        Box.transform.forward = dir;
    }
    
    public void Run(bool isRun)
    {
        if (Energy > 0)
        {
            if (isRun)
            {
                speed = baseSpeed * 2.5f;
                Energy = Energy - 1f * Time.deltaTime;
            }
            if (Energy <= 0) speed = baseSpeed;
        }

        if (!isRun)
        {
            speed = baseSpeed;
            Energy = Energy + 2f * Time.deltaTime;

            if (Energy > MaxEnergy) Energy = MaxEnergy;
        }

       //bar.fillAmount = Energy / MaxEnergy;
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
