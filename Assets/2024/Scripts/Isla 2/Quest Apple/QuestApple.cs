using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using DG.Tweening;

public class QuestApple : MonoBehaviour
{
    [Header("INTERACT")]
    [SerializeField] GameObject _iconInteract;
    [SerializeField] KeyCode _keyInteract = KeyCode.F;
    private BoxCollider _myCol;

    [Header("DIALOGS")]
    [SerializeField] Button _buttonConfirm;
    [SerializeField] TMP_Text _textDialogue;
    [SerializeField] TMP_Text _textName;
    [SerializeField, TextArea(4, 6)] string[] _linesDialogues;
    [SerializeField] string _nameNPC = "Thomas";
    [SerializeField] Dialogue _dialogue;

    [Header("ANIM")]
    [SerializeField] RuntimeAnimatorController[] _animController;
    private Animator _myAnim;

    [Header("QUEST")]
    [SerializeField] int _amountApples = 10;
    private QuestUI _questUI;
    private TreeApple[] _trees;
    private bool _questActive = false;
    private bool _questCompleted = false;
    private CharacterInventory _inventory;

    [Header("THIEFS")]
    [SerializeField] ThiefApple[] _thiefs;

    private void Awake()
    {
        _myAnim = GetComponent<Animator>();
        _myCol = GetComponent<BoxCollider>();

        _inventory = FindObjectOfType<CharacterInventory>();
        _questUI = FindObjectOfType<QuestUI>();
        _trees = FindObjectsOfType<TreeApple>();
    }

    private void Start()
    {
        _dialogue.gameObject.SetActive(false);
        _iconInteract.SetActive(false);

        foreach (var thief in _thiefs)
        {
            thief.gameObject.SetActive(false);
        }

        print("TOTAL THIEFS:" + _thiefs.Length.ToString());
    }

    private void Update()
    {
        if (_questActive)
        {
            _questUI.AddNewTask(1, "Collect apples from the trees (" + _inventory.apples.ToString() + "/" + _amountApples.ToString() + ")");

            if (_questActive && _inventory.apples >= _amountApples)
            {
                _questUI.TaskCompleted(1);
                _questCompleted = true;
                _questActive = false;
            }
        }
    }

    private void Confirm()
    {
        _myCol.enabled = false;
        _iconInteract.SetActive(false);
        _dialogue.canTalk = false;
        _dialogue.Close();
        _questUI.ActiveUIQuest("The Great Harvest", "Collect apples from the trees", string.Empty, string.Empty);

        foreach (var tree in _trees)
        {
            tree.enabled = true;
            tree.GetComponent<BoxCollider>().enabled = true;
        }

        _questActive = true;
    }

    private void SetDialogue()
    {
        _textName.text = _nameNPC;
        _iconInteract.SetActive(true);
        _buttonConfirm.onClick.AddListener(() => Confirm());


        if (!_questActive)
        {
            for (int i = 0; i < _dialogue._lines.Length; i++)
                _dialogue._lines[i] = _linesDialogues[i];
        }

        _dialogue.gameObject.SetActive(true);
        _dialogue.playerInRange = true;
        _dialogue.canTalk = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            if (_myCol.enabled && !_questActive && !_questCompleted) SetDialogue();
            else if (_questCompleted) _iconInteract.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && _questCompleted && Input.GetKeyDown(_keyInteract)) print("TERMINADO");
        //StartCoroutine(Ending());
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