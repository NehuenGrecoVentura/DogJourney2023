using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class Article : MonoBehaviour
{
    [Header("BORDERS")]
    [SerializeField] Image _borderSelected;
    [SerializeField] Image _border;

    [Header("AUDIOS")]
    [SerializeField] AudioClip[] _buttonSounds;
    private AudioSource _myAudio;

    private MarketManager _market;
    private EventTrigger _myEvent;
    private Button _myButton;
    private Image _myImage;

    private CharacterInventory _inventory;

    private void Awake()
    {
        _myAudio = GetComponent<AudioSource>();
        _myEvent = GetComponent<EventTrigger>();
        _myImage = GetComponent<Image>();
        _myButton = GetComponent<Button>();
        _market = FindObjectOfType<MarketManager>();
        _inventory = FindObjectOfType<CharacterInventory>();
    }

    void Start()
    {
        ExitArticle();
    }

    private void ButtonSelectStatus(bool borderActive, bool borderSelectActive, float scale, float time)
    {
        _border.gameObject.SetActive(borderActive);
        _borderSelected.gameObject.SetActive(borderSelectActive);
        transform.DOScale(scale, time);
    }

    public void EnterArticle()
    {
        if (_buttonSounds.Length > 0)
        {
            int random = Random.Range(0, _buttonSounds.Length);
            _myAudio.PlayOneShot(_buttonSounds[random]);
        }

        ButtonSelectStatus(false, true, 7.5f, 0.5f);
    }

    public void ExitArticle()
    {
        ButtonSelectStatus(true, false, 7f, 0.5f);
    }


    public void EnterUprade()
    {
        if (_inventory.upgradeLoot) EnterArticle();
    }

    public void ExitUpgrade()
    {
        if (_inventory.upgradeLoot) ExitArticle();
    }



    private void Success()
    {
        _border.color = Color.green;
        _border.gameObject.SetActive(true);
        Destroy(_borderSelected.gameObject);
        Destroy(_myEvent);
        _inventory.upgradeLoot = false;
        //_market.CheckUpgrades();
        Destroy(this);
    }

    public void UpgradeAxe()
    {
        if (_inventory.upgradeLoot)
        {
            _market.UpgradeAxe();
            Success();
        }

        else _market.ErrorUpgrade(0);
    }

    public void UpgradeSpeedPlayer()
    {
        if (_inventory.upgradeLoot)
        {
            _market.UpgradeSpeedPlayer();
            Success();
        }

        else _market.ErrorUpgrade(1);
    }

    public void UpgradeRegenerate()
    {
        if (_inventory.upgradeLoot)
        {
            _market.UpgradeRegenerate();
            Success();
        }

        else _market.ErrorUpgrade(2);
    }

    public void UpgradeTrolley()
    {
        if (_inventory.upgradeLoot)
        {
            _market.UpgradeTrolley();
            Success();
        }

        else _market.ErrorUpgrade(3);
    }

    public void UpgradeSpeedDog()
    {
        if (_inventory.upgradeLoot)
        {
            _market.UpgradeSpeedDog();
            Success();
        }

        else _market.ErrorUpgrade(4);
    }

    public void UpgradeDog()
    {
        if (_inventory.upgradeLoot)
        {
            _market.UpgradeSpeedDog();
            Success();
        }

        else _market.ErrorUpgrade(5);
    }

    public void SellWood()
    {
        if (_inventory.greenTrees >= 10) _market.SellWood();
        else _market.ErrorUpgrade(6);
    }
}