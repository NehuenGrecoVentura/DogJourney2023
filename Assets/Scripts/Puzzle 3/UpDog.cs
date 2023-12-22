using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UpDog : MonoBehaviour
{
    public Transform pos;
    [SerializeField] Animation _anim;
    //public NavMeshAgent _agent;
    public Transform downPos;
    public Transform downRock;
    public Transform downRockTree;
    /// <summary>
    /// //////////////////////////////
    /// </summary>
    /// 
    public Transform dog2;
    public bool dogUp2 = false;
    public NavMeshAgent _agent2;
    public CapsuleCollider dogCol2;

    public UpDogManager manager;







    private void Start()
    {
        downPos.gameObject.SetActive(false);
        _anim.Stop("Up idle");
        _anim.Stop("Up walk");
    }


    private void FixedUpdate()
    {
        
        if (dogUp2)
        {
            dog2.transform.position = pos.position;
            dogCol2.enabled = false;
            _agent2.enabled = false;
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
                dog2.transform.position = downPos.position;
                dogUp2 = false;
            }
        }

        else
        {
           
            dogCol2.enabled = true;
            _agent2.enabled = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (dogUp2 && collision.gameObject.name == "Rock Up")
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                _anim.Stop("Up idle");
                _anim.Play("Up walk");
                dog2.transform.position = downRock.position;
            }
        }

        if (dogUp2 && collision.gameObject.name == "Rock Tree")
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                _anim.Stop("Up idle");
                _anim.Play("Up walk");
                dog2.transform.position = downRockTree.position;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "bobby")
        {
           /* if (Input.GetMouseButtonDown(0))
            {
                dogUp2 = true;
            }*/


          if (!manager.dogUp)//&& Input.GetKeyDown(KeyCode.Alpha2)
           {
               if (Input.GetMouseButton(0)) 
               {
                   dogUp2 = true;
                  // manager.dogUp = true;
               }
           }
           
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "bobby")
        {
            _anim.Stop("Up idle");
            _anim.Stop("Up walk");
        }
    }
}