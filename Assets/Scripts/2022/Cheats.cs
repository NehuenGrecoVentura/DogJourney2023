using UnityEngine;

public class Cheats : MonoBehaviour
{
    [SerializeField] GameObject[] _objectsToDestroy;
    [SerializeField] LocationQuest _map;
    public GameObject canvasMap;

    [Header("BUTTONS CONFIG")]
    [SerializeField] KeyCode _buttonCheatWood;
    [SerializeField] KeyCode _buttonCheatNail;
    [SerializeField] KeyCode _buttonCheatTeletransport;
    [SerializeField] KeyCode _buttonCheatMoney;
    [SerializeField] KeyCode _buttonSkipQuest = KeyCode.L;
    [SerializeField] KeyCode _buttonUpgrade = KeyCode.F4;
    [SerializeField] KeyCode _buttonRopes = KeyCode.F6;

    [Header("INVENTORY CONFIG")]
    Inventory _inventory;

    bool _upgradeActive = false;

    [Header("TELETRANSPORT POS")]
    [SerializeField] Transform _teletransportPos;

    [Header("DOG CONFIG")]
    [SerializeField] GameObject _dog;
    [SerializeField] DogOrder2022 _dogOrder;

    [Header("BRIDGE CONFIG")]
    [SerializeField] GameObject _bridgeConstructed;

    private GateOil _gateOil;

    #region CHEAT QUEST 1
    [Header("QUEST 1")]
    [SerializeField] GameObject[] _objsQuest1;
    [SerializeField] ArrowQuest[] _arrowTrees;
    [SerializeField] GameObject[] _objsNextQuest2;
    [SerializeField] GameObject _bridge;
    TreeBall _treeBall;
    BoxTrunks _quest1;
    CutTree _doorGates;
    AxeTest _axe;
    Mailbox _mailQuest1;
    bool _quest1Cheated = false;
    #endregion

    #region CHEAT QUEST 2
    [Header("QUEST 2")]
    [SerializeField] GameObject[] _objsQuest2;
    MailQuest2 _mailQuest2;
    [HideInInspector] public bool _quest2Cheated = false;
    #endregion

    #region CHEAT QUEST 3
    [Header("QUEST 3")]
    [HideInInspector] public bool _quest3Cheated = false;
    private Quest3 _quest3;
    #endregion

    #region CHEAT QUEST 4
    [Header("QUEST 4")]
    [HideInInspector] public bool _quest4Cheated = false;
    private Quest4 _quest4;
    #endregion

    #region CHEAT QUEST 5
    [Header("QUEST 5")]
    [HideInInspector] public bool _quest5Cheated = false;
    private Quest5 _quest5;
    #endregion

    #region CHEAT QUEST 6
    [Header("QUEST 6")]
    [HideInInspector] public bool _quest6Cheated = false;
    private Quest6 _quest6;
    #endregion

    void Awake()
    {
        _inventory = GetComponent<Inventory>();
        _quest1 = FindObjectOfType<BoxTrunks>();
        _treeBall = FindObjectOfType<TreeBall>();
        _doorGates = FindObjectOfType<CutTree>();
        _axe = FindObjectOfType<AxeTest>();
        _mailQuest1 = FindObjectOfType<Mailbox>();
        _mailQuest2 = FindObjectOfType<MailQuest2>();
        _quest3 = FindObjectOfType<Quest3>();
        _quest4 = FindObjectOfType<Quest4>();
        _quest5 = FindObjectOfType<Quest5>();
        _quest6 = FindObjectOfType<Quest6>();
        _gateOil = FindObjectOfType<GateOil>();
    }

    private void Update()
    {
        Cheat();
    }

    public void Cheat()
    {
        if (Input.GetKeyDown(_buttonSkipQuest) && !_quest1Cheated) FinishQuest1();
        else if (Input.GetKeyDown(_buttonSkipQuest) && _quest2Cheated) FinishQuest2();
        else if (Input.GetKeyDown(_buttonSkipQuest) && _quest3Cheated) FinishQuest3();
        else if (Input.GetKeyDown(_buttonSkipQuest) && _quest4Cheated) FinishQuest4();
        else if (Input.GetKeyDown(_buttonSkipQuest) && _quest5Cheated) FinishQuest5();
        else if (Input.GetKeyDown(_buttonSkipQuest) && _quest6Cheated) FinishQuest6();

        if (Input.GetKeyDown(_buttonCheatWood))
            _inventory.amountWood += 10;

        else if (Input.GetKeyDown(_buttonCheatNail))
            _inventory.amountnails += 10;

        else if (Input.GetKeyDown(_buttonCheatMoney))
            _inventory.money += 10;

        else if (Input.GetKeyDown(_buttonRopes))
            _inventory.amountRopes += 10;

        else if (Input.GetKeyDown(_buttonCheatTeletransport))
        {
            canvasMap.SetActive(true);
            _dogOrder.enabled = true;
            _map.gameObject.SetActive(true);
            transform.position = _teletransportPos.position;
            _bridgeConstructed.gameObject.SetActive(true);
            foreach (var obj in _objectsToDestroy) Destroy(obj.gameObject);
            _dog.transform.position = _teletransportPos.position;
        }

        else if (Input.GetKeyDown(_buttonUpgrade) && !_upgradeActive)
        {
            _inventory.upgrade = true;
            _upgradeActive = true;
        }

        else if (Input.GetKeyDown(_buttonUpgrade) && _upgradeActive)
        {
            _inventory.upgrade = false;
            _upgradeActive = false;
        }
    }


    void FinishQuest1()
    {
        canvasMap.SetActive(true);
        _quest1.FinishQuest();
        _inventory.amountWood = 50;
        _inventory.amountnails = 50;
        Destroy(_mailQuest1);
        foreach (var obj in _objsQuest1) Destroy(obj.gameObject);
        foreach (var arrow in _arrowTrees) Destroy(arrow.gameObject);
        foreach (var obj in _objsNextQuest2) obj.SetActive(true);
        _treeBall.UnlockedDog();
        _doorGates.OpenGates();
        _axe.PickAxe();
        _quest1Cheated = true;
    }

    void FinishQuest2()
    {
        _inventory.amountWood = 50;
        _inventory.amountnails = 50;
        _inventory.upgrade = true;
        _mailQuest2.enabled = true;
        _mailQuest2.LevelCompleted();
        foreach (var obj in _objsQuest2) Destroy(obj.gameObject);
        _quest2Cheated = false;
    }

    void FinishQuest3()
    {
        _quest3.LevelCompleted();
        canvasMap.SetActive(true);
        _quest3Cheated = false;
    }

    void FinishQuest4()
    {
        _quest4._timer = 0;
        _quest4.LevelCompleted();
        canvasMap.SetActive(true);
        _quest4Cheated = false;
    }

    void FinishQuest5()
    {
        _quest5.SkipLevel();
        canvasMap.SetActive(true);
        _quest5Cheated = false;
    }

    void FinishQuest6()
    {
        _quest6.LevelCompleted();
        canvasMap.SetActive(true);
        _quest6Cheated = false;
    }
}