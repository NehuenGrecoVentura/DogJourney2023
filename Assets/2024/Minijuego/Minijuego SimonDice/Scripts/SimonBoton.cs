using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonBoton : MonoBehaviour
{
    public int ID;
    [SerializeField] private Animator _animator;
    [SerializeField] private SimonManager manager;

    private void Start()
    {
        manager = FindObjectOfType<SimonManager>();
    }

    public void Click()
    {
        _animator.enabled = true;
        _animator.SetTrigger("Click");
    }

    private void OnMouseDown()
    {
        if (!manager.SimonDiciendo)
        {
            Click();
            manager.PlayerClicking(this);
            Debug.Log("click " + ID);
        }

    }
}
