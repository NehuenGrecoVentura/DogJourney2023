using UnityEngine;
using TMPro;

public class GraphicsManager : MonoBehaviour
{
    [SerializeField] TMP_Dropdown _dropdown;
    [SerializeField] int _quality;

    void Start()
    {
        _quality = PlayerPrefs.GetInt("NumberQuality", 3);
        _dropdown.value = _quality;
    }

    public void SelectQuality()
    {
        QualitySettings.SetQualityLevel(_dropdown.value);
        PlayerPrefs.SetInt("NumberQuality", _dropdown.value);
        _quality = _dropdown.value;
    }
}