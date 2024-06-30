using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using DG.Tweening;

public class QuestApple : MonoBehaviour
{
    [Header("INTERACT")]
    [SerializeField] GameObject _iconInteract;
    [SerializeField] KeyCode _keyInteract = KeyCode.F;
    [SerializeField] BoxCollider _myCol;

    [Header("DIALOGS")]
    [SerializeField] Button _buttonConfirm;
    [SerializeField] TMP_Text _textDialogue;
    [SerializeField] TMP_Text _textName;
    [SerializeField, TextArea(4, 6)] string[] _linesDialogues;
    [SerializeField] string _nameNPC = "Thomas";
    [SerializeField] Dialogue _dialogue;

    [Header("ANIM")]
    [SerializeField] RuntimeAnimatorController[] _animController;
    [SerializeField] Animator _myAnim;

    [Header("AUDIO")]
    [SerializeField] AudioClip _soundConfirm;
    [SerializeField] AudioClip _soundMessage;
    [SerializeField] AudioSource _myAudio;

    [Header("QUEST")]
    [SerializeField] QuestUI _questUI;
    [SerializeField] BoxApple _boxApple;
    [SerializeField] Character _player;
    private TreeApple[] _trees;
    private bool _questActive = false;
    private bool _questCompleted = false;

    [Header("THIEFS")]
    [SerializeField] ThiefApple[] _thiefs;
    [SerializeField] float _timeToSpwnThiefs = 8f;
    [SerializeField] CinematicThieft _cinematic;
    private bool _spawnActivate = false;

    [Header("RADAR")]
    [SerializeField] LocationQuest _radar;

    [Header("FINISH")]
    [SerializeField] BoxMessages _boxMessage;
    [SerializeField] Manager _gm;
    [SerializeField, TextArea(4,6)] string _messageEnd;
    [SerializeField] CameraOrbit _camPlayer;
    [SerializeField] Camera _camEnding;
    [SerializeField] NavMeshAgent[] _agents;
    [SerializeField] Dog _dog;
    [SerializeField] QuestSearch _nextQuest;

    private void Awake()
    {
        _trees = FindObjectsOfType<TreeApple>();
    }

    private void Start()
    {
        _dialogue.gameObject.SetActive(false);
        _iconInteract.transform.DOScale(0f, 0f);
        _camEnding.gameObject.SetActive(false);

        foreach (var thief in _thiefs)
        {
            thief.gameObject.SetActive(false);
        }

        _myAnim.runtimeAnimatorController = _animController[1];
    }

    private void Update()
    {
        transform.LookAt(_player.transform);

        bool hasEnoughApples = _boxApple.totalInBox >= 2;
        bool isBoxFull = _boxApple.totalInBox >= _boxApple.total;

        if (_questActive)
        {
            _questUI.AddNewTask(1, "Collect apples from the trees (" + _boxApple.totalInBox.ToString() + "/" + _boxApple.total.ToString() + ")");

            if (isBoxFull)
            {
                _radar.target = transform;
                _radar.StatusRadar(true);
                _myAnim.SetBool("Quest", false);
                _myCol.enabled = true;
                CancelInvoke("SpawnThiefts");
                DesactivateThiefts();
                _questUI.TaskCompleted(1);
                _questUI.AddNewTask(2, "Take the box to Thomas");

                foreach (var item in _agents)
                {
                    item.enabled = true;
                }

                _dog.canTeletransport = true;
                _questCompleted = true;
                _spawnActivate = false;
                _questActive = false;
            }

            else if (!_questCompleted && !IsInvoking("SpawnThiefts") && hasEnoughApples && _spawnActivate)
                InvokeRepeating("SpawnThiefts", 0f, _timeToSpwnThiefs);

            else if (!_spawnActivate && hasEnoughApples) _cinematic.Activate();

            else if (IsInvoking("SpawnThiefts") && _boxApple.totalInBox <= 1)
                CancelInvoke("SpawnThiefts");


            else if (_boxApple.totalInBox <= 0) _boxApple.totalInBox = 0;
        }
    }

    public void SpawnActive()
    {
        _spawnActivate = true;
    }

    private void Confirm()
    {
        _myCol.enabled = false;
        _iconInteract.transform.DOScale(0f, 0.5f);
        _dialogue.canTalk = false;
        _dialogue.Close();
        _questUI.ActiveUIQuest("The Great Harvest", "Collect apples from the trees", string.Empty, string.Empty);

        foreach (var tree in _trees)
        {
            tree.enabled = true;
            tree.GetComponent<BoxCollider>().enabled = true;
        }

        _myAnim.SetBool("Quest", true);
        _myAudio.PlayOneShot(_soundConfirm);
        _radar.StatusRadar(false);
        _questActive = true;
    }

    private void SetDialogue()
    {
        _textName.text = _nameNPC;
        _iconInteract.transform.DOScale(0.01f, 0.5f);
        _buttonConfirm.onClick.AddListener(() => Confirm());

        if (!_questActive)
        {
            for (int i = 0; i < _dialogue._lines.Length; i++)
                _dialogue._lines[i] = _linesDialogues[i];
        }

        _dialogue.gameObject.SetActive(true);
        _dialogue.playerInRange = true;
        _dialogue.canTalk = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            if (_myCol.enabled && !_questActive && !_questCompleted) SetDialogue();
            else if (_questCompleted) _iconInteract.transform.DOScale(0.01f, 0.5f);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && _questCompleted && Input.GetKeyDown(_keyInteract))
            StartCoroutine(Ending(player));
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            _dialogue.playerInRange = false;
            _iconInteract.transform.DOScale(0f, 0.5f);
        }
    }

    private void SpawnThiefts()
    {
        int activeEnemyCount = 0;
        foreach (var thief in _thiefs)
        {
            if (thief.gameObject.activeSelf)
            {
                activeEnemyCount++;
            }
        }

        if (activeEnemyCount < 3)
        {
            foreach (var thief in _thiefs)
            {
                if (!thief.gameObject.activeSelf)
                {
                    thief.gameObject.SetActive(true);
                    break;
                }
            }
        }
    }

    private void DesactivateThiefts()
    {
        foreach (var item in _thiefs)
        {
            item.gameObject.SetActive(false);
        }
    }

    private IEnumerator Ending(Character player)
    {
        player.rabbitPicked = false;
        player.ItemsPicked(false, false, false, false);
        player.transform.LookAt(transform);
        player.FreezePlayer();
        _camPlayer.gameObject.SetActive(false);
        _camEnding.gameObject.SetActive(true);
        Destroy(_myCol);
        _iconInteract.transform.DOScale(0f, 0.5f);
        _boxMessage.SetMessage(_nameNPC);
        _myAnim.SetBool("Quest", true);

        yield return new WaitForSeconds(1f);
        _boxMessage.ShowMessage(_messageEnd);
        _myAudio.PlayOneShot(_soundMessage);

        yield return new WaitForSeconds(3f);
        _boxMessage.CloseMessage();

        yield return new WaitForSeconds(0.5f);
        _camPlayer.gameObject.SetActive(true);

        _radar.target = _nextQuest.transform;
        _nextQuest.enabled = true;
        _nextQuest.GetComponent<BoxCollider>().enabled = true;

        Destroy(_camEnding.gameObject);
        _boxMessage.DesactivateMessage();
        player.DeFreezePlayer();
        _gm.QuestCompleted();

        foreach (var item in _agents)
        {
            item.enabled = true;
        }

        _dog.canTeletransport = true;


        foreach (var tree in _trees)
        {
            tree.enabled = false;
            tree.GetComponent<BoxCollider>().enabled = false;
        }
        Destroy(this);
    }

    public void CheatSkip()
    {
        _radar.StatusRadar(true);
        _radar.target = _nextQuest.transform;
        
        _nextQuest.enabled = true;
        _nextQuest.GetComponent<BoxCollider>().enabled = true;

        foreach (var item in _thiefs)
        {
            Destroy(item.gameObject);
        }

        Destroy(_cinematic.gameObject);
        Destroy(_camEnding.gameObject);
        Destroy(_myCol);
        Destroy(_iconInteract.gameObject);
        Destroy(this);
    }
}