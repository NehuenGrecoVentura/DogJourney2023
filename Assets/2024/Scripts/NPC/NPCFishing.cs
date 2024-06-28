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

    [Header("FISHING")]
    [SerializeField] private FishingMinigame _fishing;
    private TutorialFishing _tutorial;

    [Header("AUDIO")]
    [SerializeField] AudioClip _soundConfirm;
    private AudioSource _myAudio;

    private Vector3 _initialPos;
    private Vector3 _initialPosPlayer;
    [SerializeField] Character _player;

    [Header("ANIMS")]
    [SerializeField] RuntimeAnimatorController[] _animController;
    [SerializeField] GameObject _rod;
    private Animator _myAnim;

    private void Awake()
    {
        _myAnim = GetComponent<Animator>();
        _tutorial = GetComponent<TutorialFishing>();
        _myAudio = GetComponent<AudioSource>();
        _dialogue = FindObjectOfType<Dialogue>();
    }

    void Start()
    {
        _initialPos = transform.position;
        _iconInteract.SetActive(false);
        _dialogue.canTalk = true;
        _dialogue.Set(_nameNPC);
        _tutorial.enabled = false;

        _buttonConfirm.onClick.AddListener(() => Confirm());

        for (int i = 0; i < _dialogue._lines.Length; i++)
            _dialogue._lines[i] = _lines[i];

        _dialogue.gameObject.SetActive(false);
    }

    private void Confirm()
    {
        _dialogue.canTalk = false;
        _fishing.start = true;
        _myAudio.PlayOneShot(_soundConfirm);
        _buttonConfirm.gameObject.SetActive(false);
        _dialogue.Close();

        _initialPosPlayer = _player.gameObject.transform.position;

        _tutorial.enabled = true;
        _dialogue.playerInRange = false;
        Destroy(_myCol);
        Destroy(_iconInteract);
    }

    public void SetPos()
    {
        _myAnim.runtimeAnimatorController = _animController[0];
        _rod.SetActive(true);

        _player.gameObject.transform.position = _initialPosPlayer;
        transform.position = _initialPos;
        Destroy(this);
    }

    public void SetAnimQuest()
    {
        _myAnim.runtimeAnimatorController = _animController[1];
        _myAnim.SetBool("Quest", true);
        _rod.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            _dialogue.gameObject.SetActive(true);
            _dialogue.playerInRange = true;
            _iconInteract.SetActive(true);
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