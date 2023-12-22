using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WolfCaveLvl4 : MonoBehaviour
{
    public Transform exitCave1;
    public Transform exitCave2;
    private NavMeshAgent _myAgent;
    public Transform player;
    public Transform exitChoripan;
    public Transform exitBobby;
    public Transform caveEnd;
    public Transform caveEnd2;

    private void Start()
    {
        _myAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (transform.position == exitCave1.position && gameObject.name == "choripan") _myAgent.enabled = true;
        if (transform.position == exitCave2.position && gameObject.name == "bobby") _myAgent.enabled = true;
        if (transform.position == caveEnd.position && gameObject.name == "choripan") _myAgent.enabled = true;
        if (transform.position == caveEnd2.position && gameObject.name == "bobby") _myAgent.enabled = true;
        if (player.position.z > 160f && transform.position.z < 160f)
        {
            _myAgent.enabled = false;
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (gameObject.name == "choripan")
                {
                    transform.position = exitChoripan.position;
                    _myAgent.enabled = true;
                }
                    
                if(gameObject.name == "bobby")
                {
                    transform.position = exitBobby.position;
                    _myAgent.enabled = true;
                }      
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Cave 1" && gameObject.name == "choripan")
        {
            _myAgent.enabled = false;
            transform.position = exitCave1.position;
        }

        if (other.gameObject.name == "Cave 1" && gameObject.name == "bobby")
        {
            _myAgent.enabled = false;
            transform.position = exitCave2.position;
        }

        if(other.gameObject.name== "Cave 3" && gameObject.name=="choripan")
        {
            _myAgent.enabled = false;
            transform.position = caveEnd.position;
        }

        if (other.gameObject.name == "Cave 3" && gameObject.name == "bobby")
        {
            _myAgent.enabled = false;
            transform.position = caveEnd2.position;
        }
    }
}
