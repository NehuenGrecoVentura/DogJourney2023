using UnityEngine;
using TMPro;

public class ManagerQuest1 : MonoBehaviour
{
    [SerializeField] string _thirdTextQuest = "Leave it in front of his house";
    [SerializeField] GameObject _arrowBox;
    [SerializeField] TMP_Text _textWoods;

    private CharacterInventory _inventory;
    private QuestManager _questManager;
    private Manager _gm;

    [Header("RADAR")]
    [SerializeField] Transform _boxPos;
    private LocationQuest _radar;


    private void Awake()
    {
        _inventory = FindObjectOfType<CharacterInventory>();
        _questManager = FindObjectOfType<QuestManager>();
        _radar = FindObjectOfType<LocationQuest>();
        _gm = FindObjectOfType<Manager>();
    }

    private void Update()
    {
        CheckWoods();
    }

    private void CheckWoods()
    {

        _textWoods.text = "WOODS: " + _inventory.greenTrees.ToString() + " /" + "10";

        if (_inventory.greenTrees >= 10)
        {
            _arrowBox.SetActive(true);
            _questManager.SecondSuccess(_thirdTextQuest);
            _radar.StatusRadar(true);
            _radar.target = _boxPos;
            _gm.GreenTreesNormal();
            Destroy(this);
        }
    }
}