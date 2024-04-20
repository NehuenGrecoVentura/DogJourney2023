using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitEscape2022 : MonoBehaviour
{
    [HideInInspector] public bool escape = false;
    public float time = 2f;
    int _index;
    public Transform[] waypoints;
    public WolfPatrol2022 wolf;
    private Grabbable _grabbable;
    Animator _myAnim;

  
}
