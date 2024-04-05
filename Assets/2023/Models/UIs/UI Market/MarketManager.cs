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
    [SerializeField] TMP_Text _txtAmountNails;
    [SerializeField] TMP_Text _txtCosttNails;

    [Header("ROPES")]
    [SerializeField] int _costRopes = 100;
    [SerializeField] int _amountRopes = 60;
    [SerializeField] TMP_Text _txtAmountRopes;
    [SerializeField] TMP_Text _txtCostRopes;

    //[Header("DEFAULT ARTICLES LOCKED")]
    //[SerializeField] Button[] _articlesLocked;
    //[SerializeField] EventTrigger[] _eventTriggerArticles;

    [Header("AUDIO")]
    [SerializeField] AudioClip[] _sounds;
    private AudioSource _myAudio;

    [Header("SCROLLS CONFIG")]
    [SerializeField] Scrollbar _scrollbar;
    [SerializeField] float _scrollSpeed = 0.1f;

    [Header("EXIT BUTTON")]
    [SerializeField] TMP_Text _txtButtonExit;
    [SerializeField] TMP_FontAsset _styleSelected;
    [SerializeField] TMP_FontAsset _styleNormal;
    private Vector3 _intialScale;

    private void Awake()
    {
        _myAudio = GetComponent<AudioSource>();
        _player = FindObjectOfType<Character>();
        _inventory = FindObjectOfType<CharacterInventory>();
    }

    private void Start()
    {
        _intialScale = transform.localScale;
        _intro.gameObject.SetActive(false);
        _canvas.SetActive(false);

        //foreach (var article in _articlesLocked)
        //    article.enabled = false;

        //foreach (var item in _eventTriggerArticles)
        //    item.enabled = false;
    }

    private void Update()
    {
        if (_canvas.activeSelf) _textInventory.text = "$ " + _inventory.money.ToString();

        if (Input.GetKeyDown(KeyCode.J)) ExitMarket();
        //if (Input.GetKeyDown(KeyCode.C))
        //{
        //    foreach (var article in _articlesLocked)
        //        article.enabled = true;

        //    foreach (var item in _eventTriggerArticles)
        //        item.enabled = true;
        //}
    }

    #region EVENT TRIGGER

    public void BuyNails()
    {
        BuyMaterial(_costNails, ref _inventory.nails, _amountNails, _txtAmountNails, _txtCosttNails);
    }

    public void BuyRopes()
    {
        BuyMaterial(_costNails, ref _inventory.ropes, _amountRopes, _txtAmountRopes, _txtCostRopes);
    }

    public void OpenMarket()
    {
        StartCoroutine(OpenMarketCoroutine());
    }

    public void ExitMarket()
    {
        StartCoroutine(ExitMarketCoroutine());
    }

    private void ButtonExitEvents(TMP_FontAsset style, Vector3 scale)
    {
        _txtButtonExit.fontMaterial = style.material;
        _txtButtonExit.transform.localScale = scale;

        if (_sounds.Length > 0)
        {
            int random = Random.Range(2, 3);
            _myAudio.PlayOneShot(_sounds[random]);
        }
    }

    public void ExitButtonEnter()
    {
        ButtonExitEvents(_styleSelected, new Vector3(transform.localScale.x + 0.2f, transform.localScale.y + 0.2f, transform.localScale.z + 0.2f));
    }

    public void ExitButtonExit()
    {
        ButtonExitEvents(_styleNormal, _intialScale);
    }

    #endregion

    private void BuyMaterial(int cost, ref int materialInInvetory, int addMaterial, TMP_Text textAmount, TMP_Text textCost)
    {
        if (_inventory.money >= cost)
        {
            _myAudio.PlayOneShot(_sounds[1]);
            materialInInvetory += addMaterial;
            _inventory.money -= cost;
            print("COMPRE MATERIAL");

            textAmount.transform.DOScale(new Vector3(0.5f, 0.5f, 0.5f), 0.5f)
            .OnComplete(() =>
            {
                // Cambiar el color del texto a verde
                textAmount.DOColor(Color.green, 0.5f);

                textAmount.transform.DOScale(new Vector3(0.17f, 0.17f, 0.17f), 0.5f)
                    .OnComplete(() =>
                    {
                        // Cambiar el color del texto a negro
                        textAmount.DOColor(Color.black, 1f);
                    });
            });
        }

        else
        {
            _myAudio.PlayOneShot(_sounds[0]);
            print("TE FALTA PLATA");

            textCost.color = Color.red;
            textCost.transform.DOScale(new Vector3(0.5f, 0.5f, 0.5f), 0.5f)
            .OnComplete(() =>
            {
                textCost.transform.DOScale(new Vector3(0.3f, 0.3f, 0.3f), 0.5f)
                    .OnComplete(() =>
                    {
                        textCost.DOColor(Color.white, 1f);
                    });
            });
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

    private IEnumerator ExitMarketCoroutine()
    {
        _intro.gameObject.SetActive(true);
        _intro.transform.DOScale(100f, 0f);
        _intro.transform.DOScale(0f, 2f);
        _canvas.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        _intro.gameObject.SetActive(false);
        _player.FreezePlayer(RigidbodyConstraints.FreezeRotation);
        _player.speed = _player.speedAux;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) StartCoroutine(OpenMarketCoroutine());
    }

    public void OnScroll(PointerEventData eventData)
    {
        float currentValue = _scrollbar.value;
        currentValue += eventData.scrollDelta.y * _scrollSpeed;
        currentValue = Mathf.Clamp01(currentValue);
        _scrollbar.value = currentValue;
    }
}