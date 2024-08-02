using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class CinematicTrunks : MonoBehaviour
{
    [SerializeField] Camera _camTrunks;
    [SerializeField] GameObject _boxMessage;
    [SerializeField] GameObject _boxMessageSpace;
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
    private bool _cinPlay = false;

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

    private void Update()
    {
        if (_cinPlay)
        {
            if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
                SkipCinematic();
        }
    }

    private void SkipCinematic()
    {
        StopCoroutine(Play());
        _camTrunks.gameObject.SetActive(false);
        _camPlayer.gameObject.SetActive(true);
        _player.DeFreezePlayer();
        _questUI.UIStatus(true);
        Destroy(gameObject);
    }

    private IEnumerator Play()
    {
        _cinPlay = true;
        _myAudio.Stop();
        _questUI.UIStatus(false);
        _camTrunks.gameObject.SetActive(true);
        _camPlayer.gameObject.SetActive(false);
        _boxMessageSpace.gameObject.SetActive(false);
        _player.FreezePlayer();
        _boxMessage.GetComponent<RectTransform>().DOAnchorPosY(-1000f, 1f);
        _boxMessageSpace.GetComponent<RectTransform>().DOAnchorPosY(-1000f, 1f);
        
        yield return new WaitForSeconds(2f);
        _myAudio.Play();
        _boxMessage.SetActive(true);
        _boxMessage.GetComponent<RectTransform>().DOAnchorPosY(-170f, 1f);
        
        yield return new WaitForSeconds(4f);
        _boxMessage.SetActive(false);
        _boxMessageSpace.SetActive(true);

        yield return new WaitForSeconds(3f);
        _camTrunks.gameObject.SetActive(false);
        _camPlayer.gameObject.SetActive(true);
        _player.DeFreezePlayer();
        _questUI.UIStatus(true);
        Destroy(gameObject);
    }
}