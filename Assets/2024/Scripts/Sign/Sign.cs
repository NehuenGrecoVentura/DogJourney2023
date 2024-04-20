using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Sign : MonoBehaviour
{
    [SerializeField] float _timeInScreen = 3f;
    [SerializeField] RectTransform _canvasMessage;
    [SerializeField] Collider _col;

    void Start()
    {
        _canvasMessage.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) StartCoroutine(ShowMessage());
    }

    private IEnumerator ShowMessage()
    {
        Destroy(_col);
        _canvasMessage.gameObject.SetActive(true);
        _canvasMessage.DOAnchorPosY(-70f, 0.5f);
        yield return new WaitForSeconds(_timeInScreen);
        _canvasMessage.DOAnchorPosY(-1000f, 0.5f);
        Destroy(_canvasMessage.transform.parent.gameObject, 1f);
        Destroy(this);
    }
}