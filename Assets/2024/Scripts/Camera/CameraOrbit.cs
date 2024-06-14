using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    private Vector2 angle = new Vector2(90 * Mathf.Deg2Rad, 0);
    private new Camera camera;
    private Vector2 nearPlaneSize;

    public Transform follow;
    public float maxDistance;
    public Vector2 sensitivity;

    public float zoomSensitivity = 1.0f;
    public float minZoomDistance = 1.0f; // Distancia m�nima permitida
    public float maxZoomDistance = 10.0f; // Distancia m�xima permitida

    public float suavidad = 10f;
    private bool _playerBlocked = false;
    [SerializeField] private bool _zoomBlocked = false;
    private float zoomTargetDistance = 50.0f;
    private float zoomSmoothness = 5.0f; // Suavidad del efecto de zoom
    private float previousMaxDistance;
    float t = 0.0f;
    //public float minY, maxY;
    //public float maxAllowed;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        camera = GetComponent<Camera>();
        CalculateNearPlaneSize();
    }

    private void CalculateNearPlaneSize()
    {
        float height = Mathf.Tan(camera.fieldOfView * Mathf.Deg2Rad / 2) * camera.nearClipPlane;
        float width = height * camera.aspect;

        nearPlaneSize = new Vector2(width, height);
    }

    private Vector3[] GetCameraCollisionPoints(Vector3 direction)
    {
        Vector3 position = follow.position;
        Vector3 center = position + direction * (camera.nearClipPlane + 0.2f);

        Vector3 right = transform.right * nearPlaneSize.x;
        Vector3 up = transform.up * nearPlaneSize.y;

        return new Vector3[]
        {
            center - right + up,
            center + right + up,
            center - right - up,
            center + right - up
        };
    }

    void Update()
    {
        float hor = Input.GetAxis("Mouse X");

        if (hor != 0)
        {
            angle.x += hor * Mathf.Deg2Rad * sensitivity.x;
        }

        float ver = Input.GetAxis("Mouse Y");

        if (ver != 0)
        {
            angle.y += ver * Mathf.Deg2Rad * sensitivity.y;
            angle.y = Mathf.Clamp(angle.y, -80 * Mathf.Deg2Rad, 80 * Mathf.Deg2Rad);
        }

        //if (Input.GetKeyDown(KeyCode.X))
        //{
        //    if (!_zoomBlocked)
        //    {
        //        maxDistance = 20;
        //        _zoomBlocked = true;
        //    }

        //    else
        //    {
        //        maxDistance = 50;
        //        _zoomBlocked = false;
        //    }
        //}

        //if (Input.GetKeyDown(KeyCode.X))
        //{
        //    _zoomBlocked = !_zoomBlocked; // Invertir el estado de bloqueo del zoom
        //    zoomTargetDistance = _zoomBlocked ? 20.0f : 50.0f; // Definir la distancia de zoom objetivo
        //}

        if (Input.GetKeyDown(KeyCode.X))
        {
            if (!_zoomBlocked)
            {
                zoomTargetDistance = 20.0f;
                _zoomBlocked = true;
            }

            else
            {
                zoomTargetDistance = 50.0f;
                _zoomBlocked = false;
            }
        }

        //float scroll = Input.GetAxis("Mouse ScrollWheel");

        //if (scroll != 0 && !_zoomBlocked)
        //{
        //    float zoomAmount = scroll * zoomSensitivity; // Sensibilidad del zoom
        //    maxDistance -= zoomAmount; // Ajusta la distancia m�xima de la c�mara

        //    // Limita la distancia m�xima para evitar valores negativos o problemas con valores extremos
        //    maxDistance = Mathf.Clamp(maxDistance, minZoomDistance, maxZoomDistance);
        //}
    }

    void LateUpdate()
    {
        Vector3 direction = new Vector3(
            Mathf.Cos(angle.x) * Mathf.Cos(angle.y),
            -Mathf.Sin(angle.y),
            -Mathf.Sin(angle.x) * Mathf.Cos(angle.y)
            );

        RaycastHit hit;
        float distance = maxDistance;
        Vector3[] points = GetCameraCollisionPoints(direction);

        foreach (Vector3 point in points)
        {
            if (Physics.Raycast(point, direction, out hit, maxDistance))
            {
                distance = Mathf.Min((hit.point - follow.position).magnitude, distance);
                _playerBlocked = true; // La c�mara est� colisionando
            }
        }

        if (_playerBlocked)
        {
            // Movimiento suavizado de la c�mara solo cuando est� colisionando
            Vector3 targetPosition = follow.position + direction * distance;
            transform.position = Vector3.Lerp(transform.position, targetPosition, suavidad * Time.deltaTime);
        }
        else
        {
            // Movimiento normal de la c�mara cuando no est� colisionando
            transform.position = follow.position + direction * distance;
        }

        transform.rotation = Quaternion.LookRotation(follow.position - transform.position);

        _playerBlocked = false; // Restablece el estado de colisi�n

        //if (_zoomBlocked)
        //{
        //    maxDistance = Mathf.Lerp(maxDistance, zoomTargetDistance, Time.deltaTime * zoomSmoothness);
        //}

        maxDistance = Mathf.Lerp(maxDistance, zoomTargetDistance, Time.deltaTime * zoomSmoothness);
    }
}