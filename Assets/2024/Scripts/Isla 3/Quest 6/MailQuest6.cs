using UnityEngine;

public class MailQuest6 : MonoBehaviour
{
    [SerializeField] GameObject _canvasMailQuest6;
    [SerializeField] GameObject _canvasQuest6;
    [SerializeField] GameObject _iconQuest;
    [SerializeField] KeyCode _buttonInteractive = KeyCode.Return;

    [SerializeField] AudioClip _soundAccept;
    private AudioSource _myAudio;

    private bool _isReading = false;
    private Quest6 _quest6;
    private Pause _pause;

    private void Awake()
    {
        _quest6 = GetComponent<Quest6>();
        _myAudio = GetComponent<AudioSource>();
        _pause = FindObjectOfType<Pause>();
    }

    void Start()
    {
        _quest6.enabled = false;
        _isReading = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(_buttonInteractive) && _isReading)
        {
            _pause.Defrize();
            _myAudio.PlayOneShot(_soundAccept);
            Destroy(_canvasMailQuest6);
            _canvasQuest6.SetActive(true);
            _quest6.enabled = true;
            Destroy(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            _isReading = true;
            _canvasMailQuest6.SetActive(true);
            Destroy(_iconQuest);
            _pause.Freeze();
        }
    }
}