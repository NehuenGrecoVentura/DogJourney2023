using UnityEngine;

public class WolfZone : MonoBehaviour
{
    private SleepWolf2022[] _sleepWolfs;
    public GameManager _gm;

    void Start()
    {
        _sleepWolfs = FindObjectsOfType<SleepWolf2022>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag=="Player" && !Input.GetKey(KeyCode.LeftControl))
        {
            foreach (var wolf in _sleepWolfs) wolf.Stand();
            _gm.GameOver();
        }
    }
}
