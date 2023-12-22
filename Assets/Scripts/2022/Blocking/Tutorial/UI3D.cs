using UnityEngine;

public class UI3D : MonoBehaviour
{
    [SerializeField] float _dirX, _dirY, _dirZ;
    void Update()
    {
        RotateIcon(_dirX, _dirY, _dirZ);
    }

    void RotateIcon(float x, float y, float z)
    {
        transform.Rotate(x, y, z);
    }
}
