using UnityEngine;
using System.Collections;
using DG.Tweening;

public class BoxWoods : MonoBehaviour
{
    [Header("INTERACTIVE CONFIG")]
    [SerializeField] GameObject _iconInteractive;
    [SerializeField] KeyCode _keyInteractive = KeyCode.F;
    [SerializeField] Sprite _iconTAB;
    [SerializeField] string _messageSlideText;
    private MessageSlide _messageSlide;

    [Header("CINEMATIC")]
    [SerializeField] float _timeCinematic = 3f;
    [SerializeField] Animator _animTruck;
    [SerializeField] Camera _camPlayer;
    [SerializeField] Camera _camCinematic;
    [SerializeField] GameObject _cinematic;
    [SerializeField] GameObject _canvasStatusQuest;
    [SerializeField] GameObject[] _canvasQuests;
    [SerializeField] AudioSource _truckSource;
    [SerializeField] GameObject[] _wheels;
    [SerializeField] float _speedWheels = 500f;
    private Barriel _barriel;
    [SerializeField] private bool _play = false;

    [Header("NEXT QUEST")]
    [SerializeField] int _rewardMoney = 100;
    [SerializeField] GameObject _iconQuest2Mail;
    [SerializeField] Animator[] _animGates;
    [SerializeField] AudioClip _soundNotification;
    private AudioSource _myAudio;

    [Header("RADAR")]
    [SerializeField] Transform _nextPos;
    private LocationQuest _radar;

    [Header("BOX CONFIG")]
    [SerializeField] GameObject _arrow;
    [SerializeField] GameObject _box;

    private QuestUI _questUI;
    private CharacterInventory _inventory;
    private Character _player;
    private Manager _gameManager;

    private void Awake()
    {
        _myAudio = GetComponent<AudioSource>();

        _questUI = FindObjectOfType<QuestUI>();
        _inventory = FindObjectOfType<CharacterInventory>();
        _messageSlide = FindObjectOfType<MessageSlide>();
        _gameManager = FindObjectOfType<Manager>();
        _radar = FindObjectOfType<LocationQuest>();
        _player = FindObjectOfType<Character>();
        _barriel = FindObjectOfType<Barriel>();
    }

    private void Start()
    {
        _truckSource.Stop();
        _iconInteractive.SetActive(false);
        _iconInteractive.transform.localScale = new Vector3(0, 0, 0);
        _arrow.SetActive(false);
        _animTruck.enabled = false;
        _cinematic.SetActive(false);
        _camCinematic.gameObject.SetActive(false);

        foreach (var anim in _animGates)
            anim.enabled = false;
    }

    private void Update()
    {
        if (_play)
        {
            foreach (GameObject rueda in _wheels)
            {
                rueda.transform.Rotate(Vector3.forward * _speedWheels * Time.deltaTime);
            }
        }
    }

    public void FinishQuest()
    {
        _messageSlide.ShowMessage(_messageSlideText, _iconTAB);
        _radar.StatusRadar(true);
        _radar.target = _nextPos;
        Destroy(_animTruck.gameObject);
        Destroy(_camCinematic.gameObject);
        _camPlayer.gameObject.SetActive(true);
        _player.FreezePlayer(RigidbodyConstraints.FreezeRotation);
        _player.speed = _player.speedAux;
        
        foreach (var item in _canvasQuests)
            item.gameObject.SetActive(false);

        foreach (var anim in _animGates)
            anim.enabled = true;

        _gameManager.QuestCompleted();
        _iconQuest2Mail.SetActive(true);

        _inventory.money += _rewardMoney;
        Destroy(_arrow);
        Destroy(_iconInteractive);
        Destroy(gameObject);
    }

    private void PlayCinematic()
    {
        _questUI.UIStatus(false);
        _box.gameObject.SetActive(false);
        _myAudio.PlayOneShot(_soundNotification);
        _radar.StatusRadar(false);
        _canvasStatusQuest.SetActive(false);
        _camPlayer.gameObject.SetActive(false);
        _camCinematic.gameObject.SetActive(true);
        _cinematic.SetActive(true);
        _animTruck.enabled = true;
        _arrow.SetActive(false);
        _iconInteractive.SetActive(false);
        _player.FreezePlayer(RigidbodyConstraints.FreezePosition);
        _player.speed = 0;
        _barriel.UpBarriel(_timeCinematic);
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();

        if (player != null)
        {
            if (_inventory.greenTrees >= 5)
            {
                if (Input.GetKeyDown(_keyInteractive))
                    StartCoroutine(RunTruck());

                else
                {
                    _iconInteractive.SetActive(true);
                    _iconInteractive.transform.DOScale(1f, 0.5f);
                }     
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) _iconInteractive.transform.DOScale(0f, 0.5f);
    }

    private IEnumerator RunTruck()
    {
        _play = true;
        _truckSource.Play();
        _inventory.greenTrees -= 10;
        if (_inventory.greenTrees < 0) _inventory.greenTrees = 0;
        _iconInteractive.SetActive(false);
        PlayCinematic();
        yield return new WaitForSeconds(_timeCinematic);
        FinishQuest();
    }
}