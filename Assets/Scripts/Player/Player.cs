using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float _hor, _ver;
    private bool _isCrouch = false;
    private bool _canJump = true;
    private Rigidbody _rb;
    private Animator _myAnim;
    public float speed = 5.0f;
    public float jumpForce = 20.0f;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _myAnim = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        Run();
        Crouch();
        Jump();
    }

    void Move()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(x, 0, y);
        transform.position += dir * speed * Time.deltaTime;
        if (dir != Vector3.zero) _myAnim.transform.forward = dir;
        if (Mathf.Abs(x) + Mathf.Abs(y) > 0) // SI ESTOY CAMINANDO
        {
            if (!_isCrouch) // NO ESTOY AGACHADO
            {
                _myAnim.SetBool("Run", true);
                _myAnim.SetBool("Idle", false);
            }
            else // ESTOY AGACHADO
            {
                _myAnim.SetBool("Crouch walk", true);
            }
            
        }
        else // NO ESTOY CAMINANDO
        {
            if (!_isCrouch) // NO ESTOY AGACHADO 
            {
                _myAnim.SetBool("Idle", true);
                _myAnim.SetBool("Run", false);
                _myAnim.SetBool("Crouch", false);
                _myAnim.SetBool("Crouch walk", false);
            }
            else // ESTOY AGACHADO
            {
                _myAnim.SetBool("Crouch", true);
                _myAnim.SetBool("Crouch walk", false);

            }
        }
    }

    void Run()
    {
        if(Input.GetKey(KeyCode.LeftShift)) speed = 10.0f;
        if(Input.GetKeyUp(KeyCode.LeftShift)) speed = 5.0f;
    }

    void Crouch()
    {
        if(Input.GetKey(KeyCode.LeftControl) && !_isCrouch)
        {
            _myAnim.SetBool("Crouch walk", false);
            _myAnim.SetBool("Crouch", true);
            _myAnim.SetBool("Idle", false);
            _myAnim.SetBool("Run", false);
            _isCrouch = true;
        }

        else if (Input.GetKeyUp(KeyCode.LeftControl) && _isCrouch)
        {
            _myAnim.SetBool("Crouch walk", false);
            _myAnim.SetBool("Crouch", false);
            _myAnim.SetBool("Idle", true);
            _myAnim.SetBool("Run", false);
            _isCrouch = false;
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _canJump)
        {
            _rb.AddForce(transform.up * jumpForce);
            _canJump = false;
            _myAnim.SetTrigger("Jump");
        }

       /* if (!_canJump)
        {
            _myAnim.SetBool("Jump", true);
        }*/
    }


    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == 11)
        {
           // _myAnim.SetBool("Jump", false);
            _canJump = true;
        }
    }

}
