using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class WolfStatic : MonoBehaviour
{
    [SerializeField] GameObject _gameOver;
    [SerializeField] GameObject _rabbitFollow;
    [SerializeField] GameObject _escapeCinematic;
    [SerializeField] Collider[] _myCols;
    public Transform restartPlayer;
    private Manager _manager;
    private NavMeshAgent _nv;
    private Animator _myAnim;
    private Character _player;

    [Header("AUDIO")]
    [SerializeField] AudioClip _soundWolf;
    private AudioSource _myAudio;

    private void Awake()
    {
        _nv = GetComponent<NavMeshAgent>();
        _myAnim = GetComponent<Animator>();
        _myAudio = GetComponent<AudioSource>();

        _manager = FindObjectOfType<Manager>();
        _player = FindObjectOfType<Character>();
    }

    private void Start()
    {
        _gameOver.SetActive(false);
        _rabbitFollow.SetActive(false);
        _escapeCinematic.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        var rabbit = other.GetComponent<Rabbit>();
        var dog = other.GetComponent<Dog>();

        if (player != null && !player.rabbitPicked)
        {
            _myAudio.PlayOneShot(_soundWolf);
            transform.LookAt(player.gameObject.transform.position);
            _manager.GameOver(_gameOver, 1f, "Careful! The wolf has detected you", restartPlayer);
        }

        if (rabbit != null && rabbit.rabbitPicked)
        {
            foreach (var col in _myCols)
                Destroy(col);

            Destroy(rabbit.gameObject);
            _rabbitFollow.SetActive(true);
            StartCoroutine(PursuitRabbit());
        }

        if(dog != null)
        {
            dog.scared = true;
            dog.Scared();
        }
    }

    private IEnumerator PursuitRabbit()
    {
        while (true)
        {
            _player.rabbitPicked = false;
            _escapeCinematic.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            _nv.SetDestination(_rabbitFollow.gameObject.transform.position);
            _myAnim.SetBool("Patrol", true);
            _myAnim.SetBool("Idle", false);
            Destroy(transform.parent.gameObject, 5f);
        }
    }
}