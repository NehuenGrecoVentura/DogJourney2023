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
    private LocationQuest _radar;
    public bool broomFind = false;

    private void Awake()
    {
        _col = _dogEnter.gameObject.GetComponent<Collider>();
        _questUI = FindObjectOfType<QuestUI>();
        _radar = FindObjectOfType<LocationQuest>();
    }

    private void Start()
    {
        _col.enabled = false;
        _buttonConfirm.onClick.AddListener(() => Confirm());
    }

    public void Confirm()
    {
        _radar.target = _dogEnter.gameObject.transform;
        _dialogue.Close();
        _col.enabled = true;
        _questUI.ActiveUIQuest("The Broom", "Find the lost broom", string.Empty, string.Empty);
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
                Destroy(this);
            }   
        }
    }
}