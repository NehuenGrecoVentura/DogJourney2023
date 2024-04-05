using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class MarketManager : MonoBehaviour, IScrollHandler
{
    private Character _player;
    private CharacterInventory _inventory;

    [SerializeField] GameObject _canvas;
    [SerializeField] Image _intro;
    [SerializeField] TMP_Text _textInventory;

    [Header("NAILS")]
    [SerializeField] int _costNails = 50;
    [SerializeField] int _amountNails = 20;

    [Header("ROPES")]
    [SerializeField] int _costRopes = 100;
    [SerializeField] int _amountRopes = 60;

    [Header("DEFAULT ARTICLES LOCKED")]
    [SerializeField] Button[] _articlesLocked;
    [SerializeField] EventTrigger[] _eventTriggerArticles;

    [Header("AUDIO")]
    [SerializeField] AudioClip[] _sounds;
    private AudioSource _myAudio;

    [Header("SCROLLS CONFIG")]
    [SerializeField] Scrollbar _scrollbar;
    [SerializeField] float _scrollSpeed = 0.1f;

    private void Awake()
    {
        _myAudio = GetComponent<AudioSource>();
        _player = FindObjectOfType<Character>();
        _inventory = FindObjectOfType<CharacterInventory>();
    }

    private void Start()
    {
        _intro.gameObject.SetActive(false);
        _canvas.SetActive(false);

        foreach (var article in _articlesLocked)
        {
            article.enabled = false;
        }

        foreach (var item in _eventTriggerArticles)
        {
            item.enabled = false;
        }
    }

    private void Update()
    {
        if (_canvas.activeSelf) _textInventory.text = "$ " + _inventory.money.ToString();


        if (Input.GetKeyDown(KeyCode.C))
        {
            foreach (var article in _articlesLocked)
            {
                article.enabled = true;
            }

            foreach (var item in _eventTriggerArticles)
            {
                item.enabled = true;
            }
        }


    }

    public void BuyNails()
    {
        BuyMaterial(_costNails, ref _inventory.nails, _amountNails);
    }

    public void BuyRopes()
    {
        BuyMaterial(_costNails, ref _inventory.ropes, _amountRopes);
    }

    public void OpenMarket()
    {
        StartCoroutine(OpenMarketCoroutine());
    }

    public void ExitMarket()
    {
        _player.FreezePlayer(RigidbodyConstraints.FreezeRotation);
        _player.speed = _player.speedAux;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void BuyMaterial(int cost, ref int materialInInvetory, int addMaterial)
    {
        if (_inventory.money >= cost)
        {
            _myAudio.PlayOneShot(_sounds[1]);
            materialInInvetory += addMaterial;
            _inventory.money -= cost;
            print("COMPRE MATERIAL");
        }

        else
        {
            _myAudio.PlayOneShot(_sounds[0]);
            print("TE FALTA PLATA");
        }
    }

    private IEnumerator OpenMarketCoroutine()
    {
        _intro.gameObject.SetActive(true);
        _intro.transform.DOScale(100f, 2f);
        _player.FreezePlayer(RigidbodyConstraints.FreezeAll);
        _player.speed = 0;
        yield return new WaitForSeconds(1f);
        _intro.gameObject.SetActive(false);
        _intro.transform.DOScale(0f, 2f);
        _canvas.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) StartCoroutine(OpenMarketCoroutine());
    }

    public void OnScroll(PointerEventData eventData)
    {
        // Obtén el valor actual del Scrollbar
        float currentValue = _scrollbar.value;

        // Ajusta el valor del Scrollbar en función del desplazamiento de la rueda del mouse
        currentValue += eventData.scrollDelta.y * _scrollSpeed;

        // Asegúrate de que el valor esté dentro del rango válido (entre 0 y 1)
        currentValue = Mathf.Clamp01(currentValue);

        // Asigna el nuevo valor al Scrollbar
        _scrollbar.value = currentValue;
    }
}