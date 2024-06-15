using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;


public class NPCHouses : MonoBehaviour
{
    [Header("INTERACT")]
    [SerializeField] Collider _myCol;
    [SerializeField] KeyCode _keyInteract = KeyCode.F;
    [SerializeField] GameObject _iconInteract;
    [SerializeField] Animator _myAnim;
    [SerializeField] Character _player;

    [Header("DIALOGUE")]
    [SerializeField] Dialogue _dialogue;
    [SerializeField, TextArea(4, 6)] string[] _lines;
    [SerializeField] TMP_Text _txtName;
    [SerializeField] string _nameNPC;
    [SerializeField] Button _buttonConfirm;

    [Header("MESSAGE")]
    [SerializeField] BoxMessages _boxMessages;
    [SerializeField, TextArea(4, 6)] string[] _messages;

    [Header("FADE OUT")]
    [SerializeField] Image _fadeOut;

    [Header("AUDIO")]
    [SerializeField] AudioSource _myAudio;
    [SerializeField] AudioClip _soundConfirm;

    [Header("CAMS")]
    [SerializeField] Camera _camZone3;
    [SerializeField] Camera _camHouses;
    [SerializeField] CameraOrbit _camPlayer;

    [Header("QUEST")]
    [SerializeField] QuestUI _questUI;
    [SerializeField] int _woodRequired = 10;
    [SerializeField] CharacterInventory _inventory;
    private bool _questActive = false;
    private bool _questCompleted = false;

    void Start()
    {
        _dialogue.gameObject.SetActive(false);
        _camZone3.gameObject.SetActive(false);
        _camHouses.gameObject.SetActive(false);
        _iconInteract.transform.DOScale(0f, 0f);
        _fadeOut.DOColor(Color.clear, 0f);
    }

    private void Confirm()
    {
        _myCol.enabled = false;
        _dialogue.canTalk = false;
        _dialogue.Close();
        _iconInteract.transform.DOScale(0f, 0.5f);
        _myAudio.PlayOneShot(_soundConfirm);
        _myAnim.SetBool("Quest", true);
        //_questUI.ActiveUIQuest("A Little Fire", "Travel back to the chairlift", "Get wood to carry the mountain (" + _inventory.greenTrees.ToString() + "/" + _woodRequired.ToString() + ")", string.Empty);
        StartCoroutine(IntroFocusZone());
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

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            _dialogue.playerInRange = false;
            _iconInteract.transform.DOScale(0f, 0.5f);
        }
    }

    private IEnumerator IntroFocusZone()
    {
        _player.FreezePlayer();
        _boxMessages.SetMessage("Tip");
        _fadeOut.DOColor(Color.black, 1.5f);

        yield return new WaitForSeconds(2f);
        _fadeOut.DOColor(Color.clear, 1.5f);
        _camPlayer.gameObject.SetActive(false);
        _camZone3.gameObject.SetActive(true);
        _boxMessages.ShowMessage(_messages[0]);

        yield return new WaitForSeconds(4f);
        Destroy(_camZone3.gameObject);
        _camHouses.gameObject.SetActive(true);
        _boxMessages.CloseMessage();

        yield return new WaitForSeconds(1f);
        _boxMessages.ShowMessage(_messages[1]);

        yield return new WaitForSeconds(4f);
        _fadeOut.DOColor(Color.black, 1.5f);
        _boxMessages.CloseMessage();

        yield return new WaitForSeconds(2f);
        _boxMessages.DesactivateMessage();
        _fadeOut.DOColor(Color.clear, 1.5f);
        Destroy(_camHouses.gameObject);
        _camPlayer.gameObject.SetActive(true);
        _player.DeFreezePlayer();
    }
}