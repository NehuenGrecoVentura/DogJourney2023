using UnityEngine;

public class Repos : MonoBehaviour
{
    [SerializeField] Transform _repos;

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) player.gameObject.transform.position = _repos.position;
    }
}