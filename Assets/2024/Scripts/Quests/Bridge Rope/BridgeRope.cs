using UnityEngine;

public class BridgeRope : MonoBehaviour
{
    private QuestManager _manager;

    private void Awake()
    {
        _manager = FindObjectOfType<QuestManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            _manager.FirstSuccess("Go to the market");
            _manager.InitialSecondPhase("Buy the ropes");
            Destroy(this);
        }
    }

    public void UnlockBridge()
    {
        _manager.SecondSuccess("Buy the ropes");
        Destroy(this);
    }
}