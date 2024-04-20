using UnityEngine;
using TMPro;

public class Quest5 : MonoBehaviour
{
    [SerializeField] GameObject[] _canvasQuest;
    [SerializeField] TMP_Text[] _textsQuest;
    [SerializeField] int _maxMoney = 500;
    [SerializeField] int _maxNails = 250;

    [Header("INTERACTIVE CONFIG")]
    [SerializeField] KeyCode _buttonInteractive = KeyCode.F;
    [SerializeField] GameObject _iconInteractive;

    [Header("REFS")]
    private Manager _gm;
    private Inventory _inventory;

    [Header("WIN")]
    [SerializeField] GameObject[] _objsToDestroy;
    [SerializeField] GameObject[] _nextQuest;
    [SerializeField] Transform _nextPos;
    private LocationQuest _map;

    private void Awake()
    {
        _inventory = FindObjectOfType<Inventory>();
        _gm = FindObjectOfType<Manager>();
        _map = FindObjectOfType<LocationQuest>();
    }

    private void Start()
    {
        _iconInteractive.SetActive(false);
        _canvasQuest[0].SetActive(true);
        _canvasQuest[1].SetActive(true);
        _canvasQuest[2].SetActive(false);
        _canvasQuest[3].SetActive(true);
        _canvasQuest[4].SetActive(true);
        _canvasQuest[5].SetActive(false);
        _canvasQuest[6].SetActive(false);
    }

    private void Update()
    {
        _textsQuest[0].text = "Money: " + _inventory.money.ToString() + "/" + _maxMoney.ToString();
        _textsQuest[1].text = "Nails: " + _inventory.amountnails.ToString() + "/" + _maxNails.ToString();

        if (_inventory.money >= _maxMoney)
        {
            _canvasQuest[1].SetActive(false);
            _canvasQuest[2].SetActive(true);
        }

        else
        {
            _canvasQuest[1].SetActive(true);
            _canvasQuest[2].SetActive(false);
        }

        if (_inventory.amountnails >= _maxNails)
        {
            _canvasQuest[4].SetActive(false);
            _canvasQuest[5].SetActive(true);
        }

        else
        {
            _canvasQuest[4].SetActive(true);
            _canvasQuest[5].SetActive(false);
        }

        if (_inventory.amountnails >= _maxNails && _inventory.money >= _maxMoney)
            _canvasQuest[6].SetActive(true);

        else _canvasQuest[6].SetActive(false);
    }

    public void SkipLevel()
    {
        _gm.QuestCompleted();
        _inventory.amountOil++;
        foreach (var item in _objsToDestroy)
            Destroy(item.gameObject);

        _inventory.money -= _maxMoney;
        _inventory.amountnails -= _maxNails;

        if (_inventory.money <= 0)
            _inventory.money = 0;

        if (_inventory.amountnails <= 0)
            _inventory.amountnails = 0;

        foreach (var quest6 in _nextQuest)
            quest6.SetActive(true);

        _map.target = _nextPos;
        Destroy(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            if (_inventory.money >= _maxMoney && _inventory.amountnails >= _maxNails)
            {
                if(!Input.GetKeyDown(_buttonInteractive)) 
                    _iconInteractive.SetActive(true);

                else SkipLevel();
            }

            else _iconInteractive.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) _iconInteractive.SetActive(false);
    }
}