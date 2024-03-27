using System.Collections;
using System.Collections.Generic;
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
}
