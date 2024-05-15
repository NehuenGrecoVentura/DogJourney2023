using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class HouseQuest3 : MailQuest
{
    private CharacterInventory _inventory;
    private QuestManager _quest;
    private Gates _gates;
    private Manager _manager;
    private bool _questActive = false;
    [SerializeField] Animator _animNPC;

    [SerializeField] GameObject _letterQuest;
    [SerializeField] Sprite _iconQuest;
    [SerializeField] Material _shaderPurpleTrees;
    private GameObject[] _purpleTrees;
    [SerializeField] GameObject _canvasQuest;

    [SerializeField] GameObject _iconInteract;

    [SerializeField] GameObject _broom;

    [Header("AUDIO")]
    [SerializeField] AudioClip _soundClip;
    [SerializeField] AudioClip _soundNotification;
    private AudioSource _myAudio;
    private bool _sound = false;

    [Header("NEXT QUEST")]
    [SerializeField] Camera _camPlayer;
    [SerializeField] Camera _camFocus;
    [SerializeField] Sprite _iconOil;
    [SerializeField] Image _iconSlide;
    [SerializeField] TMP_Text _textMessage;
    private HouseQuest4 _nextQuest;
    private MessageSlide _messageSlide;
    private Character _player;

    [Header("RADAR")]
    [SerializeField] Transform _npcQuest4;
    private LocationQuest _radar;


    private void Awake()
    {
        _myAudio = GetComponent<AudioSource>();

        _purpleTrees = GameObject.FindGameObjectsWithTag("Purple Leaves");
        _inventory = FindObjectOfType<CharacterInventory>();
        _messageSlide = FindObjectOfType<MessageSlide>();
        _nextQuest = FindObjectOfType<HouseQuest4>();
        _quest = FindObjectOfType<QuestManager>();
        _player = FindObjectOfType<Character>();
        _manager = FindObjectOfType<Manager>();
        _gates = FindObjectOfType<Gates>();
        _radar = FindObjectOfType<LocationQuest>();
    }

    private void Start()
    {
        _camFocus.gameObject.SetActive(false);
        _letterQuest.SetActive(false);
        _iconInteract.SetActive(false);
        _broom.SetActive(false);
    }

    private void Update()
    {
        if (_inventory.purpleTrees >= 10)
        {
            if (!_sound)
            {
                _myAudio.PlayOneShot(_soundNotification);
                _sound = true;
            }

            _radar.StatusRadar(true);
            _radar.target = transform;
            _quest.FirstSuccess(secondText);
            _quest.InitialSecondPhase("GO BACK TO THE HOUSE");
            _animNPC.SetBool("Quest", false);
            _manager.PurpleTreesNormal();
        }

        _animNPC.gameObject.transform.LookAt(_player.gameObject.transform.position);
    }

    public void ConfirmQuest()
    {
        _animNPC.SetBool("Quest", true);
        _canvasQuest.SetActive(true);
        _myAudio.PlayOneShot(_soundClip);
        Confirm(_letterQuest);
        iconQuestActive.sprite = _iconQuest;

        foreach (var leave in _purpleTrees)
            leave.GetComponent<MeshRenderer>().material = _shaderPurpleTrees;

        foreach (var tree in _gates.purpleTrees)
            tree.GetComponent<Collider>().enabled = true;

        _questActive = true;
    }

    public void FinishQuest()
    {
        StartCoroutine(FocusCam());
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && !_questActive)
        {
            _radar.StatusRadar(false);
            _letterQuest.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            player.FreezePlayer();
        }

    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && _questActive && _inventory.purpleTrees >= 10)
        {
            if (Input.GetKeyDown(KeyCode.F)) FinishQuest();
            else _iconInteract.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) _iconInteract.SetActive(false);
    }

    private IEnumerator FocusCam()
    {
        _broom.SetActive(true);
        _iconInteract.SetActive(false);
        _manager.PurpleTreesNormal();
        _radar.StatusRadar(false);
        _animNPC.SetTrigger("Finish");
        _inventory.purpleTrees -= 10;
        _manager.QuestCompleted();
        _gates.colHouses[1].enabled = true;
        _gates.iconQuestHouses[1].SetActive(true);
        _inventory.oils = 1;
        _quest.gameObject.SetActive(false);
        _nextQuest.enabled = true;
        _player.FreezePlayer();
        _player.speed = 0;
        _camFocus.gameObject.SetActive(true);
        _camPlayer.gameObject.SetActive(false);
        _messageSlide.ShowMessage("+1", _iconOil);
        yield return new WaitForSeconds(2f);
        _player.DeFreezePlayer();
        _player.speed = _player.speedAux;
        _camPlayer.gameObject.SetActive(true);
        _radar.StatusRadar(true);
        _radar.target = _npcQuest4;
        Destroy(_camFocus.gameObject);
        Destroy(this);
    }
}