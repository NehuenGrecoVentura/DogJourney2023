using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Market : MonoBehaviour
{
    [SerializeField] GameObject _uiMarket;
    [SerializeField] GameObject _iconDogsUI;
    [SerializeField] GameObject[] _hud;
    [SerializeField] Button _buttonBackToMarket;

    [Header("MESSAGES ANIMS")]
    [SerializeField] GameObject _canvasAddNewDogMessage;

    #region AUDIO CONFIG
    [Header("AUDIO CONFIG")]
    [SerializeField] AudioClip _soundBuy;
    [SerializeField] AudioClip _soundError;
    AudioSource _myAudio;
    #endregion AUDIO CONFIG

    #region TEXTS CONFIG
    [Header("TEXTS CONFIG")]
    [SerializeField] Text _textNeedMoney;
    [SerializeField] Animation _animTextNeedMoney;
    [SerializeField] Text _textInventoryMoney;
    [SerializeField] Text _textInventoryWood;
    [SerializeField] Text _textInventoryNail;
    #endregion TEXTS CONFIG

    #region PRICE CONFIG
    [Header("PRICE CONFIG")]
    [SerializeField] int _price;
    [SerializeField] int priceSold;
    [SerializeField] int amountSold;
    #endregion PRICE CONFIG

    #region REFERENCES CONFIG
    [Header("REFERENCES CONFIG")]
    TreeRegen[] _treesReg;
    SaplingTest[] _timeReg;
    Inventory _inventory;
    Pause _pause;

    Character2022 _player;
    [SerializeField] Dog2022[] _allDogs;
    #endregion REFERENCES CONFIG

    #region UPGRADE CONFIG
    [Header("UPGRADE CONFIG")]
    [SerializeField] ParticleSystem _effectUpgradePlayer;
    [SerializeField] Button _buttonBuyUpgrade;
    [SerializeField] Button _boxMenu;
    [SerializeField] GameObject _upgradeMessage;
    [SerializeField] GameObject _upgradeMenu;
    [SerializeField] GameObject[] _hideMenuGeneral;
    [SerializeField] GameObject _listUpgradesPlayer;
    [SerializeField] GameObject _listUpgradesDog;
    [SerializeField] GameObject _newDog;
    [SerializeField] Button[] _allButtonsUpgrade;
    [SerializeField] EventTrigger[] _eventTriggersButtonUpgrade;
    public bool selected = false;
    public bool activeTutorialDogs = false;
    public bool upgradeAxe = false;
    public bool upgradeSpeedPlayer = false;
    public bool upgradeAddDog = false;
    public bool upgradeTrolley = false;
    public bool upgradeSpeedDog = false;
    public bool upgradeTreeGen = false;
    public bool activeMessage = false;
    [SerializeField] GameObject _canvasTutorialDogs;
    [SerializeField] GameObject _menuConfirmBuyUpgrade;
    [SerializeField] Button[] _upgradesBar;
    #endregion PRICE CONFIG

    [SerializeField] Transform _exitPos;
    [SerializeField] Transform _freezePos;

    CameraOrbit _cam;

    private void Awake()
    {
        _myAudio = GetComponent<AudioSource>();
        _inventory = FindObjectOfType<Inventory>();
        _pause = FindObjectOfType<Pause>();
        _player = FindObjectOfType<Character2022>();
        _treesReg = FindObjectsOfType<TreeRegen>();
        _timeReg = FindObjectsOfType<SaplingTest>();
        _cam = FindObjectOfType<CameraOrbit>();
    }

    void Start()
    {
        _uiMarket.SetActive(false);
        _textNeedMoney.gameObject.SetActive(false);
        _upgradeMessage.SetActive(false);
        _upgradeMenu.SetActive(false);
        _listUpgradesDog.SetActive(false);
        _listUpgradesPlayer.SetActive(false);
        _newDog.SetActive(false);
        _menuConfirmBuyUpgrade.SetActive(false);
        _canvasTutorialDogs.SetActive(false);
        _iconDogsUI.SetActive(false);
        _boxMenu.gameObject.SetActive(false);
        _buttonBuyUpgrade.gameObject.SetActive(false);
        _canvasAddNewDogMessage.SetActive(false);
        _effectUpgradePlayer.gameObject.SetActive(false);
    }

    void Update()
    {
        TextAmountInInventory();




        if (!selected)
        {
            _buttonBackToMarket.gameObject.SetActive(true);
            _buttonBuyUpgrade.GetComponent<Image>().color = Color.white;
            _buttonBuyUpgrade.enabled = true;
        }

        else
        {
            _buttonBackToMarket.gameObject.SetActive(false);
            _buttonBuyUpgrade.GetComponent<Image>().color = Color.grey;
            _buttonBuyUpgrade.enabled = false;

        }










    }

    // Textos del inventario.
    void TextAmountInInventory()
    {
        _textInventoryMoney.text = _inventory.money.ToString(); // Texto de la cantidad de dinero que llevo en el inventario.
        _textInventoryNail.text = _inventory.amountnails.ToString(); // Texto de la cantidad de clavos que llevo en el inventario.
        _textInventoryWood.text = _inventory.amountWood.ToString(); // Texto de la cantidad de madera que llevo en el inventario.
    }

    public void BuyNails() // Tienda clavos.
    {
        // Si tengo el dinero justo o más, entonces puedo comprar los clavos.
        if (_inventory.money >= _price)
        {
            _myAudio.PlayOneShot(_soundBuy);
            _textNeedMoney.gameObject.SetActive(false);
            _inventory.amountnails += 20;
            _inventory.money -= _price;
        }

        // Sino tengo el dinero suficiente, no puedo comprar.
        else
        {
            _myAudio.PlayOneShot(_soundError);
            _textNeedMoney.gameObject.SetActive(true);
            _animTextNeedMoney.Play();
        }
    }

    public void ExitMarket() // Salida del mercado.
    {
        Time.timeScale = 1; // Reanudo el timescale
        _player.speed = _player._speedAux; // El jugador vuevle a recuperar su velocidad.
        
        //Ahora puedeo mover el mouse libremente 
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _pause.Defrize();

        _textNeedMoney.gameObject.SetActive(false);
        _uiMarket.SetActive(false); // Desactivo el mercado principal

        // Desactiva la muestra las listas de los upgrades.
        _listUpgradesDog.SetActive(false);
        _listUpgradesPlayer.SetActive(false);

        HideHUD(false); // Vuelvo a habilitar la UI del juego normal.

        // Posiciono al player en la posicion de saluda de la tienda.
        _player.gameObject.transform.position = _exitPos.position;

        // Si al salir de la tienda agregué un perro, entonces sale el tutorial de control.
        if (activeTutorialDogs) _canvasTutorialDogs.SetActive(true);

        // Si al salir de la tienda compramps una mejora, se genera un feedback.
        if (activeMessage)
        {
            _canvasAddNewDogMessage.SetActive(true);
            _effectUpgradePlayer.gameObject.SetActive(true);
        }
    }

    // Menu de la tienda de upgrades.
    public void OpenStoreUpgrade()
    {
        //_buttonBuyUpgrade.gameObject.SetActive(true); 
        //_buttonBuyUpgrade.GetComponent<Image>().color = Color.grey;
        //_buttonBuyUpgrade.enabled = false;


        _boxMenu.gameObject.SetActive(false); // Tapo el icono del boton de upgrade del anterior menu
        _upgradeMessage.SetActive(false); // Quito el mensaje de aviso de upgrade.
        _upgradeMenu.SetActive(true); // Muestro el menu de upgrade.

        // Escondo el menu principal
        foreach (var menu in _hideMenuGeneral)
            menu.SetActive(false);

        _listUpgradesPlayer.SetActive(true); // Muestro la lista de upgrades del player por defecto.

        foreach (var item in _upgradesBar)
        {
            if (item.gameObject.name == "Bar Upgrade Player") // Si estoy clickeando en el icono del player, este tiene un feedback verde
                item.GetComponent<Image>().color = Color.green;

            else if (item.gameObject.name == "Bar Upgrade Dog")
                item.GetComponent<Image>().color = Color.white;
        }
    }

    public void ShowPlayerUpgrades()
    {
        foreach (var item in _upgradesBar)
        {
            if (item.gameObject.name == "Bar Upgrade Player")
                item.GetComponent<Image>().color = Color.green;

            else if (item.gameObject.name == "Bar Upgrade Dog")
                item.GetComponent<Image>().color = Color.white;
        }
        _boxMenu.gameObject.SetActive(false);
        _listUpgradesPlayer.SetActive(true);
        _listUpgradesDog.SetActive(false);
    }

    public void ShowDogUpgrades()
    {
        _listUpgradesPlayer.SetActive(false);
        _listUpgradesDog.SetActive(true);
        foreach (var item in _upgradesBar)
        {
            if (item.gameObject.name == "Bar Upgrade Player")
                item.GetComponent<Image>().color = Color.white;

            else if (item.gameObject.name == "Bar Upgrade Dog")
                item.GetComponent<Image>().color = Color.green;
        }
    }

    public void SelectUpgrade()
    {
        if (_inventory.upgrade)
        {
            if (!selected) selected = true;
            else selected = false;
            if (selected)
            {

                _buttonBuyUpgrade.gameObject.SetActive(true);
                foreach (var button in _allButtonsUpgrade)
                {
                    button.GetComponent<Image>().color = Color.grey;
                    button.GetComponent<EventTrigger>().enabled = false;
                    button.enabled = false;
                }
            }

            else
            {
                foreach (var button in _allButtonsUpgrade)
                {
                    button.GetComponent<Image>().color = Color.white;
                    button.GetComponent<EventTrigger>().enabled = true;
                    button.enabled = false;
                }
            }
        }
    }

    public void MenuConfirm()
    {
        _buttonBackToMarket.enabled = false;
        _menuConfirmBuyUpgrade.SetActive(true);
        _listUpgradesDog.SetActive(false);
        _listUpgradesPlayer.SetActive(false);
        foreach (var button in _upgradesBar)
            button.enabled = false;
    }

    public void CancelBuy()
    {
        _buttonBackToMarket.enabled = true;
        _menuConfirmBuyUpgrade.SetActive(false);
        _listUpgradesDog.SetActive(true);
        _listUpgradesPlayer.SetActive(true);
        foreach (var button in _upgradesBar)
            button.enabled = true;
    }

    public void BuyDog()
    {
        activeMessage = true;
        activeTutorialDogs = true;
        _newDog.SetActive(true);
        _iconDogsUI.SetActive(true);
        _myAudio.PlayOneShot(_soundBuy);
        _inventory.upgrade = false;
        _menuConfirmBuyUpgrade.SetActive(false);
        _upgradeMenu.SetActive(false);
        _upgradeMessage.SetActive(false);
        _listUpgradesDog.SetActive(false);
        _listUpgradesPlayer.SetActive(false);
        foreach (var menu in _hideMenuGeneral)
            menu.SetActive(true);
    }

    public void SpeedPlayer()
    {
        _player.speed = 13;
        _myAudio.PlayOneShot(_soundBuy);
        _inventory.upgrade = false;
        _menuConfirmBuyUpgrade.SetActive(false);
        _upgradeMenu.SetActive(false);
        _upgradeMessage.SetActive(false);
        _listUpgradesDog.SetActive(false);
        _listUpgradesPlayer.SetActive(false);
        foreach (var menu in _hideMenuGeneral) menu.SetActive(true);
        activeTutorialDogs = false;
        upgradeAxe = false;
    }

    public void BuyUpgrade()
    {
        if (upgradeSpeedPlayer)
        {
            print("SPEED COMPRADO");
            _player._speedAux = 13;
            activeMessage = true;
            upgradeAxe = false;
            activeTutorialDogs = false;
            upgradeTrolley = false;
            upgradeSpeedDog = false;
            upgradeTreeGen = false;
        }

        if (upgradeAddDog)
        {
            print("PERRO AÑADIDO");
            activeMessage = true;
            activeTutorialDogs = true;
            upgradeSpeedPlayer = false;
            upgradeAxe = false;
            upgradeTrolley = false;
            upgradeSpeedDog = false;
            upgradeTreeGen = false;
            _newDog.SetActive(true);
            _iconDogsUI.SetActive(true);
        }

        if (upgradeAxe)
        {
            activeMessage = true;
            activeTutorialDogs = false;
            upgradeSpeedPlayer = false;
            upgradeAddDog = false;
            upgradeTrolley = false;
            upgradeSpeedDog = false;
            upgradeTreeGen = false;
            foreach (var tree in _treesReg) tree.hitDown = 2;
        }

        if (upgradeTrolley)
        {
            activeMessage = true;
            activeTutorialDogs = false;
            upgradeSpeedPlayer = false;
            upgradeAddDog = false;
            upgradeSpeedDog = false;
            upgradeAxe = false;
            upgradeTreeGen = false;
        }

        if (upgradeSpeedDog)
        {
            activeMessage = true;
            activeTutorialDogs = false;
            upgradeSpeedPlayer = false;
            upgradeAddDog = false;
            upgradeTreeGen = false;
            upgradeTrolley = false;
            foreach (var dog in _allDogs) dog.normalSpeed = 15;
        }

        if (upgradeTreeGen)
        {
            activeMessage = true;
            activeTutorialDogs = false;
            upgradeSpeedPlayer = false;
            upgradeAddDog = false;
            upgradeSpeedDog = false;
        }


        _boxMenu.gameObject.SetActive(true); // Muestro el icono de upgrade una vez comprado.

        _myAudio.PlayOneShot(_soundBuy);
        _inventory.upgrade = false;
        _menuConfirmBuyUpgrade.SetActive(false); // Cierro el menu de confirmación de compra de upgrade.
        //BackToMenu();

        //_menuConfirmBuyUpgrade.SetActive(false);
        _upgradeMenu.SetActive(false);
        _upgradeMessage.SetActive(false);
        _listUpgradesDog.SetActive(false);
        _listUpgradesPlayer.SetActive(false);
        foreach (var menu in _hideMenuGeneral)
            menu.SetActive(true);
    }

    public void BackToMenu()
    {
        selected = false;
        activeTutorialDogs = false;
        upgradeAxe = false;
        upgradeSpeedPlayer = false;
        upgradeAddDog = false;
        upgradeTrolley = false;
        upgradeSpeedDog = false;
        upgradeTreeGen = false;
        activeMessage = false;




        _boxMenu.gameObject.SetActive(true);
        _listUpgradesPlayer.SetActive(false);
        _listUpgradesDog.SetActive(false);
        _upgradeMessage.SetActive(false);
        _upgradeMenu.SetActive(false);
        foreach (var menu in _hideMenuGeneral)
            menu.SetActive(true);
    }

    void FreezePlayer()
    {
        _player.gameObject.transform.position = _freezePos.position;
        _player.speed = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        _cam.sensitivity = new Vector2(0, 0);
    }

    void HideHUD(bool hide)
    {
        if (!hide) foreach (var hud in _hud)
                hud.SetActive(true);

        else foreach (var hud in _hud)
                hud.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character2022>();
        if (player != null)
        {
            _uiMarket.SetActive(true);
            FreezePlayer();
            if (_inventory.upgrade)
            {
                _boxMenu.gameObject.SetActive(false);
                _upgradeMessage.SetActive(true);
                foreach (var menu in _hideMenuGeneral)
                    menu.SetActive(false);
            }

            else
            {
                foreach (var menu in _hideMenuGeneral)
                    menu.SetActive(true);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character2022>();
        if (player != null)
        {
            FreezePlayer();
            HideHUD(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character2022>();
        if (player != null)
        {
            _player.gameObject.transform.position = _exitPos.position;
            _textNeedMoney.gameObject.SetActive(false);
        }
    }
}