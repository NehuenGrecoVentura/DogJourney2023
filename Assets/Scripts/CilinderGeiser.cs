using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CilinderGeiser : MonoBehaviour
{
    private Grabbable _grabbable;

    private void Awake()
    {
        _grabbable = gameObject.AddComponent<Grabbable>();
    }



}
