using System.Collections;
using UnityEngine;

public class ThiefApple : MonoBehaviour
{
    [SerializeField] Transform _posEscape;
    [SerializeField] Transform _posFollow;
    [SerializeField] float _timeThief = 3f;
    [SerializeField] float _speed = 5f;

    private BoxApple _boxApple;
    private Animator _myAnim;
    private BoxCollider _myCol;
    private Rigidbody _myRb;

    private bool _isThief = false;
    private bool _isScared = false;
    private bool _isEscape = false;

    private Vector3 _initialPos;

    private void Awake()
    {
        _myCol = GetComponent<BoxCollider>();
        _myAnim = GetComponent<Animator>();
        _myRb = GetComponent<Rigidbody>();

        _boxApple = FindObjectOfType<BoxApple>();
    }

    private void Start()
    {
        _initialPos = transform.position;
    }

    private void FixedUpdate()
    {
        if (!_isScared && !_isThief && !_isEscape) Movement(_posFollow);
        else if (_isEscape || _isScared)
        {
            _myCol.enabled = true;
            Movement(_posEscape);
            Off();
        }    
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !_isScared && _isThief)
        {
            StopCoroutine(Thief());
            _myRb.isKinematic = false;
            _isScared = true;
            _isThief = false;
        }
    }

    private void Movement(Transform pos)
    {
        Vector3 dir = (pos.position - transform.position).normalized;

        // Aplica la velocidad de movimiento
        _myRb.velocity = dir * _speed;

        // Rotación suave hacia el objetivo (opcional)
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        _myAnim.SetBool("Move", true);
    }


    private void Off()
    {
        float distanceToTarget = Vector3.Distance(transform.position, _posEscape.position);
        if(distanceToTarget <= 2f)
        {
            transform.position = _initialPos;
            transform.LookAt(_posFollow);
            
            _isScared = false;
            _isThief = false;
            _isEscape = false;
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var boxApple = other.GetComponent<BoxApple>();
        if (boxApple != null && !_isThief) StartCoroutine(Thief());
    }

    private IEnumerator Thief()
    {
        if (!_isScared)
        {
            _myAnim.SetBool("Move", false);
            _myCol.enabled = false;
            _myRb.isKinematic = true;
            _isThief = true;
            yield return new WaitForSeconds(_timeThief);
            _boxApple.RemoveSomeApples();
            _isThief = false;
            _myRb.isKinematic = false;
            transform.LookAt(_posEscape);
            _isEscape = true;
        }

        else
        {
            StopCoroutine("Thief");
        }
    }
}