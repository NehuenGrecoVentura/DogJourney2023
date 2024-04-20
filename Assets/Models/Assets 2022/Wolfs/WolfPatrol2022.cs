using UnityEngine;
using UnityEngine.AI;

public class WolfPatrol2022 : MonoBehaviour
{
    NavMeshAgent _myAgent;
    AudioSource _myAudio;
    bool _onSight = false;
    public GameObject target;
    public Transform[] savedPoint;
    int _index;
    public float targetRadius;
    Vector3 _targetDistance;
    public GameObject player;
    public AudioClip soundWolf;
    [HideInInspector] public bool rabbitDetected = false;
    WolfPatrol2022 _patrol;
    WolfRabbit2022 _chaseRabbit;


    void Start()
    {
        _myAgent = GetComponent<NavMeshAgent>();
        _myAudio = GetComponent<AudioSource>();
        _patrol = GetComponent<WolfPatrol2022>();
        _chaseRabbit = GetComponent<WolfRabbit2022>();
        _chaseRabbit.enabled = false;
    }

    void Update()
    {
        if (!_onSight) Patrol();
        else Chase();
    }

    void Patrol()
    {
        target.transform.position = savedPoint[_index].position;
        _myAgent.destination = target.transform.position;
        _targetDistance = target.transform.position - transform.position;
        if (_targetDistance.magnitude <= targetRadius)
        {
            if (_index >= savedPoint.Length - 1) _index = 0;
            else _index++;
        }
    }

    public void Chase()
    {
        target.transform.position = player.transform.position;
        _myAgent.destination = target.transform.position;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _onSight = true;
            _myAudio.PlayOneShot(soundWolf);
        }

        if (other.gameObject.name == "Rabbit")
        {
            rabbitDetected = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _onSight = false;
        }

    }
}
