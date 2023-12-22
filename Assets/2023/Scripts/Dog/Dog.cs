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
    public float speedNormal = 10f;
    public float speedRun = 15f;
    [SerializeField] float _distToPlayer;
    [SerializeField] float _targetRadius;
    [SerializeField] GameObject _target;
    [SerializeField] Transform _posTeletransport;

    private void Awake()
    {
        _myAgent = GetComponent<NavMeshAgent>();

        _player = FindObjectOfType<Character>();
        _order = FindObjectOfType<OrderDog>();

        _model = new ModelDog(_myAgent, _targetDist, scared, _scaredPoints, speedNormal, speedRun, _distToPlayer, _targetRadius, _target, transform, _player, _order, _posTeletransport);
        _view = new ViewDog(_anim, _trolleyAudio);

        _model.EventIdle += _view.IdleAnim;
        _model.EventWalk += _view.WalkAnim;
    }

    private void Update()
    {
        //_model.TeletransportToPlayer();
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
}