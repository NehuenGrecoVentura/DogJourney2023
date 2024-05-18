using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class MarketFishing : MonoBehaviour
{
    [SerializeField] KeyCode _keyInteract = KeyCode.F;
    [SerializeField] GameObject _canvasMarket;
    [SerializeField] Collider _myCol;
    [SerializeField] MenuPause _pause;
    [SerializeField] CameraOrbit _camPlayer;
    [SerializeField] Camera _camMarket;
    [SerializeField] Character _player;
    [SerializeField] DoTweenManager _doTween;
    [SerializeField] CharacterInventory _inventory;

    [SerializeField] TMP_FontAsset[] _textsMaterials;
    [SerializeField] TMP_Text _amountFish;
    [SerializeField] TMP_Text _amountBaits;

    private void Start()
    {
        _canvasMarket.SetActive(false);
        _camMarket.gameObject.SetActive(false);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    var player = other.GetComponent<Character>();
    //    if(player != null)
    //}

    private void Update()
    {
        if (_canvasMarket.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _pause.Defreeze();
                _player.DeFreezePlayer();

                _camMarket.gameObject.SetActive(false);
                _camPlayer.gameObject.SetActive(true);

                _myCol.enabled = true;
                _canvasMarket.SetActive(false);
            }

            else
            {
                _amountBaits.text = "x " + _inventory.baits.ToString();
                _amountFish.text = "x " + _inventory.fishes.ToString();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && _myCol.enabled && Input.GetKeyDown(_keyInteract))
            OpenMarket();
    }

    private void OpenMarket()
    {
        _player.FreezePlayer();
        _pause.Freeze();
        _canvasMarket.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        _camPlayer.gameObject.SetActive(false);
        _camMarket.gameObject.SetActive(true);

        _myCol.enabled = false;
    }

    public void BuyBaits()
    {
        _inventory.baits += 5;
    }

    public void BuyFishes()
    {
        _inventory.fishes += 5;
    }

    public void SellBaits()
    {
        const int baitPrice = 50; // Precio de venta por 3 cebos
        const int singleBaitPrice = 5; // Precio de venta por 1 cebo

        if (_inventory.baits >= 3)
        {
            int baitsToSell = Mathf.Min(_inventory.baits / 3 * 3, _inventory.baits); // Calcula la cantidad de cebos a vender en múltiplos de 3
            _inventory.baits -= baitsToSell;
            _inventory.money += baitsToSell / 3 * baitPrice;
        }
        else if (_inventory.baits == 1)
        {
            _inventory.baits = 0;
            _inventory.money += singleBaitPrice;
        }
    }

    public void SellFishes()
    {
        const int fishPrice = 50;
        const int singleFishPrice = 5;

        if (_inventory.baits >= 3)
        {
            int fishesToSell = Mathf.Min(_inventory.fishes / 3 * 3, _inventory.fishes);
            _inventory.fishes -= fishesToSell;
            _inventory.money += fishesToSell / 3 * fishPrice;
        }
        else if (_inventory.fishes == 1)
        {
            _inventory.fishes = 0;
            _inventory.money += singleFishPrice;
        }
    }

    public void EnterButton(TMP_Text text)
    {
        text.font = _textsMaterials[0];
    }

    public void ExitButton(TMP_Text text)
    {
        text.font = _textsMaterials[1];
    }
}