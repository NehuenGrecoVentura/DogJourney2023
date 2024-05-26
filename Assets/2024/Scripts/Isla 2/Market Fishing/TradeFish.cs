using UnityEngine;
using TMPro;
using DG.Tweening;

public class TradeFish : MonoBehaviour
{
    [SerializeField] GameObject _canvas;
    private CharacterInventory _inventory;
    private Collider _myCol;
    private MenuPause _pause;
    private Character _player;

    [Header("INTERACT")]
    [SerializeField] GameObject _iconInteract;
    [SerializeField] KeyCode _keyInteract = KeyCode.F;

    [Header("TEXTS INVENTORY")]
    [SerializeField] TMP_Text _txtInvFishCommon;
    [SerializeField] TMP_Text _txtInvSpecialFish;
    [SerializeField] TMP_Text _txtInvBait;
    [SerializeField] TMP_Text _txtInvMoney;

    [Header("AUDIOS")]
    [SerializeField] AudioClip _soundError;
    [SerializeField] AudioClip _soundTrade;
    private AudioSource _myAudio;

    private void Awake()
    {
        _myCol = GetComponent<Collider>();
        _myAudio = GetComponent<AudioSource>();

        _inventory = FindObjectOfType<CharacterInventory>();
        _pause = FindObjectOfType<MenuPause>();
        _player = FindObjectOfType<Character>();
    }

    private void Start()
    {
        _iconInteract.transform.DOScale(0f, 0f);
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
            _txtInvMoney.text = "x " + _inventory.money.ToString();
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

    private void MakeTrade(ref int amountInInventory, ref int item, int price, int add)
    {
        if (amountInInventory >= price && price > 0)
        {
            amountInInventory -= price;
            item += add;
            _myAudio.PlayOneShot(_soundTrade);
        }

        else _myAudio.PlayOneShot(_soundError);
    }

    #region INTERACAMBIOS PECES COMUNES

    public void TradeCommonToBait()
    {
        MakeTrade(ref _inventory.fishes, ref _inventory.baits, 1, 1);
    }

    public void TradeCommonToNormalFlower()
    {
        MakeTrade(ref _inventory.fishes, ref _inventory.flowers, 1, 3);
    }

    public void TradeCommonToSpecialFlower()
    {
        MakeTrade(ref _inventory.fishes, ref _inventory.flowers, 1, 1);
    }

    public void TradeCommonToSpecialFish()
    {
        MakeTrade(ref _inventory.fishes, ref _inventory.specialFishes, 1, 1);
    }

    public void TradeCommonToNail()
    {
        MakeTrade(ref _inventory.fishes, ref _inventory.nails, 1, 1);
    }

    public void TradeCommonToApple()
    {
        MakeTrade(ref _inventory.fishes, ref _inventory.baits, 1, 3);
    }

    public void TradeCommonToBox()
    {
        if (_inventory.fishes >= 13)
        {
            _inventory.fishes -= 13;
            _inventory.upgradeLoot = true;
            _myAudio.PlayOneShot(_soundTrade);
        }

        else _myAudio.PlayOneShot(_soundError);
    }

    #endregion

    #region INTERCAMBIOS PECES ESPECIALES

    public void TradeSpecialFishToCommon()
    {
        MakeTrade(ref _inventory.specialFishes, ref _inventory.fishes, 1, 3);
    }

    public void TradeSpecialFishToNormalFlowers()
    {
        MakeTrade(ref _inventory.specialFishes, ref _inventory.flowers, 1, 10);
    }

    public void TradeSpecialFishToSpecialFlowers()
    {
        MakeTrade(ref _inventory.specialFishes, ref _inventory.flowers, 1, 2);
    }

    public void TradeSpecialFishToNail()
    {
        MakeTrade(ref _inventory.specialFishes, ref _inventory.flowers, 1, 1);
    }

    public void TradeSpecialFishToApple()
    {
        MakeTrade(ref _inventory.specialFishes, ref _inventory.apples, 1, 10);
    }

    public void TradeSpecialFishToBait()
    {
        MakeTrade(ref _inventory.specialFishes, ref _inventory.baits, 1, 5);
    }

    public void TradeSpecialFishToBox()
    {
        if (_inventory.specialFishes >= 4)
        {
            _inventory.fishes -= 4;
            _inventory.upgradeLoot = true;
            _myAudio.PlayOneShot(_soundTrade);
        }

        else _myAudio.PlayOneShot(_soundError);
    }

    #endregion

    #region INTERCAMBIOS CARNADAS

    public void TradeBaitToCommon()
    {
        MakeTrade(ref _inventory.baits, ref _inventory.fishes, 2, 5);
    }

    public void TradeBaitToSpecialFish()
    {
        MakeTrade(ref _inventory.baits, ref _inventory.specialFishes, 5, 1);
    }

    public void TradeBaitToNormalFlower()
    {
        MakeTrade(ref _inventory.flowers, ref _inventory.fishes, 1, 10);
    }

    public void TradeBaitToSpecialFlower()
    {
        MakeTrade(ref _inventory.flowers, ref _inventory.fishes, 3, 2);
    }

    public void TradeBaitToNail()
    {
        MakeTrade(ref _inventory.baits, ref _inventory.nails, 1, 1);
    }

    public void TradeBaitToApple()
    {
        MakeTrade(ref _inventory.baits, ref _inventory.apples, 1, 2);
    }

    public void TradeBaitToBox()
    {
        if (_inventory.fishes >= 20)
        {
            _inventory.fishes -= 20;
            _inventory.upgradeLoot = true;
            _myAudio.PlayOneShot(_soundTrade);
        }

        else _myAudio.PlayOneShot(_soundError);
    }

    #endregion

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