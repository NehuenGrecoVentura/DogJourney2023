using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewMarket : MonoBehaviour
{
    [Header("MARKET BASIC")]
    [SerializeField] GameObject _canvasMarket;
    [SerializeField] GameObject _menu1;
    [SerializeField] GameObject _menu2;
    [SerializeField] int _priceNails = 50;
    [SerializeField] int _priceRopes = 100;

    [Header("MESSAGE MONEY")]
    [SerializeField] TMP_Text _textErrorMoney;
    [SerializeField] float _textTimeMoney;

    [Header("TEXTS INVENTORY")]
    [SerializeField] Text[] _textsInventory;

    [Header("AUDIO CONFIG")]
    [SerializeField] AudioClip[] _audios;
    private AudioSource _myAudio;

    [Header("REFS")]
    [SerializeField] Upgrade _activeTutorialDogs;
    private FeedbackMarket _canvasTutorialDog;
    private CameraOrbit _cam;
    private Inventory _inventory;

    [HideInInspector] public bool activeUpgradeTrolley = false;
    [HideInInspector] public bool activeUpgradeTreeReg = false;


    void Awake()
    {
        _myAudio = GetComponent<AudioSource>();
        _cam = FindObjectOfType<CameraOrbit>();
        _inventory = FindObjectOfType<Inventory>();
        _canvasTutorialDog = FindObjectOfType<FeedbackMarket>();
    }

    void Start()
    {
        activeUpgradeTreeReg = false;
        activeUpgradeTrolley = false;
        _canvasMarket.SetActive(false);
    }

    void Update()
    {
        // Muestra en texto la cantidad de cosas que tenemos en el inventario.
        AmountInInventory();
    }


    public void MainMenu()
    {
        _canvasMarket.SetActive(true);
        _menu1.SetActive(true);
        _menu2.SetActive(false);
        _textErrorMoney.gameObject.SetActive(false);

    }

    public void NextMenu()
    {
        _menu2.SetActive(true);
        _menu1.SetActive(false);
        _textErrorMoney.gameObject.SetActive(false);
    }

    public void BuyNails()
    {
        // Si tengo el dinero justo o más, entonces puedo comprar los clavos.
        if (_inventory.money >= _priceNails)
        {
            _inventory.amountnails += 20;
            _inventory.money -= _priceNails;
            _myAudio.PlayOneShot(_audios[1]);
        }

        // Sino muestro por unos segundos el mensaje de que falta dinero.
        else
        {
            _myAudio.PlayOneShot(_audios[0]);
            _textErrorMoney.gameObject.SetActive(true);
            _textErrorMoney.text = "You don't have enough money.";
            StartCoroutine(MessageMoney());
        }
    }

    public void BuyRopes()
    {
        // Si tengo el dinero justo o más, entonces puedo comprar la soga.
        if (_inventory.money >= _priceRopes)
        {
            _inventory.amountRopes += 60;
            _inventory.money -= _priceRopes;
            _myAudio.PlayOneShot(_audios[1]);
        }

        // Sino muestro por unos segundos el mensaje de que falta dinero.
        else
        {
            _myAudio.PlayOneShot(_audios[0]);
            _textErrorMoney.gameObject.SetActive(true);
            _textErrorMoney.text = "You don't have enough money.";
            StartCoroutine(MessageMoney());
        }
    }

    public void SellWoods()
    {
        // Si tengo 10 o más madera en el inventario lo puedo vender.
        if (_inventory.amountWood >= 10)
        {
            _inventory.amountWood -= 10;
            _inventory.money += 20;
            _myAudio.PlayOneShot(_audios[1]);
        }

        else
        {
            _textErrorMoney.gameObject.SetActive(true);
            _textErrorMoney.text = "You don't have enough wood.";
            StartCoroutine(MessageMoney());
            _myAudio.PlayOneShot(_audios[0]);
        }
    }


    void AmountInInventory()
    {
        _textsInventory[0].text = _inventory.money.ToString();
        _textsInventory[1].text = _inventory.amountWood.ToString();
        _textsInventory[2].text = _inventory.amountnails.ToString();
    }

    IEnumerator MessageMoney()
    {
        while (true)
        {
            yield return new WaitForSeconds(_textTimeMoney);
            _textErrorMoney.gameObject.SetActive(false);
            StopCoroutine(MessageMoney());
        }
    }

    public void OpenMarket()
    {
        MainMenu();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        _cam.sensitivity = new Vector2(0, 0);
    }

    public void ExitMarket()
    {
        _canvasMarket.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _cam.sensitivity = new Vector2(3, 3);

        if (_activeTutorialDogs.activeTutorialDogs)
            _canvasTutorialDog.ActiveTutorialDogs();
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character2022>();
        if (player != null) OpenMarket();
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character2022>();
        if (player != null) ExitMarket();
    }
}