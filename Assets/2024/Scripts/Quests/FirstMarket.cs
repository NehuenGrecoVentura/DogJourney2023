using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class FirstMarket : MonoBehaviour
{
    [SerializeField] Camera _camFocusMarket;
    [SerializeField] MarketPlace _market;
    [SerializeField] RectTransform _boxMessage;
    [SerializeField] TMP_Text _textMessage;
    [SerializeField] TMP_Text _textName;
    [SerializeField, TextArea(4,6)] string _message;
    [SerializeField] GameObject _messageBuildFinished;
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
        _messageBuildFinished.SetActive(false);
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

        _boxMessage.gameObject.SetActive(true);
        _boxMessage.DOAnchorPosY(70f, 0.5f);
        _boxMessage.localScale = new Vector3(1, 1, 1);
        Character player = FindObjectOfType<Character>();
        _camPlayer.gameObject.SetActive(false);
        _camFocusMarket.gameObject.SetActive(true);
        player.gameObject.transform.LookAt(_market.gameObject.transform);
        player.speed = 0;
        player.FreezePlayer();
        yield return new WaitForSeconds(6f);
        
        _boxMessage.DOAnchorPosY(-1000f, 0.5f);
        yield return new WaitForSeconds(0.5f);
        _boxMessage.gameObject.SetActive(false);
        _camPlayer.gameObject.SetActive(true);
        _camFocusMarket.gameObject.SetActive(false);
        player.speed = player.speedAux;
        player.DeFreezePlayer();
        _myCol.enabled = true;
        _messageBuildFinished.SetActive(true);
        Destroy(gameObject);
    }
}