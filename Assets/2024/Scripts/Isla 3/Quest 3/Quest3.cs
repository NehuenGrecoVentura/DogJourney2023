using UnityEngine;
using TMPro;

public class Quest3 : MonoBehaviour
{
    [SerializeField] TMP_Text _textTrunks;
    [SerializeField] GameObject[] _stagesQuest;
    [SerializeField] GameObject _mapGO;
    private GManager _gm;
    public bool isQuest3;
    public int totalAmount = 10;

    [Header("INTERACTIVE CONFIG")]
    [SerializeField] KeyCode _buttonInteractive = KeyCode.F;
    [SerializeField] GameObject _iconInteractive;

    [Header("NEXT QUEST")]
    [SerializeField] GameObject _iconQuest4;
    [SerializeField] GameObject _colQuest4;
    [SerializeField] Transform _nextPos;
    LocationQuest _map;
    private Inventory _inventory;
    private GateOil _gateOil;

    private void Awake()
    {
        _gm = FindObjectOfType<GManager>();
        _map = FindObjectOfType<LocationQuest>();
        _inventory = FindObjectOfType<Inventory>();
        _gateOil = FindObjectOfType<GateOil>();
    }

    private void Start()
    {
        isQuest3 = true;
        _stagesQuest[1].SetActive(false);
        _stagesQuest[2].SetActive(false);
        _iconInteractive.SetActive(false);
        _gateOil.StateTrees(true, 1);
    }

    private void Update()
    {
        _textTrunks.text = "Reddish trunks " + _inventory.amountPurple.ToString() + "/" + totalAmount.ToString();
        if (_inventory.amountPurple >= totalAmount) StageCompleted();
    }

    public void StageCompleted() // TERMINO DE JUNTAR TODAS LAS MADERAS ROJAS Y SE ACTUALIZA LA UI DE QUEST.
    {
        _mapGO.SetActive(true);
        _stagesQuest[1].SetActive(true);
        _stagesQuest[2].SetActive(true);
        Destroy(_stagesQuest[0]);
        isQuest3 = false;
    }

    public void LevelCompleted()
    {
        _iconQuest4.SetActive(true);
        _gm.LevelCompleted();
        _colQuest4.SetActive(true);
        _map.target = _nextPos;
        _inventory.amountOil++;
        _inventory.amountPurple -= totalAmount;

        if (_inventory.amountPurple <= 0)
            _inventory.amountPurple = 0;

        Destroy(_stagesQuest[3]);
        Destroy(_iconInteractive);
        Destroy(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character2022>();
        if (player != null)
        {
            if (_inventory.amountPurple >= totalAmount)
            {
                if (!Input.GetKeyDown(_buttonInteractive))
                    _iconInteractive.SetActive(true);

                else LevelCompleted();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character2022>();
        if (player != null) _iconInteractive.SetActive(false);
    }
}