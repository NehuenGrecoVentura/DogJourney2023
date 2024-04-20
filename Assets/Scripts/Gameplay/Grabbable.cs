using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    public bool IsGrabbed { get => isGrabbed; }

    private Prompt _prompt;
    private Transform _interactorHand;
    private bool isGrabbed = false;

    private Collider _collider;


}
