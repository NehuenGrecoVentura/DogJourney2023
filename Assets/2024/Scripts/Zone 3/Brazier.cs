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
    [SerializeField] Transform _playerPosCinematic;
    private bool _activeNPC = false;

    [Header("CAMS")]
    [SerializeField] GameObject _canvasCinematic;
    [SerializeField] CameraOrbit _camPlayer;

    [Header("BONFIRE")]
    [SerializeField] GameObject _woods;
    [SerializeField] GameObject _fire;

    [Header("RADAR")]
    [SerializeField] NPCHouses _nextQuest;
    [SerializeField] LocationQuest _radar;
    [SerializeField] QuestUI _questUI;

    void Start()
    {
        _fadeOut.DOColor(Color.clear, 0f);
        _canvasCinematic.SetActive(false);
        _woods.SetActive(false);
        _fire.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (_activeNPC) _npc.MoveToBrazier(transform);
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && Input.GetKeyDown(KeyCode.F) && _myCol.enabled)
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
            _fire.SetActive(true);
            _npc.OnFire();
            _npc.ActiveFinal(this, _player, _canvasCinematic, _camPlayer);
            _myCol.enabled = false;
        }
    }

    private IEnumerator PlayCinematic(Character player)
    {
        //_myCol.enabled = false;
        player.transform.position = _playerPosCinematic.position;
        player.transform.rotation = Quaternion.Euler(0, 150, 0);
        player.FreezePlayer();
        player.enabled = false;
        _questUI.UIStatus(false);
        _radar.StatusRadar(false);
        _fadeOut.DOColor(Color.black, 1.5f);

        yield return new WaitForSeconds(2f);
        _fadeOut.DOColor(Color.clear, 1.5f);
        _woods.SetActive(true);
        _canvasCinematic.SetActive(true);
        _camPlayer.gameObject.SetActive(false);
        _npc.transform.LookAt(transform);
        _activeNPC = true;
        Destroy(_playerPosCinematic.gameObject);

        yield return new WaitForSeconds(5f);
        _activeNPC = false;
        _fire.SetActive(true);
        _npc.OnFire();
        _npc.ActiveFinal(this, player, _canvasCinematic, _camPlayer);
    }

    private IEnumerator 
}