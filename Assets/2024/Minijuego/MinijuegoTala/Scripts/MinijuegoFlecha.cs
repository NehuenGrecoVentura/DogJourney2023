using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class MinijuegoFlecha : MonoBehaviour
{
    [SerializeField] private Material GuessMat;
    [SerializeField] private Material QueueMat;
    [SerializeField] private Material GoodMat;
    [SerializeField] private Material WrongMat;
    [SerializeField] public int ID;
    [SerializeField] private MeshRenderer mesh;
    private int test;

    // IDs: Up=0 Left=1 Right=2 Down=3
    private void Start()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    public void Right()
    {
        mesh.material = GoodMat;
    }

    public void Wrong()
    {
        mesh.material = WrongMat;
    }

    public void Active()
    {
        mesh.material = GuessMat;
    }

    public void Queue()
    {
        mesh.material = QueueMat;
    }
}
