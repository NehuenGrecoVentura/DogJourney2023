using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class NPCFishing : MonoBehaviour
{
    [Header("INTERACT")]
    [SerializeField] GameObject _iconInteract;
    [SerializeField] KeyCode _keyInteract = KeyCode.F;
    [SerializeField] Collider _myCol;

    [Header("DIALOGUE")]
    [SerializeField, TextArea(4, 6)] string[] _lines;
    [SerializeField] string _nameNPC;
    [SerializeField] TMP_Text _textLine;
    [SerializeField] TMP_Text _textName;
    [SerializeField] Button _buttonConfirm;
    private Dialogue _dialogue;
    private bool _questActive = false;

    [Header("FISHING")]
    private FishingMinigame _fishing;

    private void Awake()
    {
        _dialogue = FindObjectOfType<Dialogue>();
        _fishing = FindObjectOfType<FishingMinigame>();
    }

    void Start()
    {
        _iconInteract.SetActive(false);
        _dialogue.canTalk = true;
        _dialogue.Set(_nameNPC);
        _buttonConfirm.onClick.AddListener(() => Confirm());

        for (int i = 0; i < _dialogue._lines.Length; i++)
            _dialogue._lines[i] = _lines[i];
    }

    private void Update()
    {
        if (_questActive)
        {

            _fishing.FishQuest(_keyInteract);

        }
    }

    private void Confirm()
    {
        _questActive = true;
        _buttonConfirm.gameObject.SetActive(false);
        _dialogue.Close();
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            _dialogue.playerInRange = true;
            _iconInteract.SetActive(true);
            _dialogue.gameObject.SetActive(true);
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
}