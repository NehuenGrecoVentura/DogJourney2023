using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestBroom : NPCManager
{
    [SerializeField] Button _buttonConfirm;
    [SerializeField] DogEnter _dogEnter;
    private Collider _col;
    private QuestUI _questUI;
    public bool broomFind = false;

    private void Awake()
    {
        _col = _dogEnter.gameObject.GetComponent<Collider>();
        _questUI = FindObjectOfType<QuestUI>();
    }

    private void Start()
    {
        _col.enabled = false;
        _buttonConfirm.onClick.AddListener(() => Confirm());
    }

    public void Confirm()
    {
        _dialogue.Close();
        _col.enabled = true;
        _questUI.ActiveUIQuest("The Broom", "Find the lost broom", string.Empty, string.Empty);
        _buttonConfirm.gameObject.SetActive(false);
    }
}