using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogInteractEnd : MonoBehaviour
{
    public ParticleSystem particle;
    void Start()
    {
        particle.Stop();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name=="bobby (1)" && Input.GetKeyDown(KeyCode.F))
        {
            particle.Play();
        }
    }
}
