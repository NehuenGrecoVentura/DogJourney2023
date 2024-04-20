using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class Dialogue : MonoBehaviour
{
    public bool playerInRange = false;
    [SerializeField] GameObject _boxDialogue;
    [SerializeField] Transform _arrowTransform;
    [TextArea(4, 6)] public string[] _lines;
    [SerializeField] TMP_Text _dialogueText;
    [SerializeField] TMP_Text _nameNPCText;
    [SerializeField] float _typingTime = 0.05f;

    [Header("BUTTONS")]
    public Button buttonConfirm;
    [SerializeField] Button _buttonCancel;

    private bool _didDialogueStart;
    private int _index;
    private Character _player;
    private CameraOrbit _cam;
    [SerializeField] KeyCode _keyInteract = KeyCode.F;
    public bool canTalk = false;

    private AudioSource _myAudio;
    [SerializeField] AudioClip _talkSound;
    [SerializeField] AudioClip[] _buttonsSounds;

    private void Awake()
    {
        _myAudio = GetComponent<AudioSource>();
        _player = FindObjectOfType<Character>();
        _cam = FindObjectOfType<CameraOrbit>();
    }

    void Start()
    {
        buttonConfirm.gameObject.SetActive(false);
        _boxDialogue.SetActive(false);
        _buttonCancel.gameObject.SetActive(false);
        gameObject.SetActive(false);
        _myAudio.Stop();
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(_keyInteract) && canTalk)
        {
            if (!_didDialogueStart) StartDialogue();
            else if (_dialogueText.text == _lines[_index]) NextDialogueLine();

            else
            {
                StopAllCoroutines();
                _dialogueText.text = _lines[_index];
            }
        }
    }

    public void Set(string nameNPC)
    {
        _nameNPCText.text = nameNPC;
    }

    private void StartDialogue()
    {
        _player.speed = 0;
        _player.FreezePlayer(RigidbodyConstraints.FreezeAll);
        _cam.enabled = false;
        _arrowTransform.DOScale(new Vector3(1.03f, 0.51f, 0.97f), 1f).SetLoops(-1, LoopType.Yoyo);
        _didDialogueStart = true;
        _boxDialogue.SetActive(true);
        _index = 0;
        StartCoroutine(ShowLine());
    }

    private void NextDialogueLine()
    {
        _index++;
        if (_index < _lines.Length) StartCoroutine(ShowLine());
        else if (_index == _lines.Length)
        {
            buttonConfirm.gameObject.SetActive(true);
            _buttonCancel.gameObject.SetActive(true);
        }
            
        else Close();
    }

    public void Close()
    {
        _player.speed = _player.speedAux;
        _player.FreezePlayer(RigidbodyConstraints.FreezeRotation);
        _cam.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _didDialogueStart = false;
        buttonConfirm.gameObject.SetActive(false);
        _buttonCancel.gameObject.SetActive(false);
        _boxDialogue.SetActive(false);
        canTalk = false;
    }


    public void EnterButton()
    {
        if (_buttonsSounds.Length > 0)
        {
            int random = Random.Range(0, _buttonsSounds.Length);
            _myAudio.PlayOneShot(_buttonsSounds[random]);
        }

        ButtonSelectStatus(1.5f, 0.5f);
    }

    public void ExitButton()
    {
        ButtonSelectStatus(1.3005f, 0.5f);
    }

    private void ButtonSelectStatus(float scale, float time)
    {

        transform.DOScale(scale, time);
    }

    private IEnumerator ShowLine()
    {
        _myAudio.PlayOneShot(_talkSound);
        _dialogueText.text = string.Empty;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
     
        foreach (char ch in _lines[_index])
        {
            _dialogueText.text += ch;
            yield return new WaitForSeconds(_typingTime);  
        }
    }
}