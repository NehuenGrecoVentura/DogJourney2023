using UnityEngine;
using System.Collections;
using DG.Tweening;

public class MessageStealth : MonoBehaviour
{
    [Header("MESSAGE")]
    [SerializeField] RectTransform _boxMessage;
    private Collider _myCol;

    private void Awake()
    {
        _myCol = GetComponent<Collider>();
    }

    void Start()
    {
        _boxMessage.gameObject.SetActive(false);
        _boxMessage.DOAnchorPosY(-1000f, 0f);
        _boxMessage.localScale = new Vector3(0.8f, 0.8f, 0.8f);

    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) StartCoroutine(Display());
    }

    private IEnumerator Display()
    {
        
        Destroy(_myCol);
        _boxMessage.gameObject.SetActive(true);
        _boxMessage.DOAnchorPosY(-100f, 0.5f);
        yield return new WaitForSeconds(4f);
        _boxMessage.DOAnchorPosY(-1000f, 0.5f);
        Destroy(_boxMessage.transform.parent.gameObject, 2f);
        Destroy(this, 2f);
    }
}