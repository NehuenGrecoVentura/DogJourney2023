using UnityEngine;
using UnityEngine.UI;

public class TextTutorialQuest : MonoBehaviour
{
    [SerializeField] Text _text1;
    [SerializeField] Text _text2;
    [SerializeField] Text _textBack;
    [SerializeField] Text _trunksAmmount;
    Inventory _inventory;

    private void Start()
    {
        _inventory = FindObjectOfType<Inventory>();
        _textBack.gameObject.SetActive(false);
    }

    private void Update()
    {
        _trunksAmmount.text = _inventory.amountWood.ToString();
        if (_inventory.amountWood >= 100)
        {
            _text1.gameObject.SetActive(false);
            _text2.gameObject.SetActive(false);
            _trunksAmmount.gameObject.SetActive(false);
            _textBack.gameObject.SetActive(true);
        }
    }
}
