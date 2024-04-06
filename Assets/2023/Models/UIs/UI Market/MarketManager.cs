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
    private TreeRegenerative[] _allTrees;
    private HitBar[] _hitBars;
    private SaplingTree[] _sapling;
    private Trunks[] _trunks;
    private Dog[] _dogs;

    [SerializeField] GameObject[] _dogUpgrade;
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

    [Header("UPGRADES BOX ICONS")]
    [SerializeField] Image[] _boxes;

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
        _allTrees = FindObjectsOfType<TreeRegenerative>();
        _hitBars = FindObjectsOfType<HitBar>();
        _sapling = FindObjectsOfType<SaplingTree>();
        _trunks = FindObjectsOfType<Trunks>();
        _dogs = FindObjectsOfType<Dog>();
    }

    private void Start()
    {
        _intialScale = transform.localScale;
        _intro.gameObject.SetActive(false);
        _canvas.SetActive(false);

        foreach (var dog in _dogUpgrade)
            dog.SetActive(false);
    }

    private void Update()
    {
        if (_canvas.activeSelf) _textInventory.text = "$ " + _inventory.money.ToString();
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

    #region UPGRADES PLAYER

    public void UpgradeAxe()
    {
        foreach (var tree in _allTrees)
        {
            tree.initialAmount = 100;
            tree.RestartAmount();
        }

        foreach (var hitBar in _hitBars)
            hitBar.UpgradeBar();

        Destroy(_boxes[0].gameObject);
        _myAudio.PlayOneShot(_sounds[1]);
    }

    public void UpgradeSpeedPlayer()
    {
        _player.speedAux = 13; // Ahora la velocidad del player es m�s r�pida al caminar.
        _player.speedRun = 20; // Ahora la velocidad del player es m�s r�pida al correr.
        _player.speed = 13;
        Destroy(_boxes[1].gameObject);
        _myAudio.PlayOneShot(_sounds[1]);
    }

    public void UpgradeRegenerate()
    {
        foreach (var sapling in _sapling)
            sapling.isUpgraded = true;

        Destroy(_boxes[2].gameObject);
        _myAudio.PlayOneShot(_sounds[1]);
    }

    #endregion

    #region UPGRADES DOG

    public void UpgradeTrolley()
    {
        foreach (var trunk in _trunks)
            trunk.isUpgraded = true;

        Destroy(_boxes[3].gameObject);
        _myAudio.PlayOneShot(_sounds[1]);
    }

    public void UpgradeSpeedDog()
    {
        foreach (var dog in _dogs)
        {
            dog.speedNormal = 15f;
            dog.speedRun = 20f;
        }

        Destroy(_boxes[4].gameObject);
        _myAudio.PlayOneShot(_sounds[1]);
    }

    public void UpgradeDog()
    {
        foreach (var dog in _dogUpgrade)
            dog.SetActive(true);

        Destroy(_boxes[5].gameObject);
        _myAudio.PlayOneShot(_sounds[1]);

    }

    #endregion

    public void SellWood()
    {
        _inventory.money += 100;
        _inventory.greenTrees -= 10;
        _myAudio.PlayOneShot(_sounds[1]);
    }

    public void ErrorUpgrade(int indexBox)
    {
        _boxes[indexBox].transform.DOScale(0.5f, 0.5f).OnComplete(() =>
        {
            _boxes[indexBox].transform.DOScale(0.13f, 0.5f);
        });

        _myAudio.PlayOneShot(_sounds[0]);
    }

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


    public void OnScroll(PointerEventData eventData)
    {
        float currentValue = _scrollbar.value;
        currentValue += eventData.scrollDelta.y * _scrollSpeed;
        currentValue = Mathf.Clamp01(currentValue);
        _scrollbar.value = currentValue;
    }
}