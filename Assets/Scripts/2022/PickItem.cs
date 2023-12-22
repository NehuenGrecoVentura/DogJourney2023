using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PickItem : MonoBehaviour
{
    public Transform posHand;
    public bool isPick = false;
    Rigidbody _rb;
    public EscapeRabbit scriptEscape;
    //public PickItem myScript;
    public NavMeshAgent agent;

    [Header("INTERACTIVE CONFIG")]
    [SerializeField] GameObject _iconDrop;
    [SerializeField] GameObject _iconPick;
    [SerializeField] GameObject _arrow;

    [Header("UI STATE CONFIG")]
    [SerializeField] UIQuestStatus _status;
    [SerializeField] string _nextDescription;

    [Header("REFERENCE PLAYER")]
    [SerializeField] Character2022 _player;
    [SerializeField] GameObject _axe;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        scriptEscape.enabled = false;
        _iconDrop.SetActive(false);
        _iconPick.SetActive(false);
    }

    private void Update()
    {
        if (isPick) Pick();
        if (Input.GetKeyDown(KeyCode.G) && isPick)
        {
            Drop();
            _player.EjecuteAnim("Idle");
        }
    }

    public void Pick()
    {
        agent.isStopped = true;
        agent.speed = 0;
        _arrow.SetActive(false);
        transform.position = posHand.position;
        transform.parent = posHand.parent;
        _rb.useGravity = false;
        _iconDrop.SetActive(true);
        _iconPick.SetActive(false);
        _status.nextDescription = _nextDescription;
        _axe.SetActive(false);


        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            _player.PickWalkAnim();

        else _player.PickIdleAnim();
    }

    public void Drop()
    {
        _axe.SetActive(true);
        _arrow.SetActive(true);
        transform.parent = null;
        _rb.useGravity = true;
        isPick = false;
        _iconDrop.SetActive(false);
        Destroy(_status);
    }

    public void Escape()
    {
        transform.parent = null;
        _rb.useGravity = true;
        isPick = false;
        agent.isStopped = false;
        scriptEscape.enabled = true;
        Destroy(this);
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character2022>();
        if (player != null)
        {
            if (Input.GetKeyDown(KeyCode.F)) isPick = true;
            else _iconPick.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character2022>();
        if (player != null)
        {
            _iconDrop.SetActive(false);
            _iconPick.SetActive(false);
        }
    }
}