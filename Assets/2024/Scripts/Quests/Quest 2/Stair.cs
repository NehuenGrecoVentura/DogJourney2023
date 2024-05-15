using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Stair : MonoBehaviour
{
    [SerializeField] float _speedClimb = 2f;
    [SerializeField] KeyCode _buttonInteractive = KeyCode.Space;
    private Character _player;
    private bool _isUp = false;
    private bool _firstContact = false;

    [Header("POS CONFIG")]
    [SerializeField] Transform _posEnd;
    [SerializeField] Transform _initialPos;
    [SerializeField] Collider _colPosEnd;

    [Header("MESSAGE")]
    [SerializeField] RectTransform _boxSpace;
    private float _time = 3f;

    private void Awake()
    {
        _player = FindObjectOfType<Character>();
    }

    private void Start()
    {
        _boxSpace.gameObject.SetActive(false);
        _boxSpace.DOAnchorPosY(-1000f, 0f);
    }

    private void Update()
    {
        if (_firstContact)
        {
            _boxSpace.gameObject.SetActive(true);
            _boxSpace.DOAnchorPosY(-100f, 0.5f);


            _time -= Time.deltaTime;
            if (_time <= 0)
            {
                //_boxSpace.gameObject.SetActive(false);
                _boxSpace.DOAnchorPosY(-1000f, 0.5f);
                _time = 0;
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();

        if (player != null)
        {
            if (Input.GetKeyDown(_buttonInteractive))
            {
                _colPosEnd.enabled = true;
                player.gameObject.transform.position = _initialPos.position;
                StartCoroutine(Climb());
            }

            if (!_firstContact)
            {
                //_messageSlide.ShowMessage("TO UP", _iconMessage);
                //StartCoroutine(ShowSPACE());
                _firstContact = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();

        if (player != null)
        {
            if (Input.GetKeyDown(_buttonInteractive))
            {
                _colPosEnd.enabled = true;
                player.gameObject.transform.position = _initialPos.position;
                StartCoroutine(Climb());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();

        if (player != null)
        {
            StopAllCoroutines();
            _player.isClimb = false;
            player.GetComponent<Rigidbody>().useGravity = true;
            player.PlayAnim("Idle");

            if (_isUp)
            {
                player.gameObject.transform.position = _posEnd.position;
                player.speed = player.speedAux;
                _isUp = false;
                StopCoroutine(Climb());
            }
        }
    }

    private IEnumerator Climb()
    {
        while (true)
        {
            _isUp = true;
            yield return new WaitForEndOfFrame();
            _player.MoveInStairs(_speedClimb);
        }
    }
}
