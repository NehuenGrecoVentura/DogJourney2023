using UnityEngine;

public class BillboardCam : MonoBehaviour
{
    private Transform _camPos;

    void Start()
    {
        _camPos = Camera.main.transform;
    }

    //void Update()
    //{
    //    // Calcula la rotaci�n necesaria para mirar hacia la c�mara
    //    Vector3 lookDirection = _camPos.position - transform.position;
    //    Quaternion rotation = Quaternion.LookRotation(lookDirection);


    //    // Extrae las rotaciones en los ejes individuales
    //    float rotationX = rotation.eulerAngles.x;
    //    float rotationY = rotation.eulerAngles.y;
    //    float rotationZ = rotation.eulerAngles.z;

    //    // Aplica la rotaci�n en los ejes X, Y, Z
    //    //transform.rotation = Quaternion.Euler(rotationX, rotationY, rotationZ);
    //    transform.rotation = Quaternion.Euler(rotationX, rotationY, rotationZ);
    //}

    void LateUpdate()
    {
        if (_camPos != null)
        {
            // Calcula la direcci�n hacia la c�mara del jugador
            Vector3 directionToCamera = transform.position - _camPos.transform.position;

            // Apunta hacia la c�mara, pero invierte el eje Z para corregir la inversi�n
            transform.rotation = Quaternion.LookRotation(-directionToCamera, Vector3.up);
        }    
    }
}