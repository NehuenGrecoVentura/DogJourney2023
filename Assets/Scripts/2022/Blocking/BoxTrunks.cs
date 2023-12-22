using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BoxTrunks : MonoBehaviour
{
    [SerializeField] GameObject _trunksInBox;
    [SerializeField] GameObject _arrowQuest;

    [Header("UI STATE QUEST")]
    [SerializeField] UIQuestStatus _status;
    [SerializeField] string _nextDescription;

    [Header("TEXTS CONFIG")]
    [SerializeField] GameObject[] _textQuest;
    [SerializeField] FirstMessage _notificationUnlockedItem;
    [SerializeField] TMP_Text _textNotification;

    [Header("INTERACTIVE CONFIG")]
    [SerializeField] GameObject _iconInteractive;
    [SerializeField] KeyCode _buttonInteractive = KeyCode.F;

    [Header("AMOUNT CONFIG")]
    [SerializeField] int _amountJob;
    [SerializeField] int _moneyJob;

    [Header("REFS CONFIG")]
    private Inventory _inventory;
    private Collider _col;
    private GManager _gameManager;
    private TextTutorialQuest _uiQuest;
    private Truck _truck;
    private SliderTrunks _sliderTrunks;

    [Header("ICON MAP CONFIG")]
    [SerializeField] GameObject _mapGO;
    [SerializeField] Transform _boxLocation;
    [SerializeField] Transform _nextLocation;
    private LocationQuest _map;

    [SerializeField] Animator _animTruck;
    [SerializeField] GameObject _uiTrunksQuest;

    [Header("OBJS TO ACTIVATE")]
    [SerializeField] Button _buttonSellInMarket;
    [SerializeField] Button _buttonRopes;
  //  [SerializeField] Image _iconRopeBlocked;
    [SerializeField] Sprite _iconRopeUncloked;

    void Awake()
    {
        _col = GetComponent<Collider>();
        _inventory = FindObjectOfType<Inventory>();
        _gameManager = FindObjectOfType<GManager>();
        _uiQuest = FindObjectOfType<TextTutorialQuest>();
        _truck = FindObjectOfType<Truck>();
        _map = FindObjectOfType<LocationQuest>();
        _sliderTrunks = FindObjectOfType<SliderTrunks>();
    }

    void Start()
    {
        _iconInteractive.gameObject.SetActive(false);
        _notificationUnlockedItem.gameObject.SetActive(false);
        _arrowQuest.gameObject.SetActive(false);  
        _trunksInBox.gameObject.SetActive(false);
        _truck.enabled = false;       
    }

    void Update()
    {
        if (_inventory.amountWood >= _amountJob)
        {
            _status.nextDescription = _nextDescription;
            _status.completed = true;
            _arrowQuest.gameObject.SetActive(true);
            _mapGO.SetActive(true);
            _map.target = _boxLocation;
        }

        else _arrowQuest.gameObject.SetActive(false);
    }

    public void FinishQuest()
    {

        _buttonRopes.GetComponent<Image>().sprite = _iconRopeUncloked;

        _buttonSellInMarket.gameObject.SetActive(true); // Una vez terminada la primera quest, se activa el boton de poder vender la madera en las tiendas
        _animTruck.enabled = true;
        _map.target = _nextLocation;
        _truck.enabled = true;
        _textNotification.text = "TREE COLLECTABLE UNLOCKED.";
        Destroy(_uiQuest);
        _inventory.amountWood -= _amountJob;
        _iconInteractive.gameObject.SetActive(false);
        _trunksInBox.gameObject.SetActive(true);
        _gameManager.LevelCompleted();
        foreach (var textQuest in _textQuest) Destroy(textQuest.gameObject);
        Destroy(_col);
        _arrowQuest.gameObject.SetActive(false);
        _inventory.money += _moneyJob;
        Destroy(_uiTrunksQuest);
        Destroy(_sliderTrunks);
        _notificationUnlockedItem.gameObject.SetActive(true);
        Destroy(_status.gameObject);
        Destroy(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character2022>();
        if (player != null)
        {
            if (!Input.GetKeyDown(_buttonInteractive) && _inventory.amountWood >= _amountJob)
                _iconInteractive.gameObject.SetActive(true);

            else if (Input.GetKeyDown(_buttonInteractive) && _inventory.amountWood >= _amountJob)
                FinishQuest();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character2022>();
        if (player != null) _iconInteractive.gameObject.SetActive(false);
    }
}