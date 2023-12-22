using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class Wolf : MonoBehaviour
{
    [SerializeField] float _speed = 10f;
    [SerializeField] RabbitEntity _rabbit;
    private Animator _myAnim;
    private Transform _escapePos;
    private NavMeshAgent _agent;
    private Collider[] _cols;
    private float _timeToDestroy;
    private bool _rabbitDetected = false;
    public Transform checkpoint;

    [Header("GAME OVER")]
    [SerializeField] TMP_Text _textGameOver;
    [SerializeField] string _newMessageGameOver;
    private GManager _gm;

    private void Awake()
    {
        _cols = GetComponents<Collider>();
        _agent = GetComponent<NavMeshAgent>();
        _myAnim = GetComponent<Animator>();
        _gm = FindObjectOfType<GManager>();
    }

    private void Start()
    {
        _agent.speed = _speed;
    }

    private void Update()
    {
        if (_rabbitDetected)
        {
            _myAnim.SetBool("Idle", false);
            _myAnim.SetBool("Patrol", true);
            _agent.SetDestination(_escapePos.position);
            Destroy(gameObject, _timeToDestroy);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character2022>();
        var rabbit = other.GetComponent<RabbitEntity>();
        
        if (rabbit != null && _rabbit._isPicked)
        {
            _rabbitDetected = true;
            _escapePos = rabbit._escapePos;
            _timeToDestroy = rabbit._timeToDestroy;
            if (_rabbitDetected) rabbit.Escape();
            transform.LookAt(rabbit.transform.position);
            foreach (var col in _cols)
                Destroy(col);
        }

        if (player != null && !_rabbit._isPicked)
        {
            _textGameOver.text = _newMessageGameOver;
            _gm._posCheckpoint = checkpoint;
            _gm.GameOver();
        }
    }
}