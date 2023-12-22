using UnityEngine;

public class PickAxe : MonoBehaviour, IPick
{
    [Header("INTERACTIVE CONFIG")]
    [SerializeField] GameObject _iconInteractive;
    [SerializeField] KeyCode _keyInteractive = KeyCode.F;
    [SerializeField] CinematicTree _cinematicObj;
    [SerializeField] Animator[] _animGates;

    [Header("AXE")]
    [SerializeField] GameObject _arrow;
    [SerializeField] GameObject[] _axes;

    [Header("TEXT QUEST")]
    [SerializeField] string _text = "Pick the axe";
    [SerializeField] string _nextText = "Collect the woods";

    private ManagerQuest1 _managerQuest;
    private QuestManager _questManager;
    private Collider _col;

    [Header("RADAR")]
    [SerializeField] Transform _nextPos;
    private LocationQuest _radar;

    private void Awake()
    {
        _col = GetComponent<Collider>();

        _questManager = FindObjectOfType<QuestManager>();
        _managerQuest = FindObjectOfType<ManagerQuest1>();
        _radar = FindObjectOfType<LocationQuest>();
    }

    private void Start()
    {
        _col.enabled = false;
        _iconInteractive.SetActive(false);
        _axes[0].SetActive(false);

        foreach (var anim in _animGates)
            anim.enabled = false;
    }

    public void Pick()
    {
        if (Input.GetKeyDown(_keyInteractive))
        {
            foreach (var anim in _animGates)
                anim.enabled = true;

            _radar.target = _nextPos;
            _axes[0].SetActive(true);
            _questManager.FirstSuccess(_text);
            _questManager.InitialSecondPhase(_nextText);
            _managerQuest.enabled = true;
            _cinematicObj.gameObject.SetActive(true);
            Destroy(_axes[1]);
            Destroy(_arrow);
            Destroy(_iconInteractive);
            Destroy(this);
        }

        else _iconInteractive.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) _iconInteractive.SetActive(false);
    }
}