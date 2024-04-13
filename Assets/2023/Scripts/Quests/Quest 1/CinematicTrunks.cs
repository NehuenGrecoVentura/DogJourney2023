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

    private Character _player;
    private CameraOrbit _camPlayer;
    private QuestUI _questUI;

    private void Awake()
    {
        _player = FindObjectOfType<Character>();
        _camPlayer = FindObjectOfType<CameraOrbit>();
        _questUI = FindObjectOfType<QuestUI>();
    }

    void Start()
    {
        StartCoroutine(Play());
    }

    private IEnumerator Play()
    {
        _questUI.UIStatus(false);
        _camTrunks.gameObject.SetActive(true);
        _camPlayer.gameObject.SetActive(false);
        _player.speed = 0;
        _player.FreezePlayer(RigidbodyConstraints.FreezeAll);

        _boxMessage.GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);

        yield return new WaitForSeconds(4f);
        _boxMessage.SetActive(true);
        _boxMessage.GetComponent<RectTransform>().DOScale(0.8f, 0.5f);


        yield return new WaitForSeconds(4f);
        //_boxMessage.transform.DOScale(0f, 0.5f);
        _boxMessage.GetComponent<RectTransform>().DOScale(0f, 0.5f);
        _camTrunks.gameObject.SetActive(false);
        _camPlayer.gameObject.SetActive(true);
        _player.speed = _player.speedAux;
        _player.FreezePlayer(RigidbodyConstraints.FreezeRotation);
        _questUI.UIStatus(true);
        Destroy(gameObject);
    }
}