using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestBroom : MonoBehaviour
{
    [SerializeField] Button _buttonConfirm;
    [SerializeField] DogEnter _dogEnter;
    [SerializeField, TextArea(4, 6)] string[] _lines;
    private Collider _col;
    private QuestUI _questUI;
    private LocationQuest _radar;
    private TableQuest _nextQuest;
    public bool broomFind = false;
    [SerializeField] string _nameNPC = "Mary";
    [SerializeField] Dialogue _dialogue;
    [SerializeField] GameObject _broomPrefab;
    [SerializeField] RuntimeAnimatorController[] _animController;
    private Animator _myAnim;

    [Header("INTERACT")]
    [SerializeField] GameObject _iconInteract;
    [SerializeField] KeyCode _keyInteract = KeyCode.F;
    private BoxCollider _myCol;
    private bool _activeQuest = false;

    private void Awake()
    {
        _myCol = GetComponent<BoxCollider>();
        _myAnim = GetComponent<Animator>();
        _col = _dogEnter.gameObject.GetComponent<Collider>();
        _questUI = FindObjectOfType<QuestUI>();
        _radar = FindObjectOfType<LocationQuest>();
        _nextQuest = FindObjectOfType<TableQuest>();
    }

    private void Start()
    {
        _iconInteract.SetActive(false);
        _myAnim.runtimeAnimatorController = _animController[1];
        _broomPrefab.SetActive(false);
        _col.enabled = false;
        _buttonConfirm.onClick.AddListener(() => Confirm());
        _nextQuest.enabled = false;

        for (int i = 0; i < _dialogue._lines.Length; i++)
            _dialogue._lines[i] = _lines[i];
    }

    public void Confirm()
    {
        _iconInteract.SetActive(false);
        _myCol.enabled = false;
        _activeQuest = true;
        _dogEnter.enabled = true;
        _radar.target = _dogEnter.gameObject.transform;
        _dialogue.Close();
        _col.enabled = true;
        _questUI.ActiveUIQuest("The Hidden Broom", "Find the lost broom", string.Empty, string.Empty);
        _buttonConfirm.gameObject.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            if (Input.GetKeyDown(_keyInteract) && _dogEnter.broomPicked)
            {
                Destroy(_myCol);
                Destroy(_iconInteract);

                ChangeController();
                _dogEnter.ActiveNextQuest();
                Destroy(this, 6f);
            }


            if(!_activeQuest && !_dogEnter.broomPicked && !Input.GetKeyDown(KeyCode.F)) _iconInteract.SetActive(true);
        }
    }

    public void ChangeController()
    {
        _myAnim.runtimeAnimatorController = _animController[0];
        _broomPrefab.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            _dialogue.gameObject.SetActive(true);
            _dialogue.playerInRange = true;
            _dialogue.Set(_nameNPC);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            if(_activeQuest || !_activeQuest) _iconInteract.SetActive(false);
            _dialogue.playerInRange = false;
        }    
    }
}