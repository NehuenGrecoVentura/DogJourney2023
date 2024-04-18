using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using DG.Tweening;

public class Dog : MonoBehaviour
{
    [Header("MODEL/VIEW")]
    private ModelDog _model;
    private ViewDog _view;

    private NavMeshAgent _myAgent;
    private Vector3 _targetDist;
    private Character _player;
    private OrderDog _order;

    [SerializeField] Animator _anim;
    [HideInInspector] public bool scared;
    [SerializeField] AudioSource _trolleyAudio;
    [SerializeField] Transform[] _scaredPoints;
    public float speedNormal;
    public float speedRun;
    public float offSpeed;
    [SerializeField] float _distToPlayer;
    [SerializeField] float _targetRadius;
    public GameObject _target;
    [SerializeField] Transform _posTeletransport;

    [Header("TELETRANSPORT")]
    [SerializeField] Camera _camPlayer;
    [SerializeField] float _offsetDistance = 2f;
    private Vector3 _teletransportPos = new Vector3();
    public bool quickEnd = false;

    [Header("AUDIO")]
    [SerializeField] AudioClip _soundScared;
    private AudioSource _myAudio;

    [Header("Carrito")] 
    [SerializeField] private Transform DesiredPoint;
    [SerializeField] private Transform DestinationPoint;
    [SerializeField] private float TroleyTime;

    private void Awake()
    {
        _myAgent = GetComponent<NavMeshAgent>();
        _myAudio = GetComponent<AudioSource>();

        _player = FindObjectOfType<Character>();
        _order = FindObjectOfType<OrderDog>();

        _model = new ModelDog(_myAgent, _targetDist, scared, _scaredPoints, speedNormal, speedRun, offSpeed, _distToPlayer, _targetRadius, _target, transform, _player, _order, _posTeletransport, _camPlayer);
        _view = new ViewDog(_anim, _trolleyAudio);

        _model.EventIdle += _view.IdleAnim;
        _model.EventWalk += _view.WalkAnim;
    }

    private void Update()
    {
        DestinationPoint.DOMove(DesiredPoint.position,TroleyTime);
        //_model.TeletransportToPlayer();
        //_model.OffScreenSpeed();


        if (!IsInView() && IsFarEnough() && _order.activeOrders && !quickEnd && _player.gameObject.transform.position.y < 18f && !scared)
        {

            transform.position = _posTeletransport.position;
            _target.transform.position = _posTeletransport.position;
            Stop();
        }

        if (scared && transform.position == _target.gameObject.transform.position) scared = false;

        //if (_player.gameObject.transform.position.y > 18f) Stop();


        float distance = Vector3.Distance(transform.position, _target.transform.position);
        if (distance <= 2.5f) Stop();

    }

    public void Scared()
    {
        //StopCoroutine(_model.OrderGO());
        _myAudio.PlayOneShot(_soundScared);
        if(scared) StartCoroutine(_model.OrderGOQick(_scaredPoints[0]));
    }

    public void Search()
    {
        _anim.Play("Search");
    }

    public void OrderGo()
    {
        if (!quickEnd && _player.transform.position.y < 18f && !scared)
        {
            StartCoroutine(_model.OrderGO());
        }
        
    }

    public void OrderGoQuick(Transform quickPos)
    {
        if (quickEnd) StartCoroutine(_model.OrderGOQick(quickPos));
    }



    public void Stop()
    {
        StopCoroutine(_model.OrderGO());
        _model.OrderStay();
    }

    bool IsInView() // Chequeo si est� dentro de la c�mara
    {
        Vector3 viewportPoint = _camPlayer.WorldToViewportPoint(transform.position);
        return viewportPoint.x > 0 && viewportPoint.x < 1 && viewportPoint.y > 0 && viewportPoint.y < 1;
    }

    bool IsFarEnough() // Chequeo si el perro est� lejos del player
    {
        float distance = Vector3.Distance(transform.position, _player.gameObject.transform.position);
        return distance > _distToPlayer;
    }

    private void OnCollisionEnter(Collision collision)
    {
        var player = collision.gameObject.GetComponent<Character>();
        if (player != null) Stop();
    }
}