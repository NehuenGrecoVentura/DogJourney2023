using UnityEngine;

public class Character : MonoBehaviour
{
    private ModelCharacter _model;
    private ViewCharacter _view;
    private ControllerCharacter _controller;

    [SerializeField] Rigidbody _myRb;
    [SerializeField] Animator _myAnim;
    [SerializeField] AudioSource _myAudio;
    [SerializeField] OrderDog _orderDog;
    private BuilderManager _construct;

    public float speed = 10f;
    public float speedRun = 15f;
    [SerializeField] float _speedCrouch = 7f;
    [HideInInspector] public float speedAux;
    [SerializeField] float _gravity = 9.7f;
    [SerializeField] Transform _camPos;

    public bool isClimb = false;
    public bool rabbitPicked = false;
    public bool isConstruct = false;
    public bool isFishing = false;

    [SerializeField] AudioClip[] _soundsCall;

    [Header("FIX RAY")]
    [SerializeField] float _rayDist;
    [SerializeField] Transform _rayPoint1;
    [SerializeField] Transform _rayPoint2;
    private bool _test1, _test2;

    [SerializeField] RuntimeAnimatorController[] _animController;

    [Header("ITEMS PICKED")]
    [SerializeField] GameObject _rod;
    [SerializeField] GameObject _axe;
    [SerializeField] GameObject _shovel;
    [SerializeField] GameObject _apples;
    [SerializeField] GameObject _basket;

    private void Awake()
    {
        //_myRb = GetComponent<Rigidbody>();
        //_myAnim = GetComponent<Animator>();
        //_myAudio = GetComponent<AudioSource>();
        //_orderDog = GetComponent<OrderDog>();
        _construct = FindObjectOfType<BuilderManager>();

        _model = new ModelCharacter(_myRb, speed, speedRun, _speedCrouch, speedAux, isClimb, transform, _camPos, _gravity, _orderDog, _test1, _test2, _rayDist, _rayPoint1, _rayPoint2);
        _view = new ViewCharacter(_myAnim, _myAudio, _soundsCall, _axe, _shovel, _apples, _basket);
        _controller = new ControllerCharacter(_model);

        _model.EventIdle += _view.IdleAnim;
        _model.EventWalk += _view.WalkAnim;
        _model.EventRun += _view.RunAnim;
        _model.EventIdleCrouch += _view.IdleCrouch;
        _model.EventWalkCrouch += _view.CrouchWalk;
        _model.EventIdleCallDog += _view.CallIdleDogAnim;
        _model.EventCallMoveDog += _view.CallMoveDogAnim;
        _model.EventHitTree += _view.HitAnim;
        _model.EventHitDig += _view.HitDig;
        _model.EventPickIdle += _view.PickIdleAnim;
        _model.EventPickWalk += _view.PickWalkAnim;
    }

    private void Start()
    {
        speedAux = speed;
    }

    private void FixedUpdate()
    {
        //if (speed != 0)
        //{
        //    _controller.Move(rabbitPicked);
        //    _myAnim.enabled = true;
        //}

        //else
        //{

        //    if (!isConstruct) _myAnim.enabled = false;
        //    else _myAnim.enabled = true;
        //}

        if (speed != 0)
        {
            _controller.Move(rabbitPicked);
            _myAnim.enabled = true;
        }

        else
        {
            if (!isConstruct) _myAnim.runtimeAnimatorController = _animController[2];
            else _myAnim.enabled = true;
        }
    }

    private void Update()
    {
        _controller.OnUpdate();
    }

    public void HitTree()
    {
        _model.HitTree();
    }

    public void HitDig()
    {
        _model.HitDig();
        isConstruct = true;
        FreezePlayer();
    }

    public void PlayAnim(string nameAnim)
    {
        _myAnim.Play(nameAnim);
    }

    //public void FreezePlayer(RigidbodyConstraints rb)
    //{
    //    _model.FreezePlayer(rb);
    //}

    public void FreezePlayer()
    {
        _myRb.isKinematic = true;
        speed = 0;
        if (!_construct) _myAnim.runtimeAnimatorController = _animController[2];
    }

    public void DeFreezePlayer()
    {
        _myRb.isKinematic = false;
        speed = speedAux;
        _myAnim.runtimeAnimatorController = _animController[0];
    }

    public void MoveInStairs(float speedClimb)
    {
        PlayAnim("Up Stairs New");
        speed = 0;
        _myRb.useGravity = false;
        _myRb.MovePosition(transform.position + Vector3.up * speedClimb * Time.fixedDeltaTime);
        transform.rotation = Quaternion.Euler(0, 90, 0);
    }

    public void SetFishingMode(bool rodActive)
    {
        isConstruct = rodActive;
        _rod.SetActive(rodActive);
    }

    public void ItemsPicked(bool axe, bool shovel, bool apples, bool basket)
    {
        _view.PickStatus(axe, shovel, apples, basket);
    }

    private void OnTriggerStay(Collider other)
    {
        var pick = other.GetComponent<IPick>();
        if (pick != null) pick.Pick();
    }
}