using UnityEngine;

public class RabbitClampY : MonoBehaviour
{
    void Update()
    {
        transform.position = new Vector3(transform.position.x, 0.137f, transform.position.z);
    }
}