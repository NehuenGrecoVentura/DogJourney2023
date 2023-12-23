using System.Collections;
using System;
using UnityEngine;

public class HouseQuest4 : MailQuest
{
    [SerializeField] GameObject _letter;
    [SerializeField] float _time = 120f;
    [SerializeField] int _totalAmount = 20;
    public bool quest4Active = false;
    public int amount = 0;

    private CharacterInventory _inventory;
    private HouseQuest5 _nextQuest;
    private QuestManager _quest;
    private Manager _manager;
    private Gates _gates;
    private Collider _myCol;

    [Header("AUDIO")]
    [SerializeField] AudioClip _soundClip;
    [SerializeField] AudioClip _soundNotification;
    private AudioSource _myAudio;
    private bool _sound = false;

    [Header("NEXT QUEST")]
    [SerializeField] Camera _camPlayer;
    [SerializeField] Camera _camFocus;
    [SerializeField] Sprite _iconOil;
    [SerializeField] Animator _nextAnimNPC;
    [SerializeField] GameObject _broom;
    private MessageSlide _messageSlide;
    private Character _player;

    [Header("RADAR")]
    [SerializeField] Transform _npcQuest5;
    private LocationQuest _radar;

    [SerializeField] GameObject _iconInteract;

    private void Awake()
    {
        _myAudio = GetComponent<AudioSource>();
        _myCol = GetComponent<Collider>();

        _inventory = FindObjectOfType<CharacterInventory>();
        _nextQuest = FindObjectOfType<HouseQuest5>();
        _quest = FindObjectOfType<QuestManager>();
        _manager = FindObjectOfType<Manager>();
        _gates = FindObjectOfType<Gates>();
        _player = FindObjectOfType<Character>();
        _messageSlide = FindObjectOfType<MessageSlide>();
        _radar = FindObjectOfType<LocationQuest>();
    }

    private void Start()
    {
        _letter.SetActive(false);
        _camFocus.gameObject.SetActive(false);
        _iconInteract.SetActive(false);
    }

    private void Update()
    {
        if (amount == _totalAmount && _time > 0)
        {
            if (!_sound)
            {
                _myAudio.PlayOneShot(_soundNotification);
                _sound = true;
            }

            _time = 0;
            StopAllCoroutines();
            quest4Active = false;
            questsTexts[2].text = "You have won the bet";
            imageStatusPhase[0].enabled = false;
            imageStatusPhase[1].enabled = true;
            imageStatusPhase[2].enabled = false;
            imageStatusPhase[3].enabled = true;
            imageStatusPhase[4].enabled = true;
            imageStatusPhase[5].enabled = false;
            _quest.QuestStatus(true, true, true, true);
            questsTexts[3].text = "Come back to receive the oil.";
            _radar.StatusRadar(true);
            _radar.target = transform;
            _manager.PurpleTreesNormal();
            _manager.GreenTreesNormal();
        }
    }
    private IEnumerator Timer()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            _time -= Time.deltaTime;
            questsTexts[1].text = _time.ToString();

            // Convierte los segundos en un objeto TimeSpan
            TimeSpan timeSpan = TimeSpan.FromSeconds(_time);

            // Obtiene los minutos y segundos del objeto TimeSpan
            int minutes = timeSpan.Minutes;
            int seconds = timeSpan.Seconds;

            // Formatea los minutos y segundos en formato MM:SS
            string formattedTime = string.Format("{0:00}:{1:00}", minutes, seconds);

            // Actualiza el TextMeshPro con el tiempo formateado
            questsTexts[1].text = formattedTime;

            questsTexts[2].text = "Trees: " + amount.ToString() + " /" + _totalAmount.ToString();
            imageStatusPhase[2].enabled = true;
            imageStatusPhase[3].enabled = false;

            if (_time <= 0)
            {
                _time = 0;
                quest4Active = false;

                if (!_sound)
                {
                    _myAudio.PlayOneShot(_soundNotification);
                    _sound = true;
                }


                _radar.StatusRadar(true);
                _radar.target = transform;
                _manager.PurpleTreesNormal();
                _manager.GreenTreesNormal();

                if (amount != _totalAmount)
                {
                    questsTexts[2].text = "You will have to pay the bet";
                    imageStatusPhase[2].enabled = false;
                    imageStatusPhase[3].enabled = true;
                    _quest.QuestStatus(true, true, true, true);
                    questsTexts[3].text = "Come back to receive the oil.";
                }

                StopAllCoroutines();
            }
        }
    }

    public void ConfirmQuest()
    {
        _manager.GreenTreesShader();
        _manager.PurpleTreesShader();

        amount = 0;
        _myAudio.PlayOneShot(_soundClip);
        Confirm(_letter);
        quest4Active = true;
        _quest.gameObject.SetActive(true);
        _quest.QuestStatus(true, true, true, false);
        StatusUI(nameQuest, secondText + "  " + _time.ToString(), iconQuestActive);
        StartCoroutine(Timer());
    }

    public void FinishQuest()
    {
        StartCoroutine(FocusCam());
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && !quest4Active && _time > 0)
        {
            _radar.StatusRadar(false);
            _letter.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            player.FreezePlayer(RigidbodyConstraints.FreezeAll);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && !quest4Active && _time <= 0)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                _iconInteract.SetActive(false);
                Destroy(_myCol);
                FinishQuest();
            }

            else _iconInteract.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) _iconInteract.SetActive(false);
    }

    private IEnumerator FocusCam()
    {
        if (amount >= _totalAmount)
        {
            _inventory.money += 200;
            _messageSlide.ShowMessage("+1 / + $200", _iconOil);
        }

        else
        {
            _inventory.money = 0;
            _messageSlide.ShowMessage("+1 / - $", _iconOil);
        }

        _broom.SetActive(false);
        _iconInteract.SetActive(false);
        _manager.PurpleTreesNormal();
        _manager.GreenTreesNormal();
        _radar.StatusRadar(false);
        _radar.target = _npcQuest5;
        _nextAnimNPC.Play("Quest");
        _inventory.oils = 2;
        _manager.QuestCompleted();
        _quest.gameObject.SetActive(false);
        _gates.iconQuestHouses[2].SetActive(true);
        _gates.colHouses[2].enabled = true;
        _nextQuest.enabled = true;
        _player.FreezePlayer(RigidbodyConstraints.FreezeAll);
        _player.speed = 0;
        _camPlayer.gameObject.SetActive(false);
        _camFocus.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        _player.FreezePlayer(RigidbodyConstraints.None);
        _player.speed = _player.speedAux;
        _camPlayer.gameObject.SetActive(true);
        _radar.StatusRadar(true);
        _radar.target = _npcQuest5;
        Destroy(_camFocus.gameObject);
        Destroy(this);
    }
}