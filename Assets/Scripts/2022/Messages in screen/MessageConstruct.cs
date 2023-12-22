using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MessageConstruct : MonoBehaviour
{
    [Header("UI MESSAGE")]
    [SerializeField] GameObject _canvasMessage;
    [SerializeField] Image _spaceBar;
    [SerializeField] TMP_Text _text;
    [SerializeField] float _timeInScreen = 2f;

    [Header("REFERENCES CONFIG")]
    [SerializeField] Inventory _inventory;
    Construct _construct;

    void Start()
    {
        _construct = GetComponent<Construct>();
        _canvasMessage.SetActive(false);
    }

    void ShowMessage()
    {
        _canvasMessage.SetActive(true);

        if (Input.GetKey(_construct.buttonInteractive))
        {
            if (_inventory.amountnails >= _construct.amountNails && _inventory.amountWood >= _construct.amountWood)
                _spaceBar.color = Color.yellow;

            else _spaceBar.color = Color.red;
        }

        else if (Input.GetKeyUp(_construct.buttonInteractive))
            _spaceBar.color = Color.white;

        if (_construct.progress >= 100)
        {
            _spaceBar.color = Color.green;
            _text.color = Color.green;
            _text.text = "GOOD";
            _timeInScreen -= Time.deltaTime;
        }

        if (_timeInScreen <= 0) Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character2022>();
        if (player != null)
        {
            //if (_inventory.amountWood >= _construct.amountWood && _inventory.amountnails >= _construct.amountNails)
            ShowMessage();

            //else _canvasMessage.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character2022>();
        if (player != null)
        {
            //if (_inventory.amountWood >= _construct.amountWood && _inventory.amountnails >= _construct.amountNails)
            ShowMessage();

            //else _canvasMessage.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character2022>();
        if (player != null)
        {
            _spaceBar.color = Color.white;
            _canvasMessage.SetActive(false);
        }


    }
}