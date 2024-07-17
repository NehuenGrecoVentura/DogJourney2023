using UnityEngine;
using UnityEngine.UI;

public class HitBar : MonoBehaviour
{
    [SerializeField] TreeRegenerative _tree;
    [SerializeField] Image _bar;
    [SerializeField] Image PointA;
    private Slider _hitBar;
    [SerializeField] private float HitPointA;
    [SerializeField] private float MinA;
    [SerializeField] private float MaxA;
    [SerializeField] private float MinRange;
    [SerializeField] private float MaxRange;
    [SerializeField] private float ExtraDamage;
    [SerializeField] private float AddZone;
    
    
    
    public float originalMin = 0f;  // Rango original mínimo
    public float originalMax = 100f;  // Rango original máximo
    public float newMin = -67f;  // Nuevo rango mínimo
    public float newMax = 83f;  // Nuevo rango máximo

    public float NormalizeValue(float x)
    {
        // Asegúrate de que x esté dentro del rango original
        float clampedX = Mathf.Clamp(x, originalMin, originalMax);

        // Normaliza el valor a un rango nuevo
        float normalizedValue = newMin + ((clampedX - originalMin) * (newMax - newMin)) / (originalMax - originalMin);
        return normalizedValue;
    }

    private void Awake()
    {
        _hitBar = GetComponent<Slider>();
        ExtraDamage = 50;
        AddZone = 40;
    }

    private void Start()
    {
        _hitBar.maxValue = _tree.amountHit;
        _hitBar.value = _hitBar.maxValue;
        _bar.color = Color.green;
    }

    public void RandomPoints() 
    {
        if (PointA == null)
        {
            return;
        }
        else
        {
            MinRange = _tree.initialAmount / 10;
            MaxRange = _tree.initialAmount - MinRange;
            HitPointA = Random.Range(MinRange, MaxRange); //punto mas a la izquierda de la barra
            MinA = HitPointA - AddZone;
            MaxA = HitPointA + AddZone;
            var TESTA = (HitPointA * 100 / _tree.initialAmount);
            Debug.Log(TESTA);
            PointA.transform.localPosition = new Vector3(NormalizeValue(TESTA), 1, 1); 
        }
        
    }

    public void CheckRandom()
    {
        if (PointA == null)
        {
            return;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (_tree.amountHit > MinA && _tree.amountHit <= MaxA)
                {
                    Debug.Log("TasAdentro");
                    _tree.amountHit =  _tree.amountHit - ExtraDamage;
                    Bar();
                }
            }
        }

    }

    public void Bar()
    {
        _hitBar.value = _tree.amountHit;
        CheckColor();
    }

    public void UpgradeBar()
    {
        _hitBar.value = _tree.initialAmount;
        _hitBar.maxValue = _tree.initialAmount;
    }

    public void CheckColor()
    {
        float sliderValue = _hitBar.value;
        if (sliderValue >= 60f) _bar.color = Color.green;
        else if (sliderValue >= 30f) _bar.color = Color.yellow;
        else _bar.color = Color.red;
    }

    private void OnEnable()
    {
        Bar();
    }
}