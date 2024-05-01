using UnityEngine;

public class MarketPlace : MonoBehaviour
{
    private MarketManager _market;
    [SerializeField] Transform _exitPos;
    private LocationQuest _radar;
    private MenuPause _pause;
    [SerializeField] GameObject _ui;

    private void Awake()
    {
        _market = FindObjectOfType<MarketManager>();
        _radar = FindObjectOfType<LocationQuest>();
        _pause = FindObjectOfType<MenuPause>();
    }

    private void Start()
    {
        _ui.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            _pause.Freeze();
            _market.isShopping = true;
            _ui.SetActive(true);
            _market.OpenMarket();
            _radar.StatusRadar(false);
        }   
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            player.gameObject.transform.position = _exitPos.position;
            _radar.StatusRadar(true);
            _market.isShopping = false;
        }      
    }
}