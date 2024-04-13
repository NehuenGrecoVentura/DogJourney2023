using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CheatManager : MonoBehaviour
{
    [SerializeField] KeyCode _keyMoney = KeyCode.Backspace;
    [SerializeField] KeyCode _keyNails = KeyCode.F1;
    [SerializeField] KeyCode _keyGreenTrees = KeyCode.F2;
    [SerializeField] KeyCode _keyPurpleTrees = KeyCode.F3;
    [SerializeField] KeyCode _keyUpgrade = KeyCode.F4;
    [SerializeField] KeyCode _keySkipQuest = KeyCode.L;

    [SerializeField] GameObject[] _initialTutorial;
    [SerializeField] GameObject[] _quest2ObjsToDestroy;
    private CharacterInventory _inventory;
    private OrderDog _ordersDogs;
    private BoxWoods _quest1;
    private Mail2 _quest2;
    private bool _quest1Skiped, _quest2Skiped = false;

    [Header("TELETRANSOPORT")]
    [SerializeField] Transform _posTeletransport;
    [SerializeField] KeyCode _keyTeletransport = KeyCode.T;

    [Header("MESSAGE BOX")]
    [SerializeField] TMP_Text _textMessage;
    [SerializeField] GameObject _boxMessage;
    [SerializeField] Image _iconMessage;

    private void Awake()
    {
        _inventory = GetComponent<CharacterInventory>();

        _ordersDogs = FindObjectOfType<OrderDog>();
        _quest1 = FindObjectOfType<BoxWoods>();
        _quest2 = FindObjectOfType<Mail2>();
    }

    void Update()
    {
        EjecuteCheat();
    }

    public void EjecuteCheat()
    {
        if (Input.GetKeyDown(_keyMoney)) _inventory.money += 100;
        if (Input.GetKeyDown(_keyNails)) _inventory.nails += 10;
        if (Input.GetKeyDown(_keyGreenTrees)) _inventory.greenTrees += 10;
        if (Input.GetKeyDown(_keyPurpleTrees)) _inventory.purpleTrees += 10;

        if (Input.GetKeyDown(_keyUpgrade))
        {
            if (!_inventory.upgradeLoot) _inventory.upgradeLoot = true;
            else _inventory.upgradeLoot = false;
        }

        if (Input.GetKeyDown(_keyTeletransport))
            transform.position = _posTeletransport.position;

        if (Input.GetKeyDown(_keySkipQuest) && Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (!_quest1Skiped)
            {
                _quest1.FinishQuest();

                foreach (var item in _initialTutorial)
                    if (item.activeSelf || !item.activeSelf) Destroy(item);

                _ordersDogs.activeOrders = true;

                _boxMessage.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 125f);
                _textMessage.rectTransform.anchoredPosition = new Vector2(0.2341f, _textMessage.rectTransform.anchoredPosition.y);
                _textMessage.rectTransform.sizeDelta = new Vector2(1030.737f, _textMessage.rectTransform.sizeDelta.y);
                _textMessage.fontSize = 40;
                _textMessage.alignment = TextAlignmentOptions.TopLeft;
                _iconMessage.gameObject.SetActive(false);

                _quest1Skiped = true;
            }
        }

        if (Input.GetKeyDown(_keySkipQuest) && Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (_quest1Skiped && !_quest2Skiped)
            {
                _quest2.FinishQuest();
                _inventory.upgradeLoot = true;

                foreach (var item in _quest2ObjsToDestroy)
                    if (item.activeSelf || !item.activeSelf) Destroy(item);

                _quest2Skiped = true;
            }
        }
    }
}