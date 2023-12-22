using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPlayer : MonoBehaviour
{
    public float speed = 5.0f;
    private Animation _myAnim;
    private bool _canJump;

    void Start()
    {
        _myAnim = GetComponent<Animation>();
        _myAnim.Play("Idle");
    }

    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(x, 0, y);
        transform.position += dir * speed * Time.deltaTime;
        if (dir != Vector3.zero) _myAnim.transform.forward = dir;
        if (Mathf.Abs(x) + Mathf.Abs(y) > 0)
        {
            _myAnim.Stop("Idle");
            _myAnim.Play("Run");
        }
        else
        {
            _myAnim.Play("Idle");
            _myAnim.Stop("Run");
        }

    }

    void Jump()
    {
        if(_canJump && Input.GetKeyDown(KeyCode.Space))
        {
            _myAnim.Play("Jump");

        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == 11) _canJump = true;
    }
}
