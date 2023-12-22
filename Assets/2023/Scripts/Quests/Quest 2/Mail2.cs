using UnityEngine;
using UnityEngine.UI;

public class Mail2 : MailQuest
{
    private Animator _myAnim;
    private Character _player;
    [SerializeField] GameObject _iconInteract;

    [Header("CURRENT QUEST")]
    [SerializeField] KeyCode _keyFinishQuest = KeyCode.F;
    [SerializeField] GameObject _letterQuest;
    [SerializeField] GameObject _canvasQuest;
    [SerializeField] Sprite _newIconQuest;

    [Header("BUTTONS MARKET")]
    [SerializeField] Image[] _backgroundColor;
    [SerializeField] Button _buttonAddDog;
    [SerializeField] Button _buttonMarket1SellWoods;
    [SerializeField] Color _colorUnlocked;
    [SerializeField] Sprite _ropeUnlocked;
    [SerializeField] Button _buttonRope;

    [Header("AUDIO")]
    [SerializeField] AudioClip _soundClip;
    private AudioSource _myAudio;

    [Header("NEXT QUEST")]
    [SerializeField] GameObject _npcMarket;
    [SerializeField] MarketShop _market;
    [SerializeField] Sprite _iconMessage;
    [SerializeField] string _messageText;
    [SerializeField] Transform _posMarket;
    [SerializeField] GameObject _broom;

    [Header("RADAR")]
    [SerializeField] Transform _boxPos;
    private LocationQuest _radar;

    private CharacterInventory _inventory;
    private bool _questActive = false;
    private QuestManager _questManager;
    private MessageSlide _message;
    private Manager _manager;

    private void Awake()
    {
        _myAudio = GetComponent<AudioSource>();
        _myAnim = GetComponent<Animator>();

        _inventory = FindObjectOfType<CharacterInventory>();
        _questManager = FindObjectOfType<QuestManager>();
        _message = FindObjectOfType<MessageSlide>();
        _player = FindObjectOfType<Character>();
        _manager = FindObjectOfType<Manager>();
        _radar = FindObjectOfType<LocationQuest>();
    }

    void Start()
    {
        iconQuest.SetActive(false);
        _letterQuest.SetActive(false);
        iconQuestActive.sprite = _newIconQuest;
        _iconInteract.SetActive(false);
        _broom.SetActive(false);
    }

    private void Update()
    {
        transform.LookAt(_player.gameObject.transform.position);
    }

    public void ConfirmQuest()
    {
        _buttonMarket1SellWoods.gameObject.SetActive(true);
        _radar.StatusRadar(true);
        _radar.target = _boxPos;
        _canvasQuest.SetActive(true);
        _myAnim.SetBool("Quest", true);
        _myAudio.PlayOneShot(_soundClip);
        _player.FreezePlayer(RigidbodyConstraints.FreezeRotation);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Destroy(_letterQuest.gameObject);
        StatusUI(nameQuest, secondText, iconQuestActive);
        iconQuestActive.sprite = _newIconQuest;
    }

    public void FinishQuest()
    {
        foreach (var background in _backgroundColor)
            background.color = _colorUnlocked;

        _myAnim.SetTrigger("Finish");
        _broom.SetActive(true);
        _radar.target = _posMarket;
        _npcMarket.SetActive(true);
        _manager.QuestCompleted();
        _questManager.HideHUDQuest();
        _message.ShowMessage(_messageText, _iconMessage);
        _buttonRope.enabled = true;
        _buttonRope.GetComponent<Image>().sprite = _ropeUnlocked;
        _buttonAddDog.gameObject.SetActive(true);
        _iconInteract.SetActive(false);
        Destroy(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            if (!_inventory.upgradeLoot && !_questActive)
            {
                _radar.StatusRadar(false);
                _letterQuest.gameObject.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                player.FreezePlayer(RigidbodyConstraints.FreezeAll);
                _questActive = true;
            }

            else if (_inventory.upgradeLoot && _questActive)
                _iconInteract.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            if (Input.GetKeyDown(_keyFinishQuest) && _inventory.upgradeLoot && _questActive)
                if (Input.GetKeyDown(_keyFinishQuest)) FinishQuest();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) _iconInteract.SetActive(false);
    }
}