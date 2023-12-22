using System.Collections;
using UnityEngine;

public class Sign : MonoBehaviour
{
    [SerializeField] float _timeInScreen = 3f;
    [SerializeField] GameObject _canvasMessage;
    [SerializeField] Collider _col;

    void Start()
    {
        _canvasMessage.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) StartCoroutine(ShowMessage());
    }

    private IEnumerator ShowMessage()
    {
        Destroy(_col);
        _canvasMessage.SetActive(true);
        yield return new WaitForSeconds(_timeInScreen);
        Destroy(_canvasMessage);
        Destroy(this);
    }
}