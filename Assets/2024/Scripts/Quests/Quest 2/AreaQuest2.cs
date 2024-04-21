using UnityEngine;
using TMPro;
using DG.Tweening;
using System.Collections;

public class AreaQuest2 : MonoBehaviour
{
    [Header("CINEMATIC")]
    [SerializeField] GameObject _cinematicRabbit;
    private CameraOrbit _camPlayer;

    [Header("MESSAGE")]
    [SerializeField] RectTransform _boxMessage;
    [SerializeField] TMP_Text _textMessage;
    [SerializeField] TMP_Text _textName;
    [SerializeField, TextArea(4,6)] string _message;
    [SerializeField] string _name = "Tip";
    private Character _player;
    private bool _firstContact = false;
    
    private void Awake()
    {
        _camPlayer = FindObjectOfType<CameraOrbit>();
        _player = FindObjectOfType<Character>();
    }

    private void Start()
    {
        _cinematicRabbit.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && !_firstContact)
        {
            player.speed = 0;
            player.FreezePlayer(RigidbodyConstraints.FreezeAll);
            StartCoroutine(FocusPuzzle());
        }
    }

    private IEnumerator FocusPuzzle()
    {
        _firstContact = true;
        _textMessage.text = _message;
        _textName.text = _name;

        _boxMessage.localScale = new Vector3(1, 1, 1);
        _boxMessage.DOAnchorPosY(-1000f, 0f);
        _camPlayer.gameObject.SetActive(false);
        _cinematicRabbit.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        _boxMessage.gameObject.SetActive(true);
        //_boxMessage.transform.DOScale(1f, 0.5f);
        _boxMessage.DOAnchorPosY(70f, 0.5f);
        yield return new WaitForSeconds(5f);
        _player.speed = _player.speedAux;
        _player.FreezePlayer(RigidbodyConstraints.FreezeRotation);
        //_boxMessage.transform.DOScale(0, 0.5f);
        _boxMessage.DOAnchorPosY(-1000f, 0.5f);
        _camPlayer.gameObject.SetActive(true);
        Destroy(_cinematicRabbit);
        yield return new WaitForSeconds(0.6f);
        _boxMessage.gameObject.SetActive(false);
        Destroy(this);
    }
}