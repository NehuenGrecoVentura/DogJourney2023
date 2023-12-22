using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WolfMessage : MonoBehaviour
{
    [SerializeField] Image _message;
    [SerializeField] float _timeInScreen;
    [SerializeField] GameObject _descriptionQuest;

    void Start()
    {
        _message.gameObject.SetActive(false);
        _descriptionQuest.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character2022>();
        if (player != null)
        {
            if (gameObject.CompareTag("Wolf")) _descriptionQuest.SetActive(true);
            _message.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character2022>();
        if (player != null) StartCoroutine(HideMessage());
    }

    IEnumerator HideMessage()
    {
        while (true)
        {
            yield return new WaitForSeconds(_timeInScreen);
            _message.gameObject.SetActive(false);
            StopAllCoroutines();
        }
    }
}