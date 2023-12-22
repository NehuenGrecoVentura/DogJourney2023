using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    public bool torch1 = false;
    public bool torch2 = false;
    public bool torch3 = false;


    private void Awake()
    {
        gameObject.AddComponent<Grabbable>();

    }
}
