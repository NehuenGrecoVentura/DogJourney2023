using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class QuestFence : MonoBehaviour
{
    [Header("INTERACT")]
    [SerializeField] GameObject _iconInteract;
    [SerializeField] KeyCode _keyInteract = KeyCode.F;

    [Header("QUEST")]
    [SerializeField] int _woodsRequired = 5;
    [SerializeField] GameObject _iconBuild;

    [Header("DIALOG")]
    [SerializeField, TextArea(4, 6)] string[] _lines;
    [SerializeField] string _nameNPC;
    [SerializeField] Button _buttonConfirm;
    [SerializeField] RectTransform _message;
    [SerializeField] TMP_Text _textMessage;
    [SerializeField] TMP_Text _textName;
    [SerializeField, TextArea(4, 6)] string[] _messages;
    [SerializeField] Image _fadeOut;
    [SerializeField] TMP_Text _taskWoods;
    private Dialogue _dialogue;

    [Header("CAMERAS")]
    [SerializeField] Camera _camNPC;
    [SerializeField] Camera _camBush;
    [SerializeField] CameraOrbit _camPlayer;

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
    private bool _completeSeeds = false;

    private Collider _myCol;
    private QuestUI _questUI;
    private CharacterInventory _inventory;
    private Character _player;

    [Header("SEEDS")]
    private bool _seedsActive = false;
    private int _totalSeeds = 3;
    private Bush[] _allBush;

    private void Awake()
    {
        _myCol = GetComponent<Collider>();
        _myAnim = GetComponent<Animator>();
        _myAudio = GetComponent<AudioSource>();
        _dialogue = FindObjectOfType<Dialogue>();
        _questUI = FindObjectOfType<QuestUI>();
        _inventory = FindObjectOfType<CharacterInventory>();
        _player = FindObjectOfType<Character>();
        _allBush = FindObjectsOfType<Bush>();
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
        if(_activeQuest) _taskWoods.text = "Get some wood (" + _inventory.greenTrees.ToString() + "/" + _woodsRequired.ToString() + ")";

        if (_activeQuest && _inventory.greenTrees >= _woodsRequired && !_completedWoods && !_seedsActive)
        {
            _myCol.enabled = true;
            _questUI.TaskCompleted(1);
            _questUI.AddNewTask(2, "Go back to the florist");
            _completedWoods = true;
        }

        if (_seedsActive)
            _questUI.AddNewTask(2, "Get seeds from the bushes (" + _inventory.seeds.ToString() + "/" + _totalSeeds.ToString() + ")");

        if (_seedsActive && _inventory.seeds >= _totalSeeds && !_completeSeeds)
        {
            _myCol.enabled = true;
            _questUI.TaskCompleted(2);
            _questUI.AddNewTask(3, "Go back to the florist");
            _completeSeeds = true;
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
        if (player != null && (!_activeQuest || _completedWoods))
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
        if (player != null && Input.GetKeyDown(_keyInteract))
        {
            if (_completedWoods && !_seedsActive) StartCoroutine(ShowMessage());
            else if (_completeSeeds && _seedsActive) StartCoroutine(MessageBuild());
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
        _myCol.enabled = false;
        _dialogue.playerInRange = false;
        _seedsActive = true;

        _textName.text = _nameNPC;
        _textMessage.text = _messages[0];
        _fadeOut.DOColor(Color.clear, 0f);
        _iconInteract.SetActive(false);
        _player.speed = 0;
        _player.FreezePlayer(RigidbodyConstraints.FreezeAll);
        _message.gameObject.SetActive(true);
        _message.localScale = new Vector3(1, 1, 1);
        _myAudio.PlayOneShot(_messageSound);
        _message.DOAnchorPosY(70f, 0.5f);
        yield return new WaitForSeconds(2f);
        _message.DOAnchorPosY(-1000f, 0.5f).OnComplete(() => _fadeOut.DOColor(Color.black, 1f));
        yield return new WaitForSeconds(2f);
        _camPlayer.gameObject.SetActive(false);
        _camBush.gameObject.SetActive(false);
        _camNPC.gameObject.SetActive(true);
        _fadeOut.DOColor(Color.clear, 1f);
        _textMessage.text = _messages[1];
        _message.gameObject.SetActive(true);
        _myAudio.PlayOneShot(_messageSound);
        _message.DOAnchorPosY(70f, 0.5f);
        yield return new WaitForSeconds(4f);
        _message.DOAnchorPosY(-1000f, 0.5f).OnComplete(() => _fadeOut.DOColor(Color.black, 1f));
        yield return new WaitForSeconds(2f);
        _fadeOut.DOColor(Color.clear, 1f);
        _camPlayer.gameObject.SetActive(false);
        _camNPC.gameObject.SetActive(false);
        _camBush.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        _message.gameObject.SetActive(true);
        _textMessage.text = _messages[2];
        _myAudio.PlayOneShot(_messageSound);
        _message.DOAnchorPosY(70f, 0.5f);
        yield return new WaitForSeconds(4f);
        _message.DOAnchorPosY(-1000f, 0.5f).OnComplete(() => _fadeOut.DOColor(Color.black, 1f));
        yield return new WaitForSeconds(2f);
        _camPlayer.gameObject.SetActive(true);
        _camBush.gameObject.SetActive(false);
        _camNPC.gameObject.SetActive(false);
        _fadeOut.DOColor(Color.clear, 1f);
        _player.speed = _player.speedAux;
        _player.FreezePlayer(RigidbodyConstraints.FreezeRotation);
        _message.gameObject.SetActive(false);


        foreach (var item in _allBush)
        {
            item.enabled = true;
            item.GetComponent<Collider>().enabled = true;
        }
    }

    private IEnumerator MessageBuild()
    {
        _myCol.enabled = false;
        _dialogue.playerInRange = false;
        _textName.text = _nameNPC;
        _textMessage.text = _messages[3];
        _fadeOut.DOColor(Color.clear, 0f);
        _iconInteract.SetActive(false);
        _player.speed = 0;
        _player.FreezePlayer(RigidbodyConstraints.FreezeAll);
        _message.gameObject.SetActive(true);
        _message.localScale = new Vector3(1, 1, 1);
        _myAudio.PlayOneShot(_messageSound);
        _message.DOAnchorPosY(70f, 0.5f);
        yield return new WaitForSeconds(2.5f);
        _message.DOAnchorPosY(-1000f, 0.5f).OnComplete(() =>
        {
            _player.speed = _player.speedAux;
            _player.FreezePlayer(RigidbodyConstraints.FreezeRotation);
            _message.gameObject.SetActive(false);
            _iconBuild.gameObject.SetActive(true);
            Destroy(this);
        });
    }

    public void CheatSkip()
    {
        _myCol.enabled = false;
        Destroy(_iconInteract);
        Destroy(_iconBuild);
        FishingQuest2 npcFishing = FindObjectOfType<FishingQuest2>();
        LocationQuest radar = FindObjectOfType<LocationQuest>();
        Manager manager = FindObjectOfType<Manager>();
        manager.QuestCompleted();
        radar.target = npcFishing.gameObject.transform;
        npcFishing.enabled = true;
        npcFishing.GetComponent<Collider>().enabled = true;
        _inventory.shovelUnlocked = true;
        Destroy(this);
    }
}