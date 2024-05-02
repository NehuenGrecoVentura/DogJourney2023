using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class FishingChain1 : MonoBehaviour
{
    [Header("INTERACT")]
    [SerializeField] GameObject _iconInteract;
    [SerializeField] GameObject _iconBubble;
    [SerializeField] KeyCode _keyInteract = KeyCode.F;
    private Collider _myCol;

    [Header("DIALOGUE")]
    [SerializeField] TMP_Text _textDialogue;
    [SerializeField] TMP_Text _textName;
    [SerializeField, TextArea(4, 6)] string[] _lines;
    [SerializeField] Button _buttonConfirm;
    [SerializeField] Dialogue _dialogue;

    [Header("QUEST")]
    private bool _questActive = false;

    [Header("REFS")]
    private Character _player;

    private void Awake()
    {
        _myCol = GetComponent<Collider>();
        _player = FindObjectOfType<Character>();
    }

    private void Start()
    {
        //_dialogue.gameObject.SetActive(false);
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
        Destroy(_iconBubble);
        _questActive = true;
    }

    private void SetDialogue()
    {
        _textName.text = "Fisherman";
        _iconInteract.SetActive(true);
        _buttonConfirm.onClick.AddListener(() => Confirm());
        _dialogue.gameObject.SetActive(true);
        _dialogue.playerInRange = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && _myCol.enabled && gameObject.activeSelf)
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