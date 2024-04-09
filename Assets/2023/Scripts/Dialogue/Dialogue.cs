using System.Collections;
using UnityEngine;
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
    private bool _didDialogueStart;
    private int _index;
    private Vector3 _initialScaleArrow;

    void Start()
    {
        _boxDialogue.SetActive(false);
        _initialScaleArrow = _arrowTransform.transform.localScale;
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!_didDialogueStart)
            {
                StartDialogue();
                _arrowTransform.DOScale(new Vector3(1.03f, 0.51f, 0.97f), 1f).SetLoops(-1, LoopType.Yoyo);
            }
                
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
        _didDialogueStart = true;
        _boxDialogue.SetActive(true);
        _index = 0;
        StartCoroutine(ShowLine());
    }

    private void NextDialogueLine()
    {
        _index++;

        if (_index < _lines.Length) StartCoroutine(ShowLine());

        else
        {
            _didDialogueStart = false;
            _boxDialogue.SetActive(false);
        }
    }

    private IEnumerator ShowLine()
    {
        _dialogueText.text = string.Empty;
        

        foreach (char ch in _lines[_index])
        {
            _dialogueText.text += ch;
            yield return new WaitForSeconds(_typingTime);
        }
    }
}