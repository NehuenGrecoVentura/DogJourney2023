using UnityEngine;

public class DestroyManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var obj = other.gameObject.GetComponent<IDestructible>();
        if (obj != null) Destroy(other.gameObject);
    }
}