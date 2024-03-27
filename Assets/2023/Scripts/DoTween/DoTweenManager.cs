using UnityEngine;
using DG.Tweening;

public class DoTweenManager : MonoBehaviour
{
    [SerializeField] float _shakeStrenght;
    [SerializeField] float _duration;

    public void Shake(Transform obj)
    {
        obj.DOShakeRotation(_duration, _shakeStrenght);
    }

    public void EffectScale(Transform obj)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(obj.DOScale(1.3f, 0.5f));
        sequence.Append(obj.DOScale(1f, 0.5f));
    }
}