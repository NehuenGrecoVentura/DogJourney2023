using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using DG.Tweening;

public class TableQuest : NPCManager
{
    
    [SerializeField] Button _buttonConfirm;
    [SerializeField] int _totalWoods = 5;
    [SerializeField] string _taskBackToBuild;
    [SerializeField] KeyCode _keyInteract = KeyCode.E;
    private QuestUI _questUI;
    private CharacterInventory _inventory;
    private Collider _myCol;
    private LocationQuest _radar;
    private bool _questCurrent = false;

    [SerializeField, TextArea(4, 6)] string[] _lines;


    [SerializeField] GameObject _message;
    [SerializeField] TMP_Text _textMessage;
    [SerializeField] string[] _messages;

    [Header("CINEMATIC")]
    [SerializeField] Camera _camCinematic;
    [SerializeField] Camera _camPlayer;
    [SerializeField] GameObject _iconTable;
    private Character _player;
    private Collider _colTable;

    private void Awake()
    {
        _myCol = GetComponent<Collider>();
        _questUI = FindObjectOfType<QuestUI>();
        _inventory = FindObjectOfType<CharacterInventory>();
        _radar = FindObjectOfType<LocationQuest>();
        _player = FindObjectOfType<Character>();
        _colTable = FindObjectOfType<BuildTable>().GetComponent<Collider>();
    }

    private void Start()
    {
        _buttonConfirm.onClick.AddListener(() => Confirm());
        _message.transform.localScale = new Vector3(0, 0, 0);

        for (int i = 0; i < _dialogue._lines.Length; i++)
            _dialogue._lines[i] = _lines[i];

        _dialogue.gameObject.SetActive(true);
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
                _questCurrent = false;
            }
        }
    }

    public void Confirm()
    {
        _questCurrent = true;
        _dialogue.Close();
        //_questUI.ActiveUIQuest("Making the table", "Get wood (" + _inventory.greenTrees.ToString() + "/" + _totalWoods.ToString() + ")",
        //    "Get nails (" + _inventory.nails.ToString() + "/" + _totalNails.ToString() + ")",
        //    string.Empty);


        _questUI.ActiveUIQuest("Making the table", "Get wood (" + _inventory.greenTrees.ToString() + "/" + _totalWoods.ToString() + ")",
            string.Empty,
            string.Empty);

        _buttonConfirm.gameObject.SetActive(false);
    }


    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            if (!_questCurrent && _inventory.greenTrees >= _totalWoods)
            {
                _dialogue.playerInRange = false;
                if (Input.GetKeyDown(_keyInteract)) StartCoroutine(TutorialBuild());
            }
        }
    }

    private IEnumerator TutorialBuild()
    {
        _inventory.nails = 10;
        _player.speed = 0;
        _player.FreezePlayer(RigidbodyConstraints.FreezeAll);
        Destroy(_myCol);
        _message.SetActive(true);
        _message.transform.DOScale(1f, 1f);
        _textMessage.text = _messages[0];
        yield return new WaitForSeconds(3f);
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
        _camPlayer.gameObject.SetActive(true);
        _player.speed = _player.speedAux;
        _player.FreezePlayer(RigidbodyConstraints.FreezeRotation);
        _colTable.enabled = true;
        Destroy(_message.transform.parent.gameObject);
        Destroy(_camCinematic.gameObject);
        Destroy(this);
    }
}