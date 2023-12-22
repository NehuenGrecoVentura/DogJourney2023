using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DogOrders : MonoBehaviour
{
    public List<DogTest> _dog;
    private int DogCount;
    private int dogNumber;
    //public Material mat;
    private BoxCollider bc;
    public DogTest test;

    [Header("DOGS SOUNDS")]
    public AudioSource _Source;
    public AudioClip[] _clip;
    public AudioClip allDogsWhistle;

    [Header("ICONS FEEDBACK ORDERS")]
    public ParticleSystem feedbackOrder;
    public ParticleSystem feedbackStop;
    public ParticleSystem feedbackReunion;

    [Header("ICONS SELECTED DOG")]
    public GameObject iconSelectedDog1;
    public GameObject iconSelectedDog2;
    public GameObject iconSelectedDog3;

    private void Awake()
    {
        DogCount = _dog.Count;
        dogNumber = 0;
        bc = FindObjectOfType<BoxCollider>();
        feedbackOrder.Stop();
        feedbackStop.Stop();
        feedbackReunion.Stop();
        Dog1Selected();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            _dog[dogNumber].OrderGO();
            whistle();
            feedbackOrder.Play();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            _dog[dogNumber].OrderStay();
            whistle();
            feedbackStop.Play();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            for (int i = 0; i < DogCount; i++)
            {
                _dog[i].OrderGO();
                //whistle();
                _Source.PlayOneShot(allDogsWhistle);
                feedbackReunion.Play();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            dogNumber = 0;
            //mat.color = Color.yellow;
            Dog1Selected();
            
        }
        if (dogNumber == 0)
        {
           // mat.color = Color.yellow;
        }

        if (_dog.Count >= 2)
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                dogNumber = 1;
                //mat.color = Color.blue;
                Dog2Selected();
            }
            if (dogNumber == 1)
            {
                // mat.color = Color.blue;
                Dog2Selected();
            }

            if (_dog.Count >= 3)
            {
                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    dogNumber = 2;

                    //mat.color = Color.red;
                    Dog3Selected();
                }
                if (dogNumber == 3)
                {
                    //mat.color = Color.red;
                    Dog3Selected();
                }
}
        }
        
        
    }
    
    public void dogFlee()
    {
        for (int i = 0; i < DogCount; i++)
        {
            _dog[i].Scared = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        test = other.GetComponent<DogTest>();
        if (test)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                  test.Pet();  
            }
        }
    }

    public void whistle()
    {
        int aux = Random.Range(0,_clip.Length);
        if (!_Source.isPlaying)
        {
            _Source.clip = _clip[aux];
            _Source.Play();
        }
    }

    void Dog1Selected()
    {
        iconSelectedDog1.gameObject.SetActive(true);
        iconSelectedDog2.gameObject.SetActive(false);
        iconSelectedDog3.gameObject.SetActive(false);
    }

    void Dog2Selected()
    {
        iconSelectedDog1.gameObject.SetActive(false);
        iconSelectedDog2.gameObject.SetActive(true);
        iconSelectedDog3.gameObject.SetActive(false);
    }

    void Dog3Selected()
    {
        iconSelectedDog1.gameObject.SetActive(false);
        iconSelectedDog2.gameObject.SetActive(false);
        iconSelectedDog3.gameObject.SetActive(true);
    }

}
