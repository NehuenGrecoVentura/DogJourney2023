using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana : MonoBehaviour
{
    private Quaternion _initialRot;

    private void Start()
    {
        _initialRot = transform.rotation;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.rotation = Quaternion.Euler(90, -180, 0);
        }

        if (Input.GetKeyDown(KeyCode.E)) transform.rotation = _initialRot;
    }
}