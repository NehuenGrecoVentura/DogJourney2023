using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class EnableChainQuest : MonoBehaviour
{
    [SerializeField] Collider _myCol;
    [SerializeField] GameObject _iconChain;

    [Header("NPCS")]
    [SerializeField] FloristChain1 _npcChainFlorist;
    [SerializeField] GameObject _npcFlorist;
    [SerializeField] ArchaeologistQuest1 _archaeologist;
    //[SerializeField] FishingChain1 _fishingChain;

    [Header("CAMERAS")]
    [SerializeField] Camera _cam1;
    [SerializeField] Camera _cam2;

    [Header("PLAYER")]
    [SerializeField] Character _player;
    [SerializeField] CameraOrbit _camPlayer;

    [Header("MESSAGE")]
    [SerializeField] RectTransform _boxMessage;
    [SerializeField] TMP_Text _textMessage;
    [SerializeField] TMP_Text _textName;
    [SerializeField, TextArea(4, 6)] string _message;
    [SerializeField] AudioSource _myAudio;

    [SerializeField] BillboardCam[] _bubbleIcons;

    private void Start()
    {
        _cam1.gameObject.SetActive(false);
        _cam2.gameObject.SetActive(false);
        _iconChain.SetActive(false);
        _myAudio.Stop();


        foreach (var item in _bubbleIcons)
        {
            item.enabled = false;
        }






    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) StartCoroutine(ActiveChainQuests());
    }

    private IEnumerator ActiveChainQuests()
    {
        Destroy(_myCol);
        Destroy(_npcFlorist);
        _npcChainFlorist.gameObject.SetActive(true);
        _camPlayer.gameObject.SetActive(false);
        _cam1.gameObject.SetActive(true);
        _iconChain.SetActive(true);
        _player.speed = 0;
        _player.FreezePlayer(RigidbodyConstraints.FreezeAll);
        _textName.text = "Special Quest";
        _textMessage.text = _message;
        _boxMessage.transform.localScale = new Vector3(1, 1, 1);
        _boxMessage.DOAnchorPosY(-1000f, 0f);
        _boxMessage.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        _myAudio.Play();
        _boxMessage.DOAnchorPosY(70f, 0f);
        yield return new WaitForSeconds(3f);
        Destroy(_cam1.gameObject);
        _cam2.gameObject.SetActive(true);

        yield return new WaitForSeconds(4f);
        Destroy(_cam2.gameObject);
        _camPlayer.gameObject.SetActive(true);
        _boxMessage.DOAnchorPosY(-1000f, 0.5f);
        _player.speed = _player.speedAux;
        _player.FreezePlayer(RigidbodyConstraints.FreezeRotation);

        _archaeologist.enabled = true;
        _archaeologist.GetComponent<BoxCollider>().enabled = true;
        //_fishingChain.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);


        foreach (var item in _bubbleIcons)
        {
            item.enabled = true;
        }


        Destroy(gameObject);
    }
}