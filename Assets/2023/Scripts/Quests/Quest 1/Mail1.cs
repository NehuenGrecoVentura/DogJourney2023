using UnityEngine;
using UnityEngine.UI;

public class Mail1 : MailQuest
{
    [SerializeField] GameObject[] _activeObjs;
    [SerializeField] GameObject _message;
    [SerializeField] Collider _colAxe;
    [SerializeField] GameObject _letterQuest;
    [SerializeField] GameObject _canvasQuest;

    [Header("RADAR CONFIG")]
    [SerializeField] Transform _nextPos;
    private LocationQuest _radar;

    [Header("AUDIO")]
    [SerializeField] AudioClip _soundClip;
    private AudioSource _myAudio;

    [Header("ICON QUEST UI")]
    [SerializeField] Sprite _iconQuest;
    [SerializeField] Image _imageIcon;

    private void Awake()
    {
        _myAudio = GetComponent<AudioSource>();
        _radar = FindObjectOfType<LocationQuest>();
    }

    private void Start()
    {
        StatusObjs(false);
        _letterQuest.gameObject.SetActive(false);
    }

    private void StatusObjs(bool active)
    {
        _colAxe.enabled = active;
        foreach (var obj in _activeObjs)
            obj.SetActive(active);
    }

    public void ConfirmQuest()
    {
        _imageIcon.sprite = _iconQuest;
        _radar.target = _nextPos;
        _myAudio.PlayOneShot(_soundClip);
        Character player = FindObjectOfType<Character>();
        player.FreezePlayer(RigidbodyConstraints.FreezeRotation);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Destroy(_letterQuest.gameObject);
        StatusUI(nameQuest, secondText, iconQuestActive);
        StatusObjs(true);
        _canvasQuest.SetActive(true);
        Destroy(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            Destroy(_message);
            _letterQuest.gameObject.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            player.FreezePlayer(RigidbodyConstraints.FreezeAll);
        }
    }
}