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
        _boxMessage.DOAnchorPosY(0f, 0f);
        _textName.text = name;
    }

    public void ShowMessage(string message)
    {
        _textMessage.text = message;
        _boxMessage.gameObject.SetActive(true);
        _boxMessage.DOAnchorPosY(70f, 0.5f);
    }

    public void CloseMessage()
    {
        _boxMessage.DOAnchorPosY(0f, 0.5f);
    }

    public void DesactivateMessage()
    {
        _boxMessage.gameObject.SetActive(false);
    }
}