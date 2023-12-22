using UnityEngine;

public class Trolley : MonoBehaviour
{
    Inventory _inventory;
    [SerializeField] GameObject _trucksLow;
    [SerializeField] GameObject _trucksMedium;
    [SerializeField] GameObject _trucksHigh;
    [SerializeField] GameObject _boxUpgrade;

    void Start()
    {
        _inventory = FindObjectOfType<Inventory>();
        _trucksLow.SetActive(false);
        _trucksMedium.SetActive(false);
        _trucksHigh.SetActive(false);
    }

    void Update()
    {
        if (_inventory.upgrade) _boxUpgrade.SetActive(true);
        else _boxUpgrade.SetActive(false);

        if (_inventory.amountWood >= 1 && _inventory.amountWood < 20)
        {
            _trucksLow.SetActive(true);
            _trucksMedium.SetActive(false);
            _trucksHigh.SetActive(false);
        }

        else if (_inventory.amountWood >= 20 && _inventory.amountWood < 30)
        {
            _trucksLow.SetActive(true);
            _trucksMedium.SetActive(true);
            _trucksHigh.SetActive(false);
        }

        else if (_inventory.amountWood >= 30)
        {
            _trucksLow.SetActive(true);
            _trucksMedium.SetActive(true);
            _trucksHigh.SetActive(true);
        }
    }
}