using UnityEngine;

public class AreaWolfSleeping : MonoBehaviour
{
    private WolfSleeping[] _wolfsSleeping;
    private Manager _manager;

    [SerializeField] Transform _posRestart;
    [SerializeField] GameObject _cinematic;

    [SerializeField] AudioClip _soundWolf;
    private AudioSource _myAudio;

    private void Awake()
    {
        _myAudio = GetComponent<AudioSource>();

        _wolfsSleeping = FindObjectsOfType<WolfSleeping>();
        _manager = FindObjectOfType<Manager>();
    }

    void Start()
    {
        _cinematic.SetActive(false);
    }

    public void StandWolfes()
    {
        foreach (var wolf in _wolfsSleeping)
            wolf.wakeUp = true;

        if(!_myAudio.isPlaying) _myAudio.PlayOneShot(_soundWolf);
        _manager.GameOver(_cinematic, 5f, "CAREFUL! YOU HAVE WOKE UP ALL THE WOLVES", _posRestart);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            if (!Input.GetKey(KeyCode.LeftControl) && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
            {
                StandWolfes();
            }
                

            else if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E))
            {
                StandWolfes();
            }
                
        }
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            if (!Input.GetKey(KeyCode.LeftControl) && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
                StandWolfes();

            else if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E))
                StandWolfes();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            foreach (var wolf in _wolfsSleeping) 
                wolf.wakeUp = false;
        }
    }
}