using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class ChainParkQuest : MonoBehaviour
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

    [Header("NOTIFICATION")]
    [SerializeField] RectTransform _notification;
    [SerializeField] DoTweenTest _doTween;

    [Header("QUEST")]
    [SerializeField] Manager _gm;
    [SerializeField] CharacterInventory _inventory;
    [SerializeField] GameObject _articleMarketSkin;
    [SerializeField] Image _fadeOut;
    [SerializeField] int _scoreRequired = 250;
    public int actualScore = 0;
    [HideInInspector] public bool questActive = false;
    [HideInInspector] public bool _questCompleted = false;

    [Header("MESSAGE")]
    [SerializeField] BoxMessages _boxMessage;
    [SerializeField, TextArea(4, 6)] string[] _messages;
    

    [Header("CAMERAS")]
    [SerializeField] Camera _camEnd;
    [SerializeField] CameraOrbit _camPlayer;

    [Header("AUDIO")]
    [SerializeField] AudioSource _myAudio;
    [SerializeField] AudioClip _soundConfirm;

    void Start()
    {
        _dialogue.gameObject.SetActive(false);
        _articleMarketSkin.SetActive(false);
        _camEnd.gameObject.SetActive(false);
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
        _articleMarketSkin.SetActive(true);
        _iconQuest.SetActive(false);
        questActive = true;
    }

    private void SetDialogue()
    {
        _iconInteract.transform.DOScale(0.01f, 0.5f);
        _buttonConfirm.onClick.AddListener(() => Confirm());

        if (!questActive)
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
            if (_myCol.enabled && !questActive && !_questCompleted) SetDialogue();
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

    public void AddScore(int addScore, TMP_Text textScore)
    {
        actualScore += addScore;
        textScore.text = "TOTAL SCORE: " + actualScore.ToString();

        if (actualScore >= _scoreRequired)
        {
            _articleMarketSkin.SetActive(true);
            questActive = false;
        }
    }

    public void Completed(AudioSource audio, AudioClip soundBuy, AudioClip soundError)
    {
        if (actualScore >= _scoreRequired)
        {
            audio.PlayOneShot(soundBuy);
            Destroy(_articleMarketSkin);
            _iconQuest.SetActive(true);
            _myCol.enabled = true;
            _questCompleted = true;

            _inventory.tickets -= _scoreRequired;
            if (_inventory.tickets <= 0) _inventory.tickets = 0;

            _doTween.ShowLootCoroutine(_notification);
        }

        else audio.PlayOneShot(soundError);
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
        _fadeOut.DOColor(Color.black, 1f);

        yield return new WaitForSeconds(1f);
        _fadeOut.DOColor(Color.clear, 1f);

        yield return new WaitForSeconds(1f);
        _boxMessage.ShowMessage(_messages[1]);

        yield return new WaitForSeconds(3f);
        _boxMessage.CloseMessage();

        yield return new WaitForSeconds(0.6f);
        _boxMessage.ShowMessage(_messages[2]);

        yield return new WaitForSeconds(3f);
        Destroy(_camEnd.gameObject);
        player.DeFreezePlayer();
        _camPlayer.gameObject.SetActive(true);
        _boxMessage.CloseMessage();
        _gm.QuestCompleted();

        yield return new WaitForSeconds(0.6f);
        _boxMessage.DesactivateMessage();
        Destroy(this);
    }
}