using UnityEngine;
using System.Collections;

public class MessageGateOil : MonoBehaviour
{
    [SerializeField] GameObject _camFocusQuest3;
    [SerializeField] GameObject _message;
    [SerializeField] GameObject _canvasQuest;
    [SerializeField] Camera _camPlayer;

    [Header("RADAR")]
    [SerializeField] Transform _npcQuest2Pos;
    private LocationQuest _radar;
    
    private Character _player;
    private Gates _gates;
    private QuestManager _questManager;

    private void Awake()
    {
        _player = FindObjectOfType<Character>();
        _gates = FindObjectOfType<Gates>();
        _radar = FindObjectOfType<LocationQuest>();
        _questManager = FindObjectOfType<QuestManager>();
    }

    private void Start()
    {
        _radar.StatusRadar(false);
        _canvasQuest.SetActive(false);
        _camFocusQuest3.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) StartCoroutine(FocusQuest3());
    }

    private IEnumerator FocusQuest3()
    {
        _questManager.HideHUDQuest();
        _gates.UnlockNPCQuest3();
        Destroy(_message);
        _player.FreezePlayer();
        _player.speed = 0;
        _camPlayer.gameObject.SetActive(false);
        _radar.StatusRadar(false);
        _camFocusQuest3.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        _player.DeFreezePlayer();
        _player.speed = _player.speedAux;
        _camPlayer.gameObject.SetActive(true);
        _radar.StatusRadar(true);
        _radar.target = _npcQuest2Pos;
        Destroy(_camFocusQuest3.gameObject);
        Destroy(gameObject);
    }
}