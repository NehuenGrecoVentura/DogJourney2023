using UnityEngine;

public class AppleDown : MonoBehaviour
{
    [SerializeField] Rigidbody _rb;
    [SerializeField] float _speedDown = 1f;

    private void OnEnable()
    {
        _rb.velocity = Vector3.down * _speedDown;
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    var player = collision.gameObject.GetComponent<Character>();
    //    var terrain = collision.gameObject.GetComponent<Terrain>();

    //    if (player != null || terrain != null) gameObject.SetActive(false);
    //    else if (player != null) print("PUNTO");
    //    else if (terrain != null) print("FALLO");
    //}

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        var terrain = other.GetComponent<Terrain>();

        if (player != null || terrain != null) gameObject.SetActive(false);
        else if (player != null) print("PUNTO");
        else if (terrain != null) print("FALLO");
    }
}