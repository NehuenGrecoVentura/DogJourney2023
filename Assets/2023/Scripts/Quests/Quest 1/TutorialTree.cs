using System.Collections;
using UnityEngine;
using TMPro;

public class TutorialTree : MonoBehaviour
{
    [SerializeField] float _timeMessage;
    [SerializeField] TMP_Text _textMessage;

    public void MouseSuccess()
    {
        StartCoroutine(HideMessage());
    }

    private IEnumerator HideMessage()
    {
        _textMessage.text = "GOOD";
        _textMessage.color = Color.green;
        yield return new WaitForSeconds(_timeMessage);
        Destroy(gameObject);
    }
}