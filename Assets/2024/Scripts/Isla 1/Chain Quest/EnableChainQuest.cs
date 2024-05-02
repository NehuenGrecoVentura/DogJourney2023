using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class EnableChainQuest : MonoBehaviour
{
    [SerializeField] Collider _myCol;
    [SerializeField] GameObject _iconChainQuest;
    [SerializeField] Camera _focusCam;

    [Header("PLAYER")]
    [SerializeField] Character _player;
    [SerializeField] CameraOrbit _camPlayer;

    [Header("MESSAGE")]
    [SerializeField] RectTransform _boxMessage;
    [SerializeField] TMP_Text _textMessage;
    [SerializeField] TMP_Text _textName;
    [SerializeField, TextArea(4, 6)] string _message;
    [SerializeField] AudioSource _myAudio;

    private void Start()
    {
        _focusCam.gameObject.SetActive(false);
        _iconChainQuest.SetActive(false);
        _myAudio.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) StartCoroutine(ActiveChainQuests());
    }

    private IEnumerator ActiveChainQuests()
    {
        Destroy(_myCol);
        _camPlayer.gameObject.SetActive(false);
        _focusCam.gameObject.SetActive(true);
        _iconChainQuest.SetActive(true);
        _iconChainQuest.SetActive(true);
        _player.speed = 0;
        _player.FreezePlayer(RigidbodyConstraints.FreezeAll);
        _textName.text = "Special Quest";
        _textMessage.text = _message;
        _boxMessage.transform.localScale = new Vector3(1, 1, 1);
        _boxMessage.DOAnchorPosY(-1000f, 0f);
        _boxMessage.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        _myAudio.Play();
        _boxMessage.DOAnchorPosY(70f, 0f);
        yield return new WaitForSeconds(4f);
        Destroy(_focusCam.gameObject);
        _camPlayer.gameObject.SetActive(true);
        _boxMessage.DOAnchorPosY(-1000f, 0.5f);
        _player.speed = _player.speedAux;
        _player.FreezePlayer(RigidbodyConstraints.FreezeRotation);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}