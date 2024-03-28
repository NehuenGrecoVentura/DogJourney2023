using UnityEngine;

public class BoxQuest2 : Box
{    
    [SerializeField] Animator _animNPCQuest2;
    private QuestUI _questUI;

    [Header("TEXT QUEST")]
    [SerializeField] string _textPick;
    [SerializeField] string _textNextStage;

    [Header("RADAR")]
    [SerializeField] Transform _nextPos;
    private LocationQuest _radar;

    [Header("AUDIO")]
    [SerializeField] AudioClip _soundNotification;
    private AudioSource _myAudio;

    private void Awake()
    {
        _myAudio = GetComponent<AudioSource>();

        _questUI = FindObjectOfType<QuestUI>();
        _radar = FindObjectOfType<LocationQuest>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var dog = other.GetComponent<Dog>();
        if (dog != null)
        {
            _myAudio.PlayOneShot(_soundNotification);
            _radar.target = _nextPos;
            inventory.upgradeLoot = true;
            //_questManager.FirstSuccess(_textPick);
            //_questManager.InitialSecondPhase(_textNextStage);
            _questUI.TaskCompleted(1);
            _questUI.AddNewTask(3, _textNextStage);
            _animNPCQuest2.SetBool("Quest", false);
            Destroy(gameObject);
        }
    }
}