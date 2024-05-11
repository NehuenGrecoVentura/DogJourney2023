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

    [SerializeField] RectTransform _rectWood;

    private void Start()
    {
        _object.gameObject.SetActive(false);
    }


    public void ShowUI(string amount)
    {
        StartCoroutine(ShowUICoroutine(amount));
    }


    public void ShowUIWood(string amount)
    {
        StartCoroutine(ShowUICoroutineWood(amount));
    }


    private IEnumerator ShowUICoroutineWood(string amount)
    {
        float initialPos = _rectWood.anchoredPosition.x;
        _rectWood.anchoredPosition = new Vector2(-1000f, _rectWood.anchoredPosition.y);
        _rectWood.gameObject.SetActive(true);
        _rectWood.DOAnchorPosX(1150f, 1f);
        yield return new WaitForSeconds(3f);
        _rectWood.DOAnchorPosX(initialPos, 1f);
        yield return new WaitForSeconds(1f);
        _rectWood.gameObject.SetActive(false);
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








    private IEnumerator UpdateInventory(RectTransform obj, TMP_Text textMessage, string message)
    {
        textMessage.text = message;
        obj.anchoredPosition = new Vector2(-1000f, obj.anchoredPosition.y);
        obj.gameObject.SetActive(true);
        obj.DOAnchorPosX(1150f, 1f);
        yield return new WaitForSeconds(3f);
        obj.DOAnchorPosX(-1000f, 1f);
        yield return new WaitForSeconds(1f);
        obj.gameObject.SetActive(false);
    }





    private IEnumerator ShowLoot(RectTransform boxMessage)
    {
        float initialPos = boxMessage.anchoredPosition.x;
        boxMessage.anchoredPosition = new Vector2(-1000f, boxMessage.anchoredPosition.y);
        boxMessage.gameObject.SetActive(true);
        boxMessage.DOAnchorPosX(1150f, 1f);
        yield return new WaitForSeconds(3f);
        boxMessage.DOAnchorPosX(initialPos, 1f);
        yield return new WaitForSeconds(1f);
        boxMessage.gameObject.SetActive(false);
    }

    public void ShowLootCoroutine(RectTransform boxMessage)
    {
        StartCoroutine(ShowLoot(boxMessage));
    }
}