using UnityEngine;

public class StairsRepos : MonoBehaviour
{
    Collider _col;

    private void Awake()
    {
        _col = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            player.gameObject.transform.position = transform.position;
            _col.enabled = false;
        }
    }
}