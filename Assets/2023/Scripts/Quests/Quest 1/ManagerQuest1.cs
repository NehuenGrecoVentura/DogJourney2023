using UnityEngine;
using TMPro;

public class ManagerQuest1 : MonoBehaviour
{
    [Header("NEXT STEP")]
    [SerializeField] string _thirdTextQuest = "Leave everything in the box next to the truck";
    [SerializeField] GameObject _arrowBox;
    [SerializeField] TMP_Text _textWoods;
    private QuestUI _questUI;

    private CharacterInventory _inventory;
    private Manager _gm;

    [Header("RADAR")]
    [SerializeField] Transform _boxPos;
    private LocationQuest _radar;

    private void Awake()
    {
        _questUI = FindObjectOfType<QuestUI>();
        _inventory = FindObjectOfType<CharacterInventory>();
        _radar = FindObjectOfType<LocationQuest>();
        _gm = FindObjectOfType<Manager>();
    }

    private void Update()
    {
        CheckWoods();
    }

    private void CheckWoods()
    {
        //_textWoods.text = "WOODS: " + _inventory.greenTrees.ToString() + " /" + "10";

        if (_inventory.greenTrees >= 10)
        {
            _questUI.TaskCompleted(2);
            _questUI.AddNewTask(3, _thirdTextQuest);
            _arrowBox.SetActive(true);
            _radar.StatusRadar(true);
            _radar.target = _boxPos;
            _gm.GreenTreesNormal();
            Destroy(this);
        }
    }
}