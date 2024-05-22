using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class QuestSearch : MonoBehaviour
{
    [Header("INTERACT")]
    [SerializeField] GameObject _iconInteract;
    [SerializeField] KeyCode _keyInteract;
    [SerializeField] Collider _myCol;

    [Header("DIALOGUE")]
    [SerializeField] Dialogue _dialogue;
    [SerializeField, TextArea(4, 6)] string[] _linesDialogues;
    [SerializeField] Button _buttonConfirm;

    [Header("CINEMATIC")]
    [SerializeField] GameObject _cinematic;
    [SerializeField] CameraOrbit _camPlayer;
    [SerializeField] DogBall _dogBall;
    [SerializeField] Transform _posDogBall;
    [SerializeField] Transform _introPosDog;
    [SerializeField] Dog _dog;
    [SerializeField] TrolleyWood _trolley;
    [SerializeField] Camera _focusDog;

    [Header("QUEST")]
    [SerializeField] string _nameNPC = "Christine";
    [SerializeField] QuestUI _questUI;
    [SerializeField] int _total = 4;
    [SerializeField] int _found = 0;
    [SerializeField] Character _player;
    [SerializeField] ItemFound _item;
    private bool _questActive = false;
    private bool _questCompleted = false;

    [Header("AUDIOS")]
    [SerializeField] AudioSource _myAudio;
    [SerializeField] AudioClip _soundConfirm;
    [SerializeField] AudioClip _soundMessage;

    [Header("MESSAGE")]
    [SerializeField] BoxMessages _boxMessage;
    [SerializeField, TextArea(4, 6)] string[] _messages;

    [Header("RADAR")]
    [SerializeField] LocationQuest _radar;

    [Header("FINISH")]
    [SerializeField] Camera _camFocus;
    [SerializeField] Manager _gm;

    void Start()
    {
        _dialogue.gameObject.SetActive(false);
        _iconInteract.SetActive(false);
        _cinematic.SetActive(false);
        _focusDog.gameObject.SetActive(false);
        _camFocus.gameObject.SetActive(false);
    }

    private void Confirm()
    {
        _myCol.enabled = false;
        _dialogue.canTalk = false;
        _dialogue.Close();
        _iconInteract.SetActive(false);
        _myAudio.PlayOneShot(_soundConfirm);
        _radar.StatusRadar(false);
        _questUI.ActiveUIQuest("Hunting Treasures", "Find buried objects", string.Empty, string.Empty);
        _questUI.AddNewTask(1, "Find buried objects (" + _found.ToString() + "/" + _total.ToString() + ")");
        StartCoroutine(StartQuest());
        _questActive = true;
    }

    private void SetDialogue()
    {
        _iconInteract.SetActive(true);
        _buttonConfirm.onClick.AddListener(() => Confirm());

        if (!_questActive)
        {
            for (int i = 0; i < _dialogue._lines.Length; i++)
                _dialogue._lines[i] = _linesDialogues[i];
        }

        _dialogue.gameObject.SetActive(true);
        _dialogue.Set(_nameNPC);
        _dialogue.playerInRange = true;
        _dialogue.canTalk = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            if (_myCol.enabled && !_questActive && !_questCompleted) SetDialogue();
            else if (_questCompleted) _iconInteract.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && _questCompleted && Input.GetKeyDown(_keyInteract))
            StartCoroutine(Finish(player));
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            _dialogue.playerInRange = false;
            _iconInteract.SetActive(false);
        }
    }

    private IEnumerator StartQuest()
    {
        _questActive = true;

        _questUI.UIStatus(false);
        _boxMessage.SetMessage(name);
        _dogBall.transform.position = _introPosDog.position;

        _camPlayer.gameObject.SetActive(false);
        _focusDog.gameObject.SetActive(true);

        NavMeshAgent agentDog = _dog.GetComponent<NavMeshAgent>();
        NavMeshAgent agentTrolley = _dog.GetComponent<NavMeshAgent>();
        Animator animDog = _dog.GetComponentInParent<Animator>();

        agentDog.enabled = false;
        agentTrolley.enabled = false;
        _dog.canTeletransport = false;
        _dog.transform.parent.transform.position = _introPosDog.position;
        _trolley.transform.position = _dog.transform.position;

        yield return new WaitForSeconds(2f);
        _cinematic.SetActive(true);
        Destroy(_focusDog.gameObject);
        _dogBall.transform.position = _posDogBall.position;
        _dog.transform.parent.transform.position = _posDogBall.position;
        _dog.Search();

        yield return new WaitForSeconds(3f);
        _boxMessage.ShowMessage(_messages[0]);
        animDog.enabled = false;
        yield return new WaitForSeconds(0.1f);
        animDog.enabled = true;
        _dog.transform.parent.LookAt(_player.transform);

        yield return new WaitForSeconds(2f);
        Destroy(_cinematic);
        _camPlayer.gameObject.SetActive(true);
        _boxMessage.CloseMessage();
        _questUI.UIStatus(true);
        _dog.canTeletransport = true;
        agentDog.enabled = true;
        agentTrolley.enabled = true;

        yield return new WaitForSeconds(1f);
        _boxMessage.DesactivateMessage();
    }

    private IEnumerator Finish(Character player)
    {
        _myCol.enabled = false;
        _iconInteract.SetActive(false);

        _boxMessage.SetMessage(_nameNPC);
        player.FreezePlayer();

        _camFocus.gameObject.SetActive(true);
        _camPlayer.gameObject.SetActive(false);

        yield return new WaitForSeconds(1f);
        _boxMessage.ShowMessage(_messages[2]);

        yield return new WaitForSeconds(3f);
        _boxMessage.CloseMessage();
        Destroy(_camFocus.gameObject);
        _camPlayer.gameObject.SetActive(true);
        player.DeFreezePlayer();
        _gm.QuestCompleted();

        yield return new WaitForSeconds(0.5f);
        _boxMessage.DesactivateMessage();
        Destroy(this);
    }

    public void AddFound(GameObject item)
    {
        if (_found < _total)
        {
            _found++;
            _questUI.AddNewTask(1, "Find buried objects (" + _found.ToString() + "/" + _total.ToString() + ")");
        }
             
        else
        {
            Destroy(_item.transform.parent.gameObject);
            _questUI.TaskCompleted(1);
            _questUI.AddNewTask(2, "Go back and show him all the items found");
            _radar.target = transform;
            _radar.StatusRadar(true);
            Destroy(item);
            _questCompleted = true;

        }
    }
}