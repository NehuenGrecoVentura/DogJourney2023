using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MachineChairlift : MonoBehaviour
{
    [Header("INTERACT")]
    [SerializeField] Transform _iconInteract;
    [SerializeField] KeyCode _keyInteract = KeyCode.F;
    [SerializeField] Collider _myCol;
    [SerializeField] GameObject _indicator;

    [Header("CAMS")]
    [SerializeField] Camera _camActive;
    [SerializeField] CameraOrbit _camPlayer;
    [SerializeField] Transform _pointMove;

    [Header("BATTERY")]
    [SerializeField] GameObject _battery;

    [Header("FADE OUT")]
    [SerializeField] Image _fadeOut;

    [Header("FINAL")]
    [SerializeField] ChainZone3 _npc;

    private void Start()
    {
        _fadeOut.DOColor(Color.clear, 0f);
        _iconInteract.DOScale(0f, 0f);
        _battery.SetActive(false);
        _camActive.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && _myCol.enabled) _iconInteract.DOScale(0.15f, 0.5f);
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && _myCol.enabled && Input.GetKeyDown(_keyInteract))
            StartCoroutine(ActiveChairlift(player));
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if(player != null) _iconInteract.DOScale(0f, 0.5f);
    }

    private IEnumerator ActiveChairlift(Character player)
    {
        _myCol.enabled = false;
        player.FreezePlayer();
        _iconInteract.DOScale(0f, 0.5f);
        Destroy(_indicator);
        _fadeOut.DOColor(Color.black, 2f);

        yield return new WaitForSeconds(1f);
        _camPlayer.gameObject.SetActive(false);
        _camActive.gameObject.SetActive(true);
        _fadeOut.DOColor(Color.clear, 1.5f);

        yield return new WaitForSeconds(2f);
        _battery.SetActive(true);

        yield return new WaitForSeconds(1f);
        _camActive.gameObject.transform.DOMove(_pointMove.position, 1f);

        yield return new WaitForSeconds(2f);
        Destroy(_camActive.gameObject);
        _npc.EndingCoroutine(player);     
    }
}