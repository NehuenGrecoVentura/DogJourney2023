using UnityEngine;
using UnityEngine.UI;

public class BrightnessManager : MonoBehaviour
{
    [SerializeField] Slider _slider;
    [SerializeField] float _sliderValue;
    [SerializeField] Image _panelBrightness;

    void Start()
    {
        _slider.value = PlayerPrefs.GetFloat("Brightness", 0.5f);
        _panelBrightness.color = new Color(_panelBrightness.color.r, _panelBrightness.color.g, _panelBrightness.color.b, _slider.value);
    }

    public void ChangeSlider(float value)
    {
        _sliderValue = value;
        PlayerPrefs.SetFloat("Brightness", _sliderValue);
        _panelBrightness.color = new Color(_panelBrightness.color.r, _panelBrightness.color.g, _panelBrightness.color.b, _slider.value);
    }
}