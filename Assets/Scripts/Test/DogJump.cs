using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DogJump : MonoBehaviour
{
    public Transform points;
    public DogTest dog;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            dog = other.gameObject.GetComponent<DogTest>();
            if (dog != null)
            {
                dog.nvm.enabled = false;
                dog.target.transform.position = points.transform.position;
                dog.gameObject.transform.position = points.transform.position;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            dog = other.gameObject.GetComponent<DogTest>();
            if (dog != null)
            {
                dog.target.transform.position = points.transform.position;
                dog.gameObject.transform.position = points.transform.position;
                dog.nvm.enabled = true;
            }
        }
    }
}
