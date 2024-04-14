using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

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
    [SerializeField] string[] _tasks;

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
    //[SerializeField] Sprite _iconMessage;
    //[SerializeField] string _messageText;
    [SerializeField] Transform _posMarket;
    [SerializeField] GameObject _broom;

    [Header("QUICK")]
    [SerializeField] Camera _dogCam;
    [SerializeField] Camera _camFocus;
    [SerializeField] Dog _dog;
    [SerializeField] Image _fadeOut;
    [SerializeField] Transform _quickPos;
    [SerializeField] RectTransform _finalDialog;
    private CameraOrbit _camPlayer;
    private bool _finish = false;

    [Header("RADAR")]
    [SerializeField] Transform _boxPos;
    private LocationQuest _radar;

    private CharacterInventory _inventory;
    private bool _questActive = false;
    private MessageSlide _message;
    private Manager _manager;

    private void Awake()
    {
        _myAudio = GetComponent<AudioSource>();
        _myAnim = GetComponent<Animator>();

        _inventory = FindObjectOfType<CharacterInventory>();
        _message = FindObjectOfType<MessageSlide>();
        _player = FindObjectOfType<Character>();
        _manager = FindObjectOfType<Manager>();
        _radar = FindObjectOfType<LocationQuest>();
        _camPlayer = FindObjectOfType<CameraOrbit>();
    }

    void Start()
    {
        //iconQuest.SetActive(false);
        _letterQuest.SetActive(false);
        iconQuestActive.sprite = _newIconQuest;
        _iconInteract.SetActive(false);
        _broom.SetActive(false);
        
        _finalDialog.gameObject.SetActive(false);

        //Cams
        _camFocus.gameObject.SetActive(false);
        _dogCam.gameObject.SetActive(false);
        _fadeOut.color = new Color(0, 0, 0, 0);
    }

    private void Update()
    {
        transform.LookAt(_player.gameObject.transform.position);

        if (Input.GetKeyDown(KeyCode.Space) && _inventory.upgradeLoot && _questActive)
            StartCoroutine(Ending());
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
        _questUI.ActiveUIQuest(nameQuest, _tasks[0], _tasks[1], _tasks[2]);
        iconQuestActive.sprite = _newIconQuest;
    }

    public void FinishQuest()
    {
        foreach (var background in _backgroundColor)
            background.color = _colorUnlocked;

        _dog.quickEnd = false;
        _camPlayer.gameObject.SetActive(true);

        _myAnim.SetTrigger("Finish");
        _broom.SetActive(true);
        //_radar.target = _posMarket;
        //_npcMarket.SetActive(true);
        _manager.QuestCompleted();
        _questUI.UIStatus(false);
        //_message.ShowMessage(_messageText, _iconMessage);
        //_buttonRope.enabled = true;
        //_buttonRope.GetComponent<Image>().sprite = _ropeUnlocked;
        //_buttonAddDog.gameObject.SetActive(true);
        _iconInteract.SetActive(false);
        _player.speed = _player.speedAux;
        _player.FreezePlayer(RigidbodyConstraints.FreezeRotation);
        Destroy(_camFocus.gameObject);
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

            else if (_inventory.upgradeLoot && _questActive && !_finish)
                _iconInteract.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            if (Input.GetKeyDown(_keyFinishQuest) && _inventory.upgradeLoot && _questActive)
                if (Input.GetKeyDown(_keyFinishQuest)) StartCoroutine(EndingNormal()); //FinishQuest();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) _iconInteract.SetActive(false);
    }

    private IEnumerator Ending()
    {
        _finalDialog.DOAnchorPosY(-70f, 0);
        _finalDialog.DOScale(0, 0);
        _camPlayer.gameObject.SetActive(false);
        _dogCam.gameObject.SetActive(true);
        _dog.quickEnd = true;
        _dog.OrderGoQuick(_quickPos);
        yield return new WaitForSeconds(2f);
        _fadeOut.DOColor(Color.black, 1f);
        _player.speed = 0;
        _player.FreezePlayer(RigidbodyConstraints.FreezeAll);
        yield return new WaitForSeconds(1f);
        _fadeOut.DOColor(new Color(0, 0, 0, 0), 1f);
        _dogCam.gameObject.SetActive(false);
        _camFocus.gameObject.SetActive(true);
        _player.gameObject.transform.position = _quickPos.position;
        _player.gameObject.transform.LookAt(gameObject.transform.position);
        yield return new WaitForSeconds(1f);
        _finalDialog.gameObject.SetActive(true);
        _finalDialog.DOScale(0.8f, 0.5f);
        yield return new WaitForSeconds(4f);
        _finalDialog.DOScale(0f, 0.5f);
        FinishQuest();
    }

    private IEnumerator EndingNormal()
    {
        _finish = true;
        _finalDialog.DOAnchorPosY(-70f, 0);
        _finalDialog.DOScale(0, 0);
        _dog.quickEnd = true;
        _dog.OrderGoQuick(_quickPos);
        //yield return new WaitForSeconds(2f);
        _fadeOut.DOColor(Color.black, 1f);
        _player.speed = 0;
        _player.FreezePlayer(RigidbodyConstraints.FreezeAll);
        yield return new WaitForSeconds(1f);
        _fadeOut.DOColor(new Color(0, 0, 0, 0), 1f);
        _camPlayer.gameObject.SetActive(false);
        _dogCam.gameObject.SetActive(false);
        _camFocus.gameObject.SetActive(true);
        _player.gameObject.transform.position = _quickPos.position;
        _player.gameObject.transform.LookAt(gameObject.transform.position);
        yield return new WaitForSeconds(1f);
        _finalDialog.gameObject.SetActive(true);
        _finalDialog.DOScale(0.8f, 0.5f);
        yield return new WaitForSeconds(4f);
        _finalDialog.DOScale(0f, 0.5f);
        FinishQuest();
    }
}