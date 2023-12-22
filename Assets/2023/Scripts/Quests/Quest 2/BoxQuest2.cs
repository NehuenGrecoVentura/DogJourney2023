using UnityEngine;

public class BoxQuest2 : Box
{    
    [SerializeField] Animator _animNPCQuest2;
    private QuestManager _questManager;

    [Header("TEXT QUEST")]
    [SerializeField] string _textPick;
    [SerializeField] string _textNextStage;

    [Header("RADAR")]
    [SerializeField] Transform _nextPos;
    private LocationQuest _radar;

    private void Awake()
    {
        _questManager = FindObjectOfType<QuestManager>();
        _radar = FindObjectOfType<LocationQuest>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var dog = other.GetComponent<Dog>();
        if (dog != null)
        {
            _radar.target = _nextPos;
            inventory.upgradeLoot = true;
            _questManager.FirstSuccess(_textPick);
            _questManager.InitialSecondPhase(_textNextStage);
            _animNPCQuest2.SetBool("Quest", false);
            Destroy(gameObject);
        }
    }
}