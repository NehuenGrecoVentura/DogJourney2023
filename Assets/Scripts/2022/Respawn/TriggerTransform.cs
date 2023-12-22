using UnityEngine;

public class TriggerTransform : MonoBehaviour
{
    [SerializeField] RespawnManager _respawnManager;
    [SerializeField] Transform _newPos;

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character2022>();
        if(player != null) _respawnManager._posRespawn = _newPos;
    }

}
