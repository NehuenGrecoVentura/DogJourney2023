using UnityEngine;

public class AppleTest : MonoBehaviour
{
    private Transform parentTransform;  // Referencia al transform del padre
    private Vector3 initialLocalPosition;  // Posici�n local inicial del hijo respecto al padre
    private Quaternion initialLocalRotation;  // Rotaci�n local inicial del hijo respecto al padre

    void Start()
    {
        // Obtener la referencia al transform del padre
        parentTransform = transform.parent;

        // Guardar la posici�n y rotaci�n local inicial del hijo respecto al padre
        initialLocalPosition = transform.localPosition;
        initialLocalRotation = transform.localRotation;
    }

    void LateUpdate()
    {
        // Mantener la posici�n y rotaci�n del hijo fija respecto al padre
        transform.localPosition = initialLocalPosition;
        transform.localRotation = initialLocalRotation;
    }
}