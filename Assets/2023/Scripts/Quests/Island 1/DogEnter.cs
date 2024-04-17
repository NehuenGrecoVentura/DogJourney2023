using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class DogEnter : MonoBehaviour
{
    [Header("SPAWN BROOM")]
    [SerializeField] GameObject _broomPrefab;
    [SerializeField] Transform _posSpawn;
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
    
    private Collider _myCol;
    private Manager _gm;
    private LocationQuest _radar;
    private Character _player;
    private AudioSource _myAudio;
    [SerializeField] AudioClip _soundQuick;
    
    [Header("NEXT QUEST")]
    [SerializeField] Collider _colTableQuest;
    private QuestBroom _maryNPC;
    private TableQuest _nextQuest;
    private QuestUI _questUI;
    private CameraOrbit _camPlayer;

    private void Awake()
    {
        _myAudio = GetComponent<AudioSource>();
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


        _fadeOut.color = new Color(0,0,0,0);
        _myAudio.Stop();
        _audioDog.Stop();
        _dogBroomCinematic.SetActive(false);
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
        if (Input.GetKeyDown(KeyCode.Space) && broomPicked) 
            StartCoroutine(Ending());
    }

    public void EndingQuest()
    {
        StartCoroutine(Ending());
    }

    public void ActiveNextQuest()
    {
        _camDog.gameObject.SetActive(false);
        _camPlayer.gameObject.SetActive(true);
        _dog.quickEnd = false;
        _mainCam.gameObject.SetActive(true);
        _player.speed = _player.speedAux;
        _player.FreezePlayer(RigidbodyConstraints.FreezeRotation);
        _colTableQuest.enabled = true;
        _nextQuest.enabled = true;
        _radar.target = _nextQuest.gameObject.transform;
        _gm.QuestCompleted();
        Destroy(_broomPrefab);
        Destroy(_maryNPC);
        Destroy(_endingQuestPos.transform.parent.gameObject);
        Destroy(gameObject);
    }

    private IEnumerator Message()
    {
        _myAudio.Play();


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
        _player.speed = 0;
        _player.FreezePlayer(RigidbodyConstraints.FreezeAll);

        yield return new WaitForSeconds(5f);
        _message.DOAnchorPosY(-1000f, 0f);
        _message.gameObject.SetActive(false);

        _questUI.UIStatus(true);
        _cinematic.SetActive(false);
        _mainCam.gameObject.SetActive(true);
        _player.speed = _player.speedAux;
        _player.FreezePlayer(RigidbodyConstraints.FreezeRotation);

        _iconInterct.SetActive(true);
    }

    private IEnumerator Search()
    {
        _questUI.UIStatus(false);
        _dogBroomCinematic.SetActive(true);
        _mainCam.gameObject.SetActive(false);
        _player.speed = 0;
        _player.FreezePlayer(RigidbodyConstraints.FreezeRotation);
        Destroy(_myCol);
        _dogBall.gameObject.transform.position = _enterPos.position;
        yield return new WaitForSeconds(2f);
        _audioDog.Play();
        yield return new WaitForSeconds(2f);
        _myAudio.Play();

        _message.gameObject.SetActive(true);
        _message.DOAnchorPosY(70f, 0.5f);

        _textMessage.text = _messageBroomFind;
        _dog.gameObject.SetActive(true);
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
        _player.FreezePlayer(RigidbodyConstraints.FreezeRotation);

        Destroy(_cinematic);
        _radar.target = _maryNPC.gameObject.transform;
        _questUI.TaskCompleted(1);
        _questUI.AddNewTask(2, "Returns the broom to its owner");
        broomPicked = true;
        _maryNPC.gameObject.GetComponent<BoxCollider>().enabled = true;
    }

    private IEnumerator Ending()
    {
        _audioDog.PlayOneShot(_soundQuick);
        _camDog.gameObject.SetActive(true);
        _camPlayer.gameObject.SetActive(false);
        _dog.quickEnd = true;
        _dog.OrderGoQuick(_maryNPC.gameObject.transform);
        yield return new WaitForSeconds(2f);
        _fadeOut.DOColor(Color.black, 1f);
        _player.speed = 0;
        _player.FreezePlayer(RigidbodyConstraints.FreezeAll);
        yield return new WaitForSeconds(1f);
        _fadeOut.DOColor(new Color(0,0,0,0), 1f);
        _maryNPC.ChangeController();
        _mainCam.gameObject.SetActive(false);
        _camEnding.gameObject.SetActive(true);
        _player.gameObject.transform.position = _endingQuestPos.position;
        _player.gameObject.transform.LookAt(_maryNPC.gameObject.transform);
        yield return new WaitForSeconds(1f);
        _textMessage.text = _messageWin;
        _myAudio.Play();
        //_message.transform.DOScale(1f, 0.5f);
        _message.gameObject.SetActive(true);
        _message.DOAnchorPosY(70f, 0.5f);


        yield return new WaitForSeconds(4f);
        //_message.transform.DOScale(0f, 0.5f);
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
        _dog.OrderGoQuick(_maryNPC.gameObject.transform);
        _fadeOut.DOColor(Color.black, 1f);
        _player.speed = 0;
        _player.FreezePlayer(RigidbodyConstraints.FreezeAll);
        yield return new WaitForSeconds(1f);
        _fadeOut.DOColor(new Color(0, 0, 0, 0), 1f);
        _maryNPC.ChangeController();
        _mainCam.gameObject.SetActive(false);
        _camEnding.gameObject.SetActive(true);
        _player.gameObject.transform.position = _endingQuestPos.position;
        _player.gameObject.transform.LookAt(_maryNPC.gameObject.transform);
        yield return new WaitForSeconds(1f);
        //_message.gameObject.SetActive(true);
        _textMessage.text = _messageWin;
        //_message.transform.DOScale(1f, 0.5f);


        _message.gameObject.SetActive(true);
        _message.DOAnchorPosY(70f, 0.5f);
        yield return new WaitForSeconds(4f);
        //_message.transform.DOScale(0f, 0.5f);
        _message.DOAnchorPosY(-1000f, 0f);
        _message.gameObject.SetActive(false);
        ActiveNextQuest();
    }
}