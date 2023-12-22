using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class RabbitEntity : MonoBehaviour
{
    [Header("POS")]
    [SerializeField] Transform _hidePos;
    [SerializeField] Transform _carrotPos;
    [SerializeField] Transform _handPos;
    public Transform _escapePos;
    public float _timeToDestroy;

    [SerializeField] float _speed = 10f;

    private Rigidbody _rb;
    private Vector3 _initialPos;
    private NavMeshAgent _agent;
    private Collider _col;
    private Character2022 _player;
    public bool _isPicked = false;
    [SerializeField] GameObject _axe;

    [SerializeField] ZoneRabbit _area;

    [SerializeField] KeyCode _buttonInteractive = KeyCode.F;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _col = GetComponent<Collider>();
        _rb = GetComponent<Rigidbody>();
        _player = FindObjectOfType<Character2022>();
    }

    private void Start()
    {
        _initialPos = transform.position;
        _col.enabled = false;
        _agent.speed = _speed;
    }

    private void Update()
    {
        if (_isPicked) PickRabbit();
        if (Input.GetKeyDown(KeyCode.G) && _isPicked) DropRabbit();
    }

    public void Out()
    {
        _agent.SetDestination(_initialPos);
    }

    public void Hide()
    {
        _agent.SetDestination(_hidePos.position);
    }

    private void PickRabbit()
    {
        StopAllCoroutines();
        _axe.SetActive(false);
        transform.position = _handPos.position;
        transform.parent = _handPos.parent;
        _agent.isStopped = true;
        _rb.useGravity = false;
        _isPicked = true;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            _player.PickWalkAnim();

        else _player.PickIdleAnim();
    }

    public void DropRabbit()
    {
        _player.EjecuteAnim("Idle");
        _axe.SetActive(true);
        transform.parent = null;
        _agent.isStopped = false;
        _rb.useGravity = true;
        _isPicked = false;
    }

    public void Escape()
    {
        DropRabbit();
        _agent.SetDestination(_escapePos.position);
        Destroy(_col);
        Destroy(gameObject, _timeToDestroy);
    }

    public IEnumerator GoToCarrot()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            _agent.SetDestination(_carrotPos.position);
            _col.enabled = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character2022>();
        if (player != null)
        {
            if (Input.GetKeyDown(_buttonInteractive))
                _isPicked = true;
        }
    }
}