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
    [SerializeField] Manager _manager;
    [SerializeField] NavMeshAgent _nv;
    [SerializeField] Animator _myAnim;
    [SerializeField] Character _player;

    [Header("AUDIO")]
    [SerializeField] AudioClip _soundWolf;
    [SerializeField] AudioSource _myAudio;

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
            _player.FreezePlayer();

            foreach (var col in _myCols)
                Destroy(col);

            Destroy(rabbit.gameObject);
            _rabbitFollow.SetActive(true);
            StartCoroutine(PursuitRabbit());
            StartCoroutine(DefreezePlayer());
        }

        if (dog != null)
        {
            _myAudio.PlayOneShot(_soundWolf);
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

            Destroy(_rabbitFollow.gameObject, 5f);
            Destroy(_escapeCinematic, 5f);
            Destroy(gameObject, 5f);
        }
    }

    private IEnumerator DefreezePlayer()
    {
        yield return new WaitForSeconds(4f);
        _player.DeFreezePlayer();
    }
}