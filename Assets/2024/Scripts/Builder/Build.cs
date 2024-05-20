using UnityEngine;

public class Build : BuilderManager
{
    [Header("MESSAGE SLIDE")]
    [SerializeField] Sprite _iconMarket;
    [SerializeField] Sprite _iconBridge;
    [SerializeField] string _messageText;

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            BuildObj(_inventory.nails, _inventory.greenTrees);
        }
    }
}