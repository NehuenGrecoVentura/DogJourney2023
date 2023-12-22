using UnityEngine;

public class Rhombuses : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player") Destroy(gameObject);
    }
}