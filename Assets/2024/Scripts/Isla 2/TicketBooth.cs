using UnityEngine;
using DG.Tweening;
using TMPro;

public class TicketBooth : MonoBehaviour
{
    [SerializeField] GameObject _canvasTicketBooth;

    [Header("INTERACT")]
    [SerializeField] Transform _iconInteract;
    [SerializeField] KeyCode _keyInteract = KeyCode.F;
    [SerializeField] Collider _myCol;
    [SerializeField] MenuPause _pause;
 
    [Header("INVENTORY TEXTS")]
    [SerializeField] TMP_Text _txtMoney;
    [SerializeField] TMP_Text _txtTickets;
    [SerializeField] CharacterInventory _inventory;

    [Header("GIFTS")]
    [SerializeField] GameObject _cap;
    [SerializeField] GameObject _actualCap;
    [SerializeField] GameObject _glassesSun;
    [SerializeField] MeshRenderer _axePlayer;
    [SerializeField] Material _matNewAxe;
    [SerializeField] int _priceCap = 250;
    [SerializeField] int _priceAxe = 200;
    [SerializeField] int _priceGlasses = 150;
    [SerializeField] int _priceWood = 50;
    [SerializeField] int _priceFish = 30;
    [SerializeField] int _priceApple = 10;
    [SerializeField] int _priceTickets = 300;
    
    [Header("AUDIO")]
    [SerializeField] AudioClip _soundBuy;
    [SerializeField] AudioClip _soundError;
    [SerializeField] AudioSource _myAudio;

    void Start()
    {
        _canvasTicketBooth.SetActive(false);
        _iconInteract.DOScale(0f, 0f);
    }

    private void Update()
    {
        if (_canvasTicketBooth.activeSelf)
        {
            _txtMoney.text = "$ " + _inventory.money.ToString();
            _txtTickets.text = "x " + _inventory.tickets.ToString();
        }
    }

    private void Open()
    {
        _canvasTicketBooth.SetActive(true);
        _iconInteract.DOScale(0f, 0.5f);
        _pause.Freeze();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        _myCol.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) _iconInteract.DOScale(0.02f, 0.5f);
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && Input.GetKeyDown(_keyInteract)) Open();
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) _iconInteract.DOScale(0f, 0.5f);
    }

    #region EVENTS NO BORRAR

    public void Close()
    {
        _canvasTicketBooth.SetActive(false);
        _pause.Defreeze();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _myCol.enabled = true;
    }

    public void BuyCap()
    {
        if (_inventory.tickets >= _priceCap)
        {
            _inventory.tickets -= _priceCap;
            _cap.SetActive(true);
            _actualCap.SetActive(false);
            _myAudio.PlayOneShot(_soundBuy);

            if (_inventory.tickets <= 0) _inventory.tickets = 0;
            else return;
        }

        else _myAudio.PlayOneShot(_soundError);
    }

    public void BuyAxe()
    {
        if (_inventory.tickets >= _priceAxe)
        {
            _inventory.tickets -= _priceAxe;
            _axePlayer.material = _matNewAxe;
            _myAudio.PlayOneShot(_soundBuy);

            if (_inventory.tickets <= 0) _inventory.tickets = 0;
            else return;
        }

        else _myAudio.PlayOneShot(_soundError);
    }

    public void BuyGlassesSun()
    {
        if (_inventory.tickets >= _priceAxe)
        {
            _inventory.tickets -= _priceAxe;
            _glassesSun.SetActive(true);
            _myAudio.PlayOneShot(_soundBuy);

            if (_inventory.tickets <= 0) _inventory.tickets = 0;
            else return;
        }

        else _myAudio.PlayOneShot(_soundError);
    }

    public void BuyWood()
    {
        if (_inventory.tickets >= _priceWood)
        {
            _inventory.tickets -= _priceWood;
            _inventory.greenTrees++;
            _myAudio.PlayOneShot(_soundBuy);

            if (_inventory.tickets <= 0) _inventory.tickets = 0;
            else return;
        }

        else _myAudio.PlayOneShot(_soundError);
    }

    public void BuyFish()
    {
        if (_inventory.tickets >= _priceFish)
        {
            _inventory.tickets -= _priceFish;
            _inventory.fishes++;
            _myAudio.PlayOneShot(_soundBuy);

            if (_inventory.tickets <= 0) _inventory.tickets = 0;
            else return;
        }

        else _myAudio.PlayOneShot(_soundError);
    }

    public void BuyApple()
    {
        if (_inventory.tickets >= _priceApple)
        {
            _inventory.tickets -= _priceApple;
            _inventory.fishes++;
            _myAudio.PlayOneShot(_soundBuy);

            if (_inventory.tickets <= 0) _inventory.tickets = 0;
            else return;
        }

        else _myAudio.PlayOneShot(_soundError);
    }

    public void BuyTickets()
    {
        if (_inventory.money >= _priceTickets)
        {
            _inventory.money -= _priceTickets;
            _inventory.tickets += 10;
            _myAudio.PlayOneShot(_soundBuy);

            if (_inventory.money <= 0) _inventory.money = 0;
            else return;
        }

        else _myAudio.PlayOneShot(_soundError);
    }

    #endregion
}