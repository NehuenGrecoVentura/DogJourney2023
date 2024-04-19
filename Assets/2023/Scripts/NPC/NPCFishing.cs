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
    [SerializeField] private FishingMinigame _fishing;

    [Header("AUDIO")]
    [SerializeField] AudioClip _soundConfirm;
    private AudioSource _myAudio;

    [Header("CINEMATIC")]
    [SerializeField] GameObject _cinematic;


    private void Awake()
    {
        _myAudio = GetComponent<AudioSource>();
        _dialogue = FindObjectOfType<Dialogue>();
    }

    void Start()
    {
        _iconInteract.SetActive(false);
        _dialogue.canTalk = true;
        _dialogue.Set(_nameNPC);
    }

    private void Confirm()
    {
        _dialogue.canTalk = false;
        _fishing.Start = true;
        _myAudio.PlayOneShot(_soundConfirm);
        _questActive = true;
        _buttonConfirm.gameObject.SetActive(false);
        _dialogue.Close();
        _cinematic.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            _dialogue.playerInRange = true;
            _iconInteract.SetActive(true);
            _dialogue.gameObject.SetActive(true);
            _buttonConfirm.onClick.AddListener(() => Confirm());

            for (int i = 0; i < _dialogue._lines.Length; i++)
                _dialogue._lines[i] = _lines[i];
        }
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && _questActive && Input.GetKeyDown(KeyCode.F))
            _fishing.Start = true;
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