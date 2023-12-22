using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayer : MonoBehaviour
{
    public Vector3 offset;
    public Transform player;
    public float rotForce;
    [Range(0, 1)] public float lerpValue;

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, player.position + offset, lerpValue);
        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotForce, Vector3.up) * offset;
        transform.LookAt(player);
    }
}
