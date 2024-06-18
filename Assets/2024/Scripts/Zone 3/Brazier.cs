using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Brazier : MonoBehaviour
{
    [SerializeField] Image _fadeOut;
    [SerializeField] Collider _myCol;

    [SerializeField] Character _player;
    [SerializeField] NPCZone3 _npc;
    [SerializeField] float _speedNPC;
    private bool _activeNPC = false;

    [Header("CAMS")]
    [SerializeField] GameObject _canvasCinematic;
    [SerializeField] CameraOrbit _camPlayer;

    void Start()
    {
        _fadeOut.DOColor(Color.clear, 0f);
        _canvasCinematic.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (_activeNPC) _npc.MoveToBrazier(transform);
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(PlayCinematic(player));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var npc = other.GetComponent<NPCZone3>();
        if(npc != null)
        {
            _activeNPC = false;
            print("TOCADO");
            StopCoroutine(PlayCinematic(_player));
            _npc.SetIdle();
            _npc.ActiveFinal(this, _player, _canvasCinematic, _camPlayer);
            _myCol.enabled = false;
        }
    }

    private IEnumerator PlayCinematic(Character player)
    {
        //_myCol.enabled = false;
        player.FreezePlayer();
        player.enabled = false;
        _fadeOut.DOColor(Color.black, 1.5f);

        yield return new WaitForSeconds(2f);
        _fadeOut.DOColor(Color.clear, 1.5f);
        _canvasCinematic.SetActive(true);
        _camPlayer.gameObject.SetActive(false);
        _npc.transform.LookAt(transform);
        _activeNPC = true;

        yield return new WaitForSeconds(5f);
        _activeNPC = false;
        _npc.SetIdle();
        _npc.ActiveFinal(this, player, _canvasCinematic, _camPlayer);
    }
}