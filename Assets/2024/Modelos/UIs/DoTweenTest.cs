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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) ShowUI("+ 1");
    }



    public void ShowUI(string amount)
    {
        StartCoroutine(ShowUICoroutine(amount));
    }

    private IEnumerator ShowUICoroutine(string amount)
    {
        Vector3 initialPos = _object.GetComponent<RectTransform>().anchoredPosition;
        initialPos.x = _posHide;
        _object.GetComponent<RectTransform>().anchoredPosition = initialPos;
        _textAmount.text = amount;
        _object.gameObject.SetActive(true);
        _object.transform.DOMoveX(_endPosX, 1f);
        yield return new WaitForSeconds(_timeInScreen);
        _object.transform.DOMoveX(initialPos.x, 1f);
        yield return new WaitForSeconds(1f);
        _object.gameObject.SetActive(false);
    }
}