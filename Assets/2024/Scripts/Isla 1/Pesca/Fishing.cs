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
    private Manager _gm;
    private Character _player;
    private Collider _myCol;
    private bool _isActive = false;

    [Header("MESSAGE")]
    [SerializeField] RectTransform _boxMessage;
    [SerializeField] Image _fadeOut;
    [SerializeField] TMP_Text _textMessage;
    [SerializeField] TMP_Text _textName;
    [SerializeField, TextArea(4, 6)] string _message;

    private void Awake()
    {
        _myCol = GetComponent<Collider>();

        _fishing = FindObjectOfType<FishingMinigame>();
        _player = FindObjectOfType<Character>();
        _camPlayer = FindObjectOfType<CameraOrbit>();
        _gm = FindObjectOfType<Manager>();
    }

    private void Start()
    {
        _myCam.gameObject.SetActive(false);
        _iconInteract.SetActive(false);
    }

    private void Update()
    {
        if (_isActive && _fishing.fishedPicked == _totalAmount) 
            StartCoroutine(FinishMiniGame());
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

    private IEnumerator FinishMiniGame()
    {
        _isActive = false;
        _fishing.Quit();
        _gm.levelFishing++;
        Destroy(_myCam.gameObject);
        _camPlayer.gameObject.SetActive(true);

        _textName.text = "Fisherman";
        _textMessage.text = _message;
        _boxMessage.localScale = new Vector3(1, 1, 1);
        _boxMessage.gameObject.SetActive(true);
        _boxMessage.DOAnchorPosY(70f, 0.5f);
        yield return new WaitForSeconds(3f);
        _boxMessage.DOAnchorPosY(-1000f, 0.5f);
        yield return new WaitForSeconds(1f);
        _boxMessage.gameObject.SetActive(false);
        _player.speed = _player.speedAux;
        _player.FreezePlayer(RigidbodyConstraints.FreezeRotation);
    }
}