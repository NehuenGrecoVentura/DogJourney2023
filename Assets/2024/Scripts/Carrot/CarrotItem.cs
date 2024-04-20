using UnityEngine;

public class CarrotItem : MonoBehaviour
{
    [Header("INTERACT")]
    [SerializeField] GameObject _iconInteractive;
    [SerializeField] KeyCode _keyInteractive;
    [SerializeField] Transform _posHand;
    [SerializeField] GameObject _sphereInteract;

    public bool carrotPicked = false;

    private bool _playerDetected = false;
    public bool objectPicked = false;

    private Animator _myAnim;

    private void Awake()
    {
        _myAnim = GetComponent<Animator>();
    }

    private void Start()
    {
        _iconInteractive.SetActive(false);
    }

    private void Update()
    {
        if (objectPicked)
        {
            _myAnim.enabled = false;
            CarryObject();
            if (Input.GetKeyDown(_keyInteractive))
                DropObject();
        }

        else
        {
            if (_playerDetected && Input.GetKeyDown(_keyInteractive))
                PickObject();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            _playerDetected = true;
            _iconInteractive.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            _playerDetected = false;
            _iconInteractive.SetActive(false);
        }
    }

    private void PickObject()
    {
        objectPicked = true;
        _iconInteractive.SetActive(false);
    }

    private void CarryObject()
    {
        _sphereInteract.SetActive(false);
        transform.position = _posHand.position;
    }

    private void DropObject()
    {
        objectPicked = false;
        transform.position = transform.position;
    }
}
