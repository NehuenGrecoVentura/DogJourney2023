using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TableQuest : NPCManager
{
    [SerializeField] Button _buttonConfirm;
    [SerializeField] int _totalWoods = 10;
    [SerializeField] int _totalNails = 10;
    [SerializeField] string _taskBackToBuild;
    [SerializeField] KeyCode _keyInteract = KeyCode.E;
    private QuestUI _questUI;
    private CharacterInventory _inventory;
    private Collider _myCol;
    private LocationQuest _radar;
    private bool _questCurrent = false;

    [SerializeField, TextArea(4, 6)] string[] _lines;

    private void Awake()
    {
        _myCol = GetComponent<Collider>();
        _questUI = FindObjectOfType<QuestUI>();
        _inventory = FindObjectOfType<CharacterInventory>();
        _radar = FindObjectOfType<LocationQuest>();
    }

    private void Start()
    {
        _myCol.enabled = false;
        _buttonConfirm.onClick.AddListener(() => Confirm());

        for (int i = 0; i < _dialogue._lines.Length; i++)
        {
            _dialogue._lines[i] = _lines[i];
        }
    }

    private void Update()
    {
        if (_questCurrent)
        {
            _questUI.ActiveUIQuest("Making the table", "Get wood (" + _inventory.greenTrees.ToString() + "/" + _totalWoods.ToString() + ")",
            "Get nails (" + _inventory.nails.ToString() + "/" + _totalNails.ToString() + ")",
            string.Empty);

            if (_inventory.greenTrees >= _totalWoods && _inventory.nails >= _totalNails)
            {
                _questUI.TaskCompleted(1);
                _questUI.TaskCompleted(2);
                _questUI.AddNewTask(3, _taskBackToBuild);
                _questCurrent = false;
            }
        }
    }

    public void Confirm()
    {
        _questCurrent = true;
        _dialogue.Close();
        _questUI.ActiveUIQuest("Making the table", "Get wood (" + _inventory.greenTrees.ToString() + "/" + _totalWoods.ToString() + ")",
            "Get nails (" + _inventory.nails.ToString() + "/" + _totalNails.ToString() + ")",
            string.Empty);
        _buttonConfirm.gameObject.SetActive(false);
    }


    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            if (!_questCurrent && _inventory.greenTrees >= _totalWoods && _inventory.nails >= _totalNails)
            {
                if (Input.GetKeyDown(_keyInteract))
                {

                }
            }
        }
    }





}