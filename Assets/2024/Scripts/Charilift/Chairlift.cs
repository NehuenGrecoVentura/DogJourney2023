using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Chairlift : MonoBehaviour
{
    [Header("INTERACT")]
    [SerializeField] Transform _iconInteract;
    [SerializeField] KeyCode _keyInteract = KeyCode.F;
    [SerializeField] Collider _myCol;

    [Header("CAMS")]
    [SerializeField] Camera _camCinematic;
    [SerializeField] CameraOrbit _camPlayer;

    [Header("CINEMATIC")]
    [SerializeField] GameObject _cinematic;
    [SerializeField] Image _fadeOut;

    void Start()
    {
        _camCinematic.gameObject.SetActive(false);
        _cinematic.SetActive(false);
        _fadeOut.DOColor(Color.clear, 0f);
        _iconInteract.DOScale(0f, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) _iconInteract.DOScale(0.1f, 0.5f);
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && Input.GetKeyDown(_keyInteract)) 
            StartCoroutine(ActiveChair(player));
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) _iconInteract.DOScale(0f, 0.5f);
    }

    private IEnumerator ActiveChair(Character player)
    {

        _fadeOut.DOColor(Color.black, 1.5f);
        _myCol.enabled = false;
        player.FreezePlayer();
        

        yield return new WaitForSeconds(2f);
        _fadeOut.DOColor(Color.clear, 1.5f);
        _camPlayer.gameObject.SetActive(false);
        _camCinematic.gameObject.SetActive(true);
        _cinematic.SetActive(true);

        yield return new WaitForSeconds(5f);
        _fadeOut.DOColor(Color.black, 1.5f);

        yield return new WaitForSeconds(2f);

        //yield return new WaitForSeconds(1.5f);
        _fadeOut.DOColor(Color.clear, 1.5f);
        _camPlayer.gameObject.SetActive(true);
        _camCinematic.gameObject.SetActive(false);
        _cinematic.SetActive(false);
        player.DeFreezePlayer();
        _myCol.enabled = true;
    }
}
