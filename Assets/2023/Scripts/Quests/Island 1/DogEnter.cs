using System.Collections;
using UnityEngine;
using DG.Tweening;

public class DogEnter : MonoBehaviour
{
    [SerializeField] GameObject _broomPrefab;
    [SerializeField] Transform _posSpawn;
    [SerializeField] KeyCode _keyInteract = KeyCode.Q;
    [SerializeField] Dog _dog;
    [SerializeField] Transform _enterPos;
    [SerializeField] Transform _exitPos;
    [SerializeField] GameObject _cinematic;

    private bool _firstContact = false;
    private Collider _myCol;
    private QuestUI _questUI;
    public bool broomPicked = false;
    private Manager _gm;
    private LocationQuest _radar;
    private Character _player;
    [SerializeField] Camera _mainCam;
    [SerializeField] GameObject _message;

    [Header("NEXT QUEST")]
    [SerializeField] Collider _colTableQuest;
    private QuestBroom _maryNPC;
    private TableQuest _nextQuest;

    private void Awake()
    {
        _myCol = GetComponent<Collider>();
        _questUI = FindObjectOfType<QuestUI>();
        _gm = FindObjectOfType<Manager>();
        _radar = FindObjectOfType<LocationQuest>();
        _maryNPC = FindObjectOfType<QuestBroom>();
        _nextQuest = FindObjectOfType<TableQuest>();
        _player = FindObjectOfType<Character>();
    }

    private void Start()
    {
        _message.transform.DOScale(0f, 0f);
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
            //_message.SetActive(true);
            StartCoroutine(Message());
            
            _firstContact = true;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && broomPicked)
            ActiveNextQuest();
    }

    public void ActiveNextQuest()
    {
        _colTableQuest.enabled = true;
        _nextQuest.enabled = true;
        _radar.target = _nextQuest.gameObject.transform;
        _gm.QuestCompleted();
        Destroy(_broomPrefab);
        Destroy(_maryNPC);
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
        _message.SetActive(true);
        _message.transform.DOScale(1f, 0.5f);
        PlayCinematic(true, false, 0, RigidbodyConstraints.FreezeAll, false);
        yield return new WaitForSeconds(5f);
        _message.SetActive(false);
        PlayCinematic(false, true, _player.speedAux, RigidbodyConstraints.FreezeRotation, true);
    }

    private IEnumerator Search()
    {
        PlayCinematic(true, false, 0, RigidbodyConstraints.FreezeAll, false);
        Destroy(_myCol);
        _dog.gameObject.SetActive(false);
        yield return new WaitForSeconds(5f);
        PlayCinematic(false, true, _player.speedAux, RigidbodyConstraints.FreezeRotation, true);
        Destroy(_cinematic);
        _radar.target = _maryNPC.gameObject.transform;
        _dog.gameObject.SetActive(true);
        _broomPrefab.SetActive(true);
        _dog._target.transform.position = _exitPos.position;
        _dog.OrderGo();
        _questUI.TaskCompleted(1);
        _questUI.AddNewTask(2, "Returns the broom to its owner");
        broomPicked = true;
    }
}