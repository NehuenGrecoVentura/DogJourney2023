using System.Collections;
using UnityEngine;

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

    [Header("MESSAGE SLIDE")]
    [SerializeField] Sprite _iconMessage;
    private MessageSlide _messageSlide;

    private void Awake()
    {
        _player = FindObjectOfType<Character>();
        _messageSlide = FindObjectOfType<MessageSlide>();
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
                _messageSlide.ShowMessage("TO UP", _iconMessage);
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
