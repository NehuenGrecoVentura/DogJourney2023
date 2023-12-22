using UnityEngine;
using UnityEngine.AI;

public class RabbitCave : MonoBehaviour
{
    [Header("INTERACTIVE CONFIG")]
    [SerializeField] GameObject _iconInteractive;
    [SerializeField] KeyCode _buttonInteractive = KeyCode.F;
    [SerializeField] GameObject _dialogue;
    [SerializeField] GameObject _arrow;

    [Header("POSITIONS CONFIG")]
    [SerializeField] Transform _outPos;
    [SerializeField] Transform _cavePos;
    [SerializeField] Transform _carrotGOPos;
    [SerializeField] Transform _handPos;
    [SerializeField] float _speed;
    float _dist;

    [Header("REFS CONFIG")]
    [SerializeField] RabbitCave _myScript;
    NavMeshAgent _agent;
    Cave2022 _cave;
    Collider _col;
    PickItem _pickRabbit;
    EscapeRabbit _escape;

    [Header("AUDIO CONFIG")]
    [SerializeField] AudioClip _soundEat;
    AudioSource _myAudio;

    [Header("OBJECTS TO DESTROY")]
    [SerializeField] GameObject[] _objectsToDestroy;


    private void Awake()
    {
        _pickRabbit = GetComponent<PickItem>();
        _agent = GetComponent<NavMeshAgent>();
        _col = GetComponent<Collider>();
        _cave = FindObjectOfType<Cave2022>();
        _escape = FindObjectOfType<EscapeRabbit>();
        _myAudio = GetComponent<AudioSource>();
    }

    void Start()
    {
        _pickRabbit.enabled = false;
        _agent.speed = _speed;
        _iconInteractive.SetActive(false);
        _escape.enabled = false;
        _arrow.SetActive(false);
    }

    private void Update()
    {
        EatCarrot();
    }

    public void Hide()
    {
        _dialogue.SetActive(false);
        _agent.SetDestination(_cavePos.position);
        _col.enabled = false;
    }

    public void Out()
    {
        _dialogue.SetActive(true);
        _agent.SetDestination(_outPos.position);
        _col.enabled = true;
    }

    public void EatCarrot()
    {
        if (_cave.carrotInArea)
        {
            _pickRabbit.enabled = true;
            _agent.SetDestination(_carrotGOPos.transform.position);
            _dist = Vector3.Distance(transform.position, _carrotGOPos.gameObject.transform.position);
            if (_dist < 1)
            {
                _speed = 0;
                _carrotGOPos.gameObject.GetComponent<MeshRenderer>().enabled = false;
                _col.enabled = true;
                _arrow.SetActive(true);
                if (!_myAudio.isPlaying)
                    _myAudio.PlayOneShot(_soundEat);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character2022>();

        if (player != null)
        {
            if (!Input.GetKeyDown(_buttonInteractive))
                _iconInteractive.SetActive(true);

            else
            {
                _iconInteractive.SetActive(false);
                foreach (var obj in _objectsToDestroy) Destroy(obj);
                Destroy(_myScript);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character2022>();

        if (player != null)
        {
            if (!Input.GetKeyDown(_buttonInteractive))
                _iconInteractive.SetActive(true);

            else
            {
                _iconInteractive.SetActive(false);
                foreach (var obj in _objectsToDestroy) Destroy(obj);
                Destroy(_myScript);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character2022>();
        if (player != null) _iconInteractive.SetActive(false);
    }
}