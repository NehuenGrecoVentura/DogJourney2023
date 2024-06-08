using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class ChainZone3 : MonoBehaviour
{
    [Header("INTERACT")]
    [SerializeField] Collider _myCol;
    [SerializeField] KeyCode _keyInteract = KeyCode.F;
    [SerializeField] GameObject _iconInteract;
    [SerializeField] GameObject _iconQuest;

    [Header("DIALOGUE")]
    [SerializeField] Dialogue _dialogue;
    [SerializeField, TextArea(4, 6)] string[] _lines;
    [SerializeField] TMP_Text _txtName;
    [SerializeField] string _nameNPC;
    [SerializeField] Button _buttonConfirm;

    [Header("QUEST")]
    [SerializeField] Manager _gm;
    [SerializeField] CharacterInventory _inventory;
    [SerializeField] Image _fadeOut;
    [SerializeField] int _woodRequired = 50;
    [SerializeField] int _nailsRequired = 100;
    [SerializeField] int _moneyRequired = 300;
    [SerializeField] int _ticketsRequired = 200;
    [SerializeField] GameObject _iconBuildBridge;
    private bool _questActive = false;
    private bool _questCompleted = false;

    [Header("NOTIFICATION")]
    [SerializeField] RectTransform _notification;
    [SerializeField] DoTweenTest _doTween;

    [Header("MESSAGE")]
    [SerializeField] BoxMessages _boxMessage;
    [SerializeField, TextArea(4, 6)] string[] _messages;

    [Header("CAMERAS")]
    [SerializeField] Camera _camEnd;
    [SerializeField] CameraOrbit _camPlayer;
    [SerializeField] Camera _camFocusIconBuild;

    [Header("AUDIO")]
    [SerializeField] AudioSource _myAudio;
    [SerializeField] AudioClip _soundConfirm;

    void Start()
    {
        _dialogue.gameObject.SetActive(false);
        _camEnd.gameObject.SetActive(false);
        _camFocusIconBuild.gameObject.SetActive(false);
        _notification.gameObject.SetActive(false);
        _iconBuildBridge.gameObject.SetActive(false);
        _iconInteract.transform.DOScale(0f, 0f);
        _fadeOut.DOColor(Color.clear, 0f);
    }

    private void Update()
    {
        if (_questActive)
        {
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.J))
            {
                _inventory.nails += 500;
                _inventory.money += 500;
                _inventory.tickets += 500;
                _inventory.greenTrees += 500;
            }

            bool full = (_inventory.greenTrees >= _woodRequired &&
                                _inventory.nails >= _nailsRequired &&
                                _inventory.money >= _moneyRequired &&
                                _inventory.tickets >= _ticketsRequired);

            if (full)
            {
                // Se cumplen todos los requisitos
                _iconQuest.SetActive(true);
                _myCol.enabled = true;
                _doTween.ShowLootCoroutine(_notification);
                _questCompleted = true;
                _questActive = false;
            }
            else
            {
                // No se cumplen todos los requisitos
                _iconQuest.SetActive(false);
                _myCol.enabled = false;
                _questCompleted = false;
            }
        }
    }

    private void Confirm()
    {
        _myCol.enabled = false;
        _dialogue.canTalk = false;
        _dialogue.Close();
        _iconInteract.transform.DOScale(0f, 0.5f);
        _myAudio.PlayOneShot(_soundConfirm);
        _iconQuest.SetActive(false);
        _questActive = true;
    }

    private void SetDialogue()
    {
        _iconInteract.transform.DOScale(0.01f, 0.5f);
        _buttonConfirm.onClick.AddListener(() => Confirm());

        if (!_questActive)
        {
            for (int i = 0; i < _dialogue._lines.Length; i++)
                _dialogue._lines[i] = _lines[i];
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

    private IEnumerator Ending(Character player)
    {
        _myCol.enabled = false;
        _iconInteract.transform.DOScale(0f, 0.5f);
        _iconQuest.transform.DOScale(0f, 0.5f);
        _boxMessage.SetMessage(_nameNPC);
        player.FreezePlayer();
        _fadeOut.DOColor(Color.black, 1f);

        yield return new WaitForSeconds(1f);
        _camPlayer.gameObject.SetActive(false);
        _camEnd.gameObject.SetActive(true);
        _fadeOut.DOColor(Color.clear, 1f);

        yield return new WaitForSeconds(1f);
        _boxMessage.ShowMessage(_messages[0]);

        yield return new WaitForSeconds(3f);
        _boxMessage.CloseMessage();
    
        yield return new WaitForSeconds(1f);
        _boxMessage.ShowMessage(_messages[1]);

        yield return new WaitForSeconds(3f);
        _boxMessage.CloseMessage();

        yield return new WaitForSeconds(0.6f);
        _boxMessage.ShowMessage(_messages[2]);

        yield return new WaitForSeconds(3f);
        Destroy(_camEnd.gameObject);
        _camFocusIconBuild.gameObject.SetActive(true);
        
        yield return new WaitForSeconds(1f);
        _iconBuildBridge.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f);
        Destroy(_camFocusIconBuild.gameObject);
        player.DeFreezePlayer();
        _camPlayer.gameObject.SetActive(true);
        _boxMessage.CloseMessage();
        _gm.QuestCompleted();

        yield return new WaitForSeconds(0.6f);
        _boxMessage.DesactivateMessage();
        Destroy(this);
    }
}