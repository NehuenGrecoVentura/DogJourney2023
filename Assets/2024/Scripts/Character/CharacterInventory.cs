using UnityEngine;
using System.Collections;
using TMPro;

public class CharacterInventory : MonoBehaviour
{
    [Header("ITEMS")]
    public int greenTrees = 0;
    public int purpleTrees = 0;
    public int nails = 0;
    public int money = 0;
    public int ropes = 0;
    public int oils = 0;
    public int seeds = 0;
    public int flowers = 0;
    public int apples = 0;
    public int fishes = 0;
    public int specialFishes = 0;
    public int baits = 0;
    public bool upgradeLoot = false;

    [Header("MY TOOLS")]
    [SerializeField] GameObject _myShovel;
    [SerializeField] GameObject _myAxe;
    public bool shovelSelected = false;
    public bool shovelUnlocked = false;
    
    [Header("CANVAS")]
    [SerializeField] KeyCode _keyInventory = KeyCode.Tab;
    [SerializeField] float _inventoryTimeOpen = 5f;
    [SerializeField] GameObject _canvasInventory;
    [SerializeField] TMP_Text[] _textsInventory;
    [SerializeField] Animator _anim;
    private Coroutine _inventoryCoroutine;
    private bool _inventoryOpen = false;

    private void Start()
    {
        _canvasInventory.SetActive(false);
        _myShovel.gameObject.SetActive(false);
    }

    private void Update()
    {
        Inventory();
    }

    private void Inventory()
    {
        if (Input.GetKeyDown(_keyInventory))
        {
            if (!_inventoryOpen)
            {
                StopCoroutine(HideInventory());
                _canvasInventory.SetActive(true);
                _inventoryOpen = true;

                if (_inventoryCoroutine != null)
                    StopCoroutine(_inventoryCoroutine);

                _inventoryCoroutine = StartCoroutine(HideInventory());
            }
            else
            {
                StartCoroutine(Close());

                if (_inventoryCoroutine != null)
                    StopCoroutine(_inventoryCoroutine);
            }
        }

        AmountItem(0, greenTrees);
        AmountItem(1, purpleTrees);
        AmountItem(2, nails);
        AmountItem(3, ropes);
        AmountItem(4, money);
        AmountItem(5, fishes);
        AmountItem(6, baits);
        AmountItem(7, specialFishes);


        if (Input.GetKeyDown(KeyCode.X))
        {
            if (shovelUnlocked)
            {
                if (!shovelSelected)
                {
                    _myShovel.gameObject.SetActive(true);
                    _myAxe.gameObject.SetActive(false);
                    shovelSelected = true;
                }

                else
                {
                    _myShovel.gameObject.SetActive(false);
                    _myAxe.gameObject.SetActive(true);
                    shovelSelected = false;

                }
            }
        }
    }

    private void AmountItem(int indexText, int item)
    {
        _textsInventory[indexText].text = "x " + item.ToString();
    }

    private IEnumerator Close()
    {
        _anim.Play("Close");
        yield return new WaitForSeconds(0.2f);
        _canvasInventory.SetActive(false);
        _inventoryOpen = false;
        StopCoroutine(Close());
    }

    private IEnumerator HideInventory()
    {
        _anim.Play("Open");
        yield return new WaitForSeconds(_inventoryTimeOpen);
        _anim.Play("Close");
        yield return new WaitForSeconds(1.5f);  
        _canvasInventory.SetActive(false);
        _inventoryOpen = false;
    }
}