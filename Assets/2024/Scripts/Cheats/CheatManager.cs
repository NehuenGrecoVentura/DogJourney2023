using UnityEngine;
using System.Collections;
using UnityEngine.AI;
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
    private DogEnter _questBroom;
    private BuildTable _questTable;
    private TutorialFishing _questFishing;
    private QuestFence _questRepair;
    private Mail2 _quest2;
    private TableQuest _tableQuest;
    private FishingQuest2 _fishingQuest2;
    private BoxQuest _boxQuest;
    private QuestApple _questApple;
    private QuestSearch _questSearch;
    private bool _quest1Skiped, _quest2Skiped, _quest3Skiped, _quest4Skiped, _quest5Skiped, _questFishing2Skiped, _questBoxSkip, _questAppleSkip, _questSearchSkip = false;


    [Header("TELETRANSOPORT")]
    [SerializeField] Transform _posTeletransport;
    [SerializeField] KeyCode _keyTeletransport = KeyCode.T;

    [Header("MESSAGE BOX")]
    [SerializeField] TMP_Text _textMessage;
    [SerializeField] GameObject _boxMessage;
    [SerializeField] Image _iconMessage;

    private TreeRegenerative[] _allTrees;

    [Header("ZONAS RESTRICTIONS")]
    [SerializeField] GameObject[] _zones;
    [SerializeField] GameObject _bridgeZone2;


    private Bush[] _allBush;
    [SerializeField] Dog _dog;
    [SerializeField] TrolleyWood _trolley;
    [SerializeField] Transform _posDogTele;
    [SerializeField] DogBall _ball;

    private void Awake()
    {
        _inventory = GetComponent<CharacterInventory>();
        _tableQuest = FindObjectOfType<TableQuest>();
        _ordersDogs = FindObjectOfType<OrderDog>();
        _quest1 = FindObjectOfType<BoxWoods>();
        _quest2 = FindObjectOfType<Mail2>();
        _questBroom = FindObjectOfType<DogEnter>();
        _questTable = FindObjectOfType<BuildTable>();
        _questFishing = FindObjectOfType<TutorialFishing>();
        _questRepair = FindObjectOfType<QuestFence>();
        _fishingQuest2 = FindObjectOfType<FishingQuest2>();
        _allTrees = FindObjectsOfType<TreeRegenerative>();
        _allBush = FindObjectsOfType<Bush>();
        _boxQuest = FindObjectOfType<BoxQuest>();
        _questApple = FindObjectOfType<QuestApple>();
        _questSearch = FindObjectOfType<QuestSearch>();
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

        if (Input.GetKeyDown(_keyTeletransport) && Input.GetKey(KeyCode.LeftControl))
        {
            transform.position = _posTeletransport.position;

            foreach (GameObject item in _zones)
            {
                Destroy(item);
            }

            foreach (Bush bush in _allBush)
            {
                bush.enabled = true;
                bush.GetComponent<Collider>().enabled = true;
            }

            _ball.enabled = true;
            _bridgeZone2.gameObject.SetActive(true);
            _inventory.shovelUnlocked = true;
            
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            StartCoroutine(Tele());
        }

        if (!Input.GetKey(_keySkipQuest)) return;

        switch (Input.inputString)
        {
            case "1" when !_quest1Skiped:
                _quest1.FinishQuest();
                foreach (var item in _initialTutorial)
                    Destroy(item);
                _ordersDogs.activeOrders = true;
                _boxMessage.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 125f);
                _textMessage.rectTransform.anchoredPosition = new Vector2(0.2341f, _textMessage.rectTransform.anchoredPosition.y);
                _textMessage.rectTransform.sizeDelta = new Vector2(1030.737f, _textMessage.rectTransform.sizeDelta.y);
                _textMessage.fontSize = 40;
                _textMessage.alignment = TextAlignmentOptions.TopLeft;
                _iconMessage.gameObject.SetActive(false);

                foreach (TreeRegenerative item in _allTrees)
                {
                    item.GetComponent<BoxCollider>().enabled = true;
                    item.enabled = true;
                }

                _quest1Skiped = true;
                break;

            case "2" when _quest1Skiped && !_quest2Skiped:
                _questBroom.ActiveNextQuest();
                _quest2Skiped = true;
                break;

            case "3" when _quest2Skiped && !_quest3Skiped:
                _questTable.CheatSkip();
                Destroy(_tableQuest);
                _quest3Skiped = true;
                break;

            case "4" when _quest3Skiped && !_quest4Skiped:
                _questFishing.CheatSkip();
                _quest4Skiped = true;
                break;

            case "5" when _quest4Skiped && !_quest5Skiped:
                _questRepair.CheatSkip();
                _quest5Skiped = true;
                break;

            case "6" when _quest5Skiped && !_questFishing2Skiped:
                _fishingQuest2.CheatSkip();
                foreach (Bush bush in _allBush)
                {
                    bush.enabled = true;
                    bush.GetComponent<Collider>().enabled = true;
                }
                _questFishing2Skiped = true;
                break;

            case "7" when _questFishing2Skiped && !_questBoxSkip:
                foreach (GameObject item in _zones)
                {
                    Destroy(item);
                }

                _bridgeZone2.gameObject.SetActive(true);
                _inventory.shovelUnlocked = true;
                _boxQuest.CheatSkip();
                _questBoxSkip = true;
                break;

            case "8" when _questBoxSkip && !_questAppleSkip:
                _questApple.CheatSkip();
                _questAppleSkip = true;
                break;

            case "9" when _questAppleSkip && !_questSearchSkip:
                _questSearch.CheatSkip();
                _questSearchSkip = true;
                break;
        }
    }



    private IEnumerator Tele()
    {
        _dog.GetComponent<NavMeshAgent>().enabled = false;
        _trolley.GetComponent<NavMeshAgent>().enabled = false;

        _ordersDogs.activeOrders = true;
        _ball.enabled = true;

        _ball.transform.position = _posDogTele.position;
        _dog.transform.position = _ball.transform.position;

        yield return new WaitForSeconds(1f);
        _dog.GetComponent<NavMeshAgent>().enabled = true;
        _trolley.GetComponent<NavMeshAgent>().enabled = true;
    }
}