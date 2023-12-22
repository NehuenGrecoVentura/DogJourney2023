using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UpDog3 : MonoBehaviour
{
    public Transform pos;
    [SerializeField] Animation _anim;
    public NavMeshAgent _agent;
    public Transform downPos;
    public Transform downRock;
    public Transform downRockTree;
    public Transform dog;
    public bool dogUp = false;
    public CapsuleCollider dogCol;
    public float speedMoveAnim;


    public UpDogManager manager;
    private void Start()
    {
        downPos.gameObject.SetActive(false);
        _anim.Stop("Up idle");
        _anim.Stop("Up walk");
    }


    private void FixedUpdate()
    {
        _anim["Up walk"].speed = speedMoveAnim;
        if (dogUp)
        {
            dog.transform.position = pos.position;
            dogCol.enabled = false;
            _agent.enabled = false;
            downPos.gameObject.SetActive(true);
            _anim.Play("Up idle");
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                _anim.Stop("Up idle");
                _anim.Play("Up walk");
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                _anim.Stop("Up idle");
                _anim.Stop("Up walk");
                dog.transform.position = downPos.position;
                dogUp = false;

            }
        }
        else
        {

            dogCol.enabled = true;
            _agent.enabled = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (dogUp && collision.gameObject.name == "Rock Up")
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                _anim.Stop("Up idle");
                _anim.Play("Up walk");
                dog.transform.position = downRock.position;
            }
        }

        if (dogUp && collision.gameObject.name == "Rock Tree")
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                _anim.Stop("Up idle");
                _anim.Play("Up walk");
                dog.transform.position = downRockTree.position;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "choripan")
       /* {
            if (Input.GetMouseButtonDown(0))
            {
                dogUp = true;
            }*/

        //   if (!manager.dogUp)   && Input.GetKeyDown(KeyCode.Alpha3)
        {
            if (Input.GetMouseButton(0) )
            {
                dogUp = true;
                //   manager.dogUp = true;
            }
        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "choripan")
        {
            _anim.Stop("Up idle");
            _anim.Stop("Up walk");
        }
    }
}