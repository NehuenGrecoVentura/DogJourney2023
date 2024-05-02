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
    [SerializeField] KeyCode _keyInteract = KeyCode.F;
    [SerializeField] Collider _myCol;

    [Header("DIALOGUE")]
    [SerializeField] TMP_Text _textDialogue;
    [SerializeField, TextArea(4, 6)] string[] _lines;
    [SerializeField] Button _buttonConfirm;
    private Dialogue _dialogue;

    [Header("QUEST")]
    public bool archaeologistChain = false;

    [Header("REFS")]
    private Character _player;

    private void Awake()
    {
        _dialogue = FindObjectOfType<Dialogue>();
        _player = FindObjectOfType<Character>();
    }

    private void Start()
    {
        _dialogue.canTalk = true;
        _dialogue.Set("Archaeologist");
        _buttonConfirm.onClick.AddListener(() => Confirm());

        for (int i = 0; i < _dialogue._lines.Length; i++)
            _dialogue._lines[i] = _lines[i];

        _dialogue.gameObject.SetActive(false);
    }

    public void Confirm()
    {
        _dialogue.canTalk = false;
        _buttonConfirm.gameObject.SetActive(false);
        _dialogue.playerInRange = false;
        _myCol.enabled = false;
        _dialogue.Close();
        _iconInteract.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && _myCol.enabled)
        {
            _dialogue.gameObject.SetActive(true);
            _dialogue.playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            _dialogue.playerInRange = false;
        }
    }
}