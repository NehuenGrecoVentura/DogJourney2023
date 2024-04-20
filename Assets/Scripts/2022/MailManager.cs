using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MailManager : MonoBehaviour
{
    [SerializeField] Image _mail;
    [SerializeField] Image _iconBox;
    [SerializeField] GameObject _iconInteractive;

    [Header("OBJECTS TO DESTROY")]
    [SerializeField] GameObject[] _objToDestroy;

    [Header("BUTTON CONFIG")]
    public KeyCode buttonAccept = KeyCode.Return;
    [SerializeField] Button _buttonRope;

    [Header("SOUND CONFIG")]
    [SerializeField] AudioClip _soundConfirm;
    AudioSource _myAudio;

    [Header("MAP CONFIG")]
    [SerializeField] Transform _nextLocation;
    [SerializeField] LocationQuest _map;

    [Header("TEXTS CONFIG")]
    [SerializeField] TMP_Text _text;
    [SerializeField] string _nextDescription;

    [Header("REFERENCES CONFIG")]
    [SerializeField] Character2022 _player;
    [SerializeField] GameObject _nextQuest;
    Collider _col;
    Pause _pause;
    GoToQuest3 _goToQuest3;

    private void Awake()
    {
        _myAudio = GetComponent<AudioSource>();
        _col = GetComponent<Collider>();
        _pause = FindObjectOfType<Pause>();
        _goToQuest3 = FindObjectOfType<GoToQuest3>();
    }

    void Start()
    {
        StartDesactivated();
    }

    void StartDesactivated()
    {
        _iconInteractive.SetActive(false);
        _text.gameObject.SetActive(false);
        _mail.gameObject.SetActive(false);
        _iconBox.gameObject.SetActive(false);
        _nextQuest.gameObject.SetActive(false);
        _buttonRope.enabled = false;
        _goToQuest3.gameObject.SetActive(false);
    }

    public void ConfirmQuest()
    {
        if (Input.GetKeyDown(buttonAccept))
        {
            _pause.Defrize();
            _col.enabled = false;
            _map.target = _nextLocation;    
            _player.speed = 10f;
            _myAudio.PlayOneShot(_soundConfirm);
            _text.gameObject.SetActive(true);
            _text.text = _nextDescription;
            foreach (var obj in _objToDestroy) Destroy(obj.gameObject);
            _iconBox.gameObject.SetActive(true);
            Destroy(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character2022>();
        if (player != null)
        {
            _pause.Freeze();
            if (!Input.GetKeyDown(buttonAccept))
            {
                _pause.Freeze();
                _player.speed = 0;
                _player.EjecuteAnim("Idle");
                _mail.gameObject.SetActive(true);
            }

            else ConfirmQuest();
        }
    }
}