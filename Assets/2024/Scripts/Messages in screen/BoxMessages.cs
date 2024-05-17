using UnityEngine;
using TMPro;
using DG.Tweening;

public class BoxMessages : MonoBehaviour
{
    [Header("MESSAGE")]
    [SerializeField] RectTransform _boxMessage;
    [SerializeField] TMP_Text _textMessage;
    [SerializeField] TMP_Text _textName;

    public void SetMessage(string name)
    {
        _boxMessage.localScale = new Vector3(1, 1, 1);
        _boxMessage.DOAnchorPosY(-1000f, 0f);
        _textName.text = name;
        _textMessage.rectTransform.anchoredPosition = new Vector2(0.2341f, _textMessage.rectTransform.anchoredPosition.y);
        _textMessage.rectTransform.sizeDelta = new Vector2(1030.737f, _textMessage.rectTransform.sizeDelta.y);
        _textMessage.fontSize = 40;
        _textMessage.alignment = TextAlignmentOptions.TopLeft;
    }

    public void ShowMessage(string message)
    {
        _textMessage.text = message;
        _boxMessage.gameObject.SetActive(true);
        _boxMessage.DOAnchorPosY(70f, 0.5f);
    }

    public void CloseMessage()
    {
        _boxMessage.DOAnchorPosY(-1000f, 0.5f);
    }

    public void DesactivateMessage()
    {
        _boxMessage.gameObject.SetActive(false);
    }
}