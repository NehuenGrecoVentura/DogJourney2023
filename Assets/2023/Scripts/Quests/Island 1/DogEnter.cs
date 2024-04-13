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

    [Header("CAMS")]
    [SerializeField] Camera _mainCam;
    [SerializeField] Camera _camEnding;

    [Header("ENTER/EXIT")]
    [SerializeField] Transform _enterPos;
    [SerializeField] Transform _exitPos;
    [SerializeField] Transform _endingQuestPos;

    [Header("MESSAGE")]
    [SerializeField] GameObject _cinematic;
    [SerializeField] GameObject _message;
    [SerializeField] string _messageBroomFind;
    [SerializeField] string _tutorialEnterDog;
    [SerializeField] TMP_Text _textMessage;
    [SerializeField] TMP_Text _textName;
    [SerializeField] Image _fadeOut;
    [SerializeField] Image _iconMessage;

    [Header("DOG")]
    [SerializeField] Dog _dog;
    [SerializeField] Camera _camDog;
    
    private Collider _myCol;
    private Manager _gm;
    private LocationQuest _radar;
    private Character _player;
    
    [Header("NEXT QUEST")]
    [SerializeField] Collider _colTableQuest;
    private QuestBroom _maryNPC;
    private TableQuest _nextQuest;
    private QuestUI _questUI;
    private CameraOrbit _camPlayer;

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
        _message.transform.DOScale(0f, 0f);
        _fadeOut.color = new Color(0,0,0,0);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        var dog = other.GetComponent<Dog>();

        if (dog != null)
        {
            print("EL PERRO EST� BUSCANDO DENTRO");
            StartCoroutine(Search());
        }

        if (player != null && !_firstContact)
        {
            StartCoroutine(Message());
            _firstContact = true;
        }
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

    private void PlayCinematic(bool playCinematic, bool mainCam, float speedPlayer, RigidbodyConstraints rb, bool questUI)
    {
        _questUI.UIStatus(questUI);
        _cinematic.SetActive(playCinematic);
        _mainCam.gameObject.SetActive(mainCam);
        _player.speed = speedPlayer;
        _player.FreezePlayer(rb);
    }

    private IEnumerator Message()
    {
        _message.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 125f);
        _textMessage.rectTransform.anchoredPosition = new Vector2(2.3927f, _textMessage.rectTransform.anchoredPosition.y);
        _textMessage.rectTransform.sizeDelta = new Vector2(1302.908f, _textMessage.rectTransform.sizeDelta.y);
        _textMessage.alignment = TextAlignmentOptions.TopLeft;
        _textName.text = "TIP";
        _iconMessage.gameObject.SetActive(false);
        _message.SetActive(true);
        _textMessage.text = _tutorialEnterDog;
        _message.transform.DOScale(1f, 0.5f);
        
        PlayCinematic(true, false, 0, RigidbodyConstraints.FreezeAll, false);
        yield return new WaitForSeconds(5f);
        _iconInterct.SetActive(true);
        _message.transform.DOScale(0, 0.5f);
        _message.SetActive(false);
        PlayCinematic(false, true, _player.speedAux, RigidbodyConstraints.FreezeRotation, true);
    }

    private IEnumerator Search()
    {
        PlayCinematic(true, false, 0, RigidbodyConstraints.FreezeAll, false);
        Destroy(_myCol);
        _dog.gameObject.SetActive(false);
        yield return new WaitForSeconds(5f);
        _message.SetActive(true);
        _message.transform.DOScale(1f, 0.5f);
        _textMessage.text = _messageBroomFind;
        _dog.gameObject.SetActive(true);
        _broomPrefab.SetActive(true);
        _dog._target.transform.position = _exitPos.position;
        _dog.OrderGo();
        yield return new WaitForSeconds(3f);
        _message.transform.DOScale(0, 0.5f);
        _message.SetActive(false);
        PlayCinematic(false, true, _player.speedAux, RigidbodyConstraints.FreezeRotation, true);
        Destroy(_cinematic);
        _radar.target = _maryNPC.gameObject.transform;
        _questUI.TaskCompleted(1);
        _questUI.AddNewTask(2, "Returns the broom to its owner");
        broomPicked = true;
    }

    private IEnumerator Ending()
    {
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
        _mainCam.gameObject.SetActive(false);
        _camEnding.gameObject.SetActive(true);
        _player.gameObject.transform.position = _endingQuestPos.position;
        _player.gameObject.transform.LookAt(_maryNPC.gameObject.transform);
        yield return new WaitForSeconds(5f);
        ActiveNextQuest();
    }
}