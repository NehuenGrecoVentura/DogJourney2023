using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QuestBroom : MonoBehaviour
{
    [SerializeField] Button _buttonConfirm;
    [SerializeField] DogEnter _dogEnter;
    [SerializeField, TextArea(4, 6)] string[] _lines;

    private Collider _col;
    private QuestUI _questUI;
    private LocationQuest _radar;
    private TableQuest _nextQuest;
    private AudioSource _myAudio;

    [SerializeField] string _nameNPC = "Mary";
    [SerializeField] Dialogue _dialogue;
    [SerializeField] GameObject _broomPrefab;
    [SerializeField] GameObject _broomPrefabDog;
    [SerializeField] RuntimeAnimatorController[] _animController;
    private Animator _myAnim;

    [Header("INTERACT")]
    [SerializeField] GameObject _iconInteract;
    [SerializeField] KeyCode _keyInteract = KeyCode.F;
    [SerializeField] Dog _dog;
    private BoxCollider _myCol;
    private bool _activeQuest = false;
    private bool _ending = false;
    private Character _player;

    private void Awake()
    {
        _myAudio = GetComponent<AudioSource>();
        _myCol = GetComponent<BoxCollider>();
        _myAnim = GetComponent<Animator>();
        _col = _dogEnter.gameObject.GetComponent<Collider>();
        _questUI = FindObjectOfType<QuestUI>();
        _radar = FindObjectOfType<LocationQuest>();
        _nextQuest = FindObjectOfType<TableQuest>();
        _player = FindObjectOfType<Character>();
    }

    private void Start()
    {
        _myAudio.Stop();
        _dialogue.canTalk = true;
        _iconInteract.SetActive(false);
        _myAnim.runtimeAnimatorController = _animController[1];
        _broomPrefab.SetActive(false);
        _col.enabled = false;
        _buttonConfirm.onClick.AddListener(() => Confirm());
        _nextQuest.enabled = false;
        _dogEnter.GetComponent<Collider>().enabled = false;

        for (int i = 0; i < _dialogue._lines.Length; i++)
            _dialogue._lines[i] = _lines[i];
    }

    public IEnumerator LookToPlayer()
    {
        yield return new WaitForSeconds(3f);
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            transform.LookAt(_player.gameObject.transform);
        }
    }

    public void Confirm()
    {
        _myAudio.Play();
        _iconInteract.SetActive(false);
        _myCol.enabled = false;
        _activeQuest = true;
        _dogEnter.enabled = true;
        _radar.target = _dogEnter.gameObject.transform;
        _dialogue.Close();
        _col.enabled = true;
        _dog.canTeletransport = false;
        _questUI.ActiveUIQuest("The Hidden Broom", "Find the lost broom", string.Empty, string.Empty);
        _buttonConfirm.gameObject.SetActive(false);
        _myAnim.SetBool("Quest", true);
        StopCoroutine(LookToPlayer());
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && _myCol.enabled)
        {
            if (Input.GetKeyDown(_keyInteract) && _dogEnter.broomPicked && !_dog.quickEnd)
            {
                SetName();
                StopCoroutine(LookToPlayer());
                _dogEnter.EndingNormal();
                _myCol.enabled = false;
                player.FreezePlayer();
                Destroy(this, 6f);
            }

            if (!_activeQuest && !_dogEnter.broomPicked && !Input.GetKeyDown(KeyCode.F) && !_ending || _activeQuest && _dogEnter.broomPicked && !_ending)
                _iconInteract.SetActive(true);
        }
    }

    public void SetName()
    {
        _dialogue.Set(_nameNPC);
    }

    public void ChangeController()
    {
        StopCoroutine(LookToPlayer());
        _broomPrefabDog.SetActive(false);
        _myAnim.runtimeAnimatorController = _animController[0];
        _broomPrefab.SetActive(true);
        _ending = true;
        Destroy(_iconInteract);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && _myCol.enabled)
        {
            _dialogue.gameObject.SetActive(true);
            _dialogue.playerInRange = true;
            _dialogue.Set(_nameNPC);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && _myCol.enabled)
        {
            if (_activeQuest || !_activeQuest) _iconInteract.SetActive(false);
            _dialogue.playerInRange = false;
        }
    }
}