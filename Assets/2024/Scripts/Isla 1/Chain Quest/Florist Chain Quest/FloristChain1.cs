using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class FloristChain1 : MonoBehaviour
{
    #region INTERACT
    [Header("INTERACT")]
    [SerializeField] GameObject _iconInteract;
    [SerializeField] GameObject _iconQuest;
    [SerializeField] KeyCode _keyInteract = KeyCode.F;
    private Collider _myCol;
    #endregion

    #region DIALOGUE
    [Header("DIALOGUE")]
    [SerializeField] TMP_Text _textName;
    [SerializeField] TMP_Text _textDialogue;
    [SerializeField, TextArea(4, 6)] string[] _lines;
    [SerializeField] Button _buttonConfirm;
    [SerializeField] Dialogue _dialogue;
    [SerializeField] BoxMessages _boxMessages;
    #endregion

    [Header("QUEST")]
    [SerializeField] LocationQuest _radar;
    [SerializeField] int _flowersRequired = 4;
    private bool _questActive = false;
    [SerializeField] private bool _questCompleted = false;
    [SerializeField] Dig[] _allDigs;
    private Manager _gm;

    [Header("INVENTORY UI")]
    [SerializeField] GameObject _canvasIconsChainsQuests;
    [SerializeField] GameObject _iconFlowers;
    [SerializeField] TMP_Text _textInventoryFlowers;
    private CharacterInventory _inventory;

    [Header("SLIDERS MESSAGES")]
    [SerializeField] RectTransform _sliderActivation;
    [SerializeField] RectTransform _sliderMoney;
    [SerializeField] TMP_Text _messageBack;
    [SerializeField] DoTweenTest _doTween;
    [SerializeField] GameObject _sliderTreasure;

    #region ENDING
    [Header("ENDING")]
    [SerializeField] RectTransform _recTransform;
    [SerializeField] TMP_Text _txtNPC;
    [SerializeField] TMP_Text _txtMessage;
    [SerializeField, TextArea(4, 6)] string[] _messagesEnding;
    [SerializeField] Image _fadeOut;
    [SerializeField] GameObject _cinematicEnd;
    private Character _player;
    private CameraOrbit _camPlayer;
    #endregion

    private void Awake()
    {
        _myCol = GetComponent<Collider>();
        _gm = FindObjectOfType<Manager>();
        _inventory = FindObjectOfType<CharacterInventory>();
        _player = FindObjectOfType<Character>();
        _camPlayer = FindObjectOfType<CameraOrbit>();
    }

    private void Start()
    {
        _dialogue.gameObject.SetActive(false);
        _iconInteract.SetActive(false);
        _cinematicEnd.SetActive(false);
    }

    private void Update()
    {
        if(_questActive && _inventory.flowers >= _flowersRequired && !_questCompleted)
        {
            _myCol.enabled = true;
            _messageBack.text = "Back to the florist";
            _doTween.ShowLootCoroutine(_sliderActivation);

            foreach (var item in _allDigs)
            {
                item.gameObject.SetActive(true);
                item.RespawnAllFlowers();
            }

            _questCompleted = true;
        }     
        
        else if (_questActive)
            _textInventoryFlowers.text = "x " + _inventory.flowers.ToString() + "/" + _flowersRequired.ToString();

    }

    private void SetDialogue()
    {
        _dialogue.canTalk = true;
        _dialogue.Set("Florist");
        _buttonConfirm.onClick.AddListener(() => Confirm());

        for (int i = 0; i < _dialogue._lines.Length; i++)
            _dialogue._lines[i] = _lines[i];

        _dialogue.gameObject.SetActive(true);
        _dialogue.playerInRange = true;
        _iconInteract.SetActive(true);
    }

    public void Confirm()
    {
        _dialogue.canTalk = false;
        //_buttonConfirm.gameObject.SetActive(false);
        //_dialogue.playerInRange = false;
        _myCol.enabled = false;
        _dialogue.Close();
        _iconInteract.SetActive(false);
        Destroy(_iconQuest);

        //_gm.ActiveTutorialChain();
        _canvasIconsChainsQuests.SetActive(true);
        _iconFlowers.SetActive(true);
        //_doTween.ShowLootCoroutine(_sliderActivation);
        _sliderTreasure.SetActive(false);

        foreach (var item in _allDigs)
        {
            item.gameObject.SetActive(true);
            item.ActiveChainQuest();
        }

        if (!_gm.chainsActive)
        {
            _gm.chainsActive = true;
            _gm.ActiveTutorialChain(_myCol);
        }

        _questActive = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        //var player = other.GetComponent<Character>();
        //if (player != null && _myCol.enabled && !_questActive && !_questCompleted)
        //    SetDialogue();


        var player = other.GetComponent<Character>();
        if (player != null && _myCol.enabled)
        {
            if (!_questActive) SetDialogue();
            if (_questActive || _questCompleted) _iconInteract.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //var player = other.GetComponent<Character>();
        //if (player != null && _myCol.enabled && _questCompleted && Input.GetKeyDown(KeyCode.F))
        //    StartCoroutine(Ending());

        var player = other.GetComponent<Character>();
        if (player != null && _myCol.enabled && Input.GetKeyDown(KeyCode.F))
        {
            if (_questCompleted) StartCoroutine(Ending());
            else if (!_questCompleted && _questActive) StartCoroutine(MessageTreasure(player));
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

    private IEnumerator Ending()
    {
        _myCol.enabled = false;
        _player.FreezePlayer();
        _radar.StatusRadar(false);
        _txtNPC.text = "Florist";
        _txtMessage.text = _messagesEnding[0];
        _recTransform.localScale = new Vector3(1, 1, 1);
        _recTransform.DOAnchorPosY(-1000f, 0f);
        _fadeOut.DOColor(Color.black, 1f);
        _camPlayer.gameObject.SetActive(false);
        _cinematicEnd.SetActive(true);
        _iconInteract.SetActive(false);

        yield return new WaitForSeconds(2f);
        _recTransform.gameObject.SetActive(true);
        _fadeOut.DOColor(Color.clear, 1f);
        _recTransform.DOAnchorPosY(70f, 0f);

        yield return new WaitForSeconds(3f);
        _recTransform.DOAnchorPosY(-1000f, 0.5f);

        yield return new WaitForSeconds(1f);
        _txtMessage.text = _messagesEnding[1];
        _recTransform.DOAnchorPosY(70f, 0f);

        yield return new WaitForSeconds(3f);
        _recTransform.DOAnchorPosY(-1000f, 0.5f);

        yield return new WaitForSeconds(1f);
        _txtMessage.text = _messagesEnding[2];
        _recTransform.DOAnchorPosY(70f, 0f);

        yield return new WaitForSeconds(3f);
        _recTransform.DOAnchorPosY(-1000f, 0.5f);

        yield return new WaitForSeconds(0.6f);
        _recTransform.gameObject.SetActive(false);
        _camPlayer.gameObject.SetActive(true);
        Destroy(_cinematicEnd);
        _player.DeFreezePlayer();
        _gm.QuestCompleted();
        _iconFlowers.SetActive(false);
        _inventory.money += 100;
        _radar.StatusRadar(true);
        _doTween.ShowLootCoroutine(_sliderMoney);
        Destroy(this);
    }

    private IEnumerator MessageTreasure(Character player)
    {
        _myCol.enabled = false;
        _boxMessages.SetMessage("Florist");
        player.FreezePlayer();
        _iconInteract.SetActive(false);
        _radar.StatusRadar(false);

        yield return new WaitForSeconds(1f);
        _boxMessages.ShowMessage(_messagesEnding[3]);

        yield return new WaitForSeconds(3f);
        player.DeFreezePlayer();
        _myCol.enabled = true;
        _radar.StatusRadar(true);

        yield return new WaitForSeconds(1f);
        _boxMessages.DesactivateMessage();
    }
}