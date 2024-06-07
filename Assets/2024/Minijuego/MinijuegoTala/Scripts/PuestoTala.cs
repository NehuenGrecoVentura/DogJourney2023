using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class PuestoTala : MonoBehaviour
{
    [Header("INTERACT")]
    [SerializeField] Transform _iconInteract;
    [SerializeField] KeyCode _keyInteract = KeyCode.F;
    [SerializeField] Collider _myCol;
    [SerializeField] MinijuegoTalaManager _manager;

    [Header("INTRO")]
    [SerializeField] Image _fadeOut;
    [SerializeField] Image[] _count;
    [SerializeField] BoxMessages _boxMessage;
    [SerializeField, TextArea(4, 6)] string _message;
    [SerializeField] CameraOrbit _camPlayer;
    [SerializeField] Camera _camMiniGame;
    private bool _firstContact = true;

    void Start()
    {
        _iconInteract.DOScale(0f, 0f);
        _fadeOut.DOColor(Color.clear, 0f);
        _camMiniGame.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) _iconInteract.DOScale(0.5f, 0.5f);
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && Input.GetKeyDown(_keyInteract))
        {
            if (_firstContact) StartCoroutine(InitialPlay(player));
            else StartCoroutine(Play(player));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) _iconInteract.DOScale(0f, 0.5f);
    }

    private IEnumerator InitialPlay(Character player)
    {
        _firstContact = false;
        _myCol.enabled = false;

        _boxMessage.SetMessage("Mini Game");
        player.FreezePlayer();
        _iconInteract.DOScale(0f, 0.5f);
        _fadeOut.DOColor(Color.black, 1f);

        yield return new WaitForSeconds(2f);
        _camMiniGame.gameObject.SetActive(true);
        _camPlayer.gameObject.SetActive(false);
        _fadeOut.DOColor(Color.clear, 1f);

        yield return new WaitForSeconds(3f);
        _boxMessage.ShowMessage(_message);

        yield return new WaitForSeconds(3f);
        _boxMessage.CloseMessage();

        for (int i = 0; i < _count.Length; i++)
        {
            _count[i].DOColor(Color.white, 0.5f);
            yield return new WaitForSeconds(1f);
            _count[i].DOColor(Color.clear, 0.5f);
        }

        _manager.StartGame();

        yield return new WaitForSeconds(0.5f);
        _boxMessage.DesactivateMessage();
    }

    private IEnumerator Play(Character player)
    {
        _myCol.enabled = false;
        player.FreezePlayer();
        _iconInteract.DOScale(0f, 0.5f);
        _fadeOut.DOColor(Color.black, 1f);

        yield return new WaitForSeconds(2f);
        _camMiniGame.gameObject.SetActive(true);
        _camPlayer.gameObject.SetActive(false);
        _fadeOut.DOColor(Color.clear, 1f);

        yield return new WaitForSeconds(2f);
        for (int i = 0; i < _count.Length; i++)
        {
            _count[i].DOColor(Color.white, 0.5f);
            yield return new WaitForSeconds(1f);
            _count[i].DOColor(Color.clear, 0.5f);
        }

        _manager.StartGame();
    }
}