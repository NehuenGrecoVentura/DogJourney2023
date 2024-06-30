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
    [SerializeField] Animator _myAnim;
    [SerializeField] GameObject _indicator;

    [Header("DIALOGUE")]
    [SerializeField] Dialogue _dialogue;
    [SerializeField, TextArea(4, 6)] string[] _lines;
    [SerializeField] TMP_Text _txtName;
    [SerializeField] string _nameNPC;
    [SerializeField] Button _buttonConfirm;

    [Header("QUEST")]
    [SerializeField] Manager _gm;
    [SerializeField] Image _fadeOut;
    private bool _questActive = false;
    [SerializeField] private bool _batteryObtained = false;
    [SerializeField] LocationQuest _radar;
    [SerializeField] MachineChairlift _machine;
    [SerializeField] Chairlift _chairlift;

    [Header("NOTIFICATION")]
    [SerializeField] RectTransform _notification1;
    [SerializeField] RectTransform _notification2;
    [SerializeField] DoTweenTest _doTween;

    [Header("MESSAGE")]
    [SerializeField] BoxMessages _boxMessage;
    [SerializeField, TextArea(4, 6)] string[] _messages;

    [Header("CAMERAS")]
    [SerializeField] Camera _camEnd;
    [SerializeField] Camera _camEndingActive;
    [SerializeField] CameraOrbit _camPlayer;

    [Header("AUDIO")]
    [SerializeField] AudioSource _myAudio;
    [SerializeField] AudioClip _soundConfirm;

    void Start()
    {
        _dialogue.gameObject.SetActive(false);
        _camEnd.gameObject.SetActive(false);
        _notification1.gameObject.SetActive(false);
        _notification2.gameObject.SetActive(false);
        _indicator.gameObject.SetActive(false);
        
        _iconInteract.transform.DOScale(0f, 0f);
        _fadeOut.DOColor(Color.clear, 0f);

        _machine.gameObject.GetComponent<BoxCollider>().enabled = false;
        _machine.enabled = false;

        _chairlift.enabled = false;
        _chairlift.gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    private void Confirm()
    {
        _myCol.enabled = true;
        _dialogue.canTalk = false;
        _dialogue.Close();
        _iconInteract.transform.DOScale(0f, 0.5f);
        _myAudio.PlayOneShot(_soundConfirm);
        _iconQuest.SetActive(false);
        _myAnim.SetBool("Quest", true);
        _doTween.ShowLootCoroutine(_notification1);
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
            if (_myCol.enabled && !_questActive) SetDialogue();
            else if (_questActive) _iconInteract.transform.DOScale(0.01f, 0.5f);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && _myCol.enabled && _questActive && Input.GetKeyDown(_keyInteract))
        {
            if (_batteryObtained) StartCoroutine(MessageBatteryObtained(player));
            else StartCoroutine(MessageInfoBattery(player));
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

    public void EndingCoroutine(Character player)
    {
        StartCoroutine(Ending(player));
    }

    private IEnumerator Ending(Character player)
    {
        _camEndingActive.gameObject.SetActive(true);
        _myAnim.SetBool("Quest", true);

        Destroy(_myCol);
        Destroy(_iconInteract.gameObject);
        Destroy(_iconQuest.gameObject);

        _boxMessage.SetMessage(_nameNPC);

        yield return new WaitForSeconds(1f);
        _boxMessage.ShowMessage(_messages[2]);

        yield return new WaitForSeconds(3f);
        _boxMessage.CloseMessage();
        player.DeFreezePlayer();
        _camPlayer.gameObject.SetActive(true);
        Destroy(_camEnd.gameObject);
        Destroy(_camEndingActive.gameObject);
        _boxMessage.CloseMessage();
        _gm.QuestCompleted();

        _radar.StatusRadar(true);
        _radar.target = _chairlift.transform;
        _chairlift.enabled = true;
        _chairlift.gameObject.GetComponent<BoxCollider>().enabled = true;

        yield return new WaitForSeconds(0.6f);
        _boxMessage.DesactivateMessage();
        Destroy(this);
    }


    private IEnumerator MessageInfoBattery(Character player)
    {
        _myCol.enabled = false;
        _boxMessage.SetMessage(_nameNPC);
        player.FreezePlayer();

        _camEnd.gameObject.SetActive(true);
        _camPlayer.gameObject.SetActive(false);

        yield return new WaitForSeconds(1f);
        _boxMessage.ShowMessage(_messages[0]);

        yield return new WaitForSeconds(3f);
        _camEnd.gameObject.SetActive(false);
        _camPlayer.gameObject.SetActive(true);
        player.DeFreezePlayer();
        _myCol.enabled = true;

        yield return new WaitForSeconds(1f);
        _boxMessage.DesactivateMessage();
    }

    private IEnumerator MessageBatteryObtained(Character player)
    {
        _myCol.enabled = false;
        _boxMessage.SetMessage(_nameNPC);
        player.FreezePlayer();
        _radar.StatusRadar(false);

        _camEnd.gameObject.SetActive(true);
        _camPlayer.gameObject.SetActive(false);

        yield return new WaitForSeconds(1f);
        _boxMessage.ShowMessage(_messages[1]);

        yield return new WaitForSeconds(3f);
        _boxMessage.CloseMessage();
        _camEnd.gameObject.SetActive(false);
        _camPlayer.gameObject.SetActive(true);
        player.DeFreezePlayer();
        _myCol.enabled = true;
        _indicator.gameObject.SetActive(true);

        _radar.StatusRadar(true);
        _radar.target = _machine.transform;
        _machine.gameObject.GetComponent<BoxCollider>().enabled = true;
        _machine.enabled = true;

        yield return new WaitForSeconds(1f);
        _boxMessage.DesactivateMessage();
    }


    public void BatteryObtained()
    {
        _radar.StatusRadar(true);
        _radar.target = transform;
        _doTween.ShowLootCoroutine(_notification2);
        _batteryObtained = true;
        _myCol.enabled = true;
    }
}