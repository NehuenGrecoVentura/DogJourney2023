using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    private Grabbable _grabbable;

    void Start()
    {
        _grabbable = gameObject.AddComponent<Grabbable>();
    }
}