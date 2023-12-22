using System.Collections;
using UnityEngine;

public class ArrowRot : MonoBehaviour
{
    [SerializeField] Vector3 _rot;

    void OnEnable()
    {
        StartCoroutine(Rot());
    }

    private IEnumerator Rot()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            transform.Rotate(_rot.x, _rot.y, _rot.z);
        }
    }
}