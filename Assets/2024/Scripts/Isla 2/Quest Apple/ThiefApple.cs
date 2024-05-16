using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ThiefApple : MonoBehaviour
{
    [SerializeField] Transform _posEscape;
    [SerializeField] float _timeThief = 3f;

    private BoxApple _boxApple;
    private Animator _myAnim;
    private NavMeshAgent _navAgent;
    private BoxCollider _myCol;

    private bool _isThief = false;
    private bool _isScared = false;
    private bool _isEscape = false;

    private void Awake()
    {
        _myCol = GetComponent<BoxCollider>();
        _navAgent = GetComponent<NavMeshAgent>();
        _myAnim = GetComponent<Animator>();
        _boxApple = FindObjectOfType<BoxApple>();
    }

    private void Update()
    {
        MoveToBox();
    }

    private void MoveToBox()
    {
        if (_boxApple != null && !_isScared && !_isThief && !_isEscape)
        {
            _navAgent.SetDestination(_boxApple.gameObject.transform.position);
            _myAnim.SetBool("Move", true);
        }
            
    }

    private void OnTriggerEnter(Collider other)
    {
        var boxApple = other.GetComponent<BoxApple>();
        if (boxApple != null && !_isThief)
        {
            StartCoroutine(Thief());
        }
    }

    private IEnumerator Thief()
    {
        _myCol.enabled = false;
        _myAnim.SetBool("Move", false);
        _navAgent.isStopped = true;
        _isThief = true;
        yield return new WaitForSeconds(_timeThief);
        StartCoroutine(Escape());
    }

    private IEnumerator Escape()
    {
        _navAgent.isStopped = false;
        _isThief = false;
        _isScared = false;
        _isEscape = true;

        _navAgent.SetDestination(_posEscape.position);
        _myAnim.SetBool("Move", true);

        yield return new WaitForSeconds(10f);
        gameObject.SetActive(false);

    }
}