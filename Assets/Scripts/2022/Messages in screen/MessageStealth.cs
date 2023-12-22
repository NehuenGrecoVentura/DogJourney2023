using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class MessageStealth : MonoBehaviour
{
    [Header("CANVAS CONFIG")]
    [SerializeField] private GameObject _canvasKey;
    [SerializeField] private GameObject _canvasStealth;

    [Header("UI CONFIG")]
    [SerializeField] private Image _iconKey;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private float _timeInScreen = 4f;

    private bool _messageActive = false;

    void Start()
    {
        _canvasKey.SetActive(false);
    }

    void Update()
    {
        if (_messageActive && Input.GetKey(KeyCode.LeftControl))
        {
            _iconKey.color = Color.green;
            _text.color = Color.green;
            _text.fontSize = 36;
            _text.text = "GOOD";
            StartCoroutine(HideMessage());
        }

        else if (_messageActive && Input.GetKeyDown(KeyCode.LeftControl))
            _iconKey.color = Color.yellow;
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            if (!_messageActive)
            {
                _canvasKey.SetActive(true);
                _messageActive = true;
            }
        }
    }

    IEnumerator HideMessage()
    {
        yield return new WaitForSeconds(_timeInScreen);
        Destroy(_canvasStealth);
        Destroy(this);
    }
}