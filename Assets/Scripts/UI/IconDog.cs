using UnityEngine;

public class IconDog : MonoBehaviour
{
    public Transform camTransform;
    Quaternion _originalRot;

    private void Start()
    {
        _originalRot = transform.rotation;
    }

    private void Update()
    {
        transform.rotation = camTransform.rotation * _originalRot;
    }
}
