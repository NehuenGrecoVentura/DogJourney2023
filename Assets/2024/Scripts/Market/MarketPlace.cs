using UnityEngine;

public class MarketPlace : MonoBehaviour
{
    private MarketManager _market;
    [SerializeField] Transform _exitPos1;
    [SerializeField] Transform _exitPos2;
    private LocationQuest _radar;
    private MenuPause _pause;

    public bool market1 = false;
    public bool market2 = false;

    private void Awake()
    {
        _market = FindObjectOfType<MarketManager>();
        _radar = FindObjectOfType<LocationQuest>();
        _pause = FindObjectOfType<MenuPause>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            _pause.Freeze();
            _market.isShopping = true;
            _market.OpenMarket();
            _radar.StatusRadar(false);


            if (gameObject.name == "Market 1")
            {
                player.gameObject.transform.position = _exitPos1.position;
                market1 = true;
            }

            else
            {
                player.gameObject.transform.position = _exitPos2.position;
                market2 = true;
            }



        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {

            if (gameObject.name == "Market 1")
            {
                player.gameObject.transform.position = _exitPos1.position;
                //market1 = false;
            }

            else
            {
                player.gameObject.transform.position = _exitPos2.position;
                //market2 = false;
            }




            _radar.StatusRadar(true);
            _market.isShopping = false;
        }
    }

    public void SetPlayerPos(Character player)
    {
        //player.gameObject.transform.position = _exitPos.position;

        if (market1)
        {
            player.gameObject.transform.position = _exitPos1.position;
            market1 = false;
        }

        if (market2)
        {
            player.gameObject.transform.position = _exitPos2.position;
            market2 = false;
        }
    }
}