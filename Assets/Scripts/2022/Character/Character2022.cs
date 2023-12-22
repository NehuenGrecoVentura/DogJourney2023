using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Character2022 : MonoBehaviour
{
    Rigidbody _rb;
    [HideInInspector] public Animator myAnim;
    [HideInInspector] public float _speedAux;
    public float speed = 10f;
    public float speedRun = 15f;
    public float speedCrouch = 7f;
    public float gravity = -9.8f;
    public float jumpForce;
    public Transform cam;
    [SerializeField] private float rayDist;
    [SerializeField] private bool test;
    [SerializeField] private bool test2;
    [SerializeField] private Transform rayPoint;
    [SerializeField] private Transform rayPoint2;
    [SerializeField] private AudioSource _myAudio;
    [SerializeField] private AudioClip walkClip;
    [SerializeField] private float audioTimer;
    GManager _gm;

    [HideInInspector] public bool isClimb = false;

    void Awake()
    {
        myAnim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        _gm = FindObjectOfType<GManager>();
    }

    private void Start()
    {
        _speedAux = speed;
    }

    private void FixedUpdate()
    {
        if (!_gm.gameOver) MoveRigibody(); // SI PIERDO NO ME MUEVO
    }

    void MoveRigibody()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        Vector3 move = Vector3.zero;
        float moveSpeed = 0;

        if ((hor != 0 || ver != 0) && !isClimb) // Si me estoy moviendo y no estoy escalando...
        {
            //Logica del movimiento general del personaje
            Vector3 forward = cam.forward;
            forward.y = 0;
            forward.Normalize();
            Vector3 right = cam.right;
            right.y = 0;
            Vector3 dir = forward * ver + right * hor;
            moveSpeed = Mathf.Clamp01(dir.magnitude);
            test = Physics.Raycast(rayPoint.position, dir, rayDist);
            test2 = Physics.Raycast(rayPoint2.position, dir, rayDist);
            if (!test && !test2)
            {
                dir.Normalize();
                _rb.MovePosition(transform.position + dir * speed * moveSpeed * Time.deltaTime);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 0.2f);

                // Si no estoy corriendo y no estoy agachado, entonces camino.
                if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.LeftControl)) WalkAnim();

                // Si no estoy agachado, entonces corro.
                else if (Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.LeftControl)) RunAnim();

                // Si no corro, entonces estoy agachado.
                else if (Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.LeftShift)) WalkCrouchAnim();
            }            
        }

        else // Si estoy quieto...
        {
            // Si no me agacho o corro, entonces estoy en idle.
            if (!Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.LeftShift)) IdleAnim();

            // Si me agacho y no corro, entonces estoy en agachado en idle.
            else if (Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.LeftShift)) IdleCrouchAnim();
        }

        move.y += gravity * Time.deltaTime;

    }

    #region ANIMS

    void WalkAnim()
    {
        myAnim.SetBool("isWalk", true);
        myAnim.SetBool("isRun", false);
        myAnim.SetBool("isCrouch", false);
        myAnim.SetBool("isWalkCrouch", false);
        speed = _speedAux;
    }

    void RunAnim()
    {
        myAnim.SetBool("isWalk", false);
        myAnim.SetBool("isRun", true);
        myAnim.SetBool("isCrouch", false);
        myAnim.SetBool("isWalkCrouch", false);
        speed = speedRun;
    }

    void IdleAnim()
    {
        myAnim.SetBool("isWalk", false);
        myAnim.SetBool("isRun", false);
        myAnim.SetBool("isCrouch", false);
        myAnim.SetBool("isWalkCrouch", false);
        speed = _speedAux;
    }

    void IdleCrouchAnim()
    {
        myAnim.SetBool("isWalk", false);
        myAnim.SetBool("isRun", false);
        myAnim.SetBool("isCrouch", true);
        myAnim.SetBool("isWalkCrouch", false);
        speed = speedCrouch;
    }

    void WalkCrouchAnim()
    {
        myAnim.SetBool("isWalk", false);
        myAnim.SetBool("isRun", false);
        myAnim.SetBool("isCrouch", false);
        myAnim.SetBool("isWalkCrouch", true);
        speed = speedCrouch;
    }

    #endregion

    public void MoveInStairs(float speedClimb)
    {
        isClimb = true;
        EjecuteAnim("Up Stairs New");
        _rb.useGravity = false;
        _rb.MovePosition(transform.position + Vector3.up * speedClimb * Time.fixedDeltaTime);
        transform.rotation = Quaternion.Euler(0, 90, 0);
    }

    public void PickIdleAnim()
    {
        EjecuteAnim("Pick Idle");
    }

    public void PickWalkAnim()
    {
        EjecuteAnim("Pick Walk");
    }


    public void EjecuteAnim(string animName)
    {
        myAnim.Play(animName);
    }
}