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
    [SerializeField, TextArea(4, 6)] string[] _messages;
    [SerializeField] GameObject _cinematic;
    [SerializeField] Camera _camDig;

    [Header("QUEST")]
    [SerializeField] int _totalBaits = 3;
    [SerializeField] TMP_Text _textTask;
    public int baitPicked = 0;
    private bool _questActive = false;
    private bool _questCompleted = false;

    [Header("NEXT QUEST")]
    [SerializeField] EnableChainQuest _chainQuest;

    private Collider _myCol;
    private Manager _gm;
    private Character _player;
    private QuestUI _questUI;
    private CameraOrbit _camPlayer;
    [SerializeField] DigBait[] _allBaits;


    private void Awake()
    {
        _myCol = GetComponent<Collider>();
        _dialogue = FindObjectOfType<Dialogue>();
        _gm = FindObjectOfType<Manager>();
        _player = FindObjectOfType<Character>();
        _questUI = FindObjectOfType<QuestUI>();
        _camPlayer = FindObjectOfType<CameraOrbit>();
    }

    private void Start()
    {
        _dialogue.gameObject.SetActive(false);
        _iconInteract.SetActive(false);
        _cinematic.SetActive(false);
        _camDig.gameObject.SetActive(false);

        foreach (var item in _allBaits)
            item.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (_questActive && baitPicked >= _totalBaits)
        {
            _myCol.enabled = true;
            _questUI.TaskCompleted(1);
            _questUI.AddNewTask(2, "Take the bait to the fisherman.");
            _questCompleted = true;
            _questActive = false;
        }

        else if (_questActive)
            _textTask.text = "Find and dig baits (" + baitPicked.ToString() + "/" + _totalBaits.ToString() + ")";
    }

    private void Confirm()
    {
        _myCol.enabled = false;
        _iconInteract.SetActive(false);
        _dialogue.canTalk = false;
        _dialogue.Close();

        _questUI.ActiveUIQuest("Fishing Excavation", "Find and dig baits (" + baitPicked.ToString() + _totalBaits.ToString() + ")", string.Empty, string.Empty);

        foreach (var item in _allBaits)
            item.gameObject.SetActive(true);

        StartCoroutine(ShowDigs());
        
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
        if (player != null && _myCol.enabled && !_questActive && !_questCompleted)
            SetDialogue();
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && _questCompleted && Input.GetKeyDown(_keyInteract))
            StartCoroutine(Ending());
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
        //_player.speed = 0;
        //_player.FreezePlayer(RigidbodyConstraints.FreezeAll);

        //_textNPCMessage.text = "Fisherman";
        //_textMessage.text = _messages[0];
        //_boxMessage.localScale = new Vector3(1, 1, 1);
        //_boxMessage.DOAnchorPosY(-1000f, 0);
        //_boxMessage.gameObject.SetActive(true);

        //yield return new WaitForSeconds(1f);

        //_camPlayer.gameObject.SetActive(false);
        //_cinematic.SetActive(true);

        //_boxMessage.DOAnchorPosY(70f, 0.5f);
        //yield return new WaitForSeconds(4f);
        //_boxMessage.DOAnchorPosY(-1000f, 0.5f);
        //yield return new WaitForSeconds(0.6f);
        //_textMessage.text = _messages[1];
        //_boxMessage.DOAnchorPosY(70f, 0.5f);
        //yield return new WaitForSeconds(4f);
        //_boxMessage.DOAnchorPosY(-1000f, 0.5f);
        //yield return new WaitForSeconds(0.6f);
        //_textMessage.text = _messages[2];
        //yield return new WaitForSeconds(4f);
        //_boxMessage.DOAnchorPosY(-1000f, 0.5f);
        //_boxMessage.gameObject.SetActive(false);

        //_player.speed = _player.speedAux;
        //_player.FreezePlayer(RigidbodyConstraints.FreezeRotation);
        //_camPlayer.gameObject.SetActive(true);

        //_gm.QuestCompleted();
        //Destroy(_cinematic);
        //_chainQuest.gameObject.SetActive(true);
        //Destroy(this);

        Destroy(_myCol);

        _player.speed = 0;
        _player.FreezePlayer(RigidbodyConstraints.FreezeAll);

        _textNPCMessage.text = "Fisherman";
        _textMessage.text = _messages[2];
        _boxMessage.localScale = new Vector3(1, 1, 1);
        _boxMessage.DOAnchorPosY(-1000f, 0);
        _boxMessage.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);
        _camPlayer.gameObject.SetActive(false);
        _cinematic.SetActive(true);

        _boxMessage.DOAnchorPosY(70f, 0.5f);
        yield return new WaitForSeconds(4f);
        _boxMessage.DOAnchorPosY(-1000f, 0.5f);
        yield return new WaitForSeconds(0.6f);
        _textMessage.text = _messages[3];
        _boxMessage.DOAnchorPosY(70f, 0.5f);
        yield return new WaitForSeconds(4f);
        _boxMessage.DOAnchorPosY(-1000f, 0.5f);
        yield return new WaitForSeconds(0.6f);
        _textMessage.text = _messages[4];
        yield return new WaitForSeconds(4f);
        _boxMessage.DOAnchorPosY(-1000f, 0.5f);
        yield return new WaitForSeconds(0.6f);
        _textMessage.text = _messages[5];
        yield return new WaitForSeconds(4f);
        _boxMessage.DOAnchorPosY(-1000f, 0.5f);
        yield return new WaitForSeconds(0.6f);
        _textMessage.text = _messages[6];
        yield return new WaitForSeconds(4f);
        _boxMessage.DOAnchorPosY(-1000f, 0.5f);

        yield return new WaitForSeconds(1f);
        _boxMessage.gameObject.SetActive(false);

        _player.speed = _player.speedAux;
        _player.FreezePlayer(RigidbodyConstraints.FreezeRotation);
        _camPlayer.gameObject.SetActive(true);

        _gm.QuestCompleted();
        Destroy(_cinematic);
        _chainQuest.gameObject.SetActive(true);
        Destroy(this);
    }


    private IEnumerator ShowDigs()
    {
        _questActive = true;

        foreach (var item in _allBaits)
            item.gameObject.SetActive(true);

        _camDig.gameObject.SetActive(true);
        _camPlayer.gameObject.SetActive(false);

        _player.speed = 0;
        _player.FreezePlayer(RigidbodyConstraints.FreezeAll);

        _textNPCMessage.text = "Digs";
        _textMessage.text = _messages[0];
        _boxMessage.localScale = new Vector3(1, 1, 1);
        _boxMessage.DOAnchorPosY(-1000f, 0);
        _boxMessage.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);
        _boxMessage.DOAnchorPosY(70f, 0.5f);

        yield return new WaitForSeconds(3f);
        _boxMessage.DOAnchorPosY(-1000f, 0.5f);

        yield return new WaitForSeconds(0.6f);
        _textMessage.text = _messages[1];
        _boxMessage.DOAnchorPosY(70f, 0.5f);

        yield return new WaitForSeconds(3f);
        _boxMessage.DOAnchorPosY(-1000f, 0.5f);

        Destroy(_camDig.gameObject);
        _camPlayer.gameObject.SetActive(true);

        _player.speed = _player.speedAux;
        _player.FreezePlayer(RigidbodyConstraints.FreezeRotation);

        yield return new WaitForSeconds(0.6f);
        _boxMessage.gameObject.SetActive(false);

    }

    public void CheatSkip()
    {
        Destroy(_cinematic);
        _gm.QuestCompleted();
        _chainQuest.gameObject.SetActive(true);
        Destroy(this);
    }
}