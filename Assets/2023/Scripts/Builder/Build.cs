using UnityEngine;

public class Build : BuilderManager
{
    [Header("MESSAGE SLIDE")]
    [SerializeField] Sprite _iconMarket;
    [SerializeField] Sprite _iconBridge;
    [SerializeField] string _messageText;
    private MessageSlide _messageSlide;
    private bool _firstContact = false;

    private void Awake()
    {
        _messageSlide = FindObjectOfType<MessageSlide>();
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            BuildObj(_inventory.nails, _inventory.greenTrees);

            if (!_firstContact && gameObject.name != "Build Stairs")
            {
                _messageSlide.ShowMessage(_messageText, _iconMarket);
                _firstContact = true;
            }

            if(_inventory.nails >= _amountItem1 && _inventory.greenTrees >= _amountItem2)
                _messageSlide.ShowMessage("PRESS TO BUILD", _iconBridge);
        }
    }
}