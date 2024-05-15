using UnityEngine;
using System.Collections;

public class HouseQuest5 : MailQuest
{
    [SerializeField] GameObject _letter;
    [SerializeField] Animator _animNPC;
    private CharacterInventory _inventory;
    private HouseQuest6 _nextQuest;
    private QuestManager _quest;
    private Manager _manager;
    private Gates _gates;
    private bool _questActive = false;

    [SerializeField] GameObject _myBroom;

    [Header("OBJETIVES")]
    [SerializeField] int _totalNails = 250;
    [SerializeField] int _totalMoney = 500;

    [Header("AUDIO")]
    [SerializeField] AudioClip _soundClip;
    [SerializeField] AudioClip _soundNotification;
    private AudioSource _myAudio;
    private bool _sound = false;

    [Header("NEXT QUEST")]
    [SerializeField] Camera _camPlayer;
    [SerializeField] Camera _camFocus;
    [SerializeField] Sprite _iconOil;
    private MessageSlide _messageSlide;
    private Character _player;

    [Header("RADAR")]
    [SerializeField] Transform _npcQuest6;
    private LocationQuest _radar;

    [SerializeField] GameObject _iconInteract;

    private void Awake()
    {
        _myAudio = GetComponent<AudioSource>();

        _inventory = FindObjectOfType<CharacterInventory>();
        _nextQuest = FindObjectOfType<HouseQuest6>();
        _quest = FindObjectOfType<QuestManager>();
        _player = FindObjectOfType<Character>();
        _manager = FindObjectOfType<Manager>();
        _gates = FindObjectOfType<Gates>();
        _messageSlide = FindObjectOfType<MessageSlide>();
        _radar = FindObjectOfType<LocationQuest>();
    }

    void Start()
    {
        _camFocus.gameObject.SetActive(false);
        _letter.SetActive(false);
        _iconInteract.SetActive(false);
    }

    void Update()
    {
        questsTexts[1].text = "NAILS: " + _inventory.nails.ToString() + " /" + _totalNails.ToString();
        questsTexts[2].text = "MONEY: " + _inventory.money.ToString() + " /" + _totalMoney.ToString();

        if (_inventory.money >= _totalMoney)
        {
            imageStatusPhase[2].enabled = false;
            imageStatusPhase[3].enabled = true;
            _quest.SecondSuccess("MONEY: " + _inventory.money.ToString() + " /" + _totalMoney.ToString());
        }

        else
        {
            imageStatusPhase[2].enabled = true;
            imageStatusPhase[3].enabled = false;
        }

        if (_inventory.nails >= _totalNails)
        {
            imageStatusPhase[0].enabled = false;
            imageStatusPhase[1].enabled = true;
            _quest.FirstSuccess("NAILS: " + " /" + _totalNails.ToString());
        }

        else
        {
            imageStatusPhase[0].enabled = true;
            imageStatusPhase[1].enabled = false;
        }

        if (_inventory.nails >= _totalNails && _inventory.money >= _totalMoney)
        {
            if (!_sound)
            {
                _myAudio.PlayOneShot(_soundNotification);
                _sound = true;
            }

            _animNPC.Play("Quest");
            questsTexts[3].text = "Give it to them at home.";
            _phasesQuests[3].SetActive(true);
            imageStatusPhase[3].enabled = true;
            imageStatusPhase[4].enabled = true;
            imageStatusPhase[5].enabled = false;
            _radar.StatusRadar(true);
            _radar.target = transform;
        }

        else _phasesQuests[3].SetActive(false);
    }

    public void ConfirmQuest()
    {
        _animNPC.Play("Idle");
        _myAudio.PlayOneShot(_soundClip);
        Confirm(_letter);
        _quest.gameObject.SetActive(true);
        _quest.QuestStatus(true, true, true, false);
        //Destroy(iconQuest);
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
            _letter.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            player.FreezePlayer();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && _inventory.nails >= _totalNails && _inventory.money >= _totalMoney && _questActive)
        {
            if (Input.GetKeyDown(KeyCode.F)) FinishQuest();
            else _iconInteract.SetActive(true);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if(player != null) _iconInteract.SetActive(false);
    }

    private IEnumerator FocusCam()
    {
        _radar.StatusRadar(false);

        _inventory.nails -= _totalNails;
        if (_inventory.nails < 0) _inventory.nails = 0;

        _inventory.money -= _totalMoney;
        if (_inventory.money < 0) _inventory.money = 0;

        _myBroom.SetActive(true);
        _iconInteract.SetActive(false);
        _inventory.oils = 3;
        _manager.QuestCompleted();
        _animNPC.SetTrigger("Finish");
        _quest.gameObject.SetActive(false);
        _gates.colHouses[3].enabled = true;
        _gates.iconQuestHouses[3].SetActive(true);
        _nextQuest.enabled = true;
        _camFocus.gameObject.SetActive(true);
        _camPlayer.gameObject.SetActive(false);
        _player.FreezePlayer();
        _player.speed = 0;
        _messageSlide.ShowMessage("+ 1", _iconOil);
        yield return new WaitForSeconds(2f);
        _player.DeFreezePlayer();
        _player.speed = _player.speedAux;
        _camPlayer.gameObject.SetActive(true);
        _radar.StatusRadar(true);
        _radar.target = _npcQuest6;
        Destroy(_camFocus.gameObject);
        Destroy(this);
    }
}