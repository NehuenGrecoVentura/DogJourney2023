using UnityEngine;
using System.Collections;

public class AreaWolfSleeping : MonoBehaviour
{
    private WolfSleeping[] _wolfsSleeping;
    private Manager _manager;

    [SerializeField] Transform _posRestart;
    [SerializeField] GameObject _cinematic;

    [SerializeField] AudioClip _soundWolf;
    private AudioSource _myAudio;

    public bool playerInArea = false;
    private bool _playerDetected = false;

    [Header("GAME OVER")]
    private CameraOrbit _camPlayer;
    private Character _player;
    [SerializeField] GameObject _restart;
    private Collider _myCol;
    [SerializeField] WolfSleeping _wolf;

    private void Awake()
    {
        _myAudio = GetComponent<AudioSource>();

        _wolfsSleeping = FindObjectsOfType<WolfSleeping>();
        _manager = FindObjectOfType<Manager>();

        _camPlayer = FindObjectOfType<CameraOrbit>();

        _player = FindObjectOfType<Character>();

        _myCol = GetComponent<Collider>();
    }

    void Start()
    {
        _cinematic.SetActive(false);
    }

    //public void StandWolfes()
    //{
    //    foreach (var wolf in _wolfsSleeping)
    //        wolf.wakeUp = true;

    //    if(!_myAudio.isPlaying) _myAudio.PlayOneShot(_soundWolf);
    //    _manager.GameOver(_cinematic, 5f, "CAREFUL! YOU HAVE WOKE UP ALL THE WOLVES", _posRestart);
    //}

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) playerInArea = true;
        {




            //if (!Input.GetKey(KeyCode.LeftControl) && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
            //{
            //    StandWolfes();
            //}


            //else if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E))
            //{
            //    StandWolfes();
            //}

        }
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            //if (!Input.GetKey(KeyCode.LeftControl) && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
            //    StandWolfes();

            //else if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E))
            //    StandWolfes();

            //if (!Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E))
            //{
            //    StartCoroutine(PlayerDetected());


            //}

            if (!Input.GetKey(KeyCode.LeftControl) && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
                StartCoroutine(PlayerDetected());

            else if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E))
                StartCoroutine(PlayerDetected());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            _myCol.enabled = true;
            StopAllCoroutines();
        }
    }

    private IEnumerator PlayerDetected()
    {
        _myCol.enabled = false;
        _wolf.PlayerDetected("CAREFUL! YOU HAVE WOKE UP ALL THE WOLVES"); // Uso de referencia a un solo lobo ya que sino en el stay se repite el game over.
        yield return new WaitForSeconds(8f);

        foreach (var item in _wolfsSleeping)
        {
            item.DownSleepWolf();
        }



        _myCol.enabled = true;
    }
}