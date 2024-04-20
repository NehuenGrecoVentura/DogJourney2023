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

    [SerializeField] TMP_Text _txtTask;

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
        _txtTask.text = "Collect the woods " + "(" + _inventory.greenTrees.ToString() + "/5)";

        if (_inventory.greenTrees >= 5)
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