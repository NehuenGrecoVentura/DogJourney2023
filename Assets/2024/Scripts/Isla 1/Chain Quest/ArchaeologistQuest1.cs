using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class ArchaeologistQuest1 : MonoBehaviour
{
    [Header("INTERACT")]
    [SerializeField] GameObject _iconInteract;
    [SerializeField] GameObject _iconQuest;
    [SerializeField] KeyCode _keyInteract = KeyCode.F;
    [SerializeField] Collider _myCol;
    [SerializeField] LocationQuest _radar;

    [Header("DIALOGUE")]
    [SerializeField] TMP_Text _textDialogue;
    [SerializeField] TMP_Text _textNAME;
    [SerializeField, TextArea(4, 6)] string[] _lines;
    [SerializeField] Button _buttonConfirm;
    [SerializeField] Image _fadeOut;
    private Dialogue _dialogue;

    [Header("MESSAGE")]
    [SerializeField] TMP_Text _nameNPC;
    [SerializeField] TMP_Text _textMessage;
    [SerializeField] TMP_Text _textAmountMoney;
    [SerializeField] RectTransform _rectMessage;
    [SerializeField] RectTransform _rectMoney;
    [SerializeField, TextArea(4, 6)] string[] _messagesEnding;
    [SerializeField] CharacterInventory _inventory;
    [SerializeField] BoxMessages _boxMes;

    [Header("QUEST")]
    private bool _questActive = false;
    [SerializeField] private bool _treasureFound = false;

    [Header("INVENTORY UI")]
    [SerializeField] GameObject _canvasIconsChainsQuests;
    [SerializeField] GameObject _iconTreasure;
    [SerializeField] DoTweenManager _message;
    [SerializeField] RectTransform _boxMessage;
    [SerializeField] TMP_Text _textSlide;
    [SerializeField] TMP_Text _textInventory;

    [Header("ENDING")]
    [SerializeField] GameObject _cinematicEnd;

    [Header("REFS")]
    private Character _player;
    private CameraOrbit _camPlayer;
    private Manager _gm;
    [SerializeField] DigTreasure[] _allTreasures;

    [Header("AUDIOS")]
    [SerializeField] AudioClip[] _sounds;
    private AudioSource _myAudio;

    private void Awake()
    {
        _myAudio = GetComponent<AudioSource>();

        _dialogue = FindObjectOfType<Dialogue>();
        _player = FindObjectOfType<Character>();
        _gm = FindObjectOfType<Manager>();
        _camPlayer = FindObjectOfType<CameraOrbit>();
    }

    private void Start()
    {
        _dialogue.gameObject.SetActive(false);
        _iconInteract.SetActive(false);
        _cinematicEnd.SetActive(false);
    }

    public void Confirm()
    {
        _dialogue.canTalk = false;
        //_buttonConfirm.gameObject.SetActive(false);
        //_dialogue.playerInRange = false;
        //_myCol.enabled = false;
        _dialogue.Close();
        _iconInteract.SetActive(false);

        if (!_gm.chainsActive)
        {
            _player.FreezePlayer();
            _gm.chainsActive = true;
            _gm.ActiveTutorialChain(_myCol);
        }

        //_gm.ActiveTutorialChain();
        _canvasIconsChainsQuests.SetActive(true);
        _iconTreasure.SetActive(true);
        //_message.AddIconInventory(_boxMessage, _textSlide, "Added to inventory");
        _myAudio.PlayOneShot(_sounds[0]);

        foreach (var item in _allTreasures)
        {
            item.gameObject.SetActive(true);
        }

        _textNAME.enableAutoSizing = false;
        Destroy(_iconQuest);
        _questActive = true;
    }

    private void SetDialogue()
    {
        _dialogue.canTalk = true;
        _dialogue.Set("Archaeologist");
        _textNAME.enableAutoSizing = true;
        _buttonConfirm.onClick.AddListener(() => Confirm());

        for (int i = 0; i < _dialogue._lines.Length; i++)
            _dialogue._lines[i] = _lines[i];

        _dialogue.gameObject.SetActive(true);
        _dialogue.playerInRange = true;
        _iconInteract.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && _myCol.enabled)
        {
            if (!_questActive) SetDialogue();
            if(_questActive || _treasureFound)_iconInteract.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && _myCol.enabled && Input.GetKeyDown(KeyCode.F))
        {
            if (_treasureFound) StartCoroutine(Ending());
            else if (!_treasureFound && _questActive) StartCoroutine(MessageTreasure(player));
        }
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

    public void MessageFound()
    {
        _message.AddIconInventory(_boxMessage, _textSlide, "Take it to the archaeologist");
        _textInventory.text = "Found";
        _myCol.enabled = true;
        _treasureFound = true;
    }

    private IEnumerator MessageTreasure(Character player)
    {
        _myCol.enabled = false;
        _boxMes.SetMessage("Archaeologist");
        player.FreezePlayer();
        _iconInteract.SetActive(false);
        _radar.StatusRadar(false);

        yield return new WaitForSeconds(1f);
        _boxMes.ShowMessage(_messagesEnding[3]);

        yield return new WaitForSeconds(3f);
        player.DeFreezePlayer();
        _myCol.enabled = true;
        _radar.StatusRadar(true);

        yield return new WaitForSeconds(1f);
        _boxMes.DesactivateMessage();
    }

    private IEnumerator Ending()
    {
        _myCol.enabled = false;
        _radar.StatusRadar(false);
        _player.FreezePlayer();
        _iconInteract.transform.DOScale(0f, 0f);
        _nameNPC.text = "Archaeologist";
        _textMessage.text = _messagesEnding[0];
        _rectMessage.localScale = new Vector3(1, 1, 1);
        _rectMessage.DOAnchorPosY(-1000f, 0f);
        _fadeOut.DOColor(Color.black, 1f);
        _camPlayer.gameObject.SetActive(false);
        _cinematicEnd.SetActive(true);

        yield return new WaitForSeconds(2f);
        _rectMessage.gameObject.SetActive(true);
        _fadeOut.DOColor(Color.clear, 1f);
        _myAudio.PlayOneShot(_sounds[1]);
        _rectMessage.DOAnchorPosY(70f, 0f);

        yield return new WaitForSeconds(3f);
        _rectMessage.DOAnchorPosY(-1000f, 0.5f);

        yield return new WaitForSeconds(1f);
        _textMessage.text = _messagesEnding[1];
        _myAudio.PlayOneShot(_sounds[1]);
        _rectMessage.DOAnchorPosY(70f, 0f);

        yield return new WaitForSeconds(3f);
        _boxMessage.DOAnchorPosY(-1000f, 0.5f);

        yield return new WaitForSeconds(1f);
        _textMessage.text = _messagesEnding[2];
        _myAudio.PlayOneShot(_sounds[1]);
        _rectMessage.DOAnchorPosY(70f, 0f);

        yield return new WaitForSeconds(3f);
        _rectMessage.DOAnchorPosY(-1000f, 0.5f);

        yield return new WaitForSeconds(0.6f);
        _rectMessage.gameObject.SetActive(false);
        _camPlayer.gameObject.SetActive(true);
        Destroy(_cinematicEnd);
        _player.DeFreezePlayer();
        _gm.QuestCompleted();
        _radar.StatusRadar(true);
        _iconTreasure.SetActive(false);
        _message.AddIconInventory(_boxMessage, _textSlide, "Minerva helmet received");
        Destroy(this);
    }
}