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
    private AudioSource _myAudio;

    private void Awake()
    {
        _myAudio = GetComponent<AudioSource>();
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
        _myAudio.Stop();
        _questUI.UIStatus(false);
        _camTrunks.gameObject.SetActive(true);
        _camPlayer.gameObject.SetActive(false);
        _player.speed = 0;
        _player.FreezePlayer();
        _boxMessage.GetComponent<RectTransform>().DOAnchorPosY(-1000f, 1f);
        yield return new WaitForSeconds(2f);
        _myAudio.Play();
        _boxMessage.SetActive(true);
        _boxMessage.GetComponent<RectTransform>().DOAnchorPosY(-170f, 1f);
        yield return new WaitForSeconds(4f);
        _boxMessage.GetComponent<RectTransform>().DOAnchorPosY(-1000f, 0.5f);
        _camTrunks.gameObject.SetActive(false);
        _camPlayer.gameObject.SetActive(true);
        _player.speed = _player.speedAux;
        _player.DeFreezePlayer();
        _questUI.UIStatus(true);
        Destroy(gameObject);
    }
}