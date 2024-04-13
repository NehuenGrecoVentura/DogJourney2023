using UnityEngine;
using System.Collections;
using DG.Tweening;

public class MessageStealth : MonoBehaviour
{
    [Header("MESSAGE")]
    [SerializeField] GameObject _boxMessage;
    private Collider _myCol;

    private void Awake()
    {
        _myCol = GetComponent<Collider>();
    }

    void Start()
    {
        _boxMessage.gameObject.SetActive(false);
        _boxMessage.transform.DOScale(0, 0);
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
        _boxMessage.transform.DOScale(0.8f, 0.5f);
        yield return new WaitForSeconds(4f);
        _boxMessage.GetComponent<RectTransform>().DOMoveY(-1000, 1f);
        Destroy(_boxMessage, 2f);
        Destroy(this);
    }
}