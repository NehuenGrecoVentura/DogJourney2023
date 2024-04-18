using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class QuestFence : MonoBehaviour
{
    [Header("INTERACT")]
    [SerializeField] GameObject _iconInteract;
    [SerializeField] KeyCode _keyInteract = KeyCode.F;

    [Header("QUEST")]
    [SerializeField] int _woodsRequired = 5;

    [Header("DIALOG")]
    [SerializeField, TextArea(4, 6)] string[] _lines;
    [SerializeField] string _nameNPC;
    [SerializeField] Button _buttonConfirm;
    [SerializeField] RectTransform _message;
    private Dialogue _dialogue;

    [Header("AUDIOS")]
    [SerializeField] AudioClip _messageSound;
    [SerializeField] AudioClip _confirmSound;
    private AudioSource _myAudio;

    [Header("ANIMS")]
    [SerializeField] RuntimeAnimatorController[] _animController;
    [SerializeField] GameObject _broom;
    private Animator _myAnim;

    private bool _activeQuest = false;
    private bool _completedWoods = false;
    private Collider _myCol;
    private QuestUI _questUI;
    private CharacterInventory _inventory;
    private Character _player;

    private void Awake()
    {
        _myCol = GetComponent<Collider>();
        _myAnim = GetComponent<Animator>();
        _myAudio = GetComponent<AudioSource>();
        _dialogue = FindObjectOfType<Dialogue>();
        _questUI = FindObjectOfType<QuestUI>();
        _inventory = FindObjectOfType<CharacterInventory>();
        _player = FindObjectOfType<Character>();
    }

    void Start()
    {
        _dialogue.canTalk = true;
        _dialogue.gameObject.SetActive(true);
        _buttonConfirm.onClick.AddListener(() => Confirm());
        _myAnim.runtimeAnimatorController = _animController[1];
        _broom.SetActive(false);
    }

    private void Update()
    {
        if (_activeQuest && _inventory.greenTrees >= _woodsRequired && !_completedWoods)
        {
            _myCol.enabled = true;
            _questUI.TaskCompleted(1);
            _questUI.AddNewTask(2, "Try to repair the fences");
            _completedWoods = true;
        }
    }

    private void Confirm()
    {
        _dialogue.canTalk = false;
        _myAudio.PlayOneShot(_confirmSound);
        _iconInteract.SetActive(false);
        _dialogue.Close();
        _myCol.enabled = false;
        _activeQuest = true;
        _questUI.ActiveUIQuest("Broken Fences", "Get some wood (" + _inventory.greenTrees.ToString() + "/" + _woodsRequired.ToString() + ")", string.Empty, string.Empty);
        _myAnim.SetBool("Quest", true);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && !_activeQuest || _completedWoods)
        {
            _iconInteract.SetActive(true);
            _dialogue.playerInRange = true;
            _dialogue.Set(_nameNPC);

            for (int i = 0; i < _dialogue._lines.Length; i++)
                _dialogue._lines[i] = _lines[i];

            _message.gameObject.SetActive(false);
            _message.DOAnchorPosY(-1000f, 0);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && _completedWoods)
        {
            if (Input.GetKeyDown(_keyInteract)) 
                StartCoroutine(ShowMessage());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            _iconInteract.SetActive(false);
            _dialogue.playerInRange = false;
        }
    }

    private IEnumerator ShowMessage()
    {
        Destroy(_myCol);
        _iconInteract.SetActive(false);
        _player.speed = 0;
        _player.FreezePlayer(RigidbodyConstraints.FreezeAll);
        _message.gameObject.SetActive(true);
        _message.localScale = new Vector3(1, 1, 1);
        _message.gameObject.SetActive(true);
        _message.DOAnchorPosY(70f, 0.5f);
        yield return new WaitForSeconds(2f);
        _message.gameObject.SetActive(true);
        _message.DOAnchorPosY(-1000f, 0.5f);
        _player.speed = _player.speedAux;
        _player.FreezePlayer(RigidbodyConstraints.FreezeRotation);
        yield return new WaitForSeconds(1f);
        _message.gameObject.SetActive(false);  
        Destroy(this);
    }
}