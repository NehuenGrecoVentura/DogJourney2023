using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class FirstMarket : MonoBehaviour
{
    [SerializeField] Camera _camFocusMarket;
    [SerializeField] MarketPlace _market;
    [SerializeField] GameObject _boxMessage;
    [SerializeField] TMP_Text _textMessage;
    [SerializeField] TMP_Text _textName;
    [SerializeField, TextArea(4,6)] string _message;
    private CameraOrbit _camPlayer;
    private Collider _myCol;
    private bool _firstContact = false;

    private void Awake()
    {
        _myCol = GetComponent<Collider>();
        _camPlayer = FindObjectOfType<CameraOrbit>();
    }

    private void Start()
    {
        _camFocusMarket.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && !_firstContact) StartCoroutine(Market());
    }

    private IEnumerator Market()
    {
        _firstContact = true;
        _myCol.enabled = false;
        _textMessage.text = _message;
        _textName.text = "Tip";
        _boxMessage.SetActive(true);
        _boxMessage.transform.DOScale(1f, 0.5f);
        Character player = FindObjectOfType<Character>();
        _camPlayer.gameObject.SetActive(false);
        _camFocusMarket.gameObject.SetActive(true);
        player.gameObject.transform.LookAt(_market.gameObject.transform);
        player.speed = 0;
        player.FreezePlayer(RigidbodyConstraints.FreezeAll);
        yield return new WaitForSeconds(6f);
        _boxMessage.transform.DOScale(0f, 0.5f);
        _camPlayer.gameObject.SetActive(true);
        _camFocusMarket.gameObject.SetActive(false);
        player.speed = player.speedAux;
        player.FreezePlayer(RigidbodyConstraints.FreezeRotation);
        _myCol.enabled = true;
        Destroy(this);
    }
}