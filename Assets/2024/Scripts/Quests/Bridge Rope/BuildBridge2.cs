using UnityEngine;

public class BuildBridge2 : BuilderManager
{
    [SerializeField] Sprite _iconBridge;
    private MessageSlide _messageSlide;

    private void Awake()
    {
        _messageSlide = FindObjectOfType<MessageSlide>();
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            BuildObj(_inventory.greenTrees, _inventory.ropes);

            if (_inventory.ropes >= _amountItem1 && _inventory.greenTrees >= _amountItem1)
                _messageSlide.ShowMessage("PRESS TO BUILD", _iconBridge);
        }
    }
}