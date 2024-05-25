using UnityEngine;

public class AppleDown : MonoBehaviour
{
    [SerializeField] Rigidbody _rb;
    [SerializeField] float _speedDown = 1f;
    private Vector3 _initialPos;

    private void Start()
    {
        _initialPos = transform.position;
    }

    private void FixedUpdate()
    {
        Vector3 movement = Vector3.down * _speedDown * Time.fixedDeltaTime;
        _rb.MovePosition(_rb.position + movement);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var player = collision.gameObject.GetComponent<Character>();
        if (player != null)
        {
            transform.position = _initialPos;
            gameObject.SetActive(false);
        }
    }
}