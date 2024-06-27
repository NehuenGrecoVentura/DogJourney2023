using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonBoton : MonoBehaviour
{
    public int ID;
    [SerializeField] private Animator _animator;
    [SerializeField] private SimonManager manager;

    [Header("AUDIO")]
    [SerializeField] AudioSource _myAudio;
    [SerializeField] AudioClip _soundButton;
    [SerializeField] AudioClip _soundError;

    [Header("SELECT")]
    [SerializeField] Outline _outlineSelect;
    private Animator _myAnim;

    private void Awake()
    {
        _myAnim = GetComponent<Animator>();
        manager = FindObjectOfType<SimonManager>();
    }

    private void Start()
    {
        _outlineSelect.enabled = false;
    }

    public void Click()
    {
        _animator.enabled = true;
        _animator.SetTrigger("Click");
        _myAudio.PlayOneShot(_soundButton);
    }

    private void OnMouseDown()
    {
        if (!manager.SimonDiciendo)
        {
            Click();
            _myAnim.SetTrigger("Push");
            manager.PlayerClicking(this, _myAudio, _soundButton, _soundError);
            //_outlineSelect.enabled = false;
        }
    }

    private void OnMouseEnter()
    {
        _outlineSelect.enabled = true;
    }

    private void OnMouseExit()
    {
        _outlineSelect.enabled = false;
    }
}