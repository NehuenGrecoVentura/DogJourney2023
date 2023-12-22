using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DogsMove : MonoBehaviour
{
    public Transform pos;
    public Transform posFinal;
    NavMeshAgent _agent;
    public Transform player;
  //  public GameObject iconUp;
  //  public GameObject iconUpFinal;
    public GameObject iconBase;
    public GeiserController geiser;
    public CollisionsLvl2 playerCol;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
   //     iconUp.gameObject.SetActive(false);
   //     iconUpFinal.gameObject.SetActive(false);
    }

    void Update()
    {
        if (player.position.z > 6)
        {
            _agent.enabled = true;
        }
    }

   /* public void TeletransporDogs()
    {
        _agent.enabled = false;
        transform.position = pos.position;
        Destroy(iconBase);
    //    iconUp.gameObject.SetActive(true);
   //     iconUpFinal.gameObject.SetActive(false);
        if (player.position.z > 6 && player.position.y >=15)
        {
            _agent.enabled = true;
        }
    }*/

    public void MoveFinal()
    {
        _agent.enabled = false;
        transform.position = posFinal.position;
    //    Destroy(iconUp);
    //    iconUpFinal.gameObject.SetActive(true);
        if (player.position.z > 6)
        {
            _agent.enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // if(other.gameObject.name== "Cave 1" && geiser.allActive) TeletransporDogs();
        // if (other.gameObject.name == "Cave 2" && playerCol.upFinal) MoveFinal();
        if (other.gameObject.name == "Cave 1" && geiser.allActive) MoveFinal();
    }

    private void OnTriggerStay(Collider other)
    {
        // if (other.gameObject.name == "Cave 1" && geiser.allActive) TeletransporDogs();
        // if (other.gameObject.name == "Cave 2" && playerCol.upFinal) MoveFinal();
        if (other.gameObject.name == "Cave 1" && geiser.allActive) MoveFinal();
    }
}
