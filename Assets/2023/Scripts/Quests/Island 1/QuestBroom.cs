using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestBroom : NPCManager
{
    [SerializeField] Button _buttonConfirm;
    [SerializeField] DogEnter _dogEnter;
    [SerializeField, TextArea(4,6)] string[] _lines;
    private Collider _col;
    private QuestUI _questUI;
    private LocationQuest _radar;
    private TableQuest _nextQuest;
    public bool broomFind = false;

    private void Awake()
    {
        _col = _dogEnter.gameObject.GetComponent<Collider>();
        _questUI = FindObjectOfType<QuestUI>();
        _radar = FindObjectOfType<LocationQuest>();
        _nextQuest = FindObjectOfType<TableQuest>();
    }

    private void Start()
    {
        _col.enabled = false;
        _buttonConfirm.onClick.AddListener(() => Confirm());
        _nextQuest.enabled = false;

        for (int i = 0; i < _dialogue._lines.Length; i++)
            _dialogue._lines[i] = _lines[i];
    }

    public void Confirm()
    {
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
            if (Input.GetKeyDown(KeyCode.E) && _dogEnter.broomPicked)
            {
                _dogEnter.ActiveNextQuest();
                Destroy(this, 6f);
            }   
        }
    }
}