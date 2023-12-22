using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialAddDog : MonoBehaviour
{
    [Header("MESSAGES")]
    [SerializeField] GameObject _canvas;
    [SerializeField] GameObject _enter;
    [SerializeField] GameObject _keySelection;
    [SerializeField] GameObject _callDog;

    [Header("TEXTS")]
    [SerializeField] TMP_Text _textSelect;
    [SerializeField] TMP_Text _textCall;

    [Header("ICONS")]
    [SerializeField] Image _icon1;
    [SerializeField] Image _icon2;
    [SerializeField] Image _iconR;

    private bool _isEnter = false;
    private bool _selectDog1 = false;
    private bool _selectDog2 = false;

    private void OnEnable()
    {
        PressEnter();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && !_isEnter)
        {
            KeySelection();
            _isEnter = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && _isEnter)
        {
            _icon1.color = Color.green;
            _selectDog1 = true;
        }
            
            
        if (Input.GetKeyDown(KeyCode.Alpha2) && _isEnter)
        {
            _icon2.color = Color.green;
            _selectDog2 = true;
        }
            
        if (_selectDog1 && _selectDog2)
        {
            StartCoroutine(Success(_textSelect, false, true));
        }

        if (Input.GetKeyDown(KeyCode.R) && _selectDog1 && _selectDog2)
        {
            _iconR.color = Color.green;
            StartCoroutine(Success(_textCall, false, true));
            Destroy(_canvas, 3f);
            Destroy(this, 3f);
        }
    }

    private void PressEnter()
    {
        _canvas.SetActive(true);
        _enter.SetActive(true);
        _keySelection.SetActive(false);
        _callDog.SetActive(false);
    }

    private void KeySelection()
    {
        Destroy(_enter);
        _keySelection.SetActive(true);
        _callDog.SetActive(false);
    }

    private IEnumerator Success(TMP_Text text, bool keySelection, bool callDog)
    {
        text.color = Color.green;
        text.text = "GOOD";
        yield return new WaitForSeconds(3f);
        _keySelection.SetActive(keySelection);
        _callDog.SetActive(callDog);
        StopCoroutine(Success(text, keySelection, callDog));
    }
}