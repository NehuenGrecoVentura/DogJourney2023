using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotRabbit : MonoBehaviour, IRotate
{
    [Header("INTERACTIVE CONFIG")]
    [SerializeField] KeyCode _buttonInteractive = KeyCode.F;
    [SerializeField] GameObject _iconInteractive;
    [SerializeField] float _speedRot;
    float _auxSpeedRot;

    [SerializeField] Transform _handPos;
    public bool carrotPick = false;

    private void Start()
    {
        _iconInteractive.SetActive(false);
        _auxSpeedRot = _speedRot;
    }

    private void Update()
    {
        if (carrotPick)
        {
            _speedRot = 0;
            transform.position = _handPos.position;
        }

        else if (carrotPick && Input.GetKeyDown(_buttonInteractive)) carrotPick = false;

        //if (!carrotPick) RotateObject(0, 0, _auxSpeedRot);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character2022>();

        if (player != null)
        {
            if (!Input.GetKeyDown(_buttonInteractive) && !carrotPick)
                _iconInteractive.SetActive(true);

            else if (Input.GetKeyDown(_buttonInteractive) && !carrotPick)
            {
                _iconInteractive.SetActive(false);
                carrotPick = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character2022>();

        if (player != null)
        {
            if (!Input.GetKeyDown(_buttonInteractive) && !carrotPick)
                _iconInteractive.SetActive(true);

            else if (Input.GetKeyDown(_buttonInteractive) && !carrotPick)
            {
                _iconInteractive.SetActive(false);
                carrotPick = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character2022>();
        if (player != null) _iconInteractive.SetActive(false);
    }

    public void RotateObject(float x, float y, float z)
    {
        transform.Rotate(x, y, z);
    }
}