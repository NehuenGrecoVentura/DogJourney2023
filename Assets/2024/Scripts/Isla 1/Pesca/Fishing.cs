using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class Fishing : MonoBehaviour
{
    [Header("INTERACT")]
    [SerializeField] GameObject _iconInteract;
    [SerializeField] KeyCode _keyInteract = KeyCode.F;

    [Header("CAMERAS")]
    [SerializeField] Camera _camRender;
    [SerializeField] Camera _myCam;
    private CameraOrbit _camPlayer;

    [Header("FISHING")]
    [SerializeField] int _totalAmount = 3;
    private FishingMinigame _fishing;
    private Character _player;
    private Collider _myCol;
    private bool _isActive = false;

    private void Awake()
    {
        _myCol = GetComponent<Collider>();

        _fishing = FindObjectOfType<FishingMinigame>();
        _player = FindObjectOfType<Character>();
        _camPlayer = FindObjectOfType<CameraOrbit>();
    }

    private void Start()
    {
        _myCam.gameObject.SetActive(false);
        _iconInteract.SetActive(false);
    }

    private void Update()
    {
        if (_isActive && _fishing.fishedPicked == _totalAmount) EndGame();
    }

    private void EndGame()
    {
        _fishing.Quit();
        _player.speed = _player.speedAux;
        _player.FreezePlayer(RigidbodyConstraints.FreezeRotation);
        _camPlayer.gameObject.SetActive(true);
        Destroy(_myCam.gameObject);
        _isActive = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && !_isActive) _iconInteract.SetActive(true);
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            if (Input.GetKeyDown(KeyCode.F) && !_isActive)
            {
                StartCoroutine(StartMiniGame());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && !_isActive) _iconInteract.SetActive(false);
    }

    private IEnumerator StartMiniGame()
    {
        _camPlayer.gameObject.SetActive(false);
        _myCam.gameObject.SetActive(true);
        _player.speed = 0;
        _player.FreezePlayer(RigidbodyConstraints.FreezeAll);
        _isActive = true;
        Destroy(_myCol);
        Destroy(_iconInteract);
        _fishing.start = true;
        yield return new WaitForSeconds(2f);
        _fishing.Gaming = true;
        _camRender.enabled = true;
    }
}