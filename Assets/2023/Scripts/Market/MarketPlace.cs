using UnityEngine;

public class MarketPlace : MonoBehaviour
{
    private MarketManager _market;
    [SerializeField] Transform _exitPos;

    private void Awake()
    {
        _market = FindObjectOfType<MarketManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) _market.OpenMarket();
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) player.gameObject.transform.position = _exitPos.position;
    }
}