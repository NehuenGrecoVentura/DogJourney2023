using UnityEngine;

public class BoxUpgradeQuest : MonoBehaviour
{
    [SerializeField] GameObject _boxInTrolley;

    [Header("INTERACTIVE CONFIG")]
    [SerializeField] GameObject _iconInteractive;
    [SerializeField] KeyCode _buttonInteractive = KeyCode.Q;
    public bool boxPicked = false;

    [Header("UI STATUS")]
    [SerializeField] UIQuestStatus _status;
    [SerializeField] string _nextDescription;

    [Header("PLAYER CONFIG")]
    [SerializeField] Character2022 _player;

    [Header("MAILBOX CONFIG")]
    [SerializeField] Collider _colMail;
    MailQuest2 _endQuest;

    [Header("MAP CONFIG")]
    [SerializeField] LocationQuest _map;
    [SerializeField] Transform _nextLocation;

    [Header("AUDIO CONFIG")]
    [SerializeField] AudioSource _audioPlayer;
    [SerializeField] AudioSource _audioTrolley;
    [SerializeField] AudioClip _silbido;

    [Header("DOG CONFIG")]
    [SerializeField] Dog2022 _dog;

    [Header("COUNTDOWN CONFIG")]
    [SerializeField] float _countDown = 1.5f;
    public bool orderBlock = false;

    private void Awake()
    {
        _endQuest = FindObjectOfType<MailQuest2>();
    }

    void Start()
    {
        _boxInTrolley.SetActive(false);
        _iconInteractive.SetActive(false);
        _endQuest.enabled = false;
    }

    public void ActiveEndQuest()
    {
        _status.completed = true;
        _status.nextDescription = _nextDescription;
        _endQuest.enabled = true;
        _boxInTrolley.SetActive(true);
        _map.target = _nextLocation;
        _colMail.enabled = true;
        boxPicked = true;
        Destroy(_iconInteractive);
        Destroy(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character2022>();

        if (player != null)
        {
            if (!Input.GetKeyDown(_buttonInteractive))
                _iconInteractive.gameObject.SetActive(true);

            else if (Input.GetKeyDown(_buttonInteractive) && _countDown >= 1.5f && !orderBlock)
                _iconInteractive.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var dog = other.gameObject.GetComponent<Dog2022>();
        if (dog != null) ActiveEndQuest();
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character2022>();
        if (player != null) _iconInteractive.gameObject.SetActive(false);
    }
}