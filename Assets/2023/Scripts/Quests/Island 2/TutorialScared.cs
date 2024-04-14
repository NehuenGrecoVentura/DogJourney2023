using System.Collections;
using UnityEngine;
using DG.Tweening;

public class TutorialScared : MonoBehaviour
{
    [SerializeField] GameObject _cinematic;
    [SerializeField] RectTransform _message;
    private Character _player;
    private CameraOrbit _camPlayer;

    private void Awake()
    {
        _player = FindObjectOfType<Character>();
        _camPlayer = FindObjectOfType<CameraOrbit>();
    }

    void Start()
    {
        _cinematic.SetActive(false);
        _message.gameObject.SetActive(false);
        _message.DOAnchorPosY(-1000f, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        var dog = other.GetComponent<Dog>();
        if (dog != null) StartCoroutine(ShowTutorial());
    }

    private IEnumerator ShowTutorial()
    {
        _player.speed = 0;
        _player.FreezePlayer(RigidbodyConstraints.FreezeAll);
        _cinematic.SetActive(true);
        _camPlayer.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        _message.gameObject.SetActive(true);
        _message.DOAnchorPosY(-70f, 1f);
        yield return new WaitForSeconds(5f);
        _camPlayer.gameObject.SetActive(true);
        _player.speed = _player.speedAux;
        _player.FreezePlayer(RigidbodyConstraints.FreezeRotation);
        Destroy(_message.transform.parent.gameObject);
        Destroy(_cinematic);
        Destroy(this);
    }
}
