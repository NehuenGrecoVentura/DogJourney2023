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
    [SerializeField] RectTransform _boxMessage;
    [SerializeField] TMP_Text _textMessage;
    [SerializeField] TMP_Text _textName;
    [SerializeField, TextArea(4, 6)] string _message;

    [Header("CINEMATIC")]
    [SerializeField] GameObject _cinematic;
    private CameraOrbit _camPlayer;
    private bool _canSkip = false;

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

    private void Update()
    {
        if (_canSkip)
        {
            if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) 
                SkipCinematic();
        } 
    }

    private void SkipCinematic()
    {
        StopCoroutine(StarCinematic());
        Destroy(_cinematic);
        _camPlayer.gameObject.SetActive(true);
        _boxMessage.gameObject.SetActive(false);
        _boxMessage.DOAnchorPosY(-1000f, 0f);
        _player.DeFreezePlayer();
        _questUI.UIStatus(true);
        _questUI.AddNewTask(2, _newTask);
        _questUI.ActiveUIQuest("The Box", "Recover the box", _newTask, "");
        _radar.StatusRadar(true);
        Destroy(this);
    }

    public IEnumerator StarCinematic()
    {
        _canSkip = true;
        _boxMessage.DOAnchorPosY(-1000f, 0);
        _boxMessage.localScale = new Vector3(1, 1, 1);

        _questUI.UIStatus(false);
        _camPlayer.gameObject.SetActive(false);
        _cinematic.SetActive(true);
        _textMessage.text = _message;
        _textName.text = "Tip";
        _radar.StatusRadar(false);
        Destroy(_col);
        _player.FreezePlayer();

        yield return new WaitForSeconds(8f);
        _boxMessage.gameObject.SetActive(true);
        _boxMessage.DOAnchorPosY(70f, 0.5f);

        yield return new WaitForSeconds(6f);
        Destroy(_cinematic);
        _camPlayer.gameObject.SetActive(true);
        _boxMessage.gameObject.SetActive(false);
        _boxMessage.DOAnchorPosY(-1000f, 0f);
        _player.DeFreezePlayer();
        _questUI.UIStatus(true);
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