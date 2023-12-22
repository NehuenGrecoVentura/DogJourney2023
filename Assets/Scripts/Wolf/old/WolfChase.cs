using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfChase : MonoBehaviour
{
    private WolfPatrol patrol;
    public Transform target;
    public float t;

    void Start()
    {
        //patrol = FindObjectOfType<WolfPatrol>();
        patrol = GetComponent<WolfPatrol>();
    }

    private void FixedUpdate()
    {
        if (patrol._playerDetected)
        {
            Vector3 a = gameObject.transform.position;
            Vector3 b = target.position;
            transform.position = Vector3.Lerp(a, b, t);
            transform.LookAt(target);
        }
    }
}
