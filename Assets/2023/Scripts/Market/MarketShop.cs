using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MarketShop : MonoBehaviour
{
    [Header("AUDIO CONFIG")]
    [SerializeField] AudioClip[] _audios;
    private AudioSource _myAudio;

    [SerializeField] int _priceNails = 50;
    [SerializeField] int _priceRopes = 100;

    [Header("MESSAGE ERROR")]
    [SerializeField] TMP_Text _textError;
    [SerializeField] float _textTimeError = 2f;

    [Header("TEXTS INVENTORY")]
    [SerializeField] Text[] _textsInventory;

    [SerializeField] GameObject _canvasMarket;
    [SerializeField] GameObject[] _menus;

    private CameraOrbit _cam;
    private CharacterInventory _inventory;

    [HideInInspector] public bool speedDogUpgraded = false;
    [SerializeField] GameObject[] _newDog;
    private bool _dogAdded = false;

    private void Awake()
    {
        _myAudio = GetComponent<AudioSource>();
        _cam = FindObjectOfType<CameraOrbit>();
        _inventory = FindObjectOfType<CharacterInventory>();
    }

    private void Start()
    {
        InitialDefault();
    }

    private void Update()
    {
        AmountInInventory(); // Ver si puedo optimizar esto
    }

    private void InitialDefault()
    {
        _canvasMarket.SetActive(false);
        _textError.gameObject.SetActive(false);

        foreach (var dog in _newDog)
            dog.SetActive(false);
    }

    private void AmountInInventory()
    {
        _textsInventory[0].text = _inventory.money.ToString();
        _textsInventory[1].text = _inventory.greenTrees.ToString();
        _textsInventory[2].text = _inventory.nails.ToString();
    }

    private void Error(string message)
    {
        _myAudio.PlayOneShot(_audios[0]);
        _textError.gameObject.SetActive(true);
        _textError.text = message;
        StartCoroutine(Message());
    }

    private IEnumerator Message()
    {
        yield return new WaitForSeconds(_textTimeError);
        _textError.gameObject.SetActive(false);
    }

    #region NO BORRAR. EVENTS TRIGGERS

    public void MainMenu()
    {
        _canvasMarket.SetActive(true);
        _menus[0].SetActive(true);
        _menus[1].SetActive(false);
        _textError.gameObject.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        _cam.sensitivity = new Vector2(0, 0);
    }

    public void NextPage()
    {
        _menus[0].SetActive(false);
        _menus[1].SetActive(true);
        _textError.gameObject.SetActive(false);
    }

    public void BuyNails()
    {
        // Si tengo el dinero justo o más, entonces puedo comprar los clavos.
        if (_inventory.money >= _priceNails)
        {
            _textError.gameObject.SetActive(false);
            _inventory.nails += 20;
            _inventory.money -= _priceNails;
            _myAudio.PlayOneShot(_audios[1]);
        }

        // Sino muestro por unos segundos el mensaje de que falta dinero.
        else Error("You don't have enough money.");
    }

    public void BuyRopes()
    {
        if (_inventory.money >= _priceRopes)
        {
            _textError.gameObject.SetActive(false);
            _inventory.ropes += 60;
            _inventory.money -= _priceNails;
            _myAudio.PlayOneShot(_audios[1]);
        }

        else Error("You don't have enough money.");
    }

    public void SellWoods()
    {
        if (_inventory.greenTrees >= 10)
        {
            _inventory.greenTrees -= 10;
            _inventory.money += 20;
            _myAudio.PlayOneShot(_audios[1]);
        }

        else Error("You don't have enough wood.");
    }

    public void AddDog()
    {
        foreach (var dog in _newDog)
            dog.SetActive(true);

        _dogAdded = true;
    }

    public void ExitMarket()
    {
        _canvasMarket.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _cam.sensitivity = new Vector2(3, 3);

        //if (_activeTutorialDogs.activeTutorialDogs)
        //    _canvasTutorialDog.ActiveTutorialDogs();
    }

    #endregion

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) MainMenu();
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) ExitMarket();
    }
}