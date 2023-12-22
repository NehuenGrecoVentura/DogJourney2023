using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : MonoBehaviour
{
    public float speed;
    private int _index;
    private int _indexExit;
    public Transform[] waypoints;
    public Transform[] waypointsExit;
    public GameObject dog;
    public GameObject egg;
    public Transform lastPoint;
    private bool _isFly = true;
    public Nidus nidus;
    private bool _eggFly = false;
    public Transform player;
    [SerializeField] BoxCollider _col;
    [SerializeField] AudioSource _myAudio;
    [SerializeField] Animator _myAnim;
    public Transform pos;
    public Transform flyPos;
    public GameObject imageHint;

    private void Start()
    {
        _col.enabled = false;
    }


    private void FixedUpdate()
    {
        if (_isFly && !nidus.eggInNidus) Fly();
        if(!_isFly && !nidus.eggInNidus) Sleep();
        if (nidus.eggInNidus) Exit();
        if(_eggFly) egg.transform.position = transform.position;
    }

    void Fly()
    {
        if (transform.position != waypoints[_index].position) transform.position = Vector3.MoveTowards(transform.position, waypoints[_index].position, speed * Time.deltaTime);
        else _index = (_index + 1) % waypoints.Length;
        transform.LookAt(waypoints[_index].position);
        dog.gameObject.transform.position = pos.position;
    }

    void Sleep()
    {
        dog.transform.position = lastPoint.position;
        transform.position = flyPos.position;
        _col.enabled = true;
    }

    void Exit()
    {
        Destroy(imageHint);
        Destroy(_col);
        _myAnim.enabled = true;
        _myAudio.Play();
        if (transform.position != waypointsExit[_indexExit].position) transform.position = Vector3.MoveTowards(transform.position, waypointsExit[_indexExit].position, speed * Time.deltaTime);
        else _indexExit = (_indexExit + 1) % waypointsExit.Length;
        transform.LookAt(waypointsExit[_indexExit].position);
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Nido") _isFly = false;
        if (other.gameObject.name == "Nest") _eggFly = true;
        if (other.gameObject.name == "PosExit")
        {
            Destroy(gameObject);
            Destroy(egg);
        }
    }

}
