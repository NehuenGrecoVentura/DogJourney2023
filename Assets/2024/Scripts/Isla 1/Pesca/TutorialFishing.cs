using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class TutorialFishing : MonoBehaviour
{
    [SerializeField] RectTransform _boxMessage;
    [SerializeField] TMP_Text _message;
    [SerializeField, TextArea(4, 6)] string[] _lines;
    [SerializeField] Image _arrow;
    [SerializeField] Image[] _count;
    [SerializeField] int _totalFishes = 5;
    [SerializeField] int _reward = 5;
    private FishingMinigame _fishing;
    private CharacterInventory _inventory;
    private Manager _gm;
    private bool _questActive = true;

    [Header("CINEMATIC")]
    [SerializeField] GameObject _cinematic;
    [SerializeField] Image _fadeOut;
    private CameraOrbit _camPlayer;
    private QuestUI _questUI;

    [Header("MESSAGE FINAL")]
    [SerializeField] TMP_Text _textMessage;
    [SerializeField, TextArea(4,6)] string _messageFinal;

    private void Awake()
    {
        _fishing = FindObjectOfType<FishingMinigame>();
        _camPlayer = FindObjectOfType<CameraOrbit>();
        _questUI = FindObjectOfType<QuestUI>();
        _inventory = FindObjectOfType<CharacterInventory>();
        _gm = FindObjectOfType<Manager>();
    }

    private void Start()
    {
        StartCoroutine(PlayCinematic());
    }

    private void Update()
    {
        if (_fishing.fishedPicked == _totalFishes && _questActive)
        {
            StartCoroutine(Ending());
        }
    }

    private IEnumerator Ending()
    {
        _fishing.Gaming = false;
        _questActive = false;
        _fadeOut.DOColor(Color.black, 1f);
        yield return new WaitForSeconds(2f);
        _fishing.Quit();
        _fadeOut.DOColor(Color.clear, 1f);
        Destroy(_cinematic);
        _camPlayer.gameObject.SetActive(true);
        _inventory.money += _reward;
        _gm.QuestCompleted();
        Destroy(this);
    }

    private IEnumerator PlayCinematic()
    {
        _questActive = true;
        _fadeOut.DOColor(Color.black, 1f);
        yield return new WaitForSeconds(2f);
        _fadeOut.DOColor(Color.clear, 1f);
        _questUI.UIStatus(false);
        _camPlayer.gameObject.SetActive(false);
        _cinematic.SetActive(true);
        StartCoroutine(TutorialSpace());
    }

    private IEnumerator TutorialSpace()
    {
        _fishing.Gaming = false;
        _message.text = _lines[0];
        _boxMessage.DOAnchorPosY(-1000f, 0);
        _boxMessage.localScale = new Vector3(0.8f, 0.8f, 0.8f);

        // Tutorial SPACEBAR
        _boxMessage.gameObject.SetActive(true);
        _boxMessage.DOAnchorPosY(70f, 0.5f);
        yield return new WaitForSeconds(3f);
        _boxMessage.DOAnchorPosY(-1000f, 0.5f);

        // TUTRORIAL KEEP FISH
        yield return new WaitForSeconds(0.5f);
        _message.text = _lines[1];
        _boxMessage.DOAnchorPosY(70f, 0.5f);

        // TUTORIAL SLIDER
        _boxMessage.DOAnchorPosY(-1000f, 0.5f);
        yield return new WaitForSeconds(0.5f);
        _message.text = _lines[2];
        _arrow.DOColor(Color.white, 0.5f);
        _boxMessage.DOAnchorPosY(70f, 0.5f);
        _arrow.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        _boxMessage.DOAnchorPosY(-1000f, 0.5f);
        _arrow.DOColor(Color.clear, 0.5f);

        // CONTEO
        _count[0].DOColor(Color.white, 0.5f);
        yield return new WaitForSeconds(1f);
        _count[0].DOColor(Color.clear, 0.5f);

        _count[1].DOColor(Color.white, 0.5f);
        yield return new WaitForSeconds(1f);
        _count[1].DOColor(Color.clear, 0.5f);

        _count[2].DOColor(Color.white, 0.5f);
        yield return new WaitForSeconds(1f);
        _count[2].DOColor(Color.clear, 0.5f);

        _count[3].DOColor(Color.white, 0.5f);
        yield return new WaitForSeconds(1f);
        _count[3].DOColor(Color.clear, 0.5f);

        yield return new WaitForSeconds(1f);
        _fishing.Gaming = true;
    }
}