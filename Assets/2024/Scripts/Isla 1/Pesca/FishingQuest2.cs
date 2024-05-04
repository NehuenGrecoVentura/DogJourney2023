using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;


public class FishingQuest2 : MonoBehaviour
{
    [Header("INTERACT")]
    [SerializeField] GameObject _iconInteract;
    [SerializeField] KeyCode _keyInteract = KeyCode.F;

    [Header("DIALOGUE")]
    [SerializeField, TextArea(4, 6)] string[] _lines;
    [SerializeField] TMP_Text _textLine;
    [SerializeField] TMP_Text _textName;
    [SerializeField] Button _buttonConfirm;
    private Dialogue _dialogue;

    [Header("MESSAGE")]
    [SerializeField] RectTransform _boxMessage;
    [SerializeField] TMP_Text _textMessage;
    [SerializeField] TMP_Text _textNPCMessage;
    [SerializeField, TextArea(4,6)] string[] _messages;
    [SerializeField] GameObject _cinematic;

    [Header("FISHING")]
    [SerializeField] GameObject[] _objs;
    [SerializeField] Fishing _fishingMinigame;
    [SerializeField] int _totalAmount = 5;
    [SerializeField] GameObject _canvasRender;
    private FishingMinigame _fish;

    [Header("NEXT QUEST")]
    [SerializeField] EnableChainQuest _chainQuest;

    private bool _questActive = false;
    private bool _questCompleted = false;
    private Collider _myCol;
    private Manager _gm;
    private Character _player;
    private QuestUI _questUI;
    private CameraOrbit _camPlayer;
    

    private void Awake()
    {
        _myCol = GetComponent<Collider>();
        _dialogue = FindObjectOfType<Dialogue>();
        _fish = FindObjectOfType<FishingMinigame>();
        _gm = FindObjectOfType<Manager>();
        _player = FindObjectOfType<Character>();
        _questUI = FindObjectOfType<QuestUI>();
        _camPlayer = FindObjectOfType<CameraOrbit>();
    }

    private void Start()
    {
        _dialogue.gameObject.SetActive(false);
        _iconInteract.SetActive(false);
        _fishingMinigame.gameObject.SetActive(false);
        _cinematic.SetActive(false);
        _fishingMinigame._totalAmount = _totalAmount;
    }

    private void Update()
    {
        //if (_questActive && (!_canvasRender.activeSelf || _fish.fishedPicked == _totalAmount))
        //{
        //    print("COMPLETADO");
        //    StartCoroutine(Ending());
        //}

        
        if(_fishingMinigame.completed && _questActive)
        {
            StartCoroutine(Ending());
        }


        if (_questCompleted)
        {
            _player.speed = 0;
            _player.FreezePlayer(RigidbodyConstraints.FreezeAll);
            _camPlayer.gameObject.SetActive(false);
        }
    }

    private void Confirm()
    {
        _myCol.enabled = false;
        _iconInteract.SetActive(false);
        _dialogue.canTalk = false;
        _dialogue.Close();

        foreach (var item in _objs)
        {
            item.SetActive(false);
        }

        _fishingMinigame.gameObject.SetActive(true);
        _fishingMinigame.StarFishing();

        print("MISION ACEPTADA");
        _questActive = true;
    }

    private void SetDialogue()
    {
        _textName.text = "Fisherman";
        _iconInteract.SetActive(true);
        _buttonConfirm.onClick.AddListener(() => Confirm());


        if (!_questCompleted)
        {
            for (int i = 0; i < _dialogue._lines.Length; i++)
                _dialogue._lines[i] = _lines[i];
        }

        _dialogue.gameObject.SetActive(true);
        _dialogue.playerInRange = true;
        _dialogue.canTalk = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && _myCol.enabled && !_questActive)
        {
            SetDialogue();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            _dialogue.playerInRange = false;
            _iconInteract.SetActive(false);
        }
    }

    private IEnumerator Ending()
    {
        _questActive = false;
        _questCompleted = true;

        _player.speed = 0;
        _player.FreezePlayer(RigidbodyConstraints.FreezeAll);

        _textNPCMessage.text = "Fisherman";
        _textMessage.text = _messages[0];
        _boxMessage.localScale = new Vector3(1, 1, 1);
        _boxMessage.DOAnchorPosY(-1000f, 0);
        _boxMessage.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);
        Destroy(_fishingMinigame.gameObject);

        _camPlayer.gameObject.SetActive(false);
        _cinematic.SetActive(true);

        foreach (var item in _objs)
            item.SetActive(true);
        
        _boxMessage.DOAnchorPosY(70f, 0.5f);
        yield return new WaitForSeconds(4f);
        _boxMessage.DOAnchorPosY(-1000f, 0.5f);
        yield return new WaitForSeconds(0.6f);
        _textMessage.text = _messages[1];
        _boxMessage.DOAnchorPosY(70f, 0.5f);
        yield return new WaitForSeconds(4f);
        _boxMessage.DOAnchorPosY(-1000f, 0.5f);
        yield return new WaitForSeconds(0.6f);
        _textMessage.text = _messages[2];
        yield return new WaitForSeconds(4f);
        _boxMessage.DOAnchorPosY(-1000f, 0.5f);
        _boxMessage.gameObject.SetActive(false);

        _player.speed = _player.speedAux;
        _player.FreezePlayer(RigidbodyConstraints.FreezeRotation);
        _camPlayer.gameObject.SetActive(true);

        _gm.QuestCompleted();
        Destroy(_cinematic);
        _chainQuest.gameObject.SetActive(true);
        Destroy(this);
    }
}