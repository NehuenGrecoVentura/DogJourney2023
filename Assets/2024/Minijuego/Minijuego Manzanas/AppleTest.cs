using UnityEngine;

public class AppleTest : MonoBehaviour
{
    private Transform parentTransform;  // Referencia al transform del padre
    private Vector3 initialLocalPosition;  // Posición local inicial del hijo respecto al padre
    private Quaternion initialLocalRotation;  // Rotación local inicial del hijo respecto al padre

    void Start()
    {
        // Obtener la referencia al transform del padre
        parentTransform = transform.parent;

        // Guardar la posición y rotación local inicial del hijo respecto al padre
        initialLocalPosition = transform.localPosition;
        initialLocalRotation = transform.localRotation;
    }

    void LateUpdate()
    {
        // Mantener la posición y rotación del hijo fija respecto al padre
        transform.localPosition = initialLocalPosition;
        transform.localRotation = initialLocalRotation;
    }
}