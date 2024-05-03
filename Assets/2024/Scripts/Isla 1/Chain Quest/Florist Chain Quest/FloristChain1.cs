using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class FloristChain1 : MonoBehaviour
{
    [Header("INTERACT")]
    [SerializeField] GameObject _iconInteract;
    [SerializeField] GameObject _iconQuest;
    [SerializeField] KeyCode _keyInteract = KeyCode.F;
    private Collider _myCol;

    [Header("DIALOGUE")]
    [SerializeField] TMP_Text _textName;
    [SerializeField] TMP_Text _textDialogue;
    [SerializeField, TextArea(4, 6)] string[] _lines;
    [SerializeField] Button _buttonConfirm;
    [SerializeField] Dialogue _dialogue;
    
    [Header("QUEST")]
    private bool _questActive = false;
    private Manager _gm;

    [Header("INVENTORY UI")]
    [SerializeField] GameObject _canvasIconsChainsQuests;
    [SerializeField] GameObject _iconFlowers;
    [SerializeField] RectTransform _boxMessage;
    [SerializeField] TMP_Text _textSlide;
    [SerializeField] DoTweenManager _message;

    private void Awake()
    {
        _myCol = GetComponent<Collider>();
        _gm = FindObjectOfType<Manager>();
    }

    private void Start()
    {
        _dialogue.gameObject.SetActive(false);
        _iconInteract.SetActive(false);
    }

    private void SetDialogue()
    {
        _dialogue.canTalk = true;
        _dialogue.Set("Florist");
        _buttonConfirm.onClick.AddListener(() => Confirm());

        for (int i = 0; i < _dialogue._lines.Length; i++)
            _dialogue._lines[i] = _lines[i];

        _dialogue.gameObject.SetActive(true);
        _dialogue.playerInRange = true;
        _iconInteract.SetActive(true);
    }

    public void Confirm()
    {
        _dialogue.canTalk = false;
        _buttonConfirm.gameObject.SetActive(false);
        _dialogue.playerInRange = false;
        _myCol.enabled = false;
        _dialogue.Close();
        _iconInteract.SetActive(false);
        Destroy(_iconQuest);
        _gm.ActiveTutorialChain();
        _canvasIconsChainsQuests.SetActive(true);
        _iconFlowers.SetActive(true);
        _message.AddIconInventory(_boxMessage, _textSlide, "Added to inventory");
        _questActive = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && _myCol.enabled && !_questActive && this.enabled)
            SetDialogue();
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
}