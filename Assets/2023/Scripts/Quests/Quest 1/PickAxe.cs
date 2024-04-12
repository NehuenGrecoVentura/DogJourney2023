using UnityEngine;
using DG.Tweening;

public class PickAxe : MonoBehaviour, IPick
{
    [Header("INTERACTIVE CONFIG")]
    [SerializeField] GameObject _iconInteractive;
    [SerializeField] KeyCode _keyInteractive = KeyCode.F;
    [SerializeField] CinematicTree _cinematicObj;
    [SerializeField] Animator[] _animGates;

    [Header("AXE")]
    [SerializeField] GameObject[] _axes;

    [Header("TEXT QUEST")]
    [SerializeField] string _text = "Pick the axe";
    [SerializeField] string _nextText = "Collect the woods";


    private ManagerQuest1 _managerQuest;
    private QuestUI _questUI;
    private Collider _col;

    [Header("RADAR")]
    [SerializeField] Transform _nextPos;
    private LocationQuest _radar;

    private void Awake()
    {
        _col = GetComponent<Collider>();
        _questUI = FindObjectOfType<QuestUI>();
        _managerQuest = FindObjectOfType<ManagerQuest1>();
        _radar = FindObjectOfType<LocationQuest>();
    }

    private void Start()
    {
        _col.enabled = false;
        _iconInteractive.SetActive(false);
        _iconInteractive.transform.localScale = new Vector3(0, 0, 0);
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
            _questUI.TaskCompleted(1);
            _questUI.AddNewTask(2, _nextText);
            _managerQuest.enabled = true;
            _cinematicObj.gameObject.SetActive(true);
            Destroy(_axes[1]);
            Destroy(_iconInteractive);
            Destroy(this);
        }

        else
        {
            _iconInteractive.SetActive(true);
            _iconInteractive.transform.DOScale(1.25f, 0.5f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) _iconInteractive.transform.DOScale(0f, 0.5f);
    } 
}