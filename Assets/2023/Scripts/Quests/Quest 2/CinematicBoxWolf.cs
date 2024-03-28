using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CinematicBoxWolf : CinematicManager
{
    [SerializeField] GameObject _cinematicPlay;
    [SerializeField] Collider _col;    
    [SerializeField] WolfStatic _wolfStatic;
    [SerializeField] string _newTask;
    [SerializeField] TMP_Text _textPhase2;
    
    private Character _player;
    private TestCinematic _cinematic;
    private LocationQuest _radar;
    private QuestUI _questUI;

    [Header("MESSAGE SLIDE")]
    [SerializeField] Sprite _iconWolf;
    [SerializeField] string _messageText;
    private MessageSlide _messageSlide;

    private void Awake()
    {
        _cinematic = GetComponent<TestCinematic>();

        _messageSlide = FindObjectOfType<MessageSlide>();
        _radar = FindObjectOfType<LocationQuest>();
        _player = FindObjectOfType<Character>();
        _questUI = FindObjectOfType<QuestUI>();
    }

    private void Start()
    {
        _cinematicPlay.SetActive(false);
    }

    public IEnumerator StarCinematic()
    {
        _radar.StatusRadar(false);
        Destroy(_col);
        ObjStatus(false);
        _player.FreezePlayer(RigidbodyConstraints.FreezePosition);
        _player.speed = 0;
        _cinematic.StartCinematic(_cinematicPlay, durationCinematic);
        yield return new WaitForSeconds(durationCinematic);
        _player.FreezePlayer(RigidbodyConstraints.FreezeRotation);
        _player.speed = _player.speedAux;
        ObjStatus(true);
        _questUI.AddNewTask(2, _newTask);
        _questUI.ActiveUIQuest("The Box", "Recover the box", _newTask, "");
        _radar.StatusRadar(true);
        _messageSlide.ShowMessage(_messageText, _iconWolf);
        Destroy(_cinematic);        
        Destroy(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) StartCoroutine(StarCinematic());          
    }
}