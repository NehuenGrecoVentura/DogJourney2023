using UnityEngine;
using TMPro;

public class Quest6 : MonoBehaviour
{
    [Header("QUEST CONFIG")]
    [SerializeField] GameObject[] _canvasQuest6;
    [SerializeField] int _amountGreenTrees = 10;
    [SerializeField] int _amountNails = 30;
    public int totalRedTrees = 10;
    [HideInInspector] public bool isQuest6 = true;
    private Inventory _inventory;

    [Header("TEXTS CONFIG")]
    [SerializeField] TMP_Text[] _textsQuests;

    [Header("INTERACTIVE CONFIG")]
    [SerializeField] GameObject _iconInteractive;
    [SerializeField] KeyCode _buttonInteractive = KeyCode.F;

    [Header("WIN")]
    [SerializeField] GameObject[] _obsToDestroy;
    [SerializeField] Transform _nextPos;
    private LocationQuest _map;
    private GManager _gm;
    private Cheats _cheats;

    private void Awake()
    {
        _inventory = FindObjectOfType<Inventory>();
        _gm = FindObjectOfType<GManager>();
        _cheats = FindObjectOfType<Cheats>();
        _map = FindObjectOfType<LocationQuest>();
    }

    void Start()
    {
        InitialDefault();
    }

    void Update()
    {
        TextsQuests();
        StageGreenTrees();
        StageNails();
        StageRedTrees();
        BackToHouse();
    }

    void InitialDefault()
    {
        _cheats._quest6Cheated = true;
        isQuest6 = true;
        _iconInteractive.SetActive(false);

        _canvasQuest6[0].SetActive(true); // Nombre de la quest
        _canvasQuest6[1].SetActive(true); // Green trees
        _canvasQuest6[2].SetActive(true); // Green trees incompleteto
        _canvasQuest6[3].SetActive(false); // Green trees completos
        _canvasQuest6[4].SetActive(true); // Red trees
        _canvasQuest6[5].SetActive(true); // Red trees incompleto
        _canvasQuest6[6].SetActive(false); // Red trees completos
        _canvasQuest6[7].SetActive(true); // Nails
        _canvasQuest6[8].SetActive(true); // Nails incompletos
        _canvasQuest6[9].SetActive(false); // Nails completos
        _canvasQuest6[10].SetActive(false); // Back to home
    }

    void TextsQuests()
    {
        _textsQuests[0].text = "Green Trees: " + _inventory.amountWood.ToString() + "/" + _amountGreenTrees.ToString();
        _textsQuests[1].text = "Purple Trees: " + _inventory.amountPurple.ToString() + "/" + totalRedTrees.ToString();
        _textsQuests[2].text = "Nails: " + _inventory.amountnails.ToString() + "/" + _amountNails.ToString();
    }

    void StageGreenTrees()
    {
        if (_inventory.amountWood >= _amountGreenTrees)
        {
            _canvasQuest6[2].SetActive(false);
            _canvasQuest6[3].SetActive(true);
        }

        else
        {
            _canvasQuest6[2].SetActive(true);
            _canvasQuest6[3].SetActive(false);
        }
    }

    public void StageRedTrees()
    {
        if (_inventory.amountPurple >= totalRedTrees)
        {
            _canvasQuest6[5].SetActive(false);
            _canvasQuest6[6].SetActive(true);
            isQuest6 = false;
        }

        else
        {
            _canvasQuest6[5].SetActive(true);
            _canvasQuest6[6].SetActive(false);
            isQuest6 = true;
        }
    }

    void StageNails()
    {
        if (_inventory.amountnails >= _amountNails)
        {
            _canvasQuest6[8].SetActive(false);
            _canvasQuest6[9].SetActive(true);
        }

        else
        {
            _canvasQuest6[8].SetActive(true);
            _canvasQuest6[9].SetActive(false);
        }
    }

    void BackToHouse()
    {
        if (_inventory.amountnails >= _amountNails && _inventory.amountPurple >= totalRedTrees && _inventory.amountWood >= _amountGreenTrees)
            _canvasQuest6[10].SetActive(true);

        else _canvasQuest6[10].SetActive(false);
    }

    public void LevelCompleted()
    {
        _gm.LevelCompleted();
        _map.target = _nextPos;
        _inventory.amountOil++;
        _inventory.amountnails -= _amountNails;
        _inventory.amountPurple -= totalRedTrees;
        _inventory.amountWood = -_amountGreenTrees;

        if (_inventory.amountnails <= 0) _inventory.amountnails = 0;
        if (_inventory.amountPurple <= 0) _inventory.amountPurple = 0;
        if (_inventory.amountWood <= 0) _inventory.amountWood = 0;

        foreach (var item in _obsToDestroy)
        {
            Destroy(item.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character2022>();
        if (player != null)
        {
            if (_inventory.amountnails >= _amountNails && _inventory.amountPurple >= totalRedTrees && _inventory.amountWood >= _amountGreenTrees)
            {
                if (!Input.GetKeyDown(_buttonInteractive))
                    _iconInteractive.SetActive(true);

                else LevelCompleted();
            }

            else _iconInteractive.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character2022>();
        if (player != null) _iconInteractive.SetActive(false);
    }
}