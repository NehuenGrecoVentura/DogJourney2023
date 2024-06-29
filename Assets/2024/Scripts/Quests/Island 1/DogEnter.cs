using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class DogEnter : MonoBehaviour
{
    [Header("SPAWN BROOM")]
    [SerializeField] GameObject _broomPrefab;
    public bool broomPicked = false;

    [Header("INTERACT")]
    [SerializeField] KeyCode _keyInteract = KeyCode.Q;
    [SerializeField] GameObject _iconInterct;
    private bool _firstContact = false;
    private bool _secondContact = false;

    [Header("CAMS")]
    [SerializeField] Camera _mainCam;
    [SerializeField] Camera _camEnding;

    [Header("ENTER/EXIT")]
    [SerializeField] Transform _enterPos;
    [SerializeField] Transform _exitPos;
    [SerializeField] Transform _endingQuestPos;
    [SerializeField] ParticleSystem _searchParticle;

    [Header("MESSAGE")]
    [SerializeField] GameObject _cinematic;
    [SerializeField] RectTransform _message;
    [SerializeField] string _messageBroomFind;
    [SerializeField] string _tutorialEnterDog;
    [SerializeField] string _messageWin;
    [SerializeField] TMP_Text _textMessage;
    [SerializeField] TMP_Text _textName;
    [SerializeField] Image _fadeOut;
    [SerializeField] Image _iconMessage;

    [Header("DOG")]
    [SerializeField] Dog _dog;
    [SerializeField] Camera _camDog;
    [SerializeField] AudioSource _audioDog;
    [SerializeField] DogBall _dogBall;
    [SerializeField] GameObject _dogBroomCinematic;
    [SerializeField] Animator _animDog;
    
    private Collider _myCol;
    private Manager _gm;
    private LocationQuest _radar;
    private Character _player;
    
    [SerializeField] AudioClip _soundQuick;
    
    [Header("NEXT QUEST")]
    [SerializeField] Collider _colTableQuest;
    private QuestBroom _maryNPC;
    private TableQuest _nextQuest;
    private QuestUI _questUI;
    private CameraOrbit _camPlayer;
    private bool _canQuick = false;

    [Header("MY AUDIO")]
    [SerializeField] AudioSource _myAudio;
    [SerializeField] AudioClip _messageSound;
    [SerializeField] AudioClip _searchSound;

    private void Awake()
    {
        _myCol = GetComponent<Collider>();
        _questUI = FindObjectOfType<QuestUI>();
        _gm = FindObjectOfType<Manager>();
        _radar = FindObjectOfType<LocationQuest>();
        _maryNPC = FindObjectOfType<QuestBroom>();
        _nextQuest = FindObjectOfType<TableQuest>();
        _player = FindObjectOfType<Character>();
        _camPlayer = FindObjectOfType<CameraOrbit>();
    }

    private void Start()
    {
        _message.DOAnchorPosY(-1000f, 0f);
        _message.gameObject.SetActive(false);


        _fadeOut.color = new Color(0, 0, 0, 0);
        _myAudio.Stop();
        _audioDog.Stop();
        _dogBroomCinematic.SetActive(false);

        _searchParticle.Stop();
        _searchParticle.playbackSpeed = 4f;
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        var dog = other.GetComponent<Dog>();

        if (dog != null)
        {
            print("EL PERRO ESTÁ BUSCANDO DENTRO");
            StartCoroutine(Search());
        }

        if (player != null && !_firstContact)
        {
            StartCoroutine(Message());
            _firstContact = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && _firstContact && !_secondContact)
        {
            if (Input.GetKeyDown(_keyInteract))
            {
                _iconInterct.SetActive(false);
                _secondContact = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) _iconInterct.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && broomPicked && _canQuick) 
            StartCoroutine(Ending());
    }

    public void EndingQuest()
    {
        StartCoroutine(Ending());
    }

    public void ActiveNextQuest()
    {
        _dog.canTeletransport = true;

        _camDog.gameObject.SetActive(false);
        _camPlayer.gameObject.SetActive(true);

        _dog.quickEnd = false;
        _mainCam.gameObject.SetActive(true);
        
        _player.enabled = true;
        _player.DeFreezePlayer();

        _colTableQuest.enabled = true;
        _nextQuest.enabled = true;
        _radar.target = _nextQuest.gameObject.transform;
        _gm.QuestCompleted();

        Destroy(_maryNPC);
        Destroy(_endingQuestPos.transform.parent.gameObject);
        Destroy(gameObject);
    }

    private IEnumerator Message()
    {
        _myAudio.PlayOneShot(_messageSound);
        _message.anchoredPosition = new Vector2(0f, 125f);
        _textMessage.rectTransform.anchoredPosition = new Vector2(0.2341f, _textMessage.rectTransform.anchoredPosition.y);
        _textMessage.rectTransform.sizeDelta = new Vector2(1030.737f, _textMessage.rectTransform.sizeDelta.y);
        _textMessage.fontSize = 40;
        _textMessage.alignment = TextAlignmentOptions.TopLeft;
        _textName.text = "TIP";
        _iconMessage.gameObject.SetActive(false);
        _textMessage.text = _tutorialEnterDog;

        _message.DOAnchorPosY(-1000f, 0f);
        yield return new WaitForSeconds(0.1f);
        _message.localScale = new Vector3(1, 1, 1);
        _message.gameObject.SetActive(true);
        _message.DOAnchorPosY(70f, 0.5f);
        
        _questUI.UIStatus(false);
        _cinematic.SetActive(true);
        _mainCam.gameObject.SetActive(false);
        _player.FreezePlayer();

        yield return new WaitForSeconds(5f);
        _message.DOAnchorPosY(-1000f, 0f);
        _message.gameObject.SetActive(false);
        _questUI.UIStatus(true);
        
        _cinematic.SetActive(false);
        _mainCam.gameObject.SetActive(true);

        _player.DeFreezePlayer();
        _iconInterct.SetActive(true);
        _dog.canTeletransport = false;
    }

    private IEnumerator Search()
    {
        _questUI.UIStatus(false);
        _dogBroomCinematic.SetActive(true);
        _mainCam.gameObject.SetActive(false);
        _player.DeFreezePlayer();
        Destroy(_myCol);
        _dogBall.gameObject.transform.position = _enterPos.position;

        yield return new WaitForSeconds(1.5f);
        _audioDog.Play();
        _dog.Search();
        _searchParticle.Play();
        _myAudio.PlayOneShot(_searchSound);

        yield return new WaitForSeconds(2f);
        _animDog.enabled = false;
        yield return new WaitForSeconds(0.1f);
        _animDog.enabled = true;

        _myAudio.Stop();
        _myAudio.PlayOneShot(_messageSound);
        _searchParticle.Stop();
        
        _message.gameObject.SetActive(true);
        _message.DOAnchorPosY(70f, 0.5f);

        _textMessage.text = _messageBroomFind;
        _broomPrefab.SetActive(true);
        _dog._target.transform.position = _exitPos.position;
        _dog.OrderGo();
        yield return new WaitForSeconds(3f);
        _message.DOAnchorPosY(-1000f, 0f);
        _message.gameObject.SetActive(false);

        _questUI.UIStatus(true);
        Destroy(_dogBroomCinematic);
        _mainCam.gameObject.SetActive(true);
        _player.speed = _player.speedAux;
        _player.DeFreezePlayer();

        Destroy(_cinematic);
        _radar.target = _maryNPC.gameObject.transform;
        _questUI.TaskCompleted(1);
        _questUI.AddNewTask(2, "Returns the broom to its owner");
        broomPicked = true;
        _canQuick = true;
        _maryNPC.gameObject.GetComponent<BoxCollider>().enabled = true;
    }

    private IEnumerator Ending()
    {
        _canQuick = false;
        _audioDog.PlayOneShot(_soundQuick);
        _camDog.gameObject.SetActive(true);
        _camPlayer.gameObject.SetActive(false);
        _dog.quickEnd = true;
        _dog.OrderGoQuick(_maryNPC.gameObject.transform);
        Destroy(_maryNPC.gameObject.GetComponent<BoxCollider>());

        yield return new WaitForSeconds(2f);
        _fadeOut.DOColor(Color.black, 1f);
        _player.FreezePlayer();
        
        yield return new WaitForSeconds(1f);
        _fadeOut.DOColor(new Color(0,0,0,0), 1f);
        Destroy(_broomPrefab);
        _maryNPC.ChangeController();
        _mainCam.gameObject.SetActive(false);
        _camEnding.gameObject.SetActive(true);
        _player.gameObject.transform.position = _endingQuestPos.position;
        _player.gameObject.transform.LookAt(_maryNPC.gameObject.transform);
        

        yield return new WaitForSeconds(1f);
        _textMessage.text = _messageWin;
        _maryNPC.SetName();
        _myAudio.PlayOneShot(_messageSound);
        _message.gameObject.SetActive(true);
        _message.DOAnchorPosY(70f, 0.5f);

        yield return new WaitForSeconds(4f);
        _message.DOAnchorPosY(-1000f, 0f);
        _message.gameObject.SetActive(false);

        ActiveNextQuest();
    }


    public void EndingNormal()
    {
        StartCoroutine(EndingNormalCoroutine());
    }

    private IEnumerator EndingNormalCoroutine()
    {
        _dog.quickEnd = true;
        _canQuick = false;
        _dog.OrderGoQuick(_maryNPC.gameObject.transform);
        _fadeOut.DOColor(Color.black, 1f);

        _player.FreezePlayer();
        _player.enabled = false;

        yield return new WaitForSeconds(1f);
        _fadeOut.DOColor(new Color(0, 0, 0, 0), 1f);
        Destroy(_broomPrefab);
        _maryNPC.ChangeController();
        _mainCam.gameObject.SetActive(false);
        _camEnding.gameObject.SetActive(true);
        _player.gameObject.transform.position = _endingQuestPos.position;
        _player.gameObject.transform.LookAt(_maryNPC.gameObject.transform);

        yield return new WaitForSeconds(1f);
        _maryNPC.SetName();
        _textMessage.text = _messageWin;
        _message.gameObject.SetActive(true);
        _message.DOAnchorPosY(70f, 0.5f);

        yield return new WaitForSeconds(4f);
        _message.DOAnchorPosY(-1000f, 0f);
        _message.gameObject.SetActive(false);
        ActiveNextQuest();
    }
}