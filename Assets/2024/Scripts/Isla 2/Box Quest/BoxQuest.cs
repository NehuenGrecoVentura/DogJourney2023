using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BoxQuest : MonoBehaviour
{
    [Header("INTERACT")]
    [SerializeField] GameObject _iconInteract;
    [SerializeField] KeyCode _keyInteract = KeyCode.F;
    [SerializeField] Collider _myCol;

    [Header("DIALOGUE")]
    [SerializeField] Dialogue _dialogue;
    [SerializeField, TextArea(4, 6)] string[] _linesDialogues;
    [SerializeField] string _nameNPC = "Albert";
    [SerializeField] Button _buttonConfirm;

    [Header("MESSAGE")]
    [SerializeField] BoxMessages _boxMessage;
    [SerializeField, TextArea(4,6)] string _message;
    [SerializeField] Image _fadeOut;

    [Header("RADAR")]
    [SerializeField] LocationQuest _radar;
    [SerializeField] BoxQuest2 _boxQuestPos;

    [Header("CAMERAS")]
    [SerializeField] Camera _camFocus;
    [SerializeField] Camera _dogCam;
    [SerializeField] CameraOrbit _camPlayer;

    [Header("QUEST")]
    [SerializeField] QuestUI _questUI;
    [SerializeField] CharacterInventory _inventory;
    [SerializeField] Dog _dog;
    [SerializeField] Character _player;
    private bool _questActive = false;
    private bool _questCompleted = false;
    private bool _canQuick = false;

    [Header("FINISH")]
    [SerializeField] Animator _myAnim;
    [SerializeField] GameObject _myBroom;
    [SerializeField] Manager _gm;
    [SerializeField] QuestApple _nextQuest;
    [SerializeField] Transform _posEnd;

    [Header("AUDIO")]
    [SerializeField] AudioClip _soundMessage;
    [SerializeField] AudioSource _myAudio;

    void Start()
    {
        _dialogue.gameObject.SetActive(false);
        _iconInteract.SetActive(false);
        _myBroom.SetActive(false);
        _camFocus.gameObject.SetActive(false);
        _dogCam.gameObject.SetActive(false);
        _fadeOut.color = new Color(0, 0, 0, 0);
    }

    private void Update()
    {
        if (_canQuick && Input.GetKeyDown(KeyCode.Space))
            StartCoroutine(EndingQuick());
    }

    private void Confirm()
    {
        _myCol.enabled = false;
        _dialogue.canTalk = false;
        _dialogue.Close();
        _iconInteract.SetActive(false);
        _radar.StatusRadar(true);
        _radar.target = _boxQuestPos.transform;
        _questUI.ActiveUIQuest("The Box", "Pick up the box", string.Empty, string.Empty);
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
            StartCoroutine(Ending());
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

    public void BoxPicked()
    {
        _inventory.upgradeLoot = true;
        _radar.target = transform;
        _myAnim.SetBool("Quest", false);
        _myCol.enabled = true;
        _questUI.TaskCompleted(1);
        _questUI.AddNewTask(3, "Return the box to its owner");
        _canQuick = true;
        _questCompleted = true;
    }

    private IEnumerator EndingQuick()
    {
        _canQuick = false;
        Destroy(_myCol);
        Destroy(_iconInteract);
        _boxMessage.SetMessage(_nameNPC);
 
        _camPlayer.gameObject.SetActive(false);
        _dogCam.gameObject.SetActive(true);
        _dog.quickEnd = true;
        _dog.OrderGoQuick(transform);

        yield return new WaitForSeconds(2f);
        _fadeOut.DOColor(Color.black, 1f);
        _player.FreezePlayer();

        yield return new WaitForSeconds(1f);
        _fadeOut.DOColor(Color.clear, 1f);
        _camFocus.gameObject.SetActive(true);
        _dogCam.gameObject.SetActive(false);
        _player.gameObject.transform.position = _posEnd.position;

        yield return new WaitForSeconds(1f);
        _boxMessage.ShowMessage(_message);

        yield return new WaitForSeconds(4f);
        Destroy(_camFocus.gameObject);
        _boxMessage.CloseMessage();
        _gm.QuestCompleted();
        _dog.quickEnd = false;
        _radar.target = _nextQuest.transform;

        yield return new WaitForSeconds(1f);
        _boxMessage.DesactivateMessage();
        Destroy(this);
    }

    private IEnumerator Ending()
    {
        _canQuick = false;
        _dog.quickEnd = false;
        Destroy(_myCol);
        Destroy(_iconInteract);
        _boxMessage.SetMessage(_nameNPC);

        _camPlayer.gameObject.SetActive(false);
        _camFocus.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);
        _player.transform.LookAt(transform);
        _boxMessage.ShowMessage(_message);

        yield return new WaitForSeconds(4f);
        Destroy(_camFocus.gameObject);
        _boxMessage.CloseMessage();
        _gm.QuestCompleted();
        _radar.target = _nextQuest.transform;
        _nextQuest.enabled = true;
        _nextQuest.GetComponent<Collider>().enabled = true;

        yield return new WaitForSeconds(1f);
        _boxMessage.DesactivateMessage();
        Destroy(this);
    }
}