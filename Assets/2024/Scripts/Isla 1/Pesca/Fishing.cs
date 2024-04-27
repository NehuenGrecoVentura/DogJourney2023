using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class Fishing : MonoBehaviour
{
    [Header("POS")]
    [SerializeField] Transform _posNPC;
    [SerializeField] Transform _posPlayer;
    private Vector3 _initialPos;
    private Quaternion _intialRot;

    [Header("INTERACT")]
    [SerializeField] GameObject _iconInteract;
    [SerializeField] KeyCode _keyInteract = KeyCode.F;

    [Header("TEXTS")]
    [SerializeField] TMP_Text[] _hud;
    [SerializeField] RectTransform _boxMessage;
    [SerializeField, TextArea(4,6)] string _message;

    [Header("REFS")]
    [SerializeField] Character _player;
    [SerializeField] CameraOrbit _camPlayer;
    [SerializeField] Camera _camFishing;
    [SerializeField] Image _fadeOut;
    [SerializeField] Collider _myCol;
    [SerializeField] FishingMinigame _fishing;
    [SerializeField] Manager _gm;
    [SerializeField] int _totalAmount = 3;
    [SerializeField] GameObject _rod;
    private bool _isPlay = false;

    private void Start()
    {
        _fadeOut.DOColor(Color.clear, 0f);
        _camFishing.gameObject.SetActive(false);
        _iconInteract.SetActive(false);
        _rod.SetActive(false);

        foreach (var item in _hud)
            item.gameObject.SetActive(false);
    }

    private void Update()
    {
        _hud[1].text = _fishing.fishedPicked.ToString();
        if (_fishing.fishedPicked == _totalAmount && _isPlay)
            StartCoroutine(FinishMiniGame());
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && !_isPlay) _iconInteract.SetActive(true);
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            if (Input.GetKeyDown(_keyInteract) && !_isPlay) StartCoroutine(PlayMiniGame());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) _iconInteract.SetActive(false);
    }

    private IEnumerator PlayMiniGame()
    {
        _isPlay = true;
        _myCol.enabled = false;
        _initialPos = _player.gameObject.transform.position;
        _intialRot = transform.rotation;
        _player.speed = 0;
        _player.FreezePlayer(RigidbodyConstraints.FreezeAll);
        _fadeOut.DOColor(Color.black, 1f);
        yield return new WaitForSeconds(1f);
        _camFishing.gameObject.SetActive(true);
        _camPlayer.gameObject.SetActive(false);
        _rod.SetActive(true);
        _player.gameObject.transform.position = transform.position;
        _player.gameObject.transform.rotation = _intialRot;
        transform.position = _initialPos;
        _fadeOut.DOColor(Color.clear, 1f);
        yield return new WaitForSeconds(1f);
        _fishing.start = true;
        foreach (var item in _hud)
            item.gameObject.SetActive(true);
    }

    private IEnumerator FinishMiniGame()
    {
        _isPlay = false;
        _fishing.Gaming = false;
        _fadeOut.DOColor(Color.black, 1f);
        yield return new WaitForSeconds(1f);

        transform.position = _player.transform.position;
        transform.rotation = _intialRot;
        _player.gameObject.transform.position = _initialPos;

        _fishing.Quit();
        _camFishing.gameObject.SetActive(false);
        _camPlayer.gameObject.SetActive(true);
        _fadeOut.DOColor(Color.clear, 1f);
        _rod.SetActive(false);

        foreach (var item in _hud)
            item.gameObject.SetActive(false);

        yield return new WaitForSeconds(2f);
        _hud[2].text = "Fisherman";
        _hud[3].text = _message;
        _boxMessage.localScale = new Vector3(1, 1, 1);
        _boxMessage.gameObject.SetActive(true);
        _boxMessage.DOAnchorPosY(70f, 0.5f);
        yield return new WaitForSeconds(3f);
        _boxMessage.DOAnchorPosY(-1000f, 0.5f);
        yield return new WaitForSeconds(0.5f);
        _boxMessage.gameObject.SetActive(false);
        _player.speed = _player.speedAux;
        _player.FreezePlayer(RigidbodyConstraints.FreezeRotation);
        _gm.UpgradeFishing();
    }
}