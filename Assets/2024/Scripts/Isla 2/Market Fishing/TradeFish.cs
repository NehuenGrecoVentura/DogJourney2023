using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class TradeFish : MonoBehaviour
{
    [SerializeField] bool[] _trades;
    [SerializeField] GameObject _canvas;
    private CharacterInventory _inventory;
    private Collider _myCol;
    private MenuPause _pause;
    private Character _player;

    [Header("INTERACT")]
    [SerializeField] GameObject _iconInteract;
    [SerializeField] KeyCode _keyInteract = KeyCode.F;

    [Header("SPRITES")]
    [SerializeField] Sprite _spriteCommonFish;
    [SerializeField] Sprite _spriteSpecialFish;
    [SerializeField] Sprite _spriteFlowers;
    [SerializeField] Sprite _spriteNail;
    [SerializeField] Sprite _spriteApple;
    [SerializeField] Sprite _spriteBait;

    [Header("TEXTS INVENTORY")]
    [SerializeField] TMP_Text _txtInvFishCommon;
    [SerializeField] TMP_Text _txtInvSpecialFish;
    [SerializeField] TMP_Text _txtInvBait;

    [Header("TEXT VALUES")]
    [SerializeField] TMP_Text _txtValueBait;
    [SerializeField] TMP_Text _txtValueApple;
    [SerializeField] TMP_Text _txtValueCommonFish;
    [SerializeField] TMP_Text _txtValueSpecialFish;
    [SerializeField] TMP_Text _txtValueNormalFlower;
    [SerializeField] TMP_Text _txtValueSpecialFlower;
    [SerializeField] TMP_Text _txtValueNail;

    [Header("TEXT AMOUNT")]
    [SerializeField] TMP_Text _txtAmountBait;
    [SerializeField] TMP_Text _txtAmountApple;
    [SerializeField] TMP_Text _txtAmountCommonFish;
    [SerializeField] TMP_Text _txtAmountSpecialFish;
    [SerializeField] TMP_Text _txtAmountNormalFlower;
    [SerializeField] TMP_Text _txtAmountSpecialFlower;
    [SerializeField] TMP_Text _txtAmountNail;

    private void Awake()
    {
        _myCol = GetComponent<Collider>();

        _inventory = FindObjectOfType<CharacterInventory>();
        _pause = FindObjectOfType<MenuPause>();
        _player = FindObjectOfType<Character>();
    }

    private void Start()
    {
        _iconInteract.transform.DOScale(0f,0f);
        _canvas.SetActive(false);
    }

    private void Update()
    {
        CheckItemsInInventory();
    }

    private void CheckItemsInInventory()
    {
        if (_canvas.activeSelf)
        {
            _txtInvFishCommon.text = "x " + _inventory.fishes.ToString();
            _txtInvSpecialFish.text = "x " + _inventory.specialFishes.ToString();
            _txtInvBait.text = "x " + _inventory.baits.ToString();
        }

        else return;
    }

    private void OpenMarket()
    {
        _iconInteract.transform.DOScale(0f, 0.5f);
        _myCol.enabled = false;
        _player.FreezePlayer();
        _pause.Freeze();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        _canvas.SetActive(true);
    }
    
    public void CloseMarket()
    {
        _myCol.enabled = true;
        _player.DeFreezePlayer();
        _pause.Defreeze();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _canvas.SetActive(false);
    }

    private void TradeItem(int index)
    {
        if (index >= 0 && index < _trades.Length)
        {
            for (int i = 0; i < _trades.Length; i++)
            {
                _trades[i] = (i == index);
            }
        }
        else return;
    }

    public void TradeCommonFish()
    {
        TradeItem(0);

        _txtValueBait.text = "$ 20";
        _txtAmountBait.text = "x 1";
        _txtAmountBait.GetComponentInChildren<Image>().sprite = _spriteBait;

        _txtValueNormalFlower.text = "$ 10";
        _txtAmountNormalFlower.text = "x 1 = 3";
        _txtAmountNormalFlower.GetComponentInChildren<Image>().sprite = _spriteFlowers;

        _txtValueSpecialFlower.text = "$ 50";
        _txtAmountSpecialFlower.text = "x 1 = 1";
        _txtAmountSpecialFlower.GetComponentInChildren<Image>().sprite = _spriteFlowers;

        _txtValueSpecialFish.text = "$ 100";
        _txtAmountSpecialFish.text = "x 1 = 3";
        _txtAmountSpecialFish.GetComponentInChildren<Image>().sprite = _spriteCommonFish;


        _txtValueNail.text = "$ 200";
        _txtAmountNail.text = "x 1";
        _txtAmountNail.GetComponentInChildren<Image>().sprite = _spriteCommonFish;

        _txtValueApple.text = "$ 10";
        _txtAmountApple.text = "x 1 = 3";
        _txtAmountApple.GetComponentInChildren<Image>().sprite = _spriteApple;
    }

    public void TradeSpecialFish()
    {
        TradeItem(1);

        _txtValueBait.text = "$ 20";
        _txtAmountBait.text = "x 5";
        _txtAmountBait.GetComponentInChildren<Image>().sprite = _spriteBait;

        _txtValueNormalFlower.text = "$ 10";
        _txtAmountNormalFlower.text = "x 1 = 10";
        _txtAmountNormalFlower.GetComponentInChildren<Image>().sprite = _spriteFlowers;

        _txtValueSpecialFlower.text = "$ 50";
        _txtAmountSpecialFlower.text = "x 1 = 2";
        _txtAmountSpecialFlower.GetComponentInChildren<Image>().sprite = _spriteFlowers;

        _txtValueCommonFish.text = "$ 30";
        _txtAmountCommonFish.text = "x 1 = 3";
        _txtAmountCommonFish.GetComponentInChildren<Image>().sprite = _spriteSpecialFish;

        _txtValueNail.text = "$ 200";
        _txtAmountNail.text = "x 1 = 0.5";
        _txtAmountNail.GetComponentInChildren<Image>().sprite = _spriteNail;

        _txtValueApple.text = "$ 10";
        _txtAmountApple.text = "x 1 = 10";
        _txtAmountApple.GetComponentInChildren<Image>().sprite = _spriteApple;
    }

    public void TradeNormalFlowers()
    {
        TradeItem(2);
    }

    public void TradeSpecialFlowers()
    {
        TradeItem(3);
    }

    public void TradeNails()
    {
        TradeItem(4);
    }

    public void TradeApples()
    {
        TradeItem(5);
    }

    public void TradeBaits()
    {
        TradeItem(6);

        _txtValueNormalFlower.text = "$ 10";
        _txtAmountNormalFlower.text = "x 1 = 2";
        _txtAmountNormalFlower.GetComponentInChildren<Image>().sprite = _spriteFlowers;

        _txtValueSpecialFlower.text = "$ 50";
        _txtAmountSpecialFlower.text = "x 1 = 0.4";
        _txtAmountSpecialFlower.GetComponentInChildren<Image>().sprite = _spriteFlowers;

        _txtValueCommonFish.text = "$ 30";
        _txtAmountCommonFish.text = "x 1 = 0.67";
        _txtAmountCommonFish.GetComponentInChildren<Image>().sprite = _spriteCommonFish;

        _txtValueSpecialFish.text = "$ 100";
        _txtAmountSpecialFish.text = "x 1 = 0.2";
        _txtAmountSpecialFish.GetComponentInChildren<Image>().sprite = _spriteSpecialFish;

        _txtValueNail.text = "$ 200";
        _txtAmountNail.text = "x 1 = 0.1";
        _txtAmountNail.GetComponentInChildren<Image>().sprite = _spriteNail;

        _txtValueApple.text = "$ 10";
        _txtAmountApple.text = "x 1 = 2";
        _txtAmountApple.GetComponentInChildren<Image>().sprite = _spriteApple;
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) _iconInteract.transform.DOScale(0.05f, 0.5f);
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && Input.GetKeyDown(_keyInteract)) 
            OpenMarket();
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) _iconInteract.transform.DOScale(0f, 0.5f);
    }
}