using UnityEngine;
using UnityEngine.UI;

public class TableQuest : NPCManager
{
    [SerializeField] Button _buttonConfirm;
    [SerializeField] int _totalWoods = 10;
    [SerializeField] int _totalNails = 10;
    [SerializeField] Transform _posRadar;
    private QuestUI _questUI;
    private CharacterInventory _inventory;
    private Collider _myCol;
    private LocationQuest _radar;

    private void Awake()
    {
        _myCol = GetComponent<Collider>();
        _questUI = FindObjectOfType<QuestUI>();
        _inventory = FindObjectOfType<CharacterInventory>();
        _radar = FindObjectOfType<LocationQuest>();
    }

    private void Start()
    {
        _myCol.enabled = false;
        _buttonConfirm.onClick.AddListener(() => Confirm());
    }

    public void Confirm()
    {
        _dialogue.Close();
        _questUI.ActiveUIQuest("Making the table", "Get wood (" + _inventory.greenTrees.ToString() + "/" + _totalWoods.ToString() + ")",
            "Get nails (" + _inventory.nails.ToString() + "/" + _totalNails.ToString() + ")", 
            string.Empty);
        _buttonConfirm.gameObject.SetActive(false);
    }
}