using UnityEngine;

public class MarketPlace : MonoBehaviour
{
    private MarketManager _market;
    [SerializeField] Transform _exitPos;
    private LocationQuest _radar;

    private void Awake()
    {
        _market = FindObjectOfType<MarketManager>();
        _radar = FindObjectOfType<LocationQuest>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
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
        }      
    }
}