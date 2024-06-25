using UnityEngine;
using UnityEngine.UI;

public class HitTreasure : MonoBehaviour
{
    [SerializeField] DigTreasure _dig;
    [SerializeField] Image _bar;
    private Slider _hitBar;

    private void Awake()
    {
        _hitBar = GetComponent<Slider>();
    }

    private void Start()
    {
        _hitBar.maxValue = _dig.amountHit;
        _hitBar.value = _hitBar.maxValue;
        _bar.color = Color.green;
    }

    public void Bar()
    {
        _hitBar.value = _dig.amountHit;
        CheckColor();
    }

    public void UpgradeBar()
    {
        _hitBar.value = _dig.amountHit;
        _hitBar.maxValue = _dig.amountHit;
    }

    public void CheckColor()
    {
        float sliderValue = _hitBar.value;
        if (sliderValue >= 60f) _bar.color = Color.green;
        else if (sliderValue >= 30f) _bar.color = Color.yellow;
        else _bar.color = Color.red;
    }
}
