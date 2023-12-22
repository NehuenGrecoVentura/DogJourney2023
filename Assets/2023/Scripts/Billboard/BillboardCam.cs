using UnityEngine;

public class BillboardCam : MonoBehaviour
{
    private Transform _camPos;

    void Start()
    {
        _camPos = Camera.main.transform;
    }

    void Update()
    {
        // Calcula la rotación necesaria para mirar hacia la cámara
        Vector3 lookDirection = _camPos.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(lookDirection);

        // Aplica la rotación solo en el eje Y (para que siempre mire hacia la cámara)
        transform.rotation = Quaternion.Euler(0, rotation.eulerAngles.y, 0);
    }
}