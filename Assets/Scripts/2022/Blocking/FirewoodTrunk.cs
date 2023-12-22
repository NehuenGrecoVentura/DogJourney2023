using UnityEngine;

public class FirewoodTrunk : MonoBehaviour
{
    [SerializeField] GameObject _iconInteractive;
    [SerializeField] KeyCode _buttonInteractive = KeyCode.F;
    private Inventory _inventory;
    [Header("AUDIO CONFIG")]
    private Character2022 _audioPlayer;
    [SerializeField] AudioClip _silbido;
    [SerializeField] AudioClip _itemSound;
    private Dog2022 _dog;
    private Trolley _trolley;
    private NewMarket _markets;
    private Quest4 _quest4;

    private void Awake()
    {
        _dog = FindObjectOfType<Dog2022>();
        _inventory = FindObjectOfType<Inventory>();
        _audioPlayer = FindObjectOfType<Character2022>();
        _trolley = FindObjectOfType<Trolley>();
        _markets = FindObjectOfType<NewMarket>();
        _quest4 = FindObjectOfType<Quest4>();
    }

    void Start()
    {
        gameObject.SetActive(false);
        _iconInteractive.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        var dog = other.GetComponent<Dog2022>();
        if (dog != null)
        {
            _iconInteractive.gameObject.SetActive(false);
            if (_markets.activeUpgradeTrolley) _inventory.amountWood += 15;
            else
            {
                if (gameObject.tag == "Purple Trunks") _inventory.amountPurple++;
                else
                {
                    _inventory.amountWood += 10;
                    if (_quest4.isQuest4) _quest4._initialAmount++;
                } 
                    
                   

            }

            gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character2022>();
        if (player != null)
        {
            if (!Input.GetKeyDown(_buttonInteractive))
                _iconInteractive.gameObject.SetActive(true);

            else
            {
                _audioPlayer.EjecuteAnim("Silbido Idle");
                _audioPlayer.GetComponent<AudioSource>().PlayOneShot(_silbido);
                _dog.OrderGO();
                _iconInteractive.gameObject.SetActive(false);
                _trolley.gameObject.GetComponent<AudioSource>().Play();
                _dog.gameObject.GetComponent<AudioSource>().Play();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character2022>();
        if (player != null) _iconInteractive.gameObject.SetActive(false);
    }
}