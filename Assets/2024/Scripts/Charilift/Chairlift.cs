using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Chairlift : MonoBehaviour
{
    [Header("INTERACT")]
    [SerializeField] Transform _iconInteract;
    [SerializeField] KeyCode _keyInteract = KeyCode.F;
    [SerializeField] Collider _myCol;

    [Header("CAMS")]
    [SerializeField] Camera _camCinematic;
    [SerializeField] CameraOrbit _camPlayer;

    [Header("CINEMATIC")]
    [SerializeField] GameObject _cinematic;
    [SerializeField] Image _fadeOut;

    [Header("MOVE")]
    [SerializeField] Transform[] _waypoints;
    [SerializeField] Transform _posExit;
    [SerializeField] Transform _posSit;
    [SerializeField] Transform _parentPlayer;
    [SerializeField] float _speed = 5f;
    private int _index = 0;
    private bool _isActive = false;
    private Vector3 _initialPos/*, _initialPlayerPos*/;

    void Start()
    {
        _camCinematic.gameObject.SetActive(false);
        _cinematic.SetActive(false);
        _fadeOut.DOColor(Color.clear, 0f);
        _iconInteract.DOScale(0f, 0f);
        _initialPos = transform.position;
    }

    private void Update()
    {
        if (_isActive)
        {
            if (_waypoints.Length == 0) return;
            Vector3 direction = _waypoints[_index].position - transform.position;
            direction.Normalize();
            transform.Translate(direction * _speed * Time.deltaTime, Space.World);

            if (Vector3.Distance(transform.position, _waypoints[_index].position) < 0.1f)
            {
                _index++;
                if (_index >= _waypoints.Length) _index = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) _iconInteract.DOScale(0.1f, 0.5f);
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && Input.GetKeyDown(_keyInteract)) 
            StartCoroutine(ActiveChair(player));
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) _iconInteract.DOScale(0f, 0.5f);
    }

    private IEnumerator ActiveChair(Character player)
    {
        _fadeOut.DOColor(Color.black, 1.5f);
        _iconInteract.DOScale(0, 0.5f);
        _myCol.enabled = false;
        Vector3 posPlayer = player.transform.position;
        
        yield return new WaitForSeconds(2f);
        _fadeOut.DOColor(Color.clear, 1.5f);
        _camPlayer.gameObject.SetActive(false);
        _camCinematic.gameObject.SetActive(true);
        _cinematic.SetActive(true);
        player.transform.parent = _posSit.parent;
        player.transform.position = _posSit.position;
        player.transform.localRotation = Quaternion.Euler(90,0, 0);
        player.SitChair();
        _isActive = true;

        yield return new WaitForSeconds(5f);
        _fadeOut.DOColor(Color.black, 1.5f);

        yield return new WaitForSeconds(2f);
        _isActive = false;
        transform.position = _initialPos;
        _fadeOut.DOColor(Color.clear, 1.5f);
        _camPlayer.gameObject.SetActive(true);
        _camCinematic.gameObject.SetActive(false);
        _cinematic.SetActive(false);
        player.transform.parent = _parentPlayer;
        player.transform.position = posPlayer;
        player.isConstruct = false;
        player.DeFreezePlayer();
        _myCol.enabled = true;
    }
}