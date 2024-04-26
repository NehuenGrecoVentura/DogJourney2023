using UnityEngine;
using TMPro;

public class Bush : MonoBehaviour
{
    [SerializeField] float _amountHit = 500f;
    [SerializeField] KeyCode _inputInteractive = KeyCode.Mouse0;
    [SerializeField] DoTweenManager _message;
    [SerializeField] RectTransform _boxMessage;
    [SerializeField] TMP_Text _textAmount;

    private void Start()
    {
        _boxMessage.gameObject.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && Input.GetKey(_inputInteractive))
        {
            player.HitTree();
            _amountHit--;

            if (_amountHit <= 0)
            {
                _amountHit = 0;
                _message.ShowUI("+2", _boxMessage, _textAmount);
                Destroy(gameObject);
            }
        }
    }
}