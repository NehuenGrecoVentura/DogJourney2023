using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class QuestSearch : MonoBehaviour
{
    [Header("INTERACT")]
    [SerializeField] GameObject _iconInteract;
    [SerializeField] KeyCode _keyInteract;
    [SerializeField] Collider _myCol;

    [Header("DIALOGUE")]
    [SerializeField] Dialogue _dialogue;
    [SerializeField, TextArea(4, 6)] string[] _linesDialogues;
    [SerializeField] Button _buttonConfirm;

    [Header("RADAR")]
    [SerializeField] LocationQuest _radar;

    [Header("QUEST")]
    [SerializeField] string _nameNPC = "Christine";
    [SerializeField] QuestUI _questUI;
    [SerializeField] int _total = 4;
    [SerializeField] int _found = 0;
    private bool _questActive = false;
    private bool _questCompleted = false;

    [Header("AUDIOS")]
    [SerializeField] AudioSource _myAudio;
    [SerializeField] AudioClip _soundConfirm;
    [SerializeField] AudioClip _soundMessage;

    [Header("MESSAGE")]
    [SerializeField] BoxMessages _boxMessage;

    void Start()
    {
        _dialogue.gameObject.SetActive(false);
        _iconInteract.SetActive(false);
    }

    private void Update()
    {
        if (_questActive)
        {
            _questUI.AddNewTask(1, "Find buried objects (" + _found.ToString() + "/" + _total.ToString() + ")");
        }
    }


    private void Confirm()
    {
        _myCol.enabled = false;
        _dialogue.canTalk = false;
        _dialogue.Close();
        _iconInteract.SetActive(false);
        _myAudio.PlayOneShot(_soundConfirm);
        _radar.StatusRadar(false);
        _questUI.ActiveUIQuest("Hunting Treasures", "Find buried objects", string.Empty, string.Empty);
        _questActive = true;
    }

    private void SetDialogue()
    {
        _iconInteract.SetActive(true);
        _buttonConfirm.onClick.AddListener(() => Confirm());

        if (!_questActive)
        {
            for (int i = 0; i < _dialogue._lines.Length; i++)
                _dialogue._lines[i] = _linesDialogues[i];
        }

        _dialogue.gameObject.SetActive(true);
        _dialogue.Set(_nameNPC);
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

    //private void OnTriggerStay(Collider other)
    //{
    //    var player = other.GetComponent<Character>();
    //    if (player != null && _questCompleted && Input.GetKeyDown(_keyInteract))
    //        StartCoroutine(Ending());
    //}

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
