using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class DoTweenTest : MonoBehaviour
{
    [SerializeField] Transform _object;
    [SerializeField] float _duration;
    [SerializeField] float _endPosX = 2010f;
    [SerializeField] float _posHide = 1484f;
    [SerializeField] float _timeInScreen = 3f;
    [SerializeField] TMP_Text _textAmount;

    private void Start()
    {
        _object.gameObject.SetActive(false);
    }

    public void ShowUI(string amount)
    {
        StartCoroutine(ShowUICoroutine(amount));
    }

    private IEnumerator ShowUICoroutine(string amount)
    {
        _textAmount.text = amount;
        _object.gameObject.SetActive(true);
        _object.DOMoveX(_endPosX, _duration);
        yield return new WaitForSeconds(_timeInScreen);
        _object.DOMoveX(_posHide, _duration);
        yield return new WaitForSeconds(1f);
        _object.gameObject.SetActive(false);
    }
}