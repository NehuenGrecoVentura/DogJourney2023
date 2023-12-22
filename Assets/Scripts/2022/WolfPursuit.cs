using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class WolfPursuit : MonoBehaviour, IDestructible
{
    [SerializeField] float _speedPursuit;
    [SerializeField] Transform _target;
    NavMeshAgent _agent;
    Wolf2022 _wolf;

    [SerializeField] GameObject _iconAlert;
    [SerializeField] GameObject _axePlayer;

    Animator _myAnim;

    [Header("AUDIO CONFIG")]
    AudioSource _myAudio;
    public AudioClip soundWolf;

    [Header("UI STATUS QUEST")]
    [SerializeField] UIQuestStatus _status;
    [SerializeField] GameObject _descriptionQuest;
    [SerializeField] string _nextDescription;
    [SerializeField] GameObject _dialogue;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _wolf = GetComponent<Wolf2022>();
        _myAudio = GetComponent<AudioSource>();
        _myAnim = GetComponent<Animator>();
    }

    void Start()
    {
        _agent.speed = _speedPursuit;
    }

    IEnumerator PursuitRabbit()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            _descriptionQuest.SetActive(true);
            _status.completed = true;
            _status.nextDescription = _nextDescription;
            _iconAlert.SetActive(true);
            _axePlayer.SetActive(true);
            _agent.SetDestination(_target.position);
            _myAnim.SetBool("Idle", false);
            _myAnim.SetBool("Patrol", true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var rabbit = other.GetComponent<PickItem>();
        if (rabbit != null) 
        {
            Destroy(_dialogue);
            _myAudio.PlayOneShot(soundWolf);
            rabbit.Escape();
            Destroy(_wolf);
            StartCoroutine(PursuitRabbit());  
        }
    }
}