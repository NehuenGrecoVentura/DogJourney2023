using UnityEngine;

public class BillboardGrass : MonoBehaviour
{
    private Camera _cam;

    private void Start()
    {
        _cam = Camera.main;
    }

    void LateUpdate()
    {
        transform.LookAt(_cam.transform);
        transform.rotation = Quaternion.Euler(90f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }


}
