using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class CinematicTrunks : MonoBehaviour
{
    [SerializeField] Camera _camTrunks;
    [SerializeField] GameObject _boxMessage;
    [SerializeField] Image _iconMessage;
    [SerializeField] Sprite _iconSpaceBar;
    [SerializeField] TMP_Text _messageText;
    [SerializeField] TMP_Text _messageNameText;
    [SerializeField] string _message;
    [SerializeField] string _name;

    void Start()
    {
        StartCoroutine(Play());
    }

    private IEnumerator Play()
    {
        _messageText.text = _message;
        _messageNameText.text = _name;
        _iconMessage.sprite = _iconSpaceBar;

        _boxMessage.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 125f);
        _messageText.rectTransform.anchoredPosition = new Vector2(91.42871f, _messageText.rectTransform.anchoredPosition.y);
        _messageText.rectTransform.sizeDelta = new Vector2(988.358f, _messageText.rectTransform.sizeDelta.y);
        _messageText.alignment = TextAlignmentOptions.TopLeft;

        Character player = FindObjectOfType<Character>();
        CameraOrbit camPlayer = FindObjectOfType<CameraOrbit>();
        
        _camTrunks.gameObject.SetActive(true);
        camPlayer.gameObject.SetActive(false);
        player.speed = 0;
        player.FreezePlayer(RigidbodyConstraints.FreezeAll);

        yield return new WaitForSeconds(2f);
        _boxMessage.SetActive(true);
        _boxMessage.transform.DOScale(1f, 0.5f);

        yield return new WaitForSeconds(3f);
        _boxMessage.transform.DOScale(0f, 0.5f);
        _camTrunks.gameObject.SetActive(false);
        camPlayer.gameObject.SetActive(true);
        player.speed = player.speedAux;
        player.FreezePlayer(RigidbodyConstraints.FreezeRotation);
        Destroy(gameObject);
    }
}