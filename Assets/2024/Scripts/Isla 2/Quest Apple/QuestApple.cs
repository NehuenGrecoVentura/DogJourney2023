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
    private QuestUI _questUI;
    private TreeApple[] _trees;
    private bool _questActive = false;
    private bool _questCompleted = false;
    private BoxApple _boxApple;

    [Header("THIEFS")]
    [SerializeField] ThiefApple[] _thiefs;
    [SerializeField] float _timeToSpwnThiefs = 8f;
    private CinematicThieft _cinematic;
    private bool _spawnActivate = false;

    private void Awake()
    {
        _myAnim = GetComponent<Animator>();
        _myCol = GetComponent<BoxCollider>();

        _questUI = FindObjectOfType<QuestUI>();
        _trees = FindObjectsOfType<TreeApple>();
        _boxApple = FindObjectOfType<BoxApple>();
        _cinematic = FindObjectOfType<CinematicThieft>();
    }

    private void Start()
    {
        _dialogue.gameObject.SetActive(false);
        _iconInteract.SetActive(false);

        foreach (var thief in _thiefs)
        {
            thief.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        bool hasEnoughApples = _boxApple.totalInBox >= 2;
        bool isBoxFull = _boxApple.totalInBox >= _boxApple.total;

        if (_questActive)
        {
            _questUI.AddNewTask(1, "Collect apples from the trees (" + _boxApple.totalInBox.ToString() + "/" + _boxApple.total.ToString() + ")");

            if (isBoxFull)
            {
                CancelInvoke("SpawnThiefts");
                DesactivateThiefts();
                _questUI.TaskCompleted(1);
                _questCompleted = true;
                _spawnActivate = false;
                _questActive = false;
            }

            else if (!_questCompleted && !IsInvoking("SpawnThiefts") && hasEnoughApples && _spawnActivate)
                InvokeRepeating("SpawnThiefts", 0f, _timeToSpwnThiefs);

            else if (!_spawnActivate && hasEnoughApples) _cinematic.Activate();

            else if (IsInvoking("SpawnThiefts") && _boxApple.totalInBox <= 1)
                CancelInvoke("SpawnThiefts");


            else if (_boxApple.totalInBox <= 0) _boxApple.totalInBox = 0;
        }
    }

    public void SpawnActive()
    {
        _spawnActivate = true;
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


    private void SpawnThiefts()
    {
        int activeEnemyCount = 0;
        foreach (var thief in _thiefs)
        {
            if (thief.gameObject.activeSelf)
            {
                activeEnemyCount++;
            }
        }

        if (activeEnemyCount < 3)
        {
            foreach (var thief in _thiefs)
            {
                if (!thief.gameObject.activeSelf)
                {
                    thief.gameObject.SetActive(true);
                    break;
                }
            }
        }
    }

    private void DesactivateThiefts()
    {
        foreach (var item in _thiefs)
        {
            item.gameObject.SetActive(false);
        }
    }
}