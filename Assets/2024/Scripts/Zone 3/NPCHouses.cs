using System.Collections;
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
    [SerializeField] Camera _camFinal;
    [SerializeField] CameraOrbit _camPlayer;
    [SerializeField] Transform _posCamMove;

    [Header("QUEST")]
    [SerializeField] GameObject _canvasBuildFinished;
    [SerializeField] GameObject _canvasSlider;
    [SerializeField] ItemsManager _items;
    [SerializeField] GameObject[] _houses;
    [SerializeField] QuestUI _questUI;
    [SerializeField] CharacterInventory _inventory;
    [SerializeField] Manager _gm;
    [SerializeField] int _houseBuilded = 0;
    [SerializeField] int _houseTotal = 3;
    [SerializeField] int _itemsTotal = 3;
    [SerializeField] Transform _posNPCEnding;
    [SerializeField] Transform _posPlayerEnding;
    public int itemsFound = 0;

    private bool _questActive = false;
    private bool _questCompleted = false;

    void Start()
    {
        _dialogue.gameObject.SetActive(false);
        _camZone3.gameObject.SetActive(false);
        _camHouses.gameObject.SetActive(false);
        _camFinal.gameObject.SetActive(false);
        _canvasBuildFinished.SetActive(false);
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
        StartCoroutine(IntroFocusZone());
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
        if (player != null)
        {
            if (_myCol.enabled && _questCompleted && Input.GetKeyDown(_keyInteract) && _myCol.enabled)
            {
                _gm.QuestCompleted();
                Destroy(this);
            }
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

        foreach (GameObject house in _houses)
        {
            house.SetActive(true);
        }

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
        _camHouses.gameObject.transform.DOMove(_posCamMove.position, 3f);
        _boxMessages.CloseMessage();

        yield return new WaitForSeconds(4f);
        _fadeOut.DOColor(Color.black, 1.5f);
        
        yield return new WaitForSeconds(2f);
        _boxMessages.DesactivateMessage();
        _fadeOut.DOColor(Color.clear, 1.5f);
        Destroy(_camHouses.gameObject);
        _camPlayer.gameObject.SetActive(true);
        _player.DeFreezePlayer();
        _items.gameObject.SetActive(true);
        
        _questActive = true;
        _questUI.ActiveUIQuest("A Last Favor", "Search for lost items (" + itemsFound.ToString() + "/" + _itemsTotal.ToString() + ")", "Build the Houses (" + _houseBuilded.ToString() + "/" + _houseTotal.ToString() + ")", string.Empty);
    }

    public void HouseBuilded()
    {
        _houseBuilded++;
        _questUI.AddNewTask(2, "Build the Houses (" + _houseBuilded.ToString() + "/" + _houseTotal.ToString() + ")");

        if (_houseBuilded >= _houseTotal)
        {
            //_myCol.enabled = true;
            _questUI.TaskCompleted(2);
            _questActive = false;
            _questCompleted = true;
            StartCoroutine(Ending());
        }
    }

    public void ItemFound()
    {
        itemsFound++;
        _questUI.AddNewTask(1, "Search for lost items (" + itemsFound.ToString() + "/" + _itemsTotal.ToString() + ")");
        if (itemsFound >= _itemsTotal)
        {
            _questUI.TaskCompleted(1);
            _questUI.AddNewTask(3, "Go back to the npc and finish the task");

            DigZone3[] digs = FindObjectsOfType<DigZone3>();

            if (digs != null)
            {
                foreach (DigZone3 dig in digs)
                {
                    Destroy(dig.gameObject);
                }
            }

            else return;

            if (_items != null) Destroy(_items.gameObject);
            else return;

            Destroy(_canvasSlider);
        }  
    }

    private IEnumerator Ending()
    {
        transform.position = _posNPCEnding.position;
        _player.transform.position = _posPlayerEnding.position;
        transform.LookAt(_player.transform);
        _player.transform.LookAt(transform);

        _boxMessages.SetMessage(_nameNPC);
        _player.FreezePlayer();
        _player.enabled = false;
        _questUI.UIStatus(false);

        yield return new WaitForSeconds(3f);
        _camFinal.gameObject.SetActive(true);
        _camPlayer.gameObject.SetActive(false);
        _fadeOut.DOColor(Color.black, 1.5f);

        yield return new WaitForSeconds(2f);
        _fadeOut.DOColor(Color.clear, 1.5f);
        yield return new WaitForSeconds(2f);
        _boxMessages.ShowMessage(_messages[2]);

        yield return new WaitForSeconds(4f);
        _boxMessages.CloseMessage();
        Destroy(_camFinal.gameObject);
        _camPlayer.gameObject.SetActive(true);
        _player.enabled = true;
        _player.DeFreezePlayer();
        _gm.QuestCompleted();
        _canvasBuildFinished.SetActive(true);
        yield return new WaitForSeconds(1f);
        Destroy(this);
    }
}