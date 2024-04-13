using UnityEngine;
using System.Collections;

public class WolfSleeping : MonoBehaviour
{
    private WolfSleeping[] _allWolfs;
    public Animator _myAnim;
    public bool wakeUp = false;
    private Character _player;
    private Manager _manager;

    [SerializeField] ParticleSystem _zzz;
    [SerializeField] GameObject _cinematicGameOver;
    [SerializeField] Transform _posRestart;
    
    [Header("AREA")]
    private AreaWolfSleeping _area;
    private Collider _colArea;

    [Header("AUDIO")]
    [SerializeField] AudioClip _wolfSound;
    private AudioSource _myAudio;


    

    private void Awake()
    {
        _myAnim = GetComponent<Animator>();
        _myAudio = GetComponent<AudioSource>();

        _player = FindObjectOfType<Character>();
        _manager = FindObjectOfType<Manager>();
        _area = FindObjectOfType<AreaWolfSleeping>();
        _allWolfs = FindObjectsOfType<WolfSleeping>();

        _colArea = _area.GetComponent<Collider>();
    }

    void Start()
    {
        _cinematicGameOver.SetActive(false);
    }

    public void WakeUpWolf()
    { 
        _myAnim.SetTrigger("Stand");
        transform.LookAt(_player.transform.position);
        _zzz.gameObject.SetActive(false);
    }

    public void DownSleepWolf()
    {
        _colArea.enabled = true;
        _zzz.gameObject.SetActive(true);
        _myAnim.SetTrigger("Sleep");
    }

    public void PlayerDetected(string messageGameOver)
    {
        _myAudio.PlayOneShot(_wolfSound);

        foreach (var wolf in _allWolfs)
            wolf.WakeUpWolf();

        _manager.GameOver(_cinematicGameOver, 5f, messageGameOver, _posRestart);
    }


    private void OnCollisionEnter(Collision collision)
    {
        var player = collision.gameObject.GetComponent<Character>();
        if (player != null && _area.playerInArea)
        {
            _myAudio.PlayOneShot(_wolfSound);

            foreach (var wolf in _allWolfs)
                wolf.WakeUpWolf();

            _manager.GameOver(_cinematicGameOver, 5f, "Don't get too close to the wolves", _posRestart);
        }
    }
}