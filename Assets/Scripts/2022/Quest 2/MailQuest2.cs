using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MailQuest2 : MonoBehaviour
{

    private Collider _col;
    private Inventory _inventory;
    private GManager _gm;
    private GoToQuest3 _goToQuest3;

    [Header("INTERACTIVE CONFIG")]
    [SerializeField] GameObject _iconInteractive;
    [SerializeField] KeyCode _buttonInteractive = KeyCode.F;

    [Header("OBJECTS TO DESTROY")]
    [SerializeField] GameObject[] _objectsToDestroy;

    [Header("NEXT QUEST CONFIG")]
    [SerializeField] LocationQuest _map;
    [SerializeField] Transform _nextLocation;
    [SerializeField] TMP_Text _textQuest;
    [SerializeField] string _nextText;
    [SerializeField] GameObject _nextQuestCanvas;

    [SerializeField] BoxUpgradeQuest _box;

    [Header("BUTTON ROPE")]
    [SerializeField] Button _buttonRope;
    [SerializeField] Image _backgroundButtonRope;
    [SerializeField] MeshRenderer _iconConstructRopeBlocked;
    [SerializeField] Material _iconConstructRopeUnlocked;
    [SerializeField] Color _unlockColor;
    InfoIcon2 _bridge2;

    void Awake()
    {
        _col = GetComponent<Collider>();
        _inventory = FindObjectOfType<Inventory>();
        _gm = FindObjectOfType<GManager>();
        _bridge2 = FindObjectOfType<InfoIcon2>();
        _goToQuest3 = FindObjectOfType<GoToQuest3>();
    }

    void Start()
    {
        _iconInteractive.SetActive(false);
    }

    public void LevelCompleted()
    {
        _iconConstructRopeBlocked.material = _iconConstructRopeUnlocked;
        UnlockButtonRope();
        _gm.LevelCompleted();
        _map.target = _nextLocation;
        foreach (var obj in _objectsToDestroy) Destroy(obj.gameObject);
        _textQuest.text = _nextText;
        Destroy(_col);
        _inventory.upgrade = true;
        Destroy(this);
    }

    void UnlockButtonRope()
    {
        _bridge2.UnlockBridge2();
        _goToQuest3.gameObject.SetActive(true);
        _buttonRope.enabled = true;
        _backgroundButtonRope.color = _unlockColor;
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character2022>();
        if (player != null)
        {
            if (!Input.GetKeyDown(_buttonInteractive) && _box.boxPicked)
                _iconInteractive.SetActive(true);

            else if (Input.GetKeyDown(_buttonInteractive) && _box.boxPicked)
                LevelCompleted(); 
        }
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character2022>();
        if (player != null)
        {
            if (!Input.GetKeyDown(_buttonInteractive) && _box.boxPicked)
                _iconInteractive.SetActive(true);

            else if (Input.GetKeyDown(_buttonInteractive) && _box.boxPicked)
                LevelCompleted();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character2022>();
        if (player != null) _iconInteractive.SetActive(false);
    }
}