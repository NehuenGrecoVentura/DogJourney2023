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
    [SerializeField, TextArea(4, 6)] string[] _lines;
    [SerializeField] TMP_Text _dialogueText;
    [SerializeField] TMP_Text _nameNPCText;
    [SerializeField] float _typingTime = 0.05f;
    public Button buttonConfirm;
    private bool _didDialogueStart;
    private int _index;
    private Character _player;
    private CameraOrbit _cam;

    private void Awake()
    {
        _player = FindObjectOfType<Character>();
        _cam = FindObjectOfType<CameraOrbit>();
    }

    void Start()
    {
        buttonConfirm.gameObject.SetActive(false);
        _boxDialogue.SetActive(false);
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
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
        else if (_index == _lines.Length) buttonConfirm.gameObject.SetActive(true);
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
        _boxDialogue.SetActive(false);
    }

    private IEnumerator ShowLine()
    {
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