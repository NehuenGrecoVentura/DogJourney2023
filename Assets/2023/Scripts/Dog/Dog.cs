using UnityEngine;
using UnityEngine.AI;

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
    [SerializeField] GameObject _target;
    [SerializeField] Transform _posTeletransport;

    [Header("TELETRANSPORT")]
    [SerializeField] Camera _camPlayer;
    [SerializeField] float _offsetDistance = 2f;
    private Vector3 _teletransportPos = new Vector3(); 

    private void Awake()
    {
        _myAgent = GetComponent<NavMeshAgent>();

        _player = FindObjectOfType<Character>();
        _order = FindObjectOfType<OrderDog>();

        _model = new ModelDog(_myAgent, _targetDist, scared, _scaredPoints, speedNormal, speedRun, offSpeed, _distToPlayer, _targetRadius, _target, transform, _player, _order, _posTeletransport, _camPlayer);
        _view = new ViewDog(_anim, _trolleyAudio);

        _model.EventIdle += _view.IdleAnim;
        _model.EventWalk += _view.WalkAnim;
    }

    private void Update()
    {
        //_model.TeletransportToPlayer();
        //_model.OffScreenSpeed();


        if (!IsInView() && IsFarEnough() && _order.activeOrders)
        {

            transform.position = _posTeletransport.position;
            _target.transform.position = _posTeletransport.position;
        }
    }

    public void OrderGo()
    {
        StartCoroutine(_model.OrderGO());
    }

    public void Stop()
    {
        StopCoroutine(_model.OrderGO());
        _model.OrderStay();
    }

    bool IsInView() // Chequeo si está dentro de la cámara
    {
        Vector3 viewportPoint = _camPlayer.WorldToViewportPoint(transform.position);
        return viewportPoint.x > 0 && viewportPoint.x < 1 && viewportPoint.y > 0 && viewportPoint.y < 1;
    }

    bool IsFarEnough() // Chequeo si el perro está lejos del player
    {
        float distance = Vector3.Distance(transform.position, _player.gameObject.transform.position);
        return distance > _distToPlayer;
    }
}