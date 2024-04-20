using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditorInternal;
using UnityEngine;

public class MusicZone : MonoBehaviour
{

    [SerializeField] private AudioClip Music1;
    [SerializeField] private AudioClip ActualClip;
    [SerializeField] private AudioSource Audio;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ActualClip = Audio.clip;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (ActualClip != Music1)
            {
                ActualClip = Music1;
                Audio.clip = ActualClip;
                Audio.Play();
            }
        }
    }
}
