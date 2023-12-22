using UnityEngine;
using System;

public class ModelCharacter
{
    private Rigidbody _rb;
    private float _speed, _speedRun, _speedCrouch, _speedAux, _gravity;
    private bool _isClimb;
    private Transform _myTransform, _camPos;
    private OrderDog _orderDog;
    public event Action EventIdle, EventWalk, EventRun, EventIdleCrouch, EventWalkCrouch, EventPickIdle, EventPickWalk, EventClimb, EventIdleCallDog, EventCallMoveDog, EventHitTree; 
    
    public ModelCharacter(Rigidbody rb, float speed, float speedRun, float speedCrouch, float speedAux, bool isClimb, Transform transform, Transform camPos, float gravity, OrderDog orderDog)
    {
        _rb = rb;
        _speed = speed;
        _speedRun = speedRun;
        _speedCrouch = speedCrouch;
        _speedAux = speedAux;
        _isClimb = isClimb;
        _myTransform = transform;
        _camPos = camPos;
        _gravity = gravity;
        _orderDog = orderDog;
        _speedAux = _speed;
    }

    public void Movement(float h, float v)
    {
        Vector3 move = Vector3.zero;
        float moveSpeed = 0;

        if ((h != 0 || v != 0) && !_isClimb) // Si me estoy moviendo y no estoy escalando...
        {
            //Logica del movimiento general del personaje
            Vector3 forward = _camPos.forward;
            forward.y = 0;
            forward.Normalize();
            Vector3 right = _camPos.right;
            right.y = 0;
            Vector3 dir = forward * v + right * h;
            moveSpeed = Mathf.Clamp01(dir.magnitude);
            dir.Normalize();
            _rb.MovePosition(_myTransform.position + dir * _speed * moveSpeed * Time.deltaTime);
            _myTransform.rotation = Quaternion.Slerp(_myTransform.rotation, Quaternion.LookRotation(dir), 0.2f);
        }

        move.y += _gravity * Time.deltaTime;
    }

    public void Idle(bool itemPicked)
    {
        if (!itemPicked) EventIdle?.Invoke();
        else EventPickIdle?.Invoke();
        _speed = _speedAux;
    }

    public void Walk(bool itemPicked)
    {
        if (!itemPicked) EventWalk?.Invoke();
        else EventPickWalk?.Invoke();
        _speed = _speedAux;
    }

    public void Run()
    {
        EventRun?.Invoke();
        _speed = _speedRun;
    }

    public void IdleCrouch()
    {
        EventIdleCrouch?.Invoke();
        _speed = _speedCrouch;
    }

    public void CrouchWalk()
    {
        EventWalkCrouch?.Invoke();
        _speed = _speedCrouch;
    }

    public void CallIdleDog()
    {
        EventIdleCallDog?.Invoke();
        _orderDog.CallDog();
    }

    public void CallMoveDog()
    {
        EventCallMoveDog?.Invoke();
        _orderDog.CallDog();
    }

    public void CallAllDogs()
    {
        EventCallMoveDog?.Invoke();
        _orderDog.CallAllDogs();
    }

    public void SelectDogs(KeyCode inputInitialDog, KeyCode inputNextDog)
    {
        _orderDog.SelectDog(inputInitialDog, inputNextDog);
    }

    public void StopDog()
    {
        _orderDog.StopDog();
    }

    public void HitTree()
    {
        EventHitTree?.Invoke();
    }

    public void FreezePlayer(RigidbodyConstraints rb)
    {
        _rb.constraints = rb;
    }
}