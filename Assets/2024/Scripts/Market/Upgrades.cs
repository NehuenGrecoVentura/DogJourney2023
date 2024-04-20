using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Upgrades : MonoBehaviour
{
    private CharacterInventory _inventory;
    private TreeRegenerative[] _allTrees;
    private SaplingTree[] _sapling;
    private HitBar[] _hitBars;
    private Character _player;
    private Image _myImage;
    private Trunks[] _trunks;

    [SerializeField] Dog[] _dogs;
    [SerializeField] Color[] _colorsStatus;
    [SerializeField] MarketShop _market;

    [SerializeField] GameObject _description;
    [SerializeField] GameObject _iconUpgrade;
    [SerializeField] Button _myButton;

    [Header("MESSAGE SLIDE")]
    [SerializeField] Sprite _iconMessage;
    [SerializeField] string _textMessage;
    private MessageSlide _messageSlide;

    private void Awake()
    {
        _myImage = GetComponent<Image>();

        _player = FindObjectOfType<Character>();
        _inventory = FindObjectOfType<CharacterInventory>();
        _allTrees = FindObjectsOfType<TreeRegenerative>();
        _sapling = FindObjectsOfType<SaplingTree>();
        _hitBars = FindObjectsOfType<HitBar>();
        _trunks = FindObjectsOfType<Trunks>();
        _messageSlide = FindObjectOfType<MessageSlide>();
    }

    private void Update()
    {
        if (_inventory.upgradeLoot)
            _myImage.color = _colorsStatus[1];

        else _myImage.color = _colorsStatus[0];
    }

    private void UpgradeSuccess()
    {
        _messageSlide.ShowMessage(_textMessage, _iconMessage);
        _description.SetActive(false);
        _myImage.color = _colorsStatus[2];
        Destroy(_iconUpgrade);
        Destroy(_description);
        Destroy(_myButton);
        _inventory.upgradeLoot = false;
        Destroy(this);
    }

    #region PLAYER UPGRADE

    public void UpgradeAxe()
    {
        if (_inventory.upgradeLoot)
        {
            foreach (var tree in _allTrees)
            {
                tree.initialAmount = 100;
                tree.RestartAmount();
            }

            foreach (var hitBar in _hitBars)
                hitBar.UpgradeBar();

            UpgradeSuccess();
        }
    }

    public void UpgradeTree()
    {
        if (_inventory.upgradeLoot)
        {
            foreach (var sapling in _sapling)
                sapling.isUpgraded = true;

            UpgradeSuccess();
        }
    }

    public void UpgradeSpeedPlayer()
    {
        if (_inventory.upgradeLoot)
        {
            _player.speedAux = 13; // Ahora la velocidad del player es más rápida al caminar.
            _player.speedRun = 20; // Ahora la velocidad del player es más rápida al correr.
            _player.speed = 13;
            UpgradeSuccess();
        }
    }

    #endregion

    #region DOG UPGRADE

    public void UpgradeTrolley()
    {
        if (_inventory.upgradeLoot)
        {
            foreach (var trunk in _trunks)
                trunk.isUpgraded = true;

            UpgradeSuccess();
        }
    }

    public void UpgradeSpeedDog()
    {
        if (_inventory.upgradeLoot)
        {
            foreach (var dog in _dogs)
            {
                dog.speedNormal = 15f;
                dog.speedRun = 20f;
            }

            UpgradeSuccess();
        }
    }

    public void UpgradeDog()
    {
        if (_inventory.upgradeLoot)
        {
            _market.AddDog();
            UpgradeSuccess();
        }
    }

    #endregion

    #region EVENTS TRIGGERS

    //Events para mostrar la descripción de los upgrades
    public void ShowDescription()
    {
        _description.SetActive(true);
    }

    public void HideDescription()
    {
        _description.SetActive(false);
    }

    #endregion
}
