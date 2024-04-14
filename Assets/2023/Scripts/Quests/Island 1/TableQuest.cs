using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using DG.Tweening;

public class TableQuest : MonoBehaviour
{
    
    [SerializeField] Button _buttonConfirm;
    [SerializeField] int _totalWoods = 5;
    [SerializeField] string _taskBackToBuild;
    
    private QuestUI _questUI;
    private CharacterInventory _inventory;
    private LocationQuest _radar;
    private bool _questCurrent = false;

    [SerializeField, TextArea(4, 6)] string[] _lines;

    [Header("DIALOGS")]
    [SerializeField] GameObject _message;
    [SerializeField] TMP_Text _textMessage;
    [SerializeField] TMP_Text _textName;
    [SerializeField] string[] _messages;
    [SerializeField] string _nameNPC = "Peter";
    [SerializeField] Dialogue _dialogue;

    [Header("CINEMATIC")]
    [SerializeField] Camera _camCinematic;
    [SerializeField] Camera _camPlayer;
    [SerializeField] GameObject _iconTable;
    private Character _player;
    private Collider _colTable;

    [Header("ANIM")]
    [SerializeField] RuntimeAnimatorController[] _animController;
    private Animator _myAnim;
    private Vector3 _initialPos;

    [Header("INTERACT")]
    [SerializeField] GameObject _iconInteract;
    [SerializeField] KeyCode _keyInteract = KeyCode.F;
    private BoxCollider _myCol;

    private void Awake()
    {
        _myAnim = GetComponent<Animator>();
        _myCol = GetComponent<BoxCollider>();
        _questUI = FindObjectOfType<QuestUI>();
        _inventory = FindObjectOfType<CharacterInventory>();
        _radar = FindObjectOfType<LocationQuest>();
        _player = FindObjectOfType<Character>();
        _colTable = FindObjectOfType<BuildTable>().GetComponent<Collider>();
    }

    private void Start()
    {
        _dialogue.canTalk = true;
        _textName.text = _nameNPC;
        _initialPos = transform.position;
        transform.position = new Vector3(transform.position.x - 1f, transform.position.y, transform.position.z);
        _myAnim.runtimeAnimatorController = _animController[1];
        _buttonConfirm.onClick.AddListener(() => Confirm());
        _message.transform.localScale = new Vector3(0, 0, 0);

        for (int i = 0; i < _dialogue._lines.Length; i++)
            _dialogue._lines[i] = _lines[i];

        _dialogue.gameObject.SetActive(false);
        _iconInteract.SetActive(false);
    }

    private void Update()
    {
        if (_questCurrent)
        {
            _questUI.ActiveUIQuest("Making the table", "Get wood (" + _inventory.greenTrees.ToString() + "/" + _totalWoods.ToString() + ")",
            string.Empty,
            string.Empty);

            if (_inventory.greenTrees >= _totalWoods)
            {
                _questUI.TaskCompleted(1);
                _questUI.AddNewTask(2, _taskBackToBuild);
                _myCol.enabled = true;
                _questCurrent = false;
            }

            else _myCol.enabled = false;
        }
    }

    public void Confirm()
    {
        _myCol.enabled = false;
        _iconInteract.SetActive(false);
        _questCurrent = true;
        _dialogue.Close();
        
        _questUI.ActiveUIQuest("Making the table", "Get wood (" + _inventory.greenTrees.ToString() + "/" + _totalWoods.ToString() + ")",
            string.Empty,
            string.Empty);

        _buttonConfirm.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && _myCol.enabled)
        {
            _dialogue.gameObject.SetActive(true);
            _dialogue.playerInRange = true;
            _dialogue.Set(_nameNPC);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && _myCol.enabled)
        {
            if (!_questCurrent && _inventory.greenTrees >= _totalWoods)
            {
                _dialogue.playerInRange = false;
                if (Input.GetKeyDown(_keyInteract)) StartCoroutine(TutorialBuild());
            }

            if (!Input.GetKeyDown(_keyInteract)) _iconInteract.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && _myCol.enabled)
        {
            _iconInteract.SetActive(false);
            _dialogue.playerInRange = false;
        }
    }

    private IEnumerator TutorialBuild()
    {
        _inventory.nails = 10;
        _player.speed = 0;
        _player.FreezePlayer(RigidbodyConstraints.FreezeAll);
        Destroy(_myCol);
        Destroy(_iconInteract);
        _message.SetActive(true);
        _message.transform.DOScale(1f, 1f);
        _textMessage.text = _messages[0];
        yield return new WaitForSeconds(3f);
        transform.position = _initialPos;
        _myAnim.runtimeAnimatorController = _animController[0];
        _iconTable.SetActive(true);
        _camPlayer.gameObject.SetActive(false);
        _camCinematic.gameObject.SetActive(true);
        _message.SetActive(false);
        _message.transform.localScale = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(3f);
        _message.SetActive(true);
        _message.transform.DOScale(1f, 1f);
        _textMessage.text = _messages[1];
        yield return new WaitForSeconds(6f);
        _message.SetActive(false);
        _message.transform.localScale = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(0.1f);
        _message.SetActive(true);
        _message.transform.DOScale(1f, 1f);
        _textMessage.text = _messages[2];
        yield return new WaitForSeconds(4f);
        _message.transform.DOScale(0f, 0.5f);
        _camPlayer.gameObject.SetActive(true);
        _player.speed = _player.speedAux;
        _player.FreezePlayer(RigidbodyConstraints.FreezeRotation);
        _colTable.enabled = true;
        Destroy(_camCinematic.gameObject);
        Destroy(this);
    }
}