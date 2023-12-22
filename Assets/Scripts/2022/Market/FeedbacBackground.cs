using UnityEngine;
using UnityEngine.UI;

public class FeedbacBackground : MonoBehaviour
{
    Image _backgroundSelect;

    private void Awake()
    {
        _backgroundSelect = GetComponent<Image>();
    }

    public void GreenSelect()
    {
        _backgroundSelect.color = Color.green;
    }

    public void Deselect()
    {
        _backgroundSelect.color = Color.white;
    }





}