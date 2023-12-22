using UnityEngine;

public class Character : MonoBehaviour
{
    private ModelCharacter _model;
    private ViewCharacter _view;
    private ControllerCharacter _controller;

    private Rigidbody _myRb;
    private Animator _myAnim;
    private AudioSource _myAudio;
    private OrderDog _orderDog;

    public float speed = 10f;
    public float speedRun = 15f;
    [SerializeField] float _speedCrouch = 7f;
    [HideInInspector] public float speedAux;
    [SerializeField] float _gravity = 9.7f;
    [SerializeField] Transform _camPos;
    public bool isClimb = false;
    public bool rabbitPicked = false;

    [SerializeField] AudioClip[] _soundsCall;

    private void Awake()
    {
        _myRb = GetComponent<Rigidbody>();
        _myAnim = GetComponent<Animator>();
        _myAudio = GetComponent<AudioSource>();
        _orderDog = GetComponent<OrderDog>();

        _model = new ModelCharacter(_myRb, speed, speedRun, _speedCrouch, speedAux, isClimb, transform, _camPos, _gravity, _orderDog);
        _view = new ViewCharacter(_myAnim, _myAudio, _soundsCall);
        _controller = new ControllerCharacter(_model);

        _model.EventIdle += _view.IdleAnim;
        _model.EventWalk += _view.WalkAnim;
        _model.EventRun += _view.RunAnim;
        _model.EventIdleCrouch += _view.IdleCrouch;
        _model.EventWalkCrouch += _view.CrouchWalk;
        _model.EventIdleCallDog += _view.CallIdleDogAnim;
        _model.EventCallMoveDog += _view.CallMoveDogAnim;
        _model.EventHitTree += _view.HitAnim;
        _model.EventPickIdle += _view.PickIdleAnim;
        _model.EventPickWalk += _view.PickWalkAnim;
    }

    private void FixedUpdate()
    {
        _controller.Move(rabbitPicked);
    }

    private void Update()
    {
        _controller.OnUpdate();
    }

    public void HitTree()
    {
        _model.HitTree();
    }

    public void PlayAnim(string nameAnim)
    {
        _myAnim.Play(nameAnim);
    }

    public void FreezePlayer(RigidbodyConstraints rb)
    {
        _model.FreezePlayer(rb);
    }

    public void MoveInStairs(float speedClimb)
    {
        PlayAnim("Up Stairs New");
        _myRb.useGravity = false;
        _myRb.MovePosition(transform.position + Vector3.up * speedClimb * Time.fixedDeltaTime);
        transform.rotation = Quaternion.Euler(0, 90, 0);
    }

    private void OnTriggerStay(Collider other)
    {
        var pick = other.GetComponent<IPick>();
        if (pick != null) pick.Pick();
    }
}