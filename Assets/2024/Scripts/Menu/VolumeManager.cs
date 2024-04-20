using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    [SerializeField] Slider _slider;
    [SerializeField] float _sliderValue;
    [SerializeField] Image imageMute;

    void Start()
    {
        _slider.value = PlayerPrefs.GetFloat("volumenAudio", 0.5f);
        AudioListener.volume = _slider.value;
        CheckMute();
    }

    public void ChangeSlider(float value)
    {
        _sliderValue = value;
        PlayerPrefs.SetFloat("volumenAudio", _sliderValue);
        AudioListener.volume = _slider.value;
        CheckMute();
    }

    public void CheckMute()
    {
        if (_sliderValue == 0) imageMute.enabled = true;
        else imageMute.enabled = false;
    }
}
