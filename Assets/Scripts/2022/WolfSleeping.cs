using UnityEngine;
using System.Collections;

public class WolfSleeping : MonoBehaviour
{
    public Animator _myAnim;
    public bool wakeUp = false;
    private Character _player;
    private Manager _manager;
    [SerializeField] ParticleSystem _zzz;

    [SerializeField] GameObject _cinematicGameOver;
    [SerializeField] Transform _posRestart;
    private AreaWolfSleeping _area;

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
    }

    void Start()
    {
        _cinematicGameOver.SetActive(false);
    }

    private void Update()
    {
        if (wakeUp)
        {
            _myAnim.SetTrigger("Stand");
            transform.LookAt(_player.transform.position);
            _zzz.gameObject.SetActive(false);
        }
        else
        {
            _zzz.gameObject.SetActive(true);
            _myAnim.SetTrigger("Sleep");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        var player = collision.gameObject.GetComponent<Character>();
        if (player != null)
        {
            _myAudio.PlayOneShot(_wolfSound);
            wakeUp = true;
            _area.StandWolfes();
        }
    }
}