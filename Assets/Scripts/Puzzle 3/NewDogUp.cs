using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NewDogUp : MonoBehaviour
{
    [SerializeField] private Transform UpPos;
    [SerializeField] private Transform downPos;
    //[SerializeField] private DogTest choripan;
    //[SerializeField] private DogTest bobby;
    //[SerializeField] private DogTest chungo;
    [SerializeField] private DogTest nearDog;
    [SerializeField] public bool isUp;
    [SerializeField] Animation _anim;
    [SerializeField] private float speedMoveAnim;
    [SerializeField] ParticleSystem[] _particlesDogs;


    public void FixedUpdate()
    {
        _anim["Up walk"].speed = speedMoveAnim * Time.realtimeSinceStartup;

        if (isUp)
        {
            foreach (var particle in _particlesDogs)
            {
                particle.Stop();
            }
            nearDog.transform.position = UpPos.position;
            //setAnim();
            _anim.Play("Up idle");
            if (Input.GetKeyDown(KeyCode.F))
            {
                _anim.Stop("Up idle");
                _anim.Stop("Up walk");
                dropDog();
            }
        }

        if (Input.GetKey(KeyCode.W) && isUp || Input.GetKey(KeyCode.A) && isUp || Input.GetKey(KeyCode.S) && isUp || Input.GetKey(KeyCode.D) && isUp)
        {
            _anim.Stop("Up idle");
            _anim.Play("Up walk");
        }

        else if (Input.GetKeyUp(KeyCode.W) && isUp || Input.GetKeyUp(KeyCode.A) && isUp || Input.GetKeyUp(KeyCode.S) && isUp || Input.GetKeyUp(KeyCode.D) && isUp)
        {
            _anim.Stop("Up walk");
            _anim.Play("Up idle");
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (nearDog == null) nearDog = other.GetComponent<DogTest>();
        if (nearDog != null)
        {
            if (Input.GetMouseButton(0))
            {
                if (!isUp) getDog();
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        var aux = other.GetComponent<DogTest>();
        if (aux != null)
        {
            if (!isUp) nearDog = null;
        }
    }

    public void getDog()
    {
        nearDog._agent.enabled = false;
        nearDog.body.enabled = false;
        isUp = true;
    }

    public void dropDog()
    {
        if (nearDog == null)
        {
            return;
        }
        nearDog.transform.position = downPos.position;
        nearDog._agent.enabled = true;
        nearDog.body.enabled = true;
        nearDog = null;
        isUp = false;
        foreach (var particle in _particlesDogs)
        {
            particle.Play();
        }
    }


    /* public void setAnim()
     {
         _anim["Up walk"].speed = speedMoveAnim;
         _anim.Play("Up idle");
         if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
         {
             _anim.Stop("Up idle");
             _anim.Play("Up walk");
         }

         if (Input.GetKeyDown(KeyCode.F))
         {
             _anim.Stop("Up idle");
             _anim.Stop("Up walk");
         }
     }*/
}
