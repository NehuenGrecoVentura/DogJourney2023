using System.Collections;
using UnityEngine;

public class NPCWomanMarket : MonoBehaviour
{
    [SerializeField] GameObject _iconQuest;
    [SerializeField] GameObject _message;
    [SerializeField] Collider _col;
    private Character _player;
    private Animator _myAnim;

    [Header("CINEMATIC")]
    [SerializeField] GameObject _cinematic;
    [SerializeField] float _timeCinematic = 10f;
    [SerializeField] Camera _camPlayer;

    [Header("RADAR")]
    [SerializeField] Transform _posGates;
    private LocationQuest _radar;

    private void Awake()
    {
        _myAnim = GetComponent<Animator>();

        _player = FindObjectOfType<Character>();
        _radar = FindObjectOfType<LocationQuest>();
    }

    private void Start()
    {
        _cinematic.SetActive(false);
        _message.SetActive(false);
        _iconQuest.SetActive(true);
    }

    void Update()
    {
        transform.LookAt(_player.gameObject.transform.position);
    }

    private IEnumerator PlayCinematic(Character player)
    {
        _radar.StatusRadar(false);
        Destroy(_col);
        Destroy(_message);
        _camPlayer.gameObject.SetActive(false);
        _cinematic.SetActive(true);
        yield return new WaitForSeconds(_timeCinematic);
        _camPlayer.gameObject.SetActive(true); 
        player.DeFreezePlayer();
        _radar.StatusRadar(true);
        _radar.target = _posGates;
        Destroy(_cinematic);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            player.FreezePlayer();
            _iconQuest.SetActive(false);
            _myAnim.SetBool("Quest", true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            if (!Input.GetKeyDown(KeyCode.Return))
            {
                player.FreezePlayer();
                _message.SetActive(true);
            }

            else StartCoroutine(PlayCinematic(player));
        }
    }
}