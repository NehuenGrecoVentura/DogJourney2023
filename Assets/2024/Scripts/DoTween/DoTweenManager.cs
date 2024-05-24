using UnityEngine;
using System.Collections;
using DG.Tweening;
using TMPro;

public class DoTweenManager : MonoBehaviour
{
    [SerializeField] float _shakeStrenght;
    [SerializeField] float _duration;

    public void Shake(Transform obj)
    {
        obj.DOShakeRotation(_duration, _shakeStrenght);
    }

    public void JumpEffect(Transform obj, Vector3 endPos, float jumpForce, int jumpCount, float duration)
    {
        obj.DOJump(endPos, jumpForce, jumpCount, duration);
    }

    public void EffectScale(Transform obj)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(obj.DOScale(1.3f, 0.5f));
        sequence.Append(obj.DOScale(1f, 0.5f));
    }

    public void ShowUI(string amount, RectTransform obj, TMP_Text textAmount)
    {
        StartCoroutine(ShowUICoroutine(amount, obj, textAmount));
    }

    private IEnumerator ShowUICoroutine(string amount, RectTransform obj, TMP_Text textAmount)
    {
        obj.anchoredPosition = new Vector2(-1000f, obj.anchoredPosition.y);
        textAmount.text = amount;
        obj.gameObject.SetActive(true);
        obj.DOAnchorPosX(1150f, 1f);
        yield return new WaitForSeconds(3f);
        obj.DOAnchorPosX(-1000f, 1f);
        yield return new WaitForSeconds(1f);
        obj.gameObject.SetActive(false);
    }


    public void AddIconInventory(RectTransform obj, TMP_Text textMessage, string message)
    {
        StartCoroutine(UpdateInventory(obj, textMessage, message));
    }

    private IEnumerator UpdateInventory(RectTransform obj, TMP_Text textMessage, string message)
    {
        textMessage.text = message;
        obj.anchoredPosition = new Vector2(-1000f, obj.anchoredPosition.y);
        obj.gameObject.SetActive(true);
        obj.DOAnchorPosX(1150f, 1f);
        yield return new WaitForSeconds(3f);
        obj.DOAnchorPosX(-1000f, 1f);
        yield return new WaitForSeconds(1f);
        obj.gameObject.SetActive(false);
    }

    public void EffectScaleLoop(Transform obj, float scale)
    {
        obj.DOScale(scale, 0.5f).SetLoops(-1, LoopType.Yoyo);
    }

    public void StopAnim(Transform obj)
    {
        obj.DOKill();
    }
}