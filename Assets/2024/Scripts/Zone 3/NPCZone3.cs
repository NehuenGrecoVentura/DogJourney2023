using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class NPCZone3 : MonoBehaviour
{
    [Header("INTERACT")]
    [SerializeField] Collider _myCol;
    [SerializeField] KeyCode _keyInteract = KeyCode.F;
    [SerializeField] GameObject _iconInteract;
    [SerializeField] Animator _myAnim;

    [Header("DIALOGUE")]
    [SerializeField] Dialogue _dialogue;
    [SerializeField, TextArea(4, 6)] string[] _lines;
    [SerializeField] TMP_Text _txtName;
    [SerializeField] string _nameNPC;
    [SerializeField] Button _buttonConfirm;

    [Header("AUDIO")]
    [SerializeField] AudioSource _myAudio;
    [SerializeField] AudioClip _soundConfirm;

    [Header("QUEST")]
    [SerializeField] QuestUI _questUI;
    [SerializeField] int _woodRequired = 10;
    [SerializeField] CharacterInventory _inventory;
    private bool _questActive = false;
    private bool _questCompleted = false;

    void Start()
    {
        _dialogue.gameObject.SetActive(false);
        _iconInteract.transform.DOScale(0f, 0f);
    }

    private void Update()
    {
        if (_questActive)
        {
            if (_inventory.greenTrees >= _woodRequired)
            {
                _questUI.TaskCompleted(2);
                _questUI.AddNewTask(3, "Go back to the mountain");
                _questCompleted = true;
                _questActive = false;
            }

            else
            {
                _questUI.AddNewTask(2, "Get wood to carry the mountain (" + _inventory.greenTrees.ToString() + "/" + _woodRequired.ToString() + ")");
                _questCompleted = false;
                _questActive = true;
            }     
        }
    }

    private void Confirm()
    {
        _myCol.enabled = false;
        _dialogue.canTalk = false;
        _dialogue.Close();
        _iconInteract.transform.DOScale(0f, 0.5f);
        _myAudio.PlayOneShot(_soundConfirm);
        _myAnim.SetBool("Quest", true);
        _questUI.ActiveUIQuest("A Little Fire", "Travel back to the chairlift", "Get wood to carry the mountain (" + _inventory.greenTrees.ToString() + "/" + _woodRequired.ToString() + ")", string.Empty);
        _questActive = true;
    }

    private void SetDialogue()
    {
        _iconInteract.transform.DOScale(0.01f, 0.5f);
        _buttonConfirm.onClick.AddListener(() => Confirm());

        if (!_questActive)
        {
            for (int i = 0; i < _dialogue._lines.Length; i++)
                _dialogue._lines[i] = _lines[i];
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
            else if (_questCompleted) _iconInteract.transform.DOScale(0.01f, 0.5f);
        }
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    var player = other.GetComponent<Character>();
    //    if (player != null && _questCompleted && Input.GetKeyDown(_keyInteract))
    //        StartCoroutine(Ending(player));
    //}

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            _dialogue.playerInRange = false;
            _iconInteract.transform.DOScale(0f, 0.5f);
        }
    }
}