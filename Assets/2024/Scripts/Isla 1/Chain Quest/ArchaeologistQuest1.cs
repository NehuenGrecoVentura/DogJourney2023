using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class ArchaeologistQuest1 : MonoBehaviour
{
    [Header("INTERACT")]
    [SerializeField] GameObject _iconInteract;
    [SerializeField] GameObject _iconQuest;
    [SerializeField] KeyCode _keyInteract = KeyCode.F;
    [SerializeField] Collider _myCol;

    [Header("DIALOGUE")]
    [SerializeField] TMP_Text _textDialogue;
    [SerializeField, TextArea(4, 6)] string[] _lines;
    [SerializeField] Button _buttonConfirm;
    private Dialogue _dialogue;

    [Header("QUEST")]
    private bool _questActive = false;

    [Header("INVENTORY UI")]
    [SerializeField] GameObject _canvasIconsChainsQuests;
    [SerializeField] GameObject _iconTreasure;
    [SerializeField] DoTweenManager _message;
    [SerializeField] RectTransform _boxMessage;
    [SerializeField] TMP_Text _textSlide;

    [Header("REFS")]
    private Character _player;
    private Manager _gm;

    private void Awake()
    {
        _dialogue = FindObjectOfType<Dialogue>();
        _player = FindObjectOfType<Character>();
        _gm = FindObjectOfType<Manager>();
    }

    private void Start()
    {
        _dialogue.gameObject.SetActive(false);
        _iconInteract.SetActive(false);
    }

    public void Confirm()
    {
        _dialogue.canTalk = false;
        _buttonConfirm.gameObject.SetActive(false);
        _dialogue.playerInRange = false;
        _myCol.enabled = false;
        _dialogue.Close();
        _iconInteract.SetActive(false);
        _gm.ActiveTutorialChain();
        _canvasIconsChainsQuests.SetActive(true);
        _iconTreasure.SetActive(true);
        _message.AddIconInventory(_boxMessage, _textSlide, "Added to inventory");
        Destroy(_iconQuest);
    }

    private void SetDialogue()
    {
        _dialogue.canTalk = true;
        _dialogue.Set("Archaeologist");
        _buttonConfirm.onClick.AddListener(() => Confirm());

        for (int i = 0; i < _dialogue._lines.Length; i++)
            _dialogue._lines[i] = _lines[i];

        _dialogue.gameObject.SetActive(true);
        _dialogue.playerInRange = true;
        _iconInteract.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && _myCol.enabled && !_questActive)
            SetDialogue();
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
}