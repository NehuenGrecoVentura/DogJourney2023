using UnityEngine;
using UnityEngine.UI;

public class SliderTrunks : MonoBehaviour
{
    [SerializeField] Slider _slider;
    [SerializeField] Text _textAmount;
    Inventory _inventory;

    void Start()
    {
        _inventory = FindObjectOfType<Inventory>();
    }

    void Update()
    {
        _slider.value = _inventory.amountWood;
        _textAmount.text = _inventory.amountWood.ToString();
    }
}