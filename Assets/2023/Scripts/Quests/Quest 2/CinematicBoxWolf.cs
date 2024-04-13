using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class CinematicBoxWolf : MonoBehaviour
{
    [Header("TASKS")]
    [SerializeField] Collider _col;    
    [SerializeField] string _newTask;
    [SerializeField] TMP_Text _textPhase2;

    [Header("MESSAGE")]
    [SerializeField] GameObject _boxMessage;
    [SerializeField] TMP_Text _textMessage;
    [SerializeField] TMP_Text _textName;
    [SerializeField, TextArea(4, 6)] string _message;

    [Header("CINEMATIC")]
    [SerializeField] GameObject _cinematic;
    private CameraOrbit _camPlayer;

    private Character _player;
    private LocationQuest _radar;
    private QuestUI _questUI;

    private void Awake()
    {
        _radar = FindObjectOfType<LocationQuest>();
        _player = FindObjectOfType<Character>();
        _questUI = FindObjectOfType<QuestUI>();
        _camPlayer = FindObjectOfType<CameraOrbit>();
    }

    private void Start()
    {
        _cinematic.SetActive(false);
    }

    public IEnumerator StarCinematic()
    {
        _camPlayer.gameObject.SetActive(false);
        _cinematic.SetActive(true);
        _textMessage.text = _message;
        _textName.text = "Tip";
        _radar.StatusRadar(false);
        Destroy(_col);
        _player.FreezePlayer(RigidbodyConstraints.FreezePosition);
        _player.speed = 0;
        yield return new WaitForSeconds(8f);
        _boxMessage.gameObject.SetActive(true);
        _boxMessage.transform.DOScale(1f, 0.5f);
        yield return new WaitForSeconds(6f);
        Destroy(_cinematic);
        _camPlayer.gameObject.SetActive(true);
        _boxMessage.transform.DOScale(0f, 0f);
        _player.FreezePlayer(RigidbodyConstraints.FreezeRotation);
        _player.speed = _player.speedAux;
        _questUI.AddNewTask(2, _newTask);
        _questUI.ActiveUIQuest("The Box", "Recover the box", _newTask, "");
        _radar.StatusRadar(true);
        Destroy(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) StartCoroutine(StarCinematic());          
    }
}