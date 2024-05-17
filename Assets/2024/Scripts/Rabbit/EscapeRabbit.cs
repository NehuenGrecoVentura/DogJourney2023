using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EscapeRabbit : MonoBehaviour
{
    [SerializeField] float _speedEscape;
    [SerializeField] Transform _dirToEscape;
    NavMeshAgent _agent;
    private Animator _myAnim;
    
    void OnEnable()
    {
        _agent = GetComponent<NavMeshAgent>();
        _myAnim = GetComponent<Animator>();

        _myAnim.SetBool("Move", true);
        _agent.speed = _speedEscape;
        _agent.SetDestination(_dirToEscape.position);
    }

    IEnumerator DestroyRabbit()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}