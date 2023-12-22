using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpGeisers : MonoBehaviour
{
    [SerializeField] Rigidbody _rb;
    private float _jumpForce;
    public GeiserPuzzle4 button1;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 13 && other.gameObject.name == "Sensor Geiser 1" && button1.button1On)
        {
            _jumpForce = 200000f;
            _rb.AddForce(transform.up * _jumpForce * Time.deltaTime);
        }
    }
}
