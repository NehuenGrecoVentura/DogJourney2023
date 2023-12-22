using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomSound : MonoBehaviour
{
    public AudioSource _Source;
    public AudioClip[] _clip;
    public float randTime;
    public float timer;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= randTime)
        {
            ChangeAndPlay();
        }
    }

    public void ChangeAndPlay()
    {
        randTime = Random.Range(0, 20);
        timer = 0;
        int aux = Random.Range(0,_clip.Length);
        if (!_Source.isPlaying)
        {
            _Source.clip = _clip[aux];
            _Source.Play();
        }
    }
}
