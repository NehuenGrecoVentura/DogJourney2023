using UnityEngine;

public class HouseQuest6 : MailQuest
{
    [SerializeField] GameObject _letter;
    [SerializeField] Animator _animNPC;
    public bool quest6Active = false;

    private CharacterInventory _inventory;
    private LocationQuest _radar;
    private QuestManager _quest;
    private Manager _manager;
    private Gates _gates;

    [SerializeField] GameObject _myBroom;

    [Header("OBJETIVES")]
    [SerializeField] int _objetiveGreenTrees = 10;
    [SerializeField] int _objetivePurpleTrees = 10;
    [SerializeField] int _objetiveNails = 30;
    private bool _isFull = false;

    [Header("AUDIO")]
    [SerializeField] AudioClip _soundClip;
    [SerializeField] AudioClip _soundNotification;
    private AudioSource _myAudio;
    private bool _sound = false;

    [Header("NEXT QUEST")]
    [SerializeField] Sprite _iconOil;
    [SerializeField] string _textMessage;
    private MessageSlide _messageSlide;


    [SerializeField] GameObject _iconInteract;

    private void Awake()
    {
        _myAudio = GetComponent<AudioSource>();

        _inventory = FindObjectOfType<CharacterInventory>();
        _quest = FindObjectOfType<QuestManager>();
        _manager = FindObjectOfType<Manager>();
        _gates = FindObjectOfType<Gates>();
        _messageSlide = FindObjectOfType<MessageSlide>();
        _radar = FindObjectOfType<LocationQuest>();
    }

    void Start()
    {
        _letter.SetActive(false);
        _iconInteract.SetActive(false);
        _myBroom.SetActive(false);
    }

    private void Update()
    {
        questsTexts[1].text = "GREEN TREES: " + _inventory.greenTrees.ToString() + " /" + _objetiveGreenTrees.ToString();
        questsTexts[2].text = "PURPLE TREES: " + _inventory.purpleTrees.ToString() + " /" + _objetivePurpleTrees.ToString();
        questsTexts[3].text = "NAILS: " + _inventory.nails.ToString() + " /" + _objetiveNails.ToString();

        if (quest6Active)
        {
            if (_inventory.greenTrees >= _objetiveGreenTrees)
            {
                imageStatusPhase[0].enabled = false;
                imageStatusPhase[1].enabled = true;
                _manager.GreenTreesNormal();
            }

            else
            {
                _manager.GreenTreesShader();
                imageStatusPhase[0].enabled = true;
                imageStatusPhase[1].enabled = false;
            }

            if (_inventory.purpleTrees >= _objetivePurpleTrees)
            {
                _manager.PurpleTreesNormal();
                imageStatusPhase[2].enabled = false;
                imageStatusPhase[3].enabled = true;
            }

            else
            {
                _manager.PurpleTreesShader();
                imageStatusPhase[2].enabled = true;
                imageStatusPhase[3].enabled = false;
            }

            if (_inventory.nails >= _objetiveNails)
            {
                imageStatusPhase[4].enabled = false;
                imageStatusPhase[5].enabled = true;
            }

            else
            {
                imageStatusPhase[4].enabled = true;
                imageStatusPhase[5].enabled = false;
            }

            if (_inventory.greenTrees >= _objetiveGreenTrees && _inventory.purpleTrees >= _objetivePurpleTrees && _inventory.nails >= _objetiveNails)
            {
                if (!_sound)
                {
                    _myAudio.PlayOneShot(_soundNotification);
                    _sound = true;
                }

                _manager.GreenTreesNormal();
                _manager.PurpleTreesNormal();
                _isFull = true;
                imageStatusPhase[6].enabled = true;
                _phasesQuests[3].gameObject.SetActive(true);
                questsTexts[4].text = "Bring all the materials";
                _animNPC.SetBool("Quest", false);
                _radar.StatusRadar(true);
                _radar.target = transform;
            }

            else
            {
                _isFull = false;
                imageStatusPhase[6].enabled = false;
                _phasesQuests[3].gameObject.SetActive(false);
            }
        }

        
    }

    public void ConfirmQuest()
    {
        _manager.PurpleTreesShader();
        _manager.GreenTreesShader();
        _animNPC.SetBool("Quest", true);
        _myAudio.PlayOneShot(_soundClip);
        Confirm(_letter);
        _quest.gameObject.SetActive(true);
        _quest.QuestStatus(true, true, true, true);
        Destroy(iconQuest);
        questsTexts[0].text = nameQuest;

        foreach (var item in _gates.puzzleQuest6)
            item.SetActive(true);

        quest6Active = true;
    }

    public void FinishQuest()
    {
        _myBroom.SetActive(true);
        _iconInteract.SetActive(false);
        _manager.PurpleTreesNormal();
        _manager.PurpleTreesNormal();
        _radar.target = _gates.transform;
        _animNPC.SetTrigger("Finish");
        _messageSlide.ShowMessage(_textMessage, _iconOil);
        _inventory.nails -= _objetiveNails;
        if (_inventory.nails < 0) _inventory.nails = 0;

        _inventory.greenTrees -= _objetiveGreenTrees;
        if (_inventory.greenTrees < 0) _inventory.greenTrees = 0;

        _inventory.purpleTrees -= _objetivePurpleTrees;
        if (_inventory.purpleTrees < 0) _inventory.purpleTrees = 0;

        _inventory.oils = 4;
        _manager.QuestCompleted();
        _quest.gameObject.SetActive(false);
        Destroy(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && !quest6Active)
        {
            _radar.StatusRadar(false);
            _letter.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            player.FreezePlayer(RigidbodyConstraints.FreezeAll);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && _isFull)
        {
            if (Input.GetKeyDown(KeyCode.F) && _isFull) FinishQuest();
            else _iconInteract.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) _iconInteract.SetActive(false);
    }
}