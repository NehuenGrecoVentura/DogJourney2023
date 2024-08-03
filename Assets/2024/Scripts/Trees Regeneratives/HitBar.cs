using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

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
    [SerializeField] private AudioSource Audio;


    [Header("SPECIAL HIT")]
    public float originalMin = 0f;  // Rango original mínimo
    public float originalMax = 100f;  // Rango original máximo
    public float newMin = -67f;  // Nuevo rango mínimo
    public float newMax = 83f;  // Nuevo rango máximo
    [SerializeField] Color _colorHit;
    private Color _initialColor;
    private Vector3 _initialScale;
    public bool hitGood = false;

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

        _initialColor = PointA.color;
        _initialScale = PointA.rectTransform.localScale;
    }

    //public void RandomPoints()
    //{
    //    if (PointA == null)
    //    {
    //        return;
    //    }
    //    else
    //    {
    //        MinRange = _tree.initialAmount / 10;
    //        MaxRange = _tree.initialAmount - MinRange;
    //        HitPointA = Random.Range(MinRange, MaxRange); //punto mas a la izquierda de la barra
    //        MinA = HitPointA - AddZone;
    //        MaxA = HitPointA + AddZone;
    //        var TESTA = (HitPointA * 100 / _tree.initialAmount);
    //        Debug.Log(TESTA);
    //        PointA.transform.localPosition = new Vector3(NormalizeValue(TESTA), 1, 1);

    //        StartCoroutine(SpecialHitAnim()); // EMPIEZA LA ANIMACION DE LA RAYA - NEHUEN
    //    }
    //}

    public void RandomPoints()
    {
        
        if (_hitBar.value >= _hitBar.maxValue || _tree.hasContact) // PONGO ESTO PARA EVITAR QUE SE GENERE OTRO NUEVO GOLPE ESPECIAL EN LA BARRA SI YA INTERACTUASTE CON ANTERIORIDAD AL ARBOL - NEHUEN
        {
            if (PointA == null) return;

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

                PointA.color = _initialColor;
            }
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
                    Audio.Play();
                    Debug.Log("TasAdentro");
                    _tree.amountHit = _tree.amountHit - ExtraDamage;
                    Bar();

                    hitGood = true;
                    PointA.color = _colorHit; // CAMBIA DE COLOR CUANDO LE ENGANCHAS - NEHUEN
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


    private IEnumerator SpecialHitAnim()
    {
        float minYScale = 0.17f;
        float maxYScale = 0.27f;
        float duration = 0.5f;
        Vector3 initialScale = PointA.rectTransform.localScale;
        initialScale.y = minYScale;
        PointA.rectTransform.localScale = initialScale;

        Sequence scaleSequence = DOTween.Sequence();
        scaleSequence.Append(PointA.rectTransform.DOScaleY(maxYScale, duration / 2f).SetEase(Ease.InOutSine))
                     .Append(PointA.rectTransform.DOScaleY(minYScale, duration / 2f).SetEase(Ease.InOutSine))
                     .SetLoops(-1); // -1 significa loop infinito

        while (true)
        {
            yield return null;
        }
    }

    private void OnEnable()
    {
        Bar();
    }
}