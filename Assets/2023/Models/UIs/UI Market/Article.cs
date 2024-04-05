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

    private CharacterInventory _inventory;

    private void Awake()
    {
        _myAudio = GetComponent<AudioSource>();
        _myEvent = GetComponent<EventTrigger>();
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

    private void Success()
    {
        _border.color = Color.green;
        _border.gameObject.SetActive(true);
        Destroy(_borderSelected.gameObject);
        Destroy(_myEvent);
        Destroy(_myButton);
    }

    public void UpgradeAxe()
    {
        //_market.UpgradeAxe();
        //if (_market.axeUpgraded) Success();

        if (_inventory.upgradeLoot)
        {
            Success();
            _inventory.upgradeLoot = false;
            Destroy(this);
        }

        else _market.UpgradeAxe();
    }

    public void UpgradeSpeedPlayer()
    {
        if (_inventory.upgradeLoot)
        {
            Success();
            _inventory.upgradeLoot = false;
            Destroy(this);
        }

        else _market.UpgradeSpeedPlayer();
    }

    public void UpgradeRegenerate()
    {
        if (_inventory.upgradeLoot)
        {
            Success();
            _inventory.upgradeLoot = false;
            Destroy(this);
        }

        else _market.UpgradeRegenerate();
    }
}