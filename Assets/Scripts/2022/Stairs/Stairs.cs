using UnityEngine;
using System.Collections;

public class Stairs : MonoBehaviour
{
    Character2022 _player;

    [Header("INTERACTIVE CONFIG")]
    [SerializeField] KeyCode _buttonInteractive;
    [SerializeField] GameObject _iconInteractive;
    [SerializeField] float _speedClimb = 2f;

    [Header("POS CONFIG")]
    [SerializeField] Transform _posEnd;
    [SerializeField] Transform _initialPos;
    [SerializeField] Collider _colPosEnd;
    bool _isUp = false;

    void Awake()
    {
        _player = FindObjectOfType<Character2022>();
    }

    void Start()
    {
        _iconInteractive.SetActive(false);
    }

    void Update()
    {
        if (_isUp) _iconInteractive.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character2022>();

        if (player != null)
        {
            if (Input.GetKeyDown(_buttonInteractive))
            {
                _isUp = true;
                _colPosEnd.enabled = true;
                player.gameObject.transform.position = _initialPos.position;
                StartCoroutine(Climb());
            }

            else _iconInteractive.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character2022>();

        if (player != null)
        {
            if (Input.GetKeyDown(_buttonInteractive))
            {
                _isUp = true;
                _colPosEnd.enabled = true;
                player.gameObject.transform.position = _initialPos.position;
                StartCoroutine(Climb());
            }
            else _iconInteractive.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character2022>();

        if (player != null)
        {
            StopAllCoroutines();
            _player.isClimb = false;
            _iconInteractive.SetActive(false);
            _isUp = false;
            player.GetComponent<Rigidbody>().useGravity = true;
            player.EjecuteAnim("Idle");
            if (Input.GetKeyDown(_buttonInteractive))
                player.gameObject.transform.position = _posEnd.position;
        }
    }

    IEnumerator Climb()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            _player.MoveInStairs(_speedClimb);
            if (_isUp) _iconInteractive.SetActive(false);
        }
    }
}