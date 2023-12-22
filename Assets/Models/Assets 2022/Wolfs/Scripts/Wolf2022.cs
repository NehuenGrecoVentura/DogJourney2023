using UnityEngine;
using TMPro;
using UnityEngine.AI;

public class Wolf2022 : MonoBehaviour
{
    [SerializeField] float _speedPatrol;
    [SerializeField] Transform[] _waypoints;
    [SerializeField] GameObject _iconAlert;
    [SerializeField] TMP_Text _textGameOver;
    [SerializeField] string _newMessageGameOver;
    NavMeshAgent _agent;
    Dog2022 _dog;

    AudioSource _myAudio;
    public AudioClip soundWolf;
    PickItem _rabbit;
    GManager _gm;

    private void Awake()
    {
        _myAudio = GetComponent<AudioSource>();
        _agent = GetComponent<NavMeshAgent>();
        _rabbit = FindObjectOfType<PickItem>();
        _gm = FindObjectOfType<GManager>();
        _dog = FindObjectOfType<Dog2022>();
    }

    void Start()
    {
        _agent.speed = _speedPatrol;
        _iconAlert.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Si el perro se acerca al lobo, se asusta.
        var dog = other.GetComponent<Dog2022>();
        if (dog != null)
        {
            _myAudio.PlayOneShot(soundWolf);
            _dog.scared = true;
        }

        // Si me acerco al lobo y no tengo el conejo agarrado, entonces pierdo.
        var player = other.GetComponent<Character2022>();
        if (player != null && !_rabbit.isPick)
        {
            transform.LookAt(player.gameObject.transform.position);
            _textGameOver.text = _newMessageGameOver;
            _gm.GameOver();
        }
            
            
    }

    private void OnTriggerStay(Collider other)
    {
        // Si el perro se mantiene cerca del lobo, se asusta.
        var dog = other.GetComponent<Dog2022>();
        if (dog != null) _dog.scared = true;
    }

}